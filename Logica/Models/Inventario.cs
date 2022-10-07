using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Inventario
    {
        public int IDInventario { get; set; }
        public string NombreItem { get; set; }
        public string CodigoBarras { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal CantidadEnStock { get; set; }
        public bool Activo { get; set; }

        //atributos compuestos

        public Empresa MiEmpresa { get; set; }
        public Categoria MiCategoria { get; set; }
        public Impuesto MiImpuesto { get; set; }

        //como la relación contra los proveedores (por medio de la tabla
        //de muchos a muchos) termina en *, quiere decir que podrían haber 
        //varios proveedores que ofrecen un item, y por lo tanto el atrubuto
        //es multivaluado, se debe usar List<>
        public List<InventarioProveedor> ListadoProveedores { get; set; }

        public Inventario()
        {
            MiEmpresa = new Empresa();
            MiCategoria = new Categoria();
            MiImpuesto = new Impuesto();
            ListadoProveedores = new List<InventarioProveedor>();
            Activo = true;
        }

        //fns y methods
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

        public Inventario ConsultarPorID()
        {
            Inventario R = new Inventario();
            //TODO: En este caso el retorno es del mismo tipo de la clase. 
            //por lo tanto hay que llenar los atributos con los datos
            //que entregue un SP Select

            return R;
        }

        public bool ConsultarPorCodigoBarras(int IDEmpresa)
        {
            //en las eliminaciones lógicas, lo que haremos será cambiar el valor del
            //campo Activo a 0 (false) 

            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //SELECT correspondiente y retornar true si
            //todo sale bien 
            return R;
        }

        public DataTable Listar(bool VerActivos = true)
        {
            DataTable R = new DataTable();


            return R;

        }

        public DataTable ListarProveedores()
        {
            DataTable R = new DataTable();

            return R;
        }

        public bool AgregarProveedor()
        {
            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //INSERT correspondiente y retornar true si
            //todo sale bien 
            return R;
        }

        public bool ModificarPrecioProveedor()
        {
            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //UPDATE correspondiente y retornar true si
            //todo sale bien 
            return R;
        }

        public bool EliminarProveedor()
        {
            //en las eliminaciones lógicas, lo que haremos será cambiar el valor del
            //campo Activo a 0 (false) 

            bool R = false;
            //TODO: ejecutar un SP que contenga la instruccion
            //UPDATE correspondiente y retornar true si
            //todo sale bien 
            return R;
        }


    }
}
