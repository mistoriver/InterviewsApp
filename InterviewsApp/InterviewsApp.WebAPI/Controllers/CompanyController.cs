using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
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
        public IActionResult Get(Guid id)
        {
            return Ok(_service.Get(id));
        }
        /// <summary>
        /// Получить список всех компаний в системе
        /// </summary>
        /// <returns>Список компаний в системе</returns>
        [HttpGet]
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
        public IActionResult CreateCompany(string newCompanyName)
        {
            _service.CreateCompany(newCompanyName);
            return Ok();
        }
        /// <summary>
        /// Оценить компанию
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <param name="rate">Новая оценка</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult RateCompany(Guid id, short rate)
        {
            return Ok(_service.RateCompany(id, rate));
        }
        /// <summary>
        /// Удалить компанию из системы
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
