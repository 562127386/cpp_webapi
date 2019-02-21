using System;
using System.Collections.Generic;
using System.Linq;

namespace com.cpp.calypso.web
{
    public static class DataHelper
    {
          

        

        public static IEnumerable<Nomenclature> GetPersonasItems(this IEnumerable<PersonaViewModel> qry)
        {
            if (qry == null) throw new ArgumentNullException("qry");
            var items = qry.AsEnumerable()
                .Select(x => new Nomenclature
                {
                    Value = x.Id,
                    Text = x.GetFullName(true)
                })
                .OrderBy(x => x.Text)
                .ToArray();
            return items;
        }

        
    }
}