using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace WebAPI.Controllers
{
    // Indicamos que la direccion de este controlador será https://servidor:puerto/api/User
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(User user)
        {
            try
            {
                var um = new UserManager();
                um.Create(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var um = new UserManager();
                var lstResult = um.RetrieveAll();
                return Ok(lstResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int id)
        {
            try
            {
                var um = new UserManager();
                var result = um.RetrieveById(id);
                if (result == null)
                    return NotFound($"Usuario con ID {id} no encontrado.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveByUserCode")]
        public ActionResult RetrieveByUserCode(string userCode)
        {
            try
            {
                var um = new UserManager();
                var user = um.RetrieveByUserCode(userCode);
                if (user == null)
                    return NotFound($"No se encontró usuario con código '{userCode}'.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveByEmail")]
        public ActionResult RetrieveByEmail(string email)
        {
            // Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"):
            // Asegura el formato básico usuario@dominio.ext, sin permitir espacios ni múltiples “@”.
            // https://stackoverflow.com/questions/5342375/regex-email-validation

            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest("Email inválido.");
            }

            try
            {
                var um = new UserManager();
                var user = um.RetrieveByEmail(email);
                if (user == null)
                    return NotFound($"No se encontró usuario con email '{email}'.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(int id, User user)
        {
            try
            {
                if (user.Id != 0 && user.Id != id)
                    return BadRequest("El Id de la ruta y del cuerpo no coinciden.");

                user.Id = id;

                var um = new UserManager();
                var updated = um.Update(user);
                return Ok(updated);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                var um = new UserManager();
                var result = um.RetrieveById(id);
                if (result == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }
                else
                {
                    um.Delete(id);
                    return Ok(new { Message = $"Usuario con ID {id} eliminado con éxito." });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}