using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InterviewsApp.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterviewController : Controller
    {
        private readonly IRepository<InterviewEntity> _repository;

        public InterviewController(IRepository<InterviewEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<InterviewEntity> TestAction()
        {
            return _repository.GetByPredicate(entity => true);
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
        }

        [HttpPost]
        public IActionResult AddInterview(InterviewEntity newInterview)
        {
            InterviewHelper.CreateInterview(newInterview);
            return Ok();
        }*/
    }
}
