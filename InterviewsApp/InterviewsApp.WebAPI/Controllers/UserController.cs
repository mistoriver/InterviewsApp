using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InterviewsApp.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetUsers(Guid id)
        {
            
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserDto newUser)
        {
            _service.CreateUser(newUser);
            return Ok();
        }
    }
}
