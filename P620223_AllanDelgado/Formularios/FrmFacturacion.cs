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
    }
}
