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
            if (id == Guid.Empty)
                return BadRequest();
            return Ok(_service.Get(id));
        }
        /// <summary>
        /// Получить список всех пользователей системы
        /// </summary>
        /// <returns>Список пользователей системы</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetUsers()
        {
            return Ok(_service.Get());
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
            _service.CreateUser(newUser);
            return Ok();
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
            var userInfo = _service.Login(loginDto);
            if (userInfo != null)
            {
                return Ok(userInfo);
            }
            return Unauthorized();
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
            _service.Delete(id);
            return Ok();
        }
    }
}
