﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Empresa
    {
        //atributos simples
        public int IDEmpresa { get; set; }
        public string CedulaEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public string EmailEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public bool Activo { get; set; }

        //funciones y métodos

        public DataTable Listar()
        {
            DataTable R = new DataTable();
            //TODO: darle funcionalidad
            return R;
        }

    }
}