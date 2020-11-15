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

        private static string PasswordError(string errorCode)
        {
            if (errorCode == "PasswordTooShort")
            {
                return "La contraseña debe contener al menos 6 caracteres. ";
            }
            else if (errorCode == "PasswordRequiresNonAlphanumeric")
            {
                return "La contraseña debe contener al menos un caracter no alfanúmerico. ";
            }
            else if (errorCode == "PasswordRequiresLower")
            {
                return "La contraseña debe contener al menos un caracter en minúscula. ";
            }
            else if (errorCode == "PasswordRequiresUpper")
            {
                return "La contraseña debe contener al menos un caracter en mayúscula. ";
            }
            else
            {
                return "El correo ya ha sido registrado. " + "Error Code (" + errorCode + ")";
            }
        }
    }
}
