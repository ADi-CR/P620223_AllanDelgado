using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Logica.Models
{
    public class Usuario
    {
        //atributos simples
        public int IDUsuario { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasennia { get; set; }
        public string CodigoRecuperacion { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }

        //atributos compuestos 
        public Empresa MiEmpresa { get; set; }
        public UsuarioRol MiRol { get; set; }

        public Usuario()
        {
            MiEmpresa = new Empresa();
            MiRol = new UsuarioRol();
            Activo = true;
        }

        //fns y methods
        public bool Agregar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            //lista de parámetros para el insert
            MiCnn.ListaParametros.Add(new SqlParameter("@Nombre", this.Nombre));
            MiCnn.ListaParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListaParametros.Add(new SqlParameter("@NombreUsuario", this.NombreUsuario));

            //TODO: Se debe encriptar la contraseña que se va a almacenar en la tabla usuario
            MiCnn.ListaParametros.Add(new SqlParameter("@Contrasennia", this.Contrasennia));
            MiCnn.ListaParametros.Add(new SqlParameter("@Email", this.Email));

            //Parametros para los FKs, normalmente son de objetos compuestos de la clase 
            MiCnn.ListaParametros.Add(new SqlParameter("@IDRol", this.MiRol.IDUsuarioRol));
            MiCnn.ListaParametros.Add(new SqlParameter("@IDEmpresa", this.MiEmpresa.IDEmpresa));

            int Resultado = MiCnn.EjecutarUpdateDeleteInsert("SPUsuarioAgregar");

            if (Resultado > 0)
            {
                R = true;
            }

            return R;
        }

        public bool Modificar()
        {
            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //UPDATE correspondiente y retornar true si
            //todo sale bien 

            Conexion MiCnn = new Conexion();

            //lista de parámetros para el insert
            MiCnn.ListaParametros.Add(new SqlParameter("@Nombre", this.Nombre));
            MiCnn.ListaParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListaParametros.Add(new SqlParameter("@NombreUsuario", this.NombreUsuario));

            //TODO: Se debe encriptar la contraseña que se va a almacenar en la tabla usuario
            MiCnn.ListaParametros.Add(new SqlParameter("@Contrasennia", this.Contrasennia));
            MiCnn.ListaParametros.Add(new SqlParameter("@Email", this.Email));

            //Parametros para los FKs, normalmente son de objetos compuestos de la clase 
            MiCnn.ListaParametros.Add(new SqlParameter("@IDRol", this.MiRol.IDUsuarioRol));
            MiCnn.ListaParametros.Add(new SqlParameter("@IDEmpresa", this.MiEmpresa.IDEmpresa));

            MiCnn.ListaParametros.Add(new SqlParameter("@ID", this.IDUsuario));

            int Resultado = MiCnn.EjecutarUpdateDeleteInsert("SPUsuarioModificar");

            if (Resultado > 0)
            {
                R = true;
            }

            return R;
        }

        public bool Eliminar()
        {
            //en las eliminaciones lógicas, lo que haremos será cambiar el valor del
            //campo Activo a 0 (false) 

            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@ID", this.IDUsuario));

            int Resultado = MiCnn.EjecutarUpdateDeleteInsert("SPUsuarioEliminar");

            if (Resultado > 0)
            {
                R = true;
            }

            return R;
        }

        public bool Activar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@ID", this.IDUsuario));

            int Resultado = MiCnn.EjecutarUpdateDeleteInsert("SPUsuarioActivar");

            if (Resultado > 0)
            {
                R = true;
            }

            return R;
        }


        public Usuario ConsultarPorID()
        {
            Usuario R = new Usuario();
            //TODO: En este caso el retorno es del mismo tipo de la clase. 
            //por lo tanto hay que llenar los atributos con los datos
            //que entregue un SP Select

            Conexion MyCnn = new Conexion();

            MyCnn.ListaParametros.Add(new SqlParameter("@ID", this.IDUsuario));

            DataTable DataUsuario = new DataTable();
            DataUsuario = MyCnn.EjecutarSelect("SPUsuarioConsultarPorID");

            //Una vez tenemos un datatable con la data procedemos a llenar las
            //propiedades del objeto de retono. 

            if (DataUsuario != null && DataUsuario.Rows.Count > 0)
            {
                DataRow Fila = DataUsuario.Rows[0];

                R.IDUsuario = Convert.ToInt32(Fila["IDUsuario"]);
                R.Nombre = Convert.ToString(Fila["Nombre"]);
                R.Cedula = Convert.ToString(Fila["Cedula"]);
                R.NombreUsuario = Convert.ToString(Fila["NombreUsuario"]);
                R.Email = Convert.ToString(Fila["Email"]);
                R.Contrasennia = string.Empty;
                R.MiRol.IDUsuarioRol = Convert.ToInt32(Fila["IDUsuarioRol"]);
                R.MiEmpresa.IDEmpresa = Convert.ToInt32(Fila["IDEmpresa"]);
                R.Activo = Convert.ToBoolean(Fila["Activo"]);

            }
            
            return R;
        }

        public bool ConsultarPorID(int pIDUsuario)
        {
            bool R = false;
           
            Conexion MyCnn = new Conexion();

            MyCnn.ListaParametros.Add(new SqlParameter("@ID", pIDUsuario));

            DataTable DataUsuario = new DataTable();
            DataUsuario = MyCnn.EjecutarSelect("SPUsuarioConsultarPorID");

            if (DataUsuario != null && DataUsuario.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }


        public bool ConsultarPorCedula()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            //como en este caso debemos evaluar por la cédula, hay que pasar 1 parámetro al SP 
            //de consulta. 
            MiCnn.ListaParametros.Add(new SqlParameter("@Cedula", this.Cedula));

            DataTable Consulta = MiCnn.EjecutarSelect("SPUsuarioConsultarPorCedula");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                R = true;
            }
            
            return R;
        }

        public bool ConsultarPorNombreUsuario()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();
                       
            MiCnn.ListaParametros.Add(new SqlParameter("@NombreUsuario", this.NombreUsuario));

            DataTable Consulta = MiCnn.EjecutarSelect("SPUsuarioConsultarPorNombreUsuario");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }

        public bool ConsultarPorEmail()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Email", this.Email));

            DataTable Consulta = MiCnn.EjecutarSelect("SPUsuarioConsultarPorEmail");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }

        public DataTable Listar(bool VerActivos = true, string FiltroBusqueda = "")
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@VerActivos", VerActivos));
            MiCnn.ListaParametros.Add(new SqlParameter("@FiltroBusqueda", FiltroBusqueda));

            R = MiCnn.EjecutarSelect("SPUsuarioListar");

            return R;
        }

        public bool ValidarLogin(string NombreUsuario, string Contrasennia)
        {
            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //SELECT correspondiente y retornar true si
            //todo sale bien 
            return R;
        }

        public bool EnviarCodigoRecuperacion(string Email)
        {
            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //SELECT correspondiente y retornar true si
            //todo sale bien 
            return R;
        }

        public bool ResetearContrasennia()
        {
            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //SELECT correspondiente y retornar true si
            //todo sale bien 
            return R;
        }




    }
}
