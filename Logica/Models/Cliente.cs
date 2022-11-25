using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Cliente
    {
        //atributos simples
        public int IDCliente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }

        //En este caso hay un atrib compuesto que une la clase Cliente con
        //la clase Empresa. Esto debido a la lectura de la relación 
        //que existe desde el diagrama entidad-relación.

        //Atributos Complejos
        public Empresa MiEmpresa { get; set; }

        //Cuando tratamos atributos complejos, estos normalmente se definen como
        //cualquier propiedad, pero esa prop no está instanciada. 
        //Por lo tanto, se debe instanciar en el contructor de la clase. 

        public Cliente()
        {
            //este es el constructor de la clase. 
            MiEmpresa = new Empresa();
        }


        //Fns y métodos 
        public bool Agregar()
        {
            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //INSERT correspondiente y retornar true si
            //todo sale bien 
            return R;
        }

        public bool Modificar()
        {
            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //UPDATE correspondiente y retornar true si
            //todo sale bien 
            return R;
        }

        public bool Eliminar()
        {
            //en las eliminaciones lógicas, lo que haremos será cambiar el valor del
            //campo Activo a 0 (false) 

            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //UPDATE correspondiente y retornar true si
            //todo sale bien 
            return R;
        }

        public Cliente ConsultarPorID(int pIdCliente)
        {
            Cliente R = new Cliente();

            Conexion MyCnn = new Conexion();

            MyCnn.ListaParametros.Add(new SqlParameter("@ID", pIdCliente));

            DataTable DataCliente = new DataTable();
            DataCliente = MyCnn.EjecutarSelect("SPClienteConsultarPorID");

            //Una vez tenemos un datatable con la data procedemos a llenar las
            //propiedades del objeto de retono. 

            if (DataCliente != null && DataCliente.Rows.Count > 0)
            {
                DataRow Fila = DataCliente.Rows[0];

                R.IDCliente = Convert.ToInt32(Fila["IdCliente"]);
                R.Nombre = Convert.ToString(Fila["Nombre"]);
                R.Cedula = Convert.ToString(Fila["Cedula"]);
                R.CorreoElectronico = Convert.ToString(Fila["CorreoElectronico"]);
                R.Telefono = Convert.ToString(Fila["Telefono"]);
                R.Direccion = Convert.ToString(Fila["Direccion"]);
                R.MiEmpresa.IDEmpresa = Convert.ToInt32(Fila["IDEmpresa"]);
                R.Activo = Convert.ToBoolean(Fila["Activo"]);
            }

            return R;
        }

        public bool ConsultarPorCedula()
        {
            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //SELECT correspondiente y retornar true si
            //todo sale bien 
            return R;
        }

        public DataTable Listar(bool VerActivos = true, string Filtro = "")
        {
            DataTable R = new DataTable();
            
            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@VerActivos", VerActivos));
            MiCnn.ListaParametros.Add(new SqlParameter("@FiltroBusqueda", Filtro));

            R = MiCnn.EjecutarSelect("SPClienteListar");

            return R;
        }


    }
}
