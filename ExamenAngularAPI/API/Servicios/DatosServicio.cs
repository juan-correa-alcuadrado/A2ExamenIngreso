using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen_Ingreso.ContextosDB;
using Examen_Ingreso.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Examen_Ingreso.Interfaces;

namespace Examen_Ingreso.Servicios
{
    public partial class DatosServicio : IDatos
    {
        private readonly ContextoDbExamen _contextobd;
        public DatosServicio(ContextoDbExamen contextobd)
        {
            this._contextobd = contextobd;

        }

        public async Task<List<Cliente>> ConsultarPorIdentificacion(int intIdentificacion) 
        {

            return await _contextobd.tblclientes.FromSqlRaw("Select * from tblclientes where intidentificacion = identificacion",
                        new SqliteParameter("identificacion", (object)intIdentificacion ?? DBNull.Value)
                        ).ToListAsync();


        }

        public async Task<List<Cliente>> Consultar()
        {

            return await _contextobd.tblclientes.FromSqlRaw("Select * from tblclientes").ToListAsync();

        }

        public async Task<List<Cliente>> ConsultarPorNombre(string strnombre)
        {

            return await _contextobd.tblclientes.FromSqlRaw("Select * from tblclientes where strnombre = strnombre",
                        new SqliteParameter("nombre", (object)strnombre ?? DBNull.Value)
                        ).ToListAsync();

        }


        public async Task<string> Crear(Cliente cliente)
        {
            _contextobd.Add(cliente);
            await _contextobd.SaveChangesAsync();
            return "Cliente " + cliente.strNombre + " con identificación " + cliente.intIdentificacion + " creado de forma exitosa";
        }

        public async Task<string> Actualizar(Cliente cliente)
        {
            _contextobd.Update(cliente);
            await _contextobd.SaveChangesAsync();
            return "Cliente con identificación " + cliente.intIdentificacion + " actualizado de forma exitosa";
        }

        public async Task<string> Eliminar(Cliente cliente)
        {
            _contextobd.Remove(cliente);
            await _contextobd.SaveChangesAsync();
            return "Cliente con identificación " + cliente.intIdentificacion + " eliminado de forma exitosa";
        }


    }
}
