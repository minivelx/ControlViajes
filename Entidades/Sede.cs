﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Sede : IEquatable<Sede>
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100), Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [StringLength(200), Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Direccion { get; set; }

        [ForeignKey("Cliente")]
        public int ? IdCliente { get; set; }

        public bool Activo { get; set; }

        public string NombreCliente { get { return Cliente?.Nombre; } }

        [JsonIgnore]
        public Cliente Cliente { get; set; }

        public bool Equals(Sede other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
