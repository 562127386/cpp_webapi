using System;
using System.Web;

namespace com.cpp.calypso.framework.Session
{
    public interface ISessionManager
    {
    }

    //public abstract class AbstractSessionManager<TEnum>:ISessionManager
    //    where TEnum : struct, IComparable, IFormattable, IConvertible
    //{

    //    public  object getSession(TEnum sessionEnum)
    //    {
    //        if (!typeof(TEnum).IsEnum) {
    //            throw new ArgumentException("TEnum must be an enumerated type");
    //        }


    //        var session = string.Format("{0}_{1}", typeof(this).Name, sessionEnum.ToString());
    //        if (Enum.IsDefined(typeof(EnumerableSessionCuenta), sessionEnum))
    //        {
    //            return HttpContext.Current.Session[session];
    //        }

    //        throw new SessionException();
    //    }

    //}

}
