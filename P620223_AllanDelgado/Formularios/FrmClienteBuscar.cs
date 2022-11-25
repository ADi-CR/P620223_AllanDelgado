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
    public partial class FrmClienteBuscar : Form
    {
        //objetos locales 

        DataTable DtLista { get; set; }

        Logica.Models.Cliente MiClienteLocal { get; set; }

        public FrmClienteBuscar()
        {
            InitializeComponent();

            DtLista= new DataTable();
            MiClienteLocal= new Logica.Models.Cliente();

        }

        private void FrmClienteBuscar_Load(object sender, EventArgs e)
        {
            LlenarLista();
        }

        private void LlenarLista()
        { 
            DtLista = new DataTable();

            DtLista = MiClienteLocal.Listar(true, TxtBuscar.Text.Trim());
            
            DgvLista.DataSource = DtLista;
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {

            if (TxtBuscar.Text.Count() > 2 || 
                string.IsNullOrEmpty(TxtBuscar.Text.Trim()))
            {
                LlenarLista();
            }

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {

            if (DgvLista.SelectedRows.Count == 1)
            {
                DataGridViewRow FilaSeleccionada = DgvLista.SelectedRows[0];

                int IdCliente = Convert.ToInt32(FilaSeleccionada.Cells["CIDCliente"].Value);

                Globales.MiFormFacturacion.TxtClienteID.Text = Convert.ToString(IdCliente);

                this.DialogResult = DialogResult.OK;

            }

        }
    }
}
