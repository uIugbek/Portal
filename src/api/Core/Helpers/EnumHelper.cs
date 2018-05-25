using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Portal.Apis.Models;

namespace Portal.Apis.Core.Helpers
{
    public class EnumHelper
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(Type enumType, IEnumerable<int> selectedItems = null)
        {
            return selectedItems == null
                ? Enum.GetValues(enumType).Cast<int>().Select(e =>
                {
                    string name = Enum.GetName(enumType, e);
                    var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                    return new SelectListItem
                    {
                        Value = e,
                        Text = descAttr.Length > 0 ? descAttr[0].Description : name
                    };
                })
                : Enum.GetValues(enumType).Cast<int>().Select(e =>
                {
                    string name = Enum.GetName(enumType, e);
                    var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                    return new SelectListItem
                    {
                        Selected = selectedItems.Contains(e),
                        Value = e,
                        Text = (descAttr.Length > 0 ? descAttr[0].Description : name)
                    };
                });
        }

        public static string ToName(Enum e)
        {
            return Enum.GetName(e.GetType(), e).ToLower();
        }

        public static string GetDescription(Enum value)
        {
            return value.GetType()
                        .GetMember(value.ToString())
                        .FirstOrDefault()
                        ?.GetCustomAttribute<DescriptionAttribute>()
                        ?.Description;
        }
    }

}