using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace intento1.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo")]
        public int Cantidad { get; set; }

        public Nullable<int> StockAlert_Id { get; set; }

        public virtual StockAlerts StockAlerts { get; set; }
        public virtual ICollection<Pedidos> Pedidos { get; set; }
    }
}