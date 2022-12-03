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
        public decimal PrecioUnitario { set; get; }
        public decimal TasaImpuesto { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal Cantidad { get; set; }
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
            Limpiar();

            if (DgvLista.SelectedRows.Count == 1)
            {
                DataGridViewRow fila = DgvLista.SelectedRows[0];

                decimal Precio = Convert.ToDecimal(fila.Cells["CPrecioVenta"].Value);
                decimal IVA = Convert.ToDecimal(fila.Cells["CTasaImpuesto"].Value);

                TxtIVA.Text = IVA.ToString();
                TxtPrecioUnitario.Text = Precio.ToString();

                Calcular();
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
                Cantidad = Convert.ToDecimal(NudCantidad.Value);
                PorcentajeDescuento = Convert.ToDecimal(TxtDescuento.Text.Trim());

                PrecioUnitario = Convert.ToDecimal(TxtPrecioUnitario.Text.Trim());
                TasaImpuesto = Convert.ToDecimal(TxtIVA.Text.Trim());

                //1. Canculo del Subtotal 
                SubTotal1 = Cantidad * PrecioUnitario;

                if (PorcentajeDescuento > 0)
                {
                    TotalDescuento = (SubTotal1 * PorcentajeDescuento) / 100;
                }

                SubTotal2 = SubTotal1 - TotalDescuento;

                if (TasaImpuesto > 0)
                {
                    TotalImpuesto = (SubTotal2 * TasaImpuesto) / 100;
                }

                Total = SubTotal2 + TotalImpuesto;

                TxtTotal.Text = string.Format("{0:N2}", Total);

            }
        
        }

        private void Limpiar()
        {
            SubTotal1 = 0;
            TotalDescuento = 0;
            SubTotal2 = 0;
            TotalImpuesto = 0;
            Total = 0;
            PrecioUnitario = 0;
            TasaImpuesto = 0;
            PorcentajeDescuento = 0;
            Cantidad = 0;

            NudCantidad.Value = 1;
            TxtPrecioUnitario.Text = "0";
            TxtDescuento.Text = "0";
            TxtIVA.Text = "0";
            TxtTotal.Text = "0";

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

        private void NudCantidad_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            if (DgvLista.SelectedRows.Count == 1 && 
                !string.IsNullOrEmpty(TxtTotal.Text.Trim()) && 
                Convert.ToDecimal(TxtTotal.Text.Trim()) > 0)
            {
                DataGridViewRow r = DgvLista.SelectedRows[0];

                //estos dos valores se leen directamente de la selección del dgvLista
                //los demás ya los teníamos calculados en la props locales de totalización 

                int IdItem = Convert.ToInt32(r.Cells["CIDInventario"].Value);
                string NombreItem = Convert.ToString(r.Cells["CNombreItem"].Value);

                DataRow NuevaFilaEnFacturacion = Globales.MiFormFacturacion.DtListaItems.NewRow();

                NuevaFilaEnFacturacion["IDInventario"] = IdItem;
                NuevaFilaEnFacturacion["NombreItem"] = NombreItem;
                NuevaFilaEnFacturacion["PrecioVenta"] = PrecioUnitario;

                NuevaFilaEnFacturacion["Cantidad"] = Cantidad;
                NuevaFilaEnFacturacion["TasaImpuesto"] = TasaImpuesto;
                NuevaFilaEnFacturacion["PorcentajeDescuento"] = PorcentajeDescuento;
                NuevaFilaEnFacturacion["SubTotal"] = SubTotal1;
                NuevaFilaEnFacturacion["SubTotal2"] = SubTotal2;
                NuevaFilaEnFacturacion["ImpuestosTotal"] = TotalImpuesto;
                NuevaFilaEnFacturacion["TotalLinea"] = Total;
                NuevaFilaEnFacturacion["DescuentoTotal"] = TotalDescuento;
                
                Globales.MiFormFacturacion.DtListaItems.Rows.Add(NuevaFilaEnFacturacion);

                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
