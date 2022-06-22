using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
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
        public IActionResult Get(Guid id)
        {
            _service.Get(id);
            return Ok();
        }
        /// <summary>
        /// Получить список собеседований пользователя
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetInterviewsByUser(Guid userId)
        {
            _service.GetByUserId(userId);
            return Ok();
        }
        /// <summary>
        /// Добавить в систему новое собеседование
        /// </summary>
        /// <param name="newInterview"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddInterview(CreateInterviewDto newInterview)
        {
            _service.CreateInterview(newInterview);
            return Ok();
        }
    }
}
