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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить данные компании
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
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
        /// Получить список всех компаний в системе
        /// </summary>
        /// <returns>Список компаний в системе</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCompanies()
        {
            var response = await _service.Get();
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Добавить в систему новую компанию
        /// </summary>
        /// <param name="newCompanyName">Название компании</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateCompany(CreateCompanyDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.CreateCompany(dto);
                if (response.Ok)
                    return Ok(response);
                return StatusCode(500, response);
            }
            return BadRequest();
        }
        /// <summary>
        /// Оценить компанию
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <param name="rate">Новая оценка</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RateCompany(Guid id, Guid userId, short rate)
        {
            var response = await _service.RateCompany(id, userId, rate);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
        /// <summary>
        /// Удалить компанию из системы
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var response = _service.Delete(id);
            //if (response.Ok)
            //    return Ok(response);
            return StatusCode(400);
        }
        /// <summary>
        /// Получить оценку, выставленную компании конкретным пользователем
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCompanyRate(Guid id, Guid userId)
        {
            var response = await _service.GetUserCompanyRate(id, userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
    }
}
