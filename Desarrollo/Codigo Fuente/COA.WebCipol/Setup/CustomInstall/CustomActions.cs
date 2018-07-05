using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CustomInstall
{
    [RunInstaller(true)]
    public partial class CustomActions : Installer
    {
        //Para configuraciones y parámetros externos
        //
        //private const string PAREMETER_NOT_SPECIFIED = "El parámetro de instalación '{0}' no ha sido especificado. La instalación será cancelada.";
        //
        //private CustomParameters _parameters = null;
        //
        // Añadir al array todos los parámetros requeridos durante la instalación.
        // Dejarlo vacío sino hay parámetros requeridos.
        //private ArrayList requiredParameters = new ArrayList { CustomParameters.PREFIJO_SALUDO };

        public CustomActions()
        {
            InitializeComponent();
        }

        public override void Commit(System.Collections.IDictionary savedState)
        {
            base.Commit(savedState);
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            //System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(this.Context.Parameters["AssemblyPath"]) + "\\ReportViewerLP.exe", "/q");
            Process myProses = Process.Start(System.IO.Path.GetDirectoryName(this.Context.Parameters["AssemblyPath"]) + "\\ReportViewerLP.exe", "/q");
            myProses.WaitForExit();
            myProses.Close();
            base.OnBeforeInstall(savedState);
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            
            Process.Start(System.IO.Path.GetDirectoryName(this.Context.Parameters["AssemblyPath"]) + "\\ReportViewer.exe");
            base.Install(stateSaver);
        }

//Para configuraciones y parámetros externos
//
//        private void ConfigureFileConfig()
//        {
//            string assemblyPath = this.Context.Parameters["assemblyPath"];
//            string fileName = Path.GetFileNameWithoutExtension(assemblyPath).Replace("CustomInstall", ".exe");

//            string exeConfigPath = Path.Combine(Directory.GetParent(assemblyPath).FullName, fileName);

//            ConfigManager config = new ConfigManager(exeConfigPath);

//            this.UpdateAppSettings(config);

//            // Si todo es correcto se realiza el commit del fichero de configuración.
//            config.Save();

//            config = null;
//        }

//        private void UpdateAppSettings(ConfigManager config)
//        {
//            PropertyInfo[] props = _parameters.GetType().GetProperties();

//            foreach (PropertyInfo prop in props)
//            {
//                string propValue = prop.GetValue(_parameters, null) as string;
//                if (!String.IsNullOrEmpty(propValue))
//                {
//                    config.SaveParam(prop.Name, propValue);
//                }
//                else
//                {
//                    // Parámetro requerido no especificado
//                    if (requiredParameters.Contains(prop.Name))
//                    {
//                        throw new InstallException(
//                            String.Format(PAREMETER_NOT_SPECIFIED, prop.Name));
//                    }
//                }
//            }
//        }

    }
}
