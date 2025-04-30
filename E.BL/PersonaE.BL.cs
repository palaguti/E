using E.DAL;
using E.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.BL
{
    public class PersonaEBL
    {
        private readonly PersonaEDAL _personaEDAL;

        public PersonaEBL(PersonaEDAL personaEDAL)
        {
            _personaEDAL = personaEDAL;
        }

        public async Task<int> CrearAsync(PersonaE pPersonaE)
        {
            return await _personaEDAL.CrearAsync(pPersonaE);
        }

        public async Task<int> ModificarAsync(PersonaE pPersonaE)
        {
            return await _personaEDAL.ModificarAsync(pPersonaE);
        }

        public async Task<int> EliminarAsync(PersonaE pPersonaE)
        {
            return await _personaEDAL.EliminarAsync(pPersonaE);
        }

        public async Task<PersonaE> ObtenerPorIdAsync(PersonaE pPersonaE)
        {
            return await _personaEDAL.ObtenerPorIdAsync(pPersonaE);
        }

        public async Task<List<PersonaE>> ObtenerTodosAsync()
        {
            return await _personaEDAL.ObtenerTodosAsync();
        }

        public async Task<List<PersonaE>> BuscarAsync(PersonaE pPersonaE)
        {
            return await _personaEDAL.BuscarAsync(pPersonaE);
        }
    }

}
