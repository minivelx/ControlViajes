using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(20)]
        public string Cedula { get; set; }

        public bool Activo { get; set; }

        [StringLength(200)]
        public string TokenFirebase { get; set; }

        [ForeignKey("Cliente")]
        public int ? IdCliente { get; set; }

        public string NombreCliente { get { return Cliente?.Nombre; } }

        [JsonIgnore]
        public Cliente Cliente { get; set; }
    }

    public class ApplicationRole : IdentityRole<string>
    {

    }

    public class IdentityUserClaim : IdentityUserClaim<string>
    {

    }

    public class IdentityUserLogin : IdentityUserLogin<string>
    {

    }

    public class ApplicationUserRole : IdentityUserRole<string>
    {

    }
}
