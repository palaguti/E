using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E.EN;
using Microsoft.EntityFrameworkCore;

namespace E.DAL
{
    public class PersonaEDAL
    {
        readonly EDBContext dbContext;

        public PersonaEDAL(EDBContext eDBContext)
        {
            dbContext = eDBContext;
        }

        public async Task<int> CrearAsync(PersonaE pPersonaE)
        {
            PersonaE personaE = new PersonaE()
            {
                NombreE = pPersonaE.NombreE,
                ApellidoE = pPersonaE.ApellidoE,
                FechaNacimientoE = pPersonaE.FechaNacimientoE,
                SueldoE = pPersonaE.SueldoE,
                EstatusE = pPersonaE.EstatusE,
            };
            dbContext.PersonaE.Add(personaE);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(PersonaE pPersonaE)
        {
            var personaE = await dbContext.PersonaE.FirstOrDefaultAsync(s => s.Id == pPersonaE.Id);
            if (personaE != null && personaE.Id != 0)
            {
                dbContext.PersonaE.Remove(personaE);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(PersonaE pPersonaE)
        {
            var personaE = await dbContext.PersonaE.FirstOrDefaultAsync(s => s.Id == pPersonaE.Id);
            if (personaE != null && personaE.Id != 0)
            {
                personaE.NombreE = pPersonaE.NombreE;
                personaE.ApellidoE = pPersonaE.ApellidoE;
                personaE.FechaNacimientoE = pPersonaE.FechaNacimientoE;
                personaE.SueldoE = pPersonaE.SueldoE;
                personaE.EstatusE = pPersonaE.EstatusE;

                dbContext.Update(personaE);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<PersonaE> ObtenerPorIdAsync(PersonaE pPersonaE)
        {
            var personaE = await dbContext.PersonaE.FirstOrDefaultAsync(s => s.Id == pPersonaE.Id);
            if (personaE != null && personaE.Id != 0)
            {
                return new PersonaE
                {
                    Id = personaE.Id,
                    NombreE = personaE.NombreE,
                    ApellidoE = personaE.ApellidoE,
                    FechaNacimientoE = personaE.FechaNacimientoE,
                    SueldoE = personaE.SueldoE,
                    EstatusE = personaE.EstatusE,

                };
            }
            else
                return new PersonaE();
        }

        public async Task<List<PersonaE>> ObtenerTodosAsync()
        {
            var personasE = await dbContext.PersonaE.ToListAsync();
            if (personasE != null && personasE.Count > 0)
            {
                var list = new List<PersonaE>();
                personasE.ForEach(p => list.Add(new PersonaE
                {
                    Id = p.Id,
                    NombreE = p.NombreE,
                    ApellidoE = p.ApellidoE,
                    FechaNacimientoE = p.FechaNacimientoE,
                    SueldoE = p.SueldoE,
                    EstatusE = p.EstatusE,
                }));
                return list;
            }
            else
                return new List<PersonaE>();
        }

        public async Task<List<PersonaE>> BuscarAsync(PersonaE pPersonaE)
        {
            var query = dbContext.PersonaE.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pPersonaE.NombreE))
            {
                query = query.Where(p => p.NombreE.Contains(pPersonaE.NombreE));
            }

            if (!string.IsNullOrWhiteSpace(pPersonaE.ApellidoE))
            {
                query = query.Where(p => p.ApellidoE.Contains(pPersonaE.ApellidoE));
            }

            if (pPersonaE.EstatusE != 0)
            {
                query = query.Where(p => p.EstatusE == pPersonaE.EstatusE);
            }

            return await query.ToListAsync();

        }
    }
}
