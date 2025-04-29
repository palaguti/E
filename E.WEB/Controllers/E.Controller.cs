using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E.WEB.Controllers
{
	public class E : Controller
	{
		private readonly PersonaEBL _personaBL;

		public E(PersonaEBL personaBL)
		{
			_personaBL = personaBL;
		}

		public async Task<IActionResult> Index()
		{
			var personas = await _personaBL.ObtenerTodosAsync();
			return View(personas);
		}

		public async Task<IActionResult> Details(int id)
		{
			var persona = await _personaBL.ObtenerPorIdAsync(new PersonaE { Id = id });
			return View(persona);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(PersonaE persona)
		{
			if (ModelState.IsValid)
			{
				await _personaBL.CrearAsync(persona);
				return RedirectToAction(nameof(Index));
			}
			return View(persona);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var persona = await _personaBL.ObtenerPorIdAsync(new PersonaE { Id = id });
			return View(persona);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(PersonaE persona)
		{
			if (ModelState.IsValid)
			{
				await _personaBL.ModificarAsync(persona);
				return RedirectToAction(nameof(Index));
			}
			return View(persona);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var persona = await _personaBL.ObtenerPorIdAsync(new PersonaE { Id = id });
			return View(persona);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _personaBL.EliminarAsync(new PersonaE { Id = id });
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> ReportePersona()
		{
			var personas = await _personaBL.ObtenerTodosAsync();
			return new ViewAsPdf("rpPersona", personas);
		}

		public async Task<JsonResult> PersonasJson()
		{
			var personas = await _personaBL.ObtenerTodosAsync();
			var datos = personas.Select(p => new
			{
				id = p.Id,
				nombre = p.Nombre,
				telefono = p.Telefono,
				correo = p.Correo
			}).ToList();
			return Json(datos);
		}

		public async Task<IActionResult> ReportePersonasExcel()
		{
			var personas = await _personaBL.ObtenerTodosAsync();
			using (var package = new ExcelPackage())
			{
				var hoja = package.Workbook.Worksheets.Add("Personas");

				hoja.Cells["A1"].Value = "Nombre";
				hoja.Cells["B1"].Value = "Telefono";
				hoja.Cells["C1"].Value = "Correo";

				int row = 2;
				foreach (var p in personas)
				{
					hoja.Cells[row, 1].Value = p.Nombre;
					hoja.Cells[row, 2].Value = p.Telefono;
					hoja.Cells[row, 3].Value = p.Correo;
					row++;
				}
				hoja.Cells["A:C"].AutoFitColumns();

				var stream = new MemoryStream();
				package.SaveAs(stream);
				stream.Position = 0;

				return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportePersonasExcel.xlsx");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SubirExcelPersona(IFormFile archivoExcel)
		{
			if (archivoExcel == null || archivoExcel.Length == 0)
			{
				return RedirectToAction(nameof(Index));
			}

			var personas = new List<PersonaE>();

			using (var stream = new MemoryStream())
			{
				await archivoExcel.CopyToAsync(stream);
				using (var package = new ExcelPackage(stream))
				{
					var hoja = package.Workbook.Worksheets[0];
					int rowCount = hoja.Dimension.Rows;

					for (int row = 2; row <= rowCount; row++)
					{
						var nombre = hoja.Cells[row, 1].Text;
						var telefono = hoja.Cells[row, 2].Text;
						var correo = hoja.Cells[row, 3].Text;

						if (!string.IsNullOrWhiteSpace(nombre))
						{
							personas.Add(new PersonaE
							{
								Nombre = nombre,
								Telefono = telefono,
								Correo = correo
							});
						}
					}
				}
			}

			if (personas.Any())
			{
				await _personaBL.AgregarTodosAsync(personas);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
