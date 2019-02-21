using Abp.Auditing;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.seguridad.aplicacion 
{
    public class CustomLogAbpAuditingStore : IAuditingStore
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static CustomLogAbpAuditingStore Instance { get; } = new CustomLogAbpAuditingStore();

        public ILogger Logger { get; set; }

        public CustomLogAbpAuditingStore()
        {
            Logger = NullLogger.Instance;
        }

        public Task SaveAsync(AuditInfo auditInfo)
        {
            if (auditInfo.Exception == null)
            {
                //Logger.InfoFormat("CustomData: {0}", auditInfo.CustomData);
                //Logger.InfoFormat("Parameters {0}", auditInfo.Parameters);
                //Logger.InfoFormat("ServiceName {0}", auditInfo.ServiceName);
                Logger.Info(auditInfo.ToString());
            }
            else
            {
                Logger.Warn(auditInfo.ToString());
            }

            return Task.FromResult(0);
        }
    }
}
