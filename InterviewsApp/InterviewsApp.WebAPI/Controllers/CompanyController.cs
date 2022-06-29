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
        public IActionResult Get(Guid id)
        {
            return Ok(_service.Get(id));
        }
        /// <summary>
        /// Получить список всех компаний в системе
        /// </summary>
        /// <returns>Список компаний в системе</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetCompanies()
        {
            return Ok(_service.Get());
        }
        /// <summary>
        /// Добавить в систему новую компанию
        /// </summary>
        /// <param name="newCompanyName">Название компании</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult CreateCompany(CreateCompanyDto dto)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.CreateCompany(dto));
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
        public IActionResult RateCompany(Guid id, Guid userId, short rate)
        {
            return Ok(_service.RateCompany(id, userId, rate));
        }
        /// <summary>
        /// Удалить компанию из системы
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok();
        }
        /// <summary>
        /// Получить оценку, выставленную компании конкретным пользователем
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetCompanyRate(Guid id, Guid userId)
        {
            return Ok(_service.GetCompanyRate(id, userId));
        }
    }
}
