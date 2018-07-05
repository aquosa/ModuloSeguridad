using System;
using System.Configuration;
using System.Collections;
using System.Configuration.Install;

namespace CustomInstall
{
    public class ConfigManager
    {
        #region Attributos

        private string _ConfigPath;
        private Configuration _Config;
        private AppSettingsSection _AppSettings;

        #endregion

        /// <summary>
        /// Inicializa una nueva instancia de la clase<see cref="ConfigUpdater"/>.
        /// </summary>
        /// <param name="configPath">The config path.</param>
        public ConfigManager(string configPath)
        {
            _ConfigPath = (configPath == null ? String.Empty : configPath);

            try
            {
                _Config = ConfigurationManager.OpenExeConfiguration(_ConfigPath);
                _AppSettings = _Config.AppSettings;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    String.Format("Error adding new configuration parameters into: '{0}'.{1}{2}",
                    _ConfigPath, ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// Añade una nueva clave (keyName) a la sección "appSettings"
        /// del fichero de configuración con un valor (value).
        /// </summary>
        /// <param name="keyName">Nombre de la clave a añadir.</param>
        /// <param name="value">Valor a asignar a la nueva clave.</param>
        public void AddParam(string keyName, string value)
        {
            if (_AppSettings.Settings[keyName] == null)
                _AppSettings.Settings.Add(keyName, value);
        }

        /// <summary>
        /// Elimina del fichero de configuración una clave (keyName) de la sección "appSettings"
        /// del fichero de configuración.
        /// </summary>
        /// <param name="keyName">Nombre de la clave a eliminar..</param>
        public void RemoveParam(string keyName)
        {
            _AppSettings.Settings.Remove(keyName);
        }

        /// <summary>
        /// Modifica o añade una clave (keyName) de la sección "appSettings".
        /// </summary>
        /// <param name="keyName">Nombre de la clare a añadir o modificar.</param>
        /// <param name="value">Valor a asignar a la clave</param>
        public void SaveParam(string keyName, string value)
        {
            if (_AppSettings.Settings[keyName] != null)
                _AppSettings.Settings[keyName].Value = value;
            else
                AddParam(keyName, value);
        }

        /// <summary>
        /// Guarda el fichero de configuración con los cambios realizados
        /// en la sección "appSetting".
        /// </summary>
        public void Save()
        {
            if (!_AppSettings.ElementInformation.IsLocked)
                _Config.Save();
            else
                throw new ApplicationException("Section was locked, could not update");
        }
    }
}
