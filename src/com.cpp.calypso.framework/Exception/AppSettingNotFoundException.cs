using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Exception lanzada cuando una clave en AppSettings no se encuentra establecido
    /// </summary>
    public class AppSettingNotFoundException : GenericException
    {
        public AppSettingNotFoundException(string key)
            : base(string.Format("La clave [{0}], no se encuentra establecido en AppSetting", key), string.Format("La clave [{0}], no se encuentra establecido en AppSetting", key))
        {
        }

    }
}
