﻿using DevExtreme.AspNet.Data;
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
    public class PresentacionDExpController : Controller
    {
        private dbventasContext _context;

        public PresentacionDExpController(dbventasContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var presentacions = _context.Presentacions.Select(i => new {
                i.Idpresentacion,
                i.Nombre,
                i.Descripcion
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Idpresentacion" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(presentacions, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Presentacion();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Presentacions.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Idpresentacion });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Presentacions.FirstOrDefaultAsync(item => item.Idpresentacion == key);
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
            var model = await _context.Presentacions.FirstOrDefaultAsync(item => item.Idpresentacion == key);

            _context.Presentacions.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Presentacion model, IDictionary values) {
            string IDPRESENTACION = nameof(Presentacion.Idpresentacion);
            string NOMBRE = nameof(Presentacion.Nombre);
            string DESCRIPCION = nameof(Presentacion.Descripcion);

            if(values.Contains(IDPRESENTACION)) {
                model.Idpresentacion = Convert.ToInt32(values[IDPRESENTACION]);
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