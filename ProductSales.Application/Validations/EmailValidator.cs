using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProductSales.Application.Validations
{
    public static class EmailValidator
    {

        public static bool EmailIsValid(string email)
        {
            List<string> publicDomain = new() { "google", "yandex", "hotmail" };

            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    foreach (var item in publicDomain)
                    {
                        if (email.Contains(item)) return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
