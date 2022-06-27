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
    public class InterviewController : ControllerBase
    {
        private readonly IInterviewService _service;

        public InterviewController(IInterviewService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить данные конкретного собеседования
        /// </summary>
        /// <param name="id">Уникальный идентификатор собеседования</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Get(Guid id, Guid userId)
        {
            return Ok(_service.Get(id, userId));
        }
        /// <summary>
        /// Получить данные конкретного собеседования c названием вакансии и компании
        /// </summary>
        /// <param name="id">Уникальный идентификатор собеседования</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetForUi(Guid id, Guid userId)
        {
            return Ok(_service.GetForUi(id, userId));
        }
        /// <summary>
        /// Получить список собеседований пользователя
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetInterviewsByUser(Guid userId)
        {
            
            return Ok(_service.GetByUserId(userId));
        }
        /// <summary>
        /// Получить список собеседований пользователя c названием вакансии и компании
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetInterviewsByUserForUi(Guid userId)
        {

            return Ok(_service.GetByUserIdForUi(userId));
        }
        /// <summary>
        /// Добавить в систему новое собеседование
        /// </summary>
        /// <param name="newInterview"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult AddInterview(CreateInterviewDto newInterview)
        {
            if (ModelState.IsValid)
            {
                _service.CreateInterview(newInterview);
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// Удалить собеседование из системы
        /// </summary>
        /// <param name="id">Уникальный идентификатор собеседования</param>
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
