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
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _service.Get(id);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Получить данные вакансии
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetByUser(Guid id, Guid userId)
        {
            var response = await _service.Get(id, userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Получить список всех вакансий в системе
        /// </summary>
        /// <returns>Список вакансий в системе</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetMultiplePositions()
        {
            var response = await _service.Get();
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Получить список вакансий пользователя
        /// </summary>
        /// <returns>Список вакансий пользователя</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetMultiplePositionsByUser(Guid userId)
        {
            var response = await _service.GetByUserId(userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Создать новую вакансию в системе
        /// </summary>
        /// <param name="position">Данные вакансии</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreatePosition(CreatePositionDto position)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.CreatePosition(position);
                if (response.Ok)
                    return Ok(response);
                return StatusCode(500, response);
            }
            return BadRequest();
        }
        /// <summary>
        /// Удалить вакансию из системы
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
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
        /// <summary>
        /// Отметить получение оффера
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        /// <param name="rate">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SetOffered(Guid id, Guid userId)
        {
            var response = await _service.UpdateSetOffered(id, userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Отметить получение отказа
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        /// <param name="rate">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SetDenied(Guid id, Guid userId)
        {
            var response = await _service.UpdateSetDenied(id, userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Обновить информацию о зарплатной вилке
        /// </summary>
        /// <param name="updatePositionDto">Информация о вакансии</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateMoney(UpdatePositionDto updatePositionDto)
        {
            var response = await _service.UpdateMoney(updatePositionDto);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }

        /// <summary>
        /// Обновить информацию о зарплатной вилке
        /// </summary>
        /// <param name="updatePositionDto">Информация о вакансии</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateCity(UpdatePositionDto updatePositionDto)
        {
            var response = await _service.UpdateCity(updatePositionDto);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
    }
}
