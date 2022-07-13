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
            var response = _service.Get(id, userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Получить список собеседований пользователя
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetMultipleInterviewsByUser(Guid userId, bool showOnlyFuture = false)
        {
            var response = _service.GetByUserId(userId, showOnlyFuture);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Получить данные конкретного собеседования
        /// </summary>
        /// <param name="id">Уникальный идентификатор собеседования</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetByPosition(Guid positionId, Guid userId)
        {
            var response = _service.GetByPosition(positionId, userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
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
                var response = _service.CreateInterview(newInterview);
                if (response.Ok)
                    return Ok(response);
                return StatusCode(500, response);
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
        public IActionResult Delete(Guid id, Guid userId)
        {
            var response = _service.Delete(id, userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult UpdateComment(UpdateCommentDto commentInfo)
        {
            var response = _service.UpdateComment(commentInfo);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult UpdateDatetime(UpdateInterviewDto interviewInfo)
        {
            var response = _service.UpdateDatetime(interviewInfo);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
    }
}
