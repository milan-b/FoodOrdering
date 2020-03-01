using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllSideDishes()
        {
            var prilozi = _hranaService.GetAllSideDishes().ToList();
            var viewModel = _mapper.Map<List<PrilogViewModel>>(prilozi);
            return Ok(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateOrUpdate([FromBody] HranaViewModel viewModel)
        {
            IActionResult result;
            if (!ModelState.IsValid)
            {
                result = ValidationProblem("Naziv ne moze biti prazan.");
            }
            else
            {
                var hrana = _mapper.Map<Hrana>(viewModel);

                hrana = _hranaService.CreateOrUpdate(hrana);
                result = Ok(hrana.HranaId);
            }

            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateSideDish([FromBody] PrilogViewModel viewModel)
        {
            IActionResult result;
            if (!ModelState.IsValid)
            {
                result = ValidationProblem("Naziv ne moze biti prazan.");
            }
            else
            {
                var sideDishId = _hranaService.CreateSideDish(viewModel.Naziv);
                result = Ok(sideDishId);
            }
            return result;
        }
    }
}