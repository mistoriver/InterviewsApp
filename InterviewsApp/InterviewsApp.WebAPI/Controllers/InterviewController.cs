using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get(Guid id, Guid userId)
        {
            var response = await _service.Get(id, userId);
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
        public async Task<IActionResult> GetMultipleInterviewsByUser(Guid userId, bool showOnlyFuture = false)
        {
            var response = await _service.GetByUserId(userId, showOnlyFuture);
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
        public async Task<IActionResult> GetByPosition(Guid positionId, Guid userId)
        {
            var response = await _service.GetByPosition(positionId, userId);
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
        public async Task<IActionResult> AddInterview(CreateInterviewDto newInterview)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.CreateInterview(newInterview);
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
        public async Task<IActionResult> Delete(Guid id, Guid userId)
        {
            var response = await _service.Delete(id, userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto commentInfo)
        {
            var response = await _service.UpdateComment(commentInfo);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateDatetime(UpdateInterviewDto interviewInfo)
        {
            var response = await _service.UpdateDatetime(interviewInfo);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
    }
}
