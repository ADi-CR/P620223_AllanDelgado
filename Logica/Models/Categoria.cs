using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Categoria
    {
        //primero escribimos las propiedades simple
        
        public int IDCategoria { get; set; }

        public string NombreCategoria { get; set; }


        //escribirmos las funciones y métodos (operaciones) 
        public DataTable Listar()
        { 
            DataTable R = new DataTable();
            
            //TODO: hay que programar la llamada al SP que retorna datos 


            return R;
        }


    }
}
