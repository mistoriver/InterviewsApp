using InterviewsApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace InterviewsApp.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocalizationController : ControllerBase
    {
        private readonly ILocalizationService _service;

        public LocalizationController(ILocalizationService service)
        {
            _service = service;
        }


        /// <summary>
        /// Получить список переводов по коду языка
        /// </summary>
        /// <param name="langCode">Код языка</param>
        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetByLang(string langCode)
        {
            var response = await _service.GetByLanguage(langCode);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }

        /// <summary>
        /// Получить список переводов для пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        [HttpGet]
        //Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var response = await _service.GetByUserId(userId);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }

        /// <summary>
        /// Установить локаль для пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="languageCode">Код языка</param>
        [HttpPut]
        //Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SetForUser(Guid userId, string langCode)
        {
            var response = await _service.SetLocalizationForUser(userId, langCode);
            if (response.Ok)
                return Ok(response);
            return StatusCode(500, response);
        }
    }
}
