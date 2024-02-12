using System;
using System.Collections.Generic;

namespace APIZapato.Models
{
    public partial class Producto
    {
        public int Idproducto { get; set; }
        public string? CodifoBarra { get; set; }
        public string? Descripcion { get; set; }
        public string? Marca { get; set; }
        public int? IdCategoria { get; set; }
        public decimal? Precio { get; set; }

        public virtual Categori? oCategoria { get; set; }
    }
}
