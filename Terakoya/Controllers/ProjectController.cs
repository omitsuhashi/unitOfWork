using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using Terakoya.Data;
using Terakoya.Data.Repositories.Interfaces;
using Terakoya.Models;

namespace Terakoya.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IReadRepository<ProjectModel> _repository;

        public ProjectController(
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
            _repository = unitOfWork.GetReadOnlyRepository<ProjectModel>();
        }

        [HttpGet("id")]
        public IActionResult Get(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetList([FromQuery]int start = 1)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Search([FromQuery]string q, [FromQuery]int start = 1)
        {
            throw new NotImplementedException();
        }
    }
}
