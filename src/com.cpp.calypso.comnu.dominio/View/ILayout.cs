using System;

namespace com.cpp.calypso.comun.dominio
{
    public interface ILayout
    {
        IGenerateWidget GenerateWidget { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelType"></param>
        void ProcessView(Type modelType);
    }
}
