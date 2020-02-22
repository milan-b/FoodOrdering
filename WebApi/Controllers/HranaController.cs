using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    public class HranaController : Controller
    {
        readonly IHranaService _hranaService;
        readonly private IMapper _mapper;

        public HranaController(IHranaService hranaService, IMapper mapper)
        {
            _hranaService = hranaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var hrana = _hranaService.GetAll().ToList();
            var viewModel = _mapper.Map<List<HranaViewModel>>(hrana);
            return Ok(viewModel);
        }
    }
}