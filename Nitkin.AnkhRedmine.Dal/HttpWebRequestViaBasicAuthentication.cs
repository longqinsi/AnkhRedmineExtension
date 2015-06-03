using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Nitkin.AnkhRedmine.Dal
{
    public class HttpWebRequestViaBasicAuthentication
    {
        private static bool SendWebRequest(TcpClient tc, NetworkStream ns, string url, string request, bool isHttps,
            out string response, out int? responseCode, out string responseMessage)
        {
            ns.WriteTimeout = 15000;
            ns.ReadTimeout = 15000;

            tc.Client.NoDelay = true;
            if (!isHttps)
            {
                return GetResponseInternal(ns, request, out response, out responseCode, out responseMessage);
            }
            // Secure HTTP
            using (var ssl = new SslStream(ns, false, ValidateServerCertificate, null))
            {
                ssl.AuthenticateAsClient(url, null, SslProtocols.Tls, false);
                return GetResponseInternal(ssl, request, out response, out responseCode, out responseMessage);
            }
            // Normal HTTP
        }

        private static bool GetResponseInternal(Stream stream, string request, out string response, out int? responseCode, out string responseMessage)
        {
            response = "";
            responseCode = null;
            responseMessage = null;
            using (var sw = new System.IO.StreamWriter(stream))
            {
                sw.Write(request);
                sw.Flush();
                List<byte> getBytes = new List<byte>();
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int receiveCount = stream.Read(buffer, 0, buffer.Length);
                    if (receiveCount <= 0)
                    {
                        break;
                    }
                    getBytes.AddRange(buffer.Take(receiveCount));
                }
                var getBytesArray = getBytes.ToArray();
                Dictionary<string, string> headers = new Dictionary<string, string>();
                Encoding contentEncoding = null;
                using (MemoryStream ms = new MemoryStream(getBytesArray))
                {
                    using (StreamReader sr = new StreamReader(ms, Encoding.ASCII))
                    {
                        if (!sr.EndOfStream)
                        {
                            string firstLine = sr.ReadLine();
                            if (!String.IsNullOrWhiteSpace(firstLine))
                            {
                                Regex httpHeaderRegex = new Regex(@"^HTTP/\d\.\d ([0-9]+) ([0-9a-zA-Z\-_]+)$");
                                Match match = httpHeaderRegex.Match(firstLine.Trim());
                                if (match.Success)
                                {
                                    int responseCodeTemp;
                                    if (int.TryParse(match.Groups[1].Value, out responseCodeTemp))
                                    {
                                        responseCode = responseCodeTemp;
                                    }
                                    responseMessage = match.Groups[2].Value;
                                }
                            }
                        }
                        var success = responseCode.HasValue && responseCode.Value == 200;
                        if (!success)
                        {
                            return false;
                        }
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                break;
                            }
                            var index = line.IndexOf(':');
                            if (index > 0 && index < line.Length)
                            {
                                headers[line.Substring(0, index).Trim()] =
                                    line.Substring(index + 1, line.Length - index - 1).Trim();
                            }
                        }
                    }
                    if (headers.ContainsKey("Content-Type"))
                    {
                        string[] charsetArray =
                            (headers["Content-Type"].Split(';').FirstOrDefault(a => a.Trim().StartsWith("charset=")) ?? "")
                                .Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (charsetArray.Length == 2 && !string.IsNullOrWhiteSpace(charsetArray[1]))
                        {
                            try
                            {
                                contentEncoding = Encoding.GetEncoding(charsetArray[1].Trim());
                            }
                            catch (ArgumentException)
                            {
                            }
                        }
                    }
                    if (contentEncoding == null)
                    {
                        contentEncoding = Encoding.ASCII;
                    }
                }
                using (MemoryStream ms = new MemoryStream(getBytesArray))
                {
                    using (StreamReader sr = new StreamReader(ms, contentEncoding))
                    {
                        response = sr.ReadToEnd();
                    }
                }

            }
            if (!string.IsNullOrWhiteSpace(response))
            {
                StringBuilder sb = new StringBuilder();
                using (StringReader stringReader = new StringReader(response))
                {
                    while (!string.IsNullOrWhiteSpace(stringReader.ReadLine())) ;
                    string lengthLine = stringReader.ReadLine();
                    if (string.IsNullOrWhiteSpace(lengthLine))
                    {
                        return false;
                    }
                    long contentLength;
                    try
                    {
                        contentLength = Convert.ToInt64(lengthLine, 16);
                    }
                    catch (FormatException)
                    {
                        responseMessage = "回复的正文第一行不是有效的16进制长度值。第一行内容:" + lengthLine;
                        return false;
                    }
                    catch (OverflowException)
                    {
                        responseMessage = "回复的正文第一行所代表的长度值大于Int64.MaxValue或小于Int64.MinValue。第一行内容:" + lengthLine;
                        return false;
                    }
                    if (contentLength < 0)
                    {
                        responseMessage = "回复的正文第一行所代表的长度值小于0。第一行内容:" + lengthLine;
                        return false;
                    }
                    if (contentLength == 0)
                    {
                        response = "";
                        return true;
                    }
                    using (TextWriter tw = new StringWriter(sb))
                    {
                        while (true)
                        {
                            var line = stringReader.ReadLine();
                            if (line == "0")
                            {
                                break;
                            }
                            tw.WriteLine(line);
                            if (sb.Length >= contentLength)
                            {
                                break;
                            }
                        }
                    }
                }
                response = sb.ToString();
            }
            return true;
        }

        // The following method is invoked by the RemoteCertificateValidationDelegate. 
        private static bool ValidateServerCertificate(
        object sender,
        X509Certificate certificate,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors)
        {
            return true; // Accept all certs
        }

        /// <summary>
        /// Requests the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static bool GetResponse(string url, string username, string password, out string response, out int? responseCode, out string responseMessage)
        {
            var encodedCredential = Lookup(username, password);
            var address = new Uri(url);
            var strHttpRequest1 = "GET " + address.PathAndQuery + " HTTP/1.1\r\n";
            //strHttpRequest1 += "Connection: Keep-Alive\r\n";
            strHttpRequest1 += "Connection: Close\r\n";
            strHttpRequest1 += "Authorization: Basic " + encodedCredential + "\r\n";
            strHttpRequest1 += "Host: " + address.Host + ":" + address.Port + "\r\n\r\n";

            List<string> responses = new List<string>();
            using (var tc = new TcpClient())
            {
                tc.Connect(address.Host, address.Port);
                using (var ns = tc.GetStream())
                {
                    var isHttps = address.Scheme == Uri.UriSchemeHttps;
                    return SendWebRequest(tc, ns, address.Host, strHttpRequest1, isHttps, out response, out responseCode, out responseMessage);
                }
            }
        }


        private static string Lookup(string username, string password, string domain = null)
        {
            string rawString = ((!string.IsNullOrWhiteSpace(domain)) ? (domain + "\\") : "") + username + ":" + password;

            // The response is an "Authorization:" header where the value is
            // the text "Basic" followed by BASE64 encoded (as defined by RFC1341) value

            byte[] bytes = EncodingRightGetBytes(rawString);
            return Convert.ToBase64String(bytes);
        }

        private static byte[] EncodingRightGetBytes(string rawString)
        {
            //
            // in order to know if there will not be any '?' translations (which means
            // we should use the Default Encoding) we need to attempt encoding and then decoding.
            // <STRIP>this is a limitation only on win9x, if we ever drop support for this platform there might be
            // a more efficient way of doing this.</STRIP>
            //

            byte[] bytes = Encoding.Default.GetBytes(rawString);
            string rawCopy = Encoding.Default.GetString(bytes);
            bool canMapToCurrentCodePage = string.Compare(rawString, rawCopy, StringComparison.Ordinal) == 0;

            //<STRIP>
            // if mapping to the current code page leaves characters out of the
            // [0x00, 0xFF] range, then we need to use the new encoding that IIS6.0
            // will support. do it when they decide it's good enough.
            // </STRIP>
            if (!canMapToCurrentCodePage)
            {
                //<STRIP>
                // for now throw. when IIS 6.0 adds support test it.
                //</STRIP>
                throw new NotSupportedException();
                /*
                GlobalLog.Print("BasicClient::EncodingRightGetBytes(): using:" + Encoding.UTF8.EncodingName);
                bytes = Encoding.UTF8.GetBytes(rawString);

                string blob = "=?utf-8?B?" + Convert.ToBase64String(bytes) + "?=";
                bytes = Encoding.ASCII.GetBytes(blob);
                */
            }

            return bytes;
        }
    }
}
