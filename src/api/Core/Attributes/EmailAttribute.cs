using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Portal.Apis.Core.Attributes
{
    public class EmailAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            try
            {
                bool result = Regex.IsMatch(value.ToString(),
               @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
               RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                if (!result)
                    ErrorMessage = "The Email field is not a valid e-mail address.";
                return result;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}