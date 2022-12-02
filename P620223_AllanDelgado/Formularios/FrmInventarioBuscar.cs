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
    public partial class FrmInventarioBuscar : Form
    {
        public DataTable DtLista { get; set; }
        public Logica.Models.Inventario MyLocalItem { get; set; }

        #region PropiedadesDeTotalizacion
        public decimal SubTotal1 { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal SubTotal2 { get; set; }
        public decimal TotalImpuesto { get; set; }
        public decimal Total { get; set; }
        #endregion

        public FrmInventarioBuscar()
        {
            InitializeComponent();

            DtLista= new DataTable();
            MyLocalItem = new Logica.Models.Inventario();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmInventarioBuscar_Load(object sender, EventArgs e)
        {
            CargarItems();
        }

        private void CargarItems()
        { 
            DtLista = MyLocalItem.Listar();
            
            DgvLista.DataSource = DtLista;
            DgvLista.ClearSelection();
        }

        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvLista.SelectedRows.Count == 1)
            {
                DataGridViewRow fila = DgvLista.SelectedRows[0];

                decimal Precio = Convert.ToDecimal(fila.Cells["CPrecioVenta"].Value);
                decimal IVA = Convert.ToDecimal(fila.Cells["CTasaImpuesto"].Value);

                TxtIVA.Text = IVA.ToString();
                TxtPrecioUnitario.Text = Precio.ToString();

            }
        }

        private void TxtDescuento_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtDescuento.Text.Trim()))
            {
                TxtDescuento.Text = "0";
                TxtDescuento.SelectAll();
            }
        }

        private void NudCantidad_Leave(object sender, EventArgs e)
        {
            Calcular();
        }

        private void TxtDescuento_Leave(object sender, EventArgs e)
        {
            if (ValidarDescuento())
            {
                Calcular();
            }
           
        }

        private void Calcular()
        {
            //Este método calcula los valores de
            //subtotal, descuento, impuesto y total para la línea 
            //en este caso particular se debe validar que el descuento
            //no supere el 100%
            if (ValidarDescuento())
            {

            }
        
        }

        private bool ValidarDescuento()
        { 
            bool R = false;
            try
            {
                if (!string.IsNullOrEmpty(TxtDescuento.Text.Trim()) &&
                Convert.ToDecimal(TxtDescuento.Text.Trim()) >= 0 &&
                Convert.ToDecimal(TxtDescuento.Text.Trim()) <= 100)
                {
                    R = true;
                }
                else
                {
                    if (Convert.ToDecimal(TxtDescuento.Text.Trim()) > 100)
                    {
                        MessageBox.Show("Los descuentos no pueden ser superiores a 100", "Error de validación", MessageBoxButtons.OK);
                        return false;
                    }
                    if (Convert.ToDecimal(TxtDescuento.Text.Trim()) < 0)
                    {
                        MessageBox.Show("Los descuentos no pueden ser números negativos", "Error de validación", MessageBoxButtons.OK);
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                TxtDescuento.Text = "0";
                TxtDescuento.SelectAll();
            }

            return R;
        }

        private void TxtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, false);
        }
    }
}
