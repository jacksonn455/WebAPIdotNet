using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebAPI.Models; // importando classe

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class UserController : ControllerBase
    {

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<User> CreateUser(
            [FromHeader(Name ="username")] string username,
            [FromBody] User user // importado objeto
        ) {

            if(!user.Email.Contains("@")){
                return StatusCode(500, new {
                message = "Erro ao inserir o e-mail"
            });
            }

            if(user.Username.Length < 5){
            return BadRequest(new {
                message = "Erro ao inserir nome"
            });
            }

            return Ok(user); //return 200

            /*
            User use2 = JsonSerializer.Deserialize<User>("{\"username\":\"Teste\",\"email\":\"jackson@joptas.com\",\"age\":24}", new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            });

            return JsonSerializer.Serialize(use2, new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }); */
        }

        [HttpGet]
        public string Filter(
            [FromQuery(Name="q")] string query,
            [FromQuery(Name="oq")] string oQuery
        ) {  
            return $"Buscando por {query} ... {oQuery}"; //interpolar
        }

        [HttpPost("category/{categoryId2}/create/{userId}")]
        public string CreateUserCategory(int categoryId2, int userId) {
            return $"Category id: {categoryId2} // user id: {userId}";
        }

        [HttpDelete("{id:int?}")]
        public string DeleteUser(int? id ) {
            return "Deletando Usuario " + id;
        }
    }
}