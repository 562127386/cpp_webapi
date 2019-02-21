using Abp.Configuration;
using Abp.Dependency;
using Abp.Logging;
using Abp.Threading;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.aplicacion
{
    /// <summary>
    /// Utilizar como fuente de configuraciones de Abp los AppSettings de los archivos (web.config / app.config).
    /// Para realizar una reemplazo del comportamiento de Abp default. 
    /// </summary>
    public class AppSettingAbpSettingStore : ISettingStore, ITransientDependency
    {
        public Task CreateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn(string.Format("ISettingStore is not implemented, using AppSettingAbpSettingStore which does not support CreateAsync. Key {0}", setting.Name));
            return AbpTaskCache.CompletedTask;
        }

        public Task DeleteAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn(string.Format("ISettingStore is not implemented, using AppSettingAbpSettingStore which does not support DeleteAsync. Key {0}", setting.Name));
            return AbpTaskCache.CompletedTask;
        }

        public async Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId)
        {
            return await Task.Run(() =>
            {
                return GetAllListInternal();
            });

        }

        public Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                return Task.FromResult<SettingInfo>(null);
            }

            return Task.FromResult(new SettingInfo(tenantId, userId, name, value));
        }

        private List<SettingInfo> GetAllListInternal()
        {

            string[] appSettingsSystem = ConfigurationManager.AppSettings.AllKeys
                            //.Where(key => key.StartsWith(string.Format("Sistema:")))
                            .Select(key => key)
                            .ToArray();

            var listParam = new List<SettingInfo>();
            var random = new Random();
            foreach (var key in appSettingsSystem)
            {

                var value = ConfigurationManager.AppSettings[key];
                if (value != null)
                {
                    var item = new SettingInfo(null, null, key, value);
                    listParam.Add(item);
                }
            }
            return listParam;
        }

        public Task UpdateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn(string.Format("ISettingStore is not implemented, using AppSettingAbpSettingStore which does not support UpdateAsync. Key {0}", setting.Name));
            return AbpTaskCache.CompletedTask;
        }
    }
}
