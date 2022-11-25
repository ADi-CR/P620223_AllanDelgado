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
    public partial class FrmPrincipalMDI : Form
    {
        public FrmPrincipalMDI()
        {
            InitializeComponent();
        }

        private void FrmPrincipalMDI_Load(object sender, EventArgs e)
        {
            //mostrar el usuario que está logueado en el sistema. 
            string InfoUsuario = string.Format("{0}({1})",
                                    Globales.MiUsuarioGlobal.Nombre,
                                    Globales.MiUsuarioGlobal.MiRol.Rol);

            LblUsuarioLogueado.Text = InfoUsuario;

            //se debe filtrar las opciones de acceso a menus según el rol que 
            //tenga el usuario 

            switch (Globales.MiUsuarioGlobal.MiRol.IDUsuarioRol)
            {
                case 1:
                    //no hace falta ocultar opciones ya que es admin
                break;

                case 2:
                    //sería tipo bodeguero
                    MnuProcesos.Visible= false;
                    
                    MnuUsuariosGestion.Visible= false;
                    MnuImpuestosGestion.Visible= false;
                    MnuClientesGestion.Visible= false;

                    MnuListaClientes.Visible= false;
                    MnuFacturasReimpresion.Visible= false;
                    MnuVentasCliente.Visible= false;
                    MnuVentasRangoFechas.Visible= false;
                    MnuVentasUsuario.Visible= false;
                break;

                case 3:
                    //sería facturador
                    MnuUsuariosGestion.Visible= false;
                    MnuImpuestosGestion.Visible= false;
                    MnuInventarioGestion.Visible= false;
                    MnuCategoriasGestion.Visible= false;
                    MnuProveedoresGestion.Visible= false;

                    MnuListaProveedores.Visible= false;

                break;
            }

            TmrFechaHora.Enabled = true;

        }

        private void FrmPrincipalMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            //al cerrar el formulario principal se debería terminar la aplicación 

            //en el caso que lo que queremos es volver a mostrar el form de login
            //deberíamos agregar un objeto global para dicho formulario. 

            Application.Exit();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //en el caso del form de gestión de usuarios es necesario establecerlo
            //en globales 

            //si queremos que un formulario se muestre solo una vez deberíamos verificar
            //que esté visible o no y en caso lo esté se reinstacia y muestra de nuevo 

            // el simbolo ! niega una condición lógica
            if (!Globales.MiFormMantUsuarios.Visible)
            {
                Globales.MiFormMantUsuarios = new FrmUsuariosGestion();
                Globales.MiFormMantUsuarios.Show();
            }

        }

        private void TmrFechaHora_Tick(object sender, EventArgs e)
        {
            //Asignar al label de fecha y hora el valor en formato extendido de la fecha
            // y además la hora 

            string fecha = DateTime.Now.Date.ToLongDateString();
            string hora = DateTime.Now.ToLongTimeString();

            LblFechaYHora.Text = fecha + "/" + hora;

        }

        private void MnuFacturacion_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormFacturacion.Visible)
            {
                Globales.MiFormFacturacion = new FrmFacturacion(); 
                Globales.MiFormFacturacion.Show();

            }
        }
    }
}
