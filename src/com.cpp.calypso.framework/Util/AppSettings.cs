using System.ComponentModel;
using System.Configuration;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Clase para recuperar valores de AppSettings establecido el tipo del valor como un generic
    /// 
    /// Ejemplo.
    /// 
    /// Recuperar el valor "IsDebug" como un valor boolean
    /// <add key="IsDebug" value="true" />
    ///  
    /// var isDebug = AppSettings.Get<bool>("IsDebug");
    /// </summary>
    public static class AppSettings
    {
        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting)) throw new AppSettingNotFoundException(key);

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }
    }
}
