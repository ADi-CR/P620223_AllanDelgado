using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Factura
    {
        //simples
        public int IDFactura { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string Notas { get; set; }

        //atributos compuestos 
        public Cliente MiCliente { get; set; }
        public Usuario MiUsuario { get; set; }
        public FacturaTipo MiTipoFactura { get; set; }

        public List<FacturaInventario> Detalle { get; set; }

        public Factura()
        {
            MiCliente = new Cliente(); 
            MiUsuario = new Usuario();
            MiTipoFactura = new FacturaTipo();  
            Detalle = new List<FacturaInventario>();

            Fecha = DateTime.Now;
        }

        //fns

        public int Agregar()
        {
            int R = 0;

            return R;
        }

        public bool Imprimir()
        {
            bool R = false;

            return R;
        }

        public DataTable ListarPorFechas(DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable R = new DataTable();

            return R;
        }


    }
}
