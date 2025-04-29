using System;
using E.EN;
using E.BL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E.DAL
{
	public class PersonaEDAL
	{
		readonly EDBContext dbContext;

		public PersonaEDAL(EDBContext dbContext)
		{
			dbContext = eDBContext;
		}

		public async Task<int> CrearAsync(PersonaE pPersonaE)
		{
			_dbContext.PersonaE.Add(pPersonaE);
			return await _dbContext.SaveChangesAsync();
		}

		public async Task<int> EliminarAsync(PersonaE pPersonaE)
		{
			var personaE = await _dbContext.PersonaE.FirstOrDefaultAsync(s => s.Id == pPersonaE.Id);
			if (personaE != null)
			{
				_dbContext.PersonaE.Remove(personaE);
				return await _dbContext.SaveChangesAsync();
			}
			return 0;
		}

		public async Task<int> ModificarAsync(PersonaE pPersonaE)
		{
			var personaE = await _dbContext.PersonaE.FirstOrDefaultAsync(s => s.Id == pPersonaE.Id);
			if (personaE != null)
			{
				personaE.NombreE = pPersonaE.NombreE;
				personaE.ApellidoE = pPersonaE.ApellidoE;
				personaE.FechaNacimientoE = pPersonaE.FechaNacimientoE;
				personaE.SueldoE = pPersonaE.SueldoE;
				personaE.EstatusE = pPersonaE.EstatusE;
				personaE.ComentarioE = pPersonaE.ComentarioE;

				_dbContext.PersonaE.Update(personaE);
				return await _dbContext.SaveChangesAsync();
			}
			return 0;
		}

		public async Task<PersonaE> ObtenerPorIdAsync(PersonaE persona)
		{
			var personaEncontrada = await _dbContext.PersonaE.FirstOrDefaultAsync(p => p.Id == persona.Id);
			if (personaEncontrada != null)
			{
				return personaEncontrada;
			}
			return new PersonaE();
		}

		public async Task<List<PersonaE>> ObtenerTodosAsync()
		{
			return await _dbContext.PersonaE.ToListAsync();
		}

		public async Task<List<PersonaE>> BuscarAsync(PersonaE persona)
		{
			var query = _dbContext.PersonaE.AsQueryable();

			if (!string.IsNullOrWhiteSpace(persona.NombreE))
			{
				query = query.Where(p => p.NombreE.Contains(persona.NombreE));
			}

			if (!string.IsNullOrWhiteSpace(persona.ApellidoE))
			{
				query = query.Where(p => p.ApellidoE.Contains(persona.ApellidoE));
			}

			if (!string.IsNullOrWhiteSpace(persona.EstatusE))
			{
				query = query.Where(p => p.EstatusE == persona.EstatusE);
			}

			return await query.ToListAsync();
		}
	}
}