// VsPkg.cs : Implementation of Ankh_Sample_Extension
//

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Nitkin.Ankh.Redmine.Extension.IssueTracker;
using Microsoft.Win32;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

namespace Nitkin.Ankh.Redmine.Extension
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the registration utility (regpkg.exe) that this class needs
    // to be registered as package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // A Visual Studio component can be registered under different regitry roots; for instance
    // when you debug your package you want to register it in the experimental hive. This
    // attribute specifies the registry root to use if no one is provided to regpkg.exe with
    // the /root switch.
    [DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\9.0")]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", false, IconResourceID = 400)]
    // In order be loaded inside Visual Studio in a machine that has not the VS SDK installed, 
    // package needs to have a valid load key (it can be requested at 
    // http://msdn.microsoft.com/vstudio/extend/). This attributes tells the shell that this 
    // package has a load key embedded in its resources.
    [ProvideLoadKey("Standard", "1.0", AppConstants.PackageName, AppConstants.CompanyName, 104)]
    [ProvideService(typeof(IssueTracker.AnkhConnector), ServiceName = AppConstants.ConnectorName)]
    [ProvideIssueRepositoryConnector(typeof(IssueTracker.AnkhConnector), AppConstants.ConnectorName, typeof(NitkinAnkhRedmineExtensionPackage), "#110")]
    [Guid(GuidList.guidAnkhSampleExtensionPkgString)]
    public sealed class NitkinAnkhRedmineExtensionPackage : Package
    {

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public NitkinAnkhRedmineExtensionPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
            
        }
        
 

        
        /////////////////////////////////////////////////////////////////////////////
        // Overriden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

           


            IServiceContainer ctrnr = this as IServiceContainer;
            ctrnr.AddService(typeof(IssueTracker.AnkhConnector), new ServiceCreatorCallback(CreateTrackerConnector), true);
        }
        private object CreateTrackerConnector(IServiceContainer container, Type serviceType)
        {
            if ((container == this) && (typeof(AnkhConnector) == serviceType))
            {
                return new AnkhConnector();
            }
            return null;
        }


        #endregion

    }
}