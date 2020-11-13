using System;
using System.Collections;
using System.Collections.Generic;

namespace ControlViajes
{
    public static class ErrorModelValidation
    {
        public static string ShowError(Dictionary<string, object>.ValueCollection ErrorCollection)
        {
            List<string> lstErrores = new List<string>();

            foreach (var item in ErrorCollection)
            {
                if (item is IEnumerable)
                {
                    var listaError = item as ICollection;
                    foreach (string desmenuzado in listaError)
                    {
                        lstErrores.Add(desmenuzado);
                    }
                }
                else
                {
                    lstErrores.Add(item.ToString());
                }
            }

            return string.Join(". " + Environment.NewLine, lstErrores);
        }
    }
}
