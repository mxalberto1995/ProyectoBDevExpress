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
    public class ProveedorDExpController : Controller
    {
        private dbventasContext _context;

        public ProveedorDExpController(dbventasContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var proveedors = _context.Proveedors.Select(i => new {
                i.Idproveedor,
                i.RazonSocial,
                i.SectorComercial,
                i.TipoDocumento,
                i.NumDocumento,
                i.Direccion,
                i.Telefono,
                i.Email,
                i.Url
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Idproveedor" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(proveedors, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Proveedor();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Proveedors.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Idproveedor });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Proveedors.FirstOrDefaultAsync(item => item.Idproveedor == key);
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
            var model = await _context.Proveedors.FirstOrDefaultAsync(item => item.Idproveedor == key);

            _context.Proveedors.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Proveedor model, IDictionary values) {
            string IDPROVEEDOR = nameof(Proveedor.Idproveedor);
            string RAZON_SOCIAL = nameof(Proveedor.RazonSocial);
            string SECTOR_COMERCIAL = nameof(Proveedor.SectorComercial);
            string TIPO_DOCUMENTO = nameof(Proveedor.TipoDocumento);
            string NUM_DOCUMENTO = nameof(Proveedor.NumDocumento);
            string DIRECCION = nameof(Proveedor.Direccion);
            string TELEFONO = nameof(Proveedor.Telefono);
            string EMAIL = nameof(Proveedor.Email);
            string URL = nameof(Proveedor.Url);

            if(values.Contains(IDPROVEEDOR)) {
                model.Idproveedor = Convert.ToInt32(values[IDPROVEEDOR]);
            }

            if(values.Contains(RAZON_SOCIAL)) {
                model.RazonSocial = Convert.ToString(values[RAZON_SOCIAL]);
            }

            if(values.Contains(SECTOR_COMERCIAL)) {
                model.SectorComercial = Convert.ToString(values[SECTOR_COMERCIAL]);
            }

            if(values.Contains(TIPO_DOCUMENTO)) {
                model.TipoDocumento = Convert.ToString(values[TIPO_DOCUMENTO]);
            }

            if(values.Contains(NUM_DOCUMENTO)) {
                model.NumDocumento = Convert.ToString(values[NUM_DOCUMENTO]);
            }

            if(values.Contains(DIRECCION)) {
                model.Direccion = Convert.ToString(values[DIRECCION]);
            }

            if(values.Contains(TELEFONO)) {
                model.Telefono = Convert.ToString(values[TELEFONO]);
            }

            if(values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(URL)) {
                model.Url = Convert.ToString(values[URL]);
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