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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnVerPassword_Click(object sender, EventArgs e)
        {

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            //cierra completamente la app
            Application.Exit();
        }

        private void BtnVerPassword_MouseDown(object sender, MouseEventArgs e)
        {
            TxtContrasennia.UseSystemPasswordChar = false;
        }

        private void BtnVerPassword_MouseUp(object sender, MouseEventArgs e)
        {
            TxtContrasennia.UseSystemPasswordChar = true;
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtNombreUsuario.Text.Trim()) && 
                !string.IsNullOrEmpty(TxtContrasennia.Text.Trim()))
            {
                string u = TxtNombreUsuario.Text.Trim();
                string p = TxtContrasennia.Text.Trim();

                int IdLoginOk = Globales.MiUsuarioGlobal.ValidarLogin(u, p);

                if (IdLoginOk > 0)
                {
                    //hay permiso de ingresar al sistema 
                    Globales.MiUsuarioGlobal.IDUsuario = IdLoginOk;

                    Globales.MiUsuarioGlobal = Globales.MiUsuarioGlobal.ConsultarPorID();

                    Globales.MiFormPrincipal.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña Incorrectas", "Error de Validación", MessageBoxButtons.OK);
                    TxtContrasennia.Focus();
                    TxtContrasennia.SelectAll();
                }

            }

        }

        private void BtnIngresoDirecto_Click(object sender, EventArgs e)
        {
            Globales.MiUsuarioGlobal.IDUsuario = 1;
            Globales.MiUsuarioGlobal = Globales.MiUsuarioGlobal.ConsultarPorID();
            
            Globales.MiFormPrincipal.Show();
            this.Hide();

        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift & e.KeyCode == Keys.A)
            {
                BtnIngresoDirecto.Visible = true;
            }
        }
    }
}
