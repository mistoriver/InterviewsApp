using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя</param>
        /// <returns>Информация о пользователе</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetById(Guid id)
        {
            var response = _service.Get(id);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Получить список всех пользователей системы
        /// </summary>
        /// <returns>Список пользователей системы</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetUsers()
        {
            var response = _service.Get();
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Зарегистрировать нового пользователя в системе
        /// </summary>
        /// <param name="newUser">Данные пользователя</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(CreateUserDto newUser)
        {
            if (ModelState.IsValid)
            {
                var response = _service.CreateUser(newUser);
                if (response.Ok)
                    return Ok(response);
                return StatusCode(500, response);
            }
            return BadRequest();
        }
        /// <summary>
        /// Аутентифицировать пользователя в системе
        /// </summary>
        /// <param name="loginDto">Параметры аутентификации</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginUserDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var response = _service.Login(loginDto);
                if (response.Ok)
                    return Ok(response);
                return Unauthorized(response);
            }
            return BadRequest();
        }
        /// <summary>
        /// Удалить пользователя из системы
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Delete(Guid id)
        {
            var response = _service.Delete(id);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
    }
}
