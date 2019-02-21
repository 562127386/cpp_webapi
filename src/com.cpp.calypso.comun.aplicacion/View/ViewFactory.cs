using com.cpp.calypso.comun.dominio;
using System;

namespace com.cpp.calypso.comun.aplicacion
{

    public static class ViewFactory
    {
        public static View CreateViewSearch(string nameViewSearch)
        {
            View view = new View();
            view.Name = nameViewSearch;
            //view.Model = GenerateNameModel(type);
            view.Arch = string.Empty;

            //Search
            view.Layout = new Search();
            return view;
        }
    }
}