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
    }
}
