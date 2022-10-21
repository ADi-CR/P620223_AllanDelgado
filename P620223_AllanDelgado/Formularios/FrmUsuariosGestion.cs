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
    public partial class FrmUsuariosGestion : Form
    {
        private Logica.Models.Usuario MiUsuarioLocal { get; set; }


        public FrmUsuariosGestion()
        {
            InitializeComponent();

            MiUsuarioLocal = new Logica.Models.Usuario();
        }

        private void FrmUsuariosGestion_Load(object sender, EventArgs e)
        {
            //establecer como padre el formulario principal, de forma que se anide dentro del mismo. 
            MdiParent = Globales.MiFormPrincipal;

            CargarRolesDeUsuario();
            CargarEmpresas();

        }

        private void CargarRolesDeUsuario()
        { 
            //Paso 1 y 1.1 de Seq Usuario Rol Listar
            Logica.Models.UsuarioRol MiRolUsuario = new Logica.Models.UsuarioRol();

            //paso 2 y 2.5
            DataTable dt = new DataTable();
            dt = MiRolUsuario.Listar();

            //paso 3 
            if (dt != null && dt.Rows.Count > 0)
            {
                CboxRolUsuario.ValueMember = "IDUsuarioRol";
                CboxRolUsuario.DisplayMember = "Rol";
                CboxRolUsuario.DataSource = dt;
                CboxRolUsuario.SelectedIndex = -1;
            }
        }

        private void CargarEmpresas()
        {
            Logica.Models.Empresa MiEmpresa = new Logica.Models.Empresa();

            DataTable dt = new DataTable();
            dt = MiEmpresa.Listar();

            if (dt != null && dt.Rows.Count > 0)
            {
                CboxEmpresa.ValueMember = "ID";
                CboxEmpresa.DisplayMember = "D";
                CboxEmpresa.DataSource = dt;
                CboxEmpresa.SelectedIndex = -1;
            }
        }








    }
}
