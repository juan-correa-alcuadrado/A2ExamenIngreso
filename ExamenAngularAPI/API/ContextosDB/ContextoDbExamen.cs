
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen_Ingreso.Entidades;

namespace Examen_Ingreso.ContextosDB
{
    /// <summary>
    /// Contexto de conexión hacia la base de datos
    /// </summary>
    public class ContextoDbExamen : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ContextoDbExamen(DbContextOptions<ContextoDbExamen> opciones) : base(opciones)
        {

        }



        public DbSet<Cliente> tblclientes { get; set; }

    }
}
