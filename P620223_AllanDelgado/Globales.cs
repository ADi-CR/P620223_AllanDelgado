using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P620223_AllanDelgado
{
    public static class Globales
    {
        //los objetos que definamos en esta clase 
        //serán accesibles desde la totalidad de 
        //la aplicación. Además esta clase al ser
        //static se auto instancia

        //el siguiente formulario principal, se puede 
        //invocar desde cualquier lugar (login en nuestro caso) 
        public static Form MiFormPrincipal = new Formularios.FrmPrincipalMDI();

        public static Formularios.FrmUsuariosGestion MiFormMantUsuarios = new Formularios.FrmUsuariosGestion();

    }
}
