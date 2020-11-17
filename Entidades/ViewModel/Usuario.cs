using System.Collections.Generic;

namespace Entidades
{
    public class Usuario
    {
        public Usuario()
        {
            Roles = new List<RolViewModel>();
        }

        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Cedula { get; set; }

        public string Email { get; set; }

        public string Celular { get; set; }

        public bool Activo { get; set; }

        public string NombreCliente { get; set; }

        public List<RolViewModel> Roles { get; set; }

    }
}
