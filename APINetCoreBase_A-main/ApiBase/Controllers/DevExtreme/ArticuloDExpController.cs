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
    public class ArticuloDExpController : Controller
    {
        private dbventasContext _context;

        public ArticuloDExpController(dbventasContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var articulos = _context.Articulos.Select(i => new {
                i.Idarticulo,
                i.Codigo,
                i.Nombre,
                i.Descripcion,
                i.Idcategoria,
                i.Idpresentacion
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Idarticulo" };
            // loadOptions.PaginateViaPrimaryKey = true;
          
            return Json(await DataSourceLoader.LoadAsync(articulos, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Articulo();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Articulos.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Idarticulo });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Articulos.FirstOrDefaultAsync(item => item.Idarticulo == key);
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
            var model = await _context.Articulos.FirstOrDefaultAsync(item => item.Idarticulo == key);

            _context.Articulos.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> CategoriaLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Categoria
                         orderby i.Nombre
                         select new {
                             Value = i.Idcategoria,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PresentacionsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Presentacions
                         orderby i.Nombre
                         select new {
                             Value = i.Idpresentacion,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Articulo model, IDictionary values) {
            string IDARTICULO = nameof(Articulo.Idarticulo);
            string CODIGO = nameof(Articulo.Codigo);
            string NOMBRE = nameof(Articulo.Nombre);
            string DESCRIPCION = nameof(Articulo.Descripcion);
            string IDCATEGORIA = nameof(Articulo.Idcategoria);
            string IDPRESENTACION = nameof(Articulo.Idpresentacion);

            if(values.Contains(IDARTICULO)) {
                model.Idarticulo = Convert.ToInt32(values[IDARTICULO]);
            }

            if(values.Contains(CODIGO)) {
                model.Codigo = Convert.ToString(values[CODIGO]);
            }

            if(values.Contains(NOMBRE)) {
                model.Nombre = Convert.ToString(values[NOMBRE]);
            }

            if(values.Contains(DESCRIPCION)) {
                model.Descripcion = Convert.ToString(values[DESCRIPCION]);
            }

            if(values.Contains(IDCATEGORIA)) {
                model.Idcategoria = Convert.ToInt32(values[IDCATEGORIA]);
            }

            if(values.Contains(IDPRESENTACION)) {
                model.Idpresentacion = Convert.ToInt32(values[IDPRESENTACION]);
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