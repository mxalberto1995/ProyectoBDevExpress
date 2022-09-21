using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DevExtremePB_Api.Models;

namespace DevExtremePB_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CategoriaDExpController : Controller
    {
        private dbventasContext _context;

        public CategoriaDExpController(dbventasContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var categoria = _context.Categoria.Select(i => new {
                i.Idcategoria,
                i.Nombre,
                i.Descripcion
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Idcategoria" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(categoria, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Categorium();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Categoria.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Idcategoria });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Categoria.FirstOrDefaultAsync(item => item.Idcategoria == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Categoria.FirstOrDefaultAsync(item => item.Idcategoria == key);

            _context.Categoria.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Categorium model, IDictionary values) {
            string IDCATEGORIA = nameof(Categorium.Idcategoria);
            string NOMBRE = nameof(Categorium.Nombre);
            string DESCRIPCION = nameof(Categorium.Descripcion);

            if(values.Contains(IDCATEGORIA)) {
                model.Idcategoria = Convert.ToInt32(values[IDCATEGORIA]);
            }

            if(values.Contains(NOMBRE)) {
                model.Nombre = Convert.ToString(values[NOMBRE]);
            }

            if(values.Contains(DESCRIPCION)) {
                model.Descripcion = Convert.ToString(values[DESCRIPCION]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}