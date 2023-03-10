using Common.DTO;
using Contracts.Service;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersCRUD.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // cualquier solicitud a cualquier método en UsersCRUDController 
                                                          //tenga un token de autenticación válido que se emitió con el esquema de autenticación JWT
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("UsersList")] //los atributos de autorización de método anulan los atributos de autorización de controlador.
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")] 
        public ResponseDTO GetAllUsers()
        {
            return _userService.GetAllUsers();
        }
        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<ResponseDTO> AddUser([FromBody] UserDTO newUser)
        {
            var response = _userService.AddUser(newUser);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("Delete/{userName}")]
        public ActionResult<ResponseDTO> DeleteUser(string userName)
        {
            var deleteUser = new UserDTO();
            deleteUser.UserName = userName;
            var response = _userService.DeleteUser(deleteUser);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }


        [HttpPut("Modify")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<ResponseDTO> ModifyUser([FromBody] UserDTO modifyUser)
        {
            var response = _userService.ModifyUser(modifyUser);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

    }
}
