using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P620223_AllanDelgado.Formularios
{
    public partial class FrmFacturacion : Form
    {
        //Objetos Locales 
        public Logica.Models.Factura MiFacturaLocal { get; set; }  
        public DataTable DtListaItems { get; set; }


        public FrmFacturacion()
        {
            InitializeComponent();

            MiFacturaLocal= new Logica.Models.Factura();

            DtListaItems= new DataTable();

        }

        private void FrmFacturacion_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;

            LlenarComboTiposFactura();

            Limpiar();
        }


        private void Limpiar()
        {
            TxtClienteID.Clear();
            LblClienteNombre.Text = string.Empty;
            TxtNotas.Clear();
            CboxFacturaTipo.SelectedIndex = -1;

            TxtSubtotal.Text = "0";
            TxtDescuentos.Text = "0";
            TxtImpuestos.Text = "0";
            TxtTotal.Text = "0";

            DtListaItems = MiFacturaLocal.CargarEsquemaListaDetalle();

            DgvLista.DataSource= DtListaItems;

        }

        private void LlenarComboTiposFactura()
        { 
            DataTable dt = new DataTable();

            dt = MiFacturaLocal.MiTipoFactura.Listar();

            if (dt != null && dt.Rows.Count > 0)
            {
                CboxFacturaTipo.ValueMember = "ID";
                CboxFacturaTipo.DisplayMember= "D";
                CboxFacturaTipo.DataSource= dt;
                CboxFacturaTipo.SelectedIndex = -1;
            }
        }

        private void BtnClienteBuscar_Click(object sender, EventArgs e)
        {
            //se crea una nueva instancia del form de búsqueda de clientes
            // y se espera una respuesta del mismo. 

            Form FormBusquedaCliente = new FrmClienteBuscar();

            DialogResult resp = FormBusquedaCliente.ShowDialog();

            if (resp == DialogResult.OK) 
            {
                //cargar la info del usuario en la composición de MiFacturaLocal 
                //y además mostrar el nombre del usuario en el Label 

                if (!string.IsNullOrEmpty(TxtClienteID.Text.Trim()) )
                {
                    int IdCliente = Convert.ToInt32(TxtClienteID.Text.Trim());

                    //se incluyen los datos en la composición del cliente

                    MiFacturaLocal.MiCliente = MiFacturaLocal.MiCliente.ConsultarPorID(IdCliente);

                    LblClienteNombre.Text = MiFacturaLocal.MiCliente.Nombre;
                }

            }


        }

        private void MnuAgregarItem_Click(object sender, EventArgs e)
        {
            Form FormBuscarItem = new FrmInventarioBuscar();

            DialogResult resp = FormBuscarItem.ShowDialog();

            if (resp == DialogResult.OK) 
            {
                
                DgvLista.DataSource = DtListaItems;
                DgvLista.ClearSelection();

                Totalizar();

            }

        }

        private void Totalizar()
        {
            decimal SubTotal = 0; 
            decimal Descuentos = 0;
            decimal Impuestos = 0; 
            decimal Total = 0;

            if (DtListaItems.Rows.Count > 0)
            {
                foreach (DataRow item in DtListaItems.Rows)
                {
                    //el += lo que hace es sumar al valor anterior un valor adicional
                    // sería lo mismo que hacer: SubTotal = Subtotal + algo
                    SubTotal += Convert.ToDecimal(item["SubTotal"]);
                    Descuentos += Convert.ToDecimal(item["DescuentoTotal"]);
                    Impuestos += Convert.ToDecimal(item["ImpuestosTotal"]);
                    Total += Convert.ToDecimal(item["TotalLinea"]);
                }
            }

            TxtSubtotal.Text = string.Format("{0:N2}", SubTotal);
            TxtDescuentos.Text = string.Format("{0:N2}", Descuentos);
            TxtImpuestos.Text = string.Format("{0:N2}", Impuestos);
            TxtTotal.Text = string.Format("{0:N2}", Total);
        }

        private void BtnCrearFactura_Click(object sender, EventArgs e)
        {
            //validar requerimientos mínimos 
            if (ValidarFactura())
            {
                //cuando tenemos una estructura de encabezado-detalle 
                //como la factura, primero debemos agreagar el encabezado
                //en su tabla respectiva y obtener el ID que se genera 
                //en el PK. 
                //Luego usando ese ID se recorre la lista del detalle 
                //y se hace el insert en la tabla de detalle con FK = ID

                MiFacturaLocal.MiTipoFactura.IDFacturaTipo = Convert.ToInt32(CboxFacturaTipo.SelectedValue);
                MiFacturaLocal.MiCliente.IDCliente = Convert.ToInt32(TxtClienteID.Text.Trim());
                //como el nombre del clientes o el nombre del tipo de factura no son necesarios para hacer el Insert
                //se pueden omitir en este punto. 

                MiFacturaLocal.MiUsuario.IDUsuario = Globales.MiUsuarioGlobal.IDUsuario;

                MiFacturaLocal.Notas = TxtNotas.Text.Trim();

                CargarDetalleFactura();

                if (MiFacturaLocal.Agregar() > 0)
                {

                    MessageBox.Show("Factura procesada correctamente", ":)", MessageBoxButtons.OK);

                    //TODO: Presentar gráficamente un reporte de la factura en formato POS (punto de venta) 

                    Limpiar();

                }



            }

        }

        private void CargarDetalleFactura()
        {

            foreach (DataRow item in DtListaItems.Rows)
            {

                Logica.Models.FacturaInventario detalle = new Logica.Models.FacturaInventario();

                detalle.MiInventario.IDInventario = Convert.ToInt32(item["IDInventario"]);
                detalle.Costo = 0; //TODO: deberíamos agregar el valor del costo, se puede hacer por medio de una función
                detalle.PrecioVenta = Convert.ToDecimal(item["PrecioVenta"]);
                detalle.Cantidad = Convert.ToDecimal(item["Cantidad"]);
                detalle.PorcentajeImpuesto = Convert.ToDecimal(item["TasaImpuesto"]);

                detalle.PorcentajeDescuento = Convert.ToDecimal(item["PorcentajeDescuento"]);
                detalle.SubTotal = Convert.ToDecimal(item["SubTotal"]);
                detalle.DescuentoTotal = Convert.ToDecimal(item["DescuentoTotal"]);
                detalle.SubTotal2 = Convert.ToDecimal(item["SubTotal2"]);
                detalle.ImpuestosTotal = Convert.ToDecimal(item["ImpuestosTotal"]);
                detalle.TotalLinea = Convert.ToDecimal(item["TotalLinea"]);

                //una vez llenamos los datos del detalle se agrega a la factura local.

                MiFacturaLocal.Detalle.Add(detalle);

            }
        
        }



        private bool ValidarFactura()
        { 
            bool R = false;

            if (!string.IsNullOrEmpty(LblClienteNombre.Text.Trim()) && 
                CboxFacturaTipo.SelectedIndex > -1 && 
                DtListaItems.Rows.Count > 0)
            {
                R = true;
            }

            //TODO: validar casos contrarios con el else

            return R; 
        
        }







    }
}
