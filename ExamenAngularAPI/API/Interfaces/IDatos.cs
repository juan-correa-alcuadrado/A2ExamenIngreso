using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen_Ingreso.Entidades;

namespace Examen_Ingreso.Interfaces
{
    public interface IDatos
    {

        Task<List<Cliente>> ConsultarPorIdentificacion(int intIdentificacion);

        Task<List<Cliente>> ConsultarPorNombre(string strNombre);

        Task<List<Cliente>> Consultar();
        Task<string> Crear(Cliente cliente);
        Task<string> Actualizar(Cliente cliente);
        Task<string> Eliminar(Cliente cliente);
    }
}
