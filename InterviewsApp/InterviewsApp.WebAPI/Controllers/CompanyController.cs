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
            _service.Get(id);
            return Ok();
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
            _service.RateCompany(id, rate);
            return Ok();
        }
    }
}
