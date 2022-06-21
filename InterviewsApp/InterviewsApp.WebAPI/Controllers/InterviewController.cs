using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InterviewsApp.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterviewController : Controller
    {
        private readonly IInterviewService _service;

        public InterviewController(IInterviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetIntervews(Guid id)
        {
            _service.Get(id);
            return Ok();
        }

        /*[HttpGet]
        public IActionResult Get(InterviewRequest intReq)
        {
            if (!string.IsNullOrWhiteSpace(intReq.UserId))
                if (string.IsNullOrWhiteSpace(intReq.InterviewId))
                    if (string.IsNullOrWhiteSpace(intReq.PositionId))
                        return Ok(_repository.GetInterviewsByUser(intReq.UserId));
                    else
                        return Ok(InterviewHelper.GetInterviewsByUserAndPosition(intReq.UserId, intReq.PositionId));
                else Ok(InterviewHelper.GetInterview(intReq.InterviewId));
            return BadRequest("Невозможно запросить список встреч без указания идентификатора пользователя!");
        }*/

        [HttpPost]
        public IActionResult AddInterview(CreateInterviewDto newInterview)
        {
            _service.CreateInterview(newInterview);
            return Ok();
        }
    }
}
