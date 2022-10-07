using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class FacturaInventario
    {
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Cantidad  { get; set; }
        public decimal PorcentajeImpuesto { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal SubTotal { get; set; } //cantidad * precio venta
        public decimal DescuentoTotal { get; set; }
        public decimal SubTotal2 { get; set; } //Subtotal - DescuentoTotal
        public decimal ImpuestosTotal { get; set; }
        public decimal TotalLinea { get; set; } //Subtotal2 + ImpuestoTotal

        //atrinutos compuestos
        public Inventario MiInventario { get; set; }

        public FacturaInventario()
        {
            MiInventario = new Inventario();
        }

        //Crear funciones para la mate de totalizaciones para no tener 
        //que usar en la clase principal 
        public decimal TotalizarLinea()
        {
            //totalizar subtotal
            SubTotal = Cantidad * PrecioVenta;
            //monto del descuento 
            DescuentoTotal = (SubTotal * PorcentajeDescuento) / 100;
            //Subtotal2 
            SubTotal2 = SubTotal - DescuentoTotal;
            //monto del impuesto
            ImpuestosTotal = (SubTotal2 * PorcentajeImpuesto) / 100;
            //total final 
            TotalLinea = SubTotal2 + ImpuestosTotal;

            return TotalLinea; 
        
        }
    }
}
