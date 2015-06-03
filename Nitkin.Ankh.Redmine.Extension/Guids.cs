using System;

namespace Nitkin.Ankh.Redmine.Extension
{
    static class GuidList
    {
        public const string guidAnkhSampleExtensionPkgString = "73507E46-0653-4f1e-99AF-F6735FE1565A";// "189df72a-33fb-44c6-9e60-a969882db1d2";
        public const string guidAnkhSampleExtensionCmdSetString = "40D30C1F-4EC8-4a36-85C4-CAAF822DA509";//"8d9f4e43-7321-4884-b7d0-c81c1ed490fc";
        public const string guidAnkhSampleExtensionConnectorString = "121C7911-C0FD-483e-8BE7-16F40B3E7EF4";// "B0B92E9E-3B9F-45fa-B43F-AF11D2A14923";

        public static readonly Guid guidAnkhSampleExtensionCmdSet = new Guid(guidAnkhSampleExtensionCmdSetString);
    };
}