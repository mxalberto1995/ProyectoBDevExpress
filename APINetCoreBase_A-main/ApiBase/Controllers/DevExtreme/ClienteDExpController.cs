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

namespace DevExtremePB_Api.Controllers.DevExtreme
{
    [Route("api/[controller]/[action]")]
    public class ClienteDExpController : Controller
    {
        private dbventasContext _context;

        public ClienteDExpController(dbventasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var clientes = _context.Clientes.Select(i => new
            {
                i.Idcliente,
                i.Nombre,
                i.Apellidos,
                i.Sexo,
                i.FechaNacimiento,
                i.TipoDocumento,
                i.NumDocumento,
                i.Direccion,
                i.Telefono,
                i.Email
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Idcliente" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(clientes, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new Cliente();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Clientes.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Idcliente });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {
            var model = await _context.Clientes.FirstOrDefaultAsync(item => item.Idcliente == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key)
        {
            var model = await _context.Clientes.FirstOrDefaultAsync(item => item.Idcliente == key);

            _context.Clientes.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Cliente model, IDictionary values)
        {
            string IDCLIENTE = nameof(Cliente.Idcliente);
            string NOMBRE = nameof(Cliente.Nombre);
            string APELLIDOS = nameof(Cliente.Apellidos);
            string SEXO = nameof(Cliente.Sexo);
            string FECHA_NACIMIENTO = nameof(Cliente.FechaNacimiento);
            string TIPO_DOCUMENTO = nameof(Cliente.TipoDocumento);
            string NUM_DOCUMENTO = nameof(Cliente.NumDocumento);
            string DIRECCION = nameof(Cliente.Direccion);
            string TELEFONO = nameof(Cliente.Telefono);
            string EMAIL = nameof(Cliente.Email);

            if (values.Contains(IDCLIENTE))
            {
                model.Idcliente = Convert.ToInt32(values[IDCLIENTE]);
            }

            if (values.Contains(NOMBRE))
            {
                model.Nombre = Convert.ToString(values[NOMBRE]);
            }

            if (values.Contains(APELLIDOS))
            {
                model.Apellidos = Convert.ToString(values[APELLIDOS]);
            }

            if (values.Contains(SEXO))
            {
                model.Sexo = Convert.ToString(values[SEXO]);
            }

            if (values.Contains(FECHA_NACIMIENTO))
            {
                model.FechaNacimiento = values[FECHA_NACIMIENTO] != null ? Convert.ToDateTime(values[FECHA_NACIMIENTO]) : null;
            }

            if (values.Contains(TIPO_DOCUMENTO))
            {
                model.TipoDocumento = Convert.ToString(values[TIPO_DOCUMENTO]);
            }

            if (values.Contains(NUM_DOCUMENTO))
            {
                model.NumDocumento = Convert.ToString(values[NUM_DOCUMENTO]);
            }

            if (values.Contains(DIRECCION))
            {
                model.Direccion = Convert.ToString(values[DIRECCION]);
            }

            if (values.Contains(TELEFONO))
            {
                model.Telefono = Convert.ToString(values[TELEFONO]);
            }

            if (values.Contains(EMAIL))
            {
                model.Email = Convert.ToString(values[EMAIL]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return string.Join(" ", messages);
        }
    }
}