using System;
using System.Globalization;
using Microsoft.VisualStudio.Shell;

namespace Nitkin.Ankh.Redmine.Extension
{
	/// <summary>
	/// This attribute registers the package as Issue Repository Connector.
	/// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    [System.Runtime.InteropServices.Guid("072C0B48-7BCC-49ce-927C-1EC92279E8CC")]
    public sealed class ProvideIssueRepositoryConnector : RegistrationAttribute
    {
        // Fields
        private Type _connectorService = null;
        private string _regName = null;
        private string _uiName = null;
        private Type _uiNamePkg = null;
        private const string REG_KEY_CONNECTORS = "IssueRepositoryConnectors";
        private const string REG_KEY_NAME = "Name";
        private const string REG_VALUE_PACKAGE = "Package";
        private const string REG_VALUE_SERVICE = "Service";

        // Methods
        public ProvideIssueRepositoryConnector(Type connectorServiceType, string regName, Type uiNamePkg, string uiName)
        {
            this._connectorService = connectorServiceType;
            this._regName = regName;
            this._uiNamePkg = uiNamePkg;
            this._uiName = uiName;
        }

        public override void Register(RegistrationAttribute.RegistrationContext context)
        {
            context.Log.WriteLine(string.Format(CultureInfo.CurrentCulture, "Issue Repository Connector:\t\t{0}\n", new object[] { this.RegName }));
            using (RegistrationAttribute.Key key = context.CreateKey("IssueRepositoryConnectors"))
            {
                using (RegistrationAttribute.Key key2 = key.CreateSubkey(this.RegGuid.ToString("B").ToUpperInvariant()))
                {
                    key2.SetValue("", this.RegName);
                    key2.SetValue("Service", this.IssueRepositoryConnectorService.ToString("B").ToUpperInvariant());
                    using (RegistrationAttribute.Key key3 = key2.CreateSubkey("Name"))
                    {
                        key3.SetValue("", this.UIName);
                        key3.SetValue("Package", this.UINamePkg.ToString("B").ToUpperInvariant());
                        //key3.Close();
                    }
                    //key2.Close();
                }
                //key.Close();
            }
        }

        public override void Unregister(RegistrationAttribute.RegistrationContext context)
        {
            context.RemoveKey(@"IssueRepositoryConnectors\" + this.RegGuid.ToString("B"));
        }

        // Properties
        public Guid IssueRepositoryConnectorService
        {
            get
            {
                return this._connectorService.GUID;
            }
        }

        public Guid RegGuid
        {
            get
            {
                return this.IssueRepositoryConnectorService;
            }
        }

        public string RegName
        {
            get
            {
                return this._regName;
            }
        }

        public string UIName
        {
            get
            {
                return this._uiName;
            }
        }

        public Guid UINamePkg
        {
            get
            {
                return this._uiNamePkg.GUID;
            }
        }
    }


}
