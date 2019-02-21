
using com.cpp.calypso.comun.dominio;
using System;

namespace com.cpp.calypso.comun.aplicacion
{
    /// <summary>
    /// 
    /// </summary>
    public interface IViewService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeModel"></param>
        /// <param name="typeLayout"></param>
        /// <returns></returns>
        View Get(Type typeModel, Type typeLayout);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        View Get(string name);

        /// <summary>
        /// Registrar nueva vista, o recuperar si ya se encuentra registrada. 
        /// </summary>
        /// <param name="nameViewSearch"></param>
        /// <param name="actionCreate"></param>
        /// <returns></returns>
        View RegisterOrGet(string nameViewSearch, Func<string, View> actionCreate);


        ///// <summary>
        ///// Get View 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //View Get(int id);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //View Get(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>


    }
}
