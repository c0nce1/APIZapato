using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIZapato.Models
{
    public partial class Categori
    {
        public Categori()
        {
            Productos = new HashSet<Producto>();
        }

        public int Idcategoria { get; set; }
        public string? Descripcion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
