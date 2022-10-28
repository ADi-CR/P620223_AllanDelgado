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

        public DataTable ListaUsuarios { get; set; }

        public FrmUsuariosGestion()
        {
            InitializeComponent();

            MiUsuarioLocal = new Logica.Models.Usuario();

            ListaUsuarios = new DataTable();

        }

        private void FrmUsuariosGestion_Load(object sender, EventArgs e)
        {
            //establecer como padre el formulario principal, de forma que se anide dentro del mismo. 
            MdiParent = Globales.MiFormPrincipal;

            CargarRolesDeUsuario();
            CargarEmpresas();

            LlenarListaUsuarios();
        }

        private void LlenarListaUsuarios()
        {
            ListaUsuarios = new DataTable();

            ListaUsuarios = MiUsuarioLocal.Listar(CbVerActivos.Checked, 
                                                  TxtBuscar.Text.Trim());

            DgvLista.DataSource = ListaUsuarios;

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

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            //lo primero que debemos hacer  es validar que los datos mínimos necesarios
            //están completos. 

            //Luego, se procede a realizar las 3 validaciones que se indican por medio de
            //los include (uses) en el diagrama de casos de uso expandido
            //Si las 3 validaciones son NEGATIVAS se tiene permiso para agregar el usuario. 

            if (ValidarCamposRequeridos())
            {
                //los campos están completos 
                bool CPorCedula;
                bool CPorNombreUsuario;
                bool CPorEmail;

                //se crea una nueva instacia del usuario local y se agregan los valores
                //de los atributos
                MiUsuarioLocal = new Logica.Models.Usuario();

                MiUsuarioLocal.Nombre = TxtNombre.Text.Trim();
                MiUsuarioLocal.Cedula = TxtCedula.Text.Trim();
                MiUsuarioLocal.NombreUsuario = TxtNombreUsuario.Text.Trim();
                MiUsuarioLocal.Email = TxtCorreo.Text.Trim();
                MiUsuarioLocal.Contrasennia = TxtContrasennia.Text.Trim();

                MiUsuarioLocal.MiEmpresa.IDEmpresa = Convert.ToInt32(CboxEmpresa.SelectedValue);
                MiUsuarioLocal.MiRol.IDUsuarioRol = Convert.ToInt32(CboxRolUsuario.SelectedValue);

                CPorCedula = MiUsuarioLocal.ConsultarPorCedula();
                CPorNombreUsuario = MiUsuarioLocal.ConsultarPorNombreUsuario();
                CPorEmail = MiUsuarioLocal.ConsultarPorEmail();

                if (!CPorCedula && !CPorNombreUsuario && !CPorEmail)
                {
                    string Msg = string.Format("¿Está seguro de agregar al usuario: {0}?", MiUsuarioLocal.Nombre);

                    DialogResult respuesta = MessageBox.Show(Msg, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {
                        //si las 3 validaciones son negativas, agregamos el usuario
                        bool Ok = MiUsuarioLocal.Agregar();

                        if (Ok)
                        {
                            MessageBox.Show("Usuario Agregado Correctamente!", ":)", MessageBoxButtons.OK);
                            LimpiarFormulario();
                        }
                        else
                        {
                            MessageBox.Show("No se agregó el usuario", ":(", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    if (CPorCedula)
                    {
                        MessageBox.Show("Ya existe un usuario con la Cédula digitada", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                    if (CPorNombreUsuario)
                    {
                        MessageBox.Show("Ya existe un usuario con el Nombre de Usuario digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                    if (CPorEmail)
                    {
                        MessageBox.Show("Ya existe un usuario con el Correo digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                }

            }

        }

        private bool ValidarCamposRequeridos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(TxtNombre.Text.Trim()) && 
                !string.IsNullOrEmpty(TxtCedula.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtNombreUsuario.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtCorreo.Text.Trim()) && 
                !string.IsNullOrEmpty(TxtContrasennia.Text.Trim()) &&
                CboxEmpresa.SelectedIndex > -1 && 
                CboxRolUsuario.SelectedIndex > -1)
            {
                R = true;
            }
            else
            {
                //se le informa al usuario qué validación está fallando 
                //estas validaciones en negativo deben ser puntuales para poder
                //explicar al usuario qué está mal. 
                if (string.IsNullOrEmpty(TxtNombre.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el Nombre", "Error de validación!", MessageBoxButtons.OK);
                    TxtNombre.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(TxtCedula.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar la Cédula", "Error de validación!", MessageBoxButtons.OK);
                    TxtCedula.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(TxtNombreUsuario.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el Nombre de Usuario", "Error de validación!", MessageBoxButtons.OK);
                    TxtNombreUsuario.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(TxtCorreo.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el Correo", "Error de validación!", MessageBoxButtons.OK);
                    TxtCorreo.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(TxtContrasennia.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar la Contraseña", "Error de validación!", MessageBoxButtons.OK);
                    TxtContrasennia.Focus();
                    return false;
                }

                //validaciones de combobox 
                if (CboxEmpresa.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar una Empresa", "Error de validación!", MessageBoxButtons.OK);
                    CboxEmpresa.Focus();
                    return false;
                }

                if (CboxRolUsuario.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar en Rol de Usuario", "Error de validación!", MessageBoxButtons.OK);
                    CboxRolUsuario.Focus();
                    return false;
                }

            }

            return R; 
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            TxtCodigo.Clear();
            TxtNombre.Clear();
            TxtCedula.Clear();
            TxtNombreUsuario.Clear();
            TxtCorreo.Clear();
            TxtContrasennia.Clear();

            CboxRolUsuario.SelectedIndex = -1;
            CboxEmpresa.SelectedIndex = -1;
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true);
        }

        private void TxtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void TxtNombreUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false ,true);
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) &&
                TxtBuscar.Text.Count() > 2)
            {
                LlenarListaUsuarios();
            }
            else if (string.IsNullOrEmpty(TxtBuscar.Text.Trim()))
            {
                LlenarListaUsuarios();
            }
        }
    }
}
