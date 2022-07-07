using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Examen_Ingreso.Entidades
{
    public class Cliente
    {
        [Key]
        public int intIdentificacion { get; set; }

        public string strNombre { get; set; }

        public int intEdad { get; set; }

        public Double? dblIngresosMensuales { get; set; }
    }
}
