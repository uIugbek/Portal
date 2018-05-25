using System;
using System.Collections.Generic;

namespace Portal.Apis.Core.DAL.Entities
{
    public class Country : BaseEntity
    {
        // 5 symb
        public string Code { get; set; }
         // 250 symb / locale 
        public string Name { get; set; }
    }
    // public class Country_Locale : LocalizableEntity_Locale<Country>
    // {
    //     // 250 symb / locale 
    //     public string Name { get; set; }
    // }
}