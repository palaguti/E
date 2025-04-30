using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E.BL;
using E.EN;

namespace E.WEB.Controllers
{
    public class PersonaEController : Controller
    {
        readonly PersonaEBL _personaEBL;

        public PersonaEController(PersonaEBL personaEBL)
        {
            _personaEBL = personaEBL;
        }

        public async Task<ActionResult> Index()
        {
            var personas = await _personaEBL.ObtenerTodosAsync();
            return View(personas);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonaE persona)
        {
            try
            {
                await _personaEBL.CrearAsync(persona);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var persona = await _personaEBL.ObtenerPorIdAsync(new PersonaE { Id = id });
            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PersonaE persona)
        {
            try
            {
                await _personaEBL.ModificarAsync(persona);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var persona = await _personaEBL.ObtenerPorIdAsync(new PersonaE { Id = id });
            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePersona(int id)
        {
            try
            {
                await _personaEBL.EliminarAsync(new PersonaE { Id = id });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
