using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoExpress.Backend.MOD.MOD
{
    public class Producto
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public decimal Precio_Unitario { get; set; }
        public int Cantidad { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime? Fecha_Ultimo_Abastecimiento { get; set; }
        public int Cantidad_Ultimo_Abastecimiento { get; set; }

    }
}
