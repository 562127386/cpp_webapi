using System.Reflection;

namespace com.cpp.calypso.comun.dominio
{
    public interface IGenerateWidget
    {
        string AutoGenerate(PropertyInfo property);
    }
}
