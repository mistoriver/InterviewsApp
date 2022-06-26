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
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _service;

        public PositionController(IPositionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить данные вакансии
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Get(Guid id)
        {
            return Ok(_service.Get(id));
        }
        /// <summary>
        /// Получить список всех вакансий в системе
        /// </summary>
        /// <returns>Список вакансий в системе</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetPositions()
        {
            return Ok(_service.Get());
        }
        /// <summary>
        /// Получить список вакансий пользователя
        /// </summary>
        /// <returns>Список вакансий пользователя</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetPositionsByUser(Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest();
            return Ok(_service.GetByUserId(userId));
        }
        /// <summary>
        /// Создать новую вакансию в системе
        /// </summary>
        /// <param name="position">Данные вакансии</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult CreatePosition(CreatePositionDto position)
        {
            if (ModelState.IsValid)
            {
                _service.CreatePosition(position);
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// Удалить вакансию из системы
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансию</param>
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
