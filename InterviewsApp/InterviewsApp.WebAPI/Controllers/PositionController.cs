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
            var response = _service.Get(id);
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
        public IActionResult GetByUser(Guid id, Guid userId)
        {
            var response = _service.Get(id, userId);
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
        public IActionResult GetMultiplePositions()
        {
            var response = _service.Get();
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
        public IActionResult GetMultiplePositionsByUser(Guid userId)
        {
            var response = _service.GetByUserId(userId);
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
        public IActionResult CreatePosition(CreatePositionDto position)
        {
            if (ModelState.IsValid)
            {
                var response = _service.CreatePosition(position);
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
        /// <summary>
        /// Отметить получение оффера
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        /// <param name="rate">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult SetOffered(Guid id, Guid userId)
        {
            var response = _service.UpdateSetOffered(id, userId);
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
        public IActionResult SetDenied(Guid id, Guid userId)
        {
            var response = _service.UpdateSetDenied(id, userId);
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
        public IActionResult UpdateMoney(UpdatePositionDto updatePositionDto)
        {
            var response = _service.UpdateMoney(updatePositionDto);
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
        public IActionResult UpdateCity(UpdatePositionDto updatePositionDto)
        {
            var response = _service.UpdateCity(updatePositionDto);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
    }
}
