using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    public class MeniController : Controller
    {
        readonly private IMeniService _meniService;
        readonly private IMapper _mapper;

        public MeniController(IMeniService meniService, IMapper mapper)
        {
            _meniService = meniService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetMeni(DateTime date)
        {
            var meni = _meniService.GetByDate(date);
            if(meni == null)
            {
                meni = new Meni { Datum = date };
            }
            var meniViewModel = new MeniViewModel();
            MapMeniToMeniVM(meni, meniViewModel);
            return Ok(meniViewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllMenis()
        {
            var menis = _meniService.GetAll().ToList();
            var viewModel = _mapper.Map<List<MeniForCalendarViewModel>>(menis);
            return Ok(viewModel);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Cook)]
        [HttpPost]
        public IActionResult CreateOrUpdate([FromBody] MeniViewModel viewModel)
        {
            //TODO Forbid update if any order exist
            IActionResult result;
            if (!ModelState.IsValid)
            {
                result = ValidationProblem("Greška! Molimo Vas pokušajte ponovo.");
            }
            else
            {
                var menu = new Meni();
                MapMenuVMToMenu(viewModel, menu);
                if (menu.Hrana.Count() < 1)
                {
                    result = ValidationProblem("Meni mora sadrzati hranu");
                }
                else
                {
                    menu = _meniService.CreateOrUpdate(menu);
                    result = Ok(menu.MeniId);
                }
            }
            return result;
        }


        #region Mappers
        private void MapMeniToMeniVM(Meni meni, MeniViewModel viewModel)
        {
            viewModel.MenuId = meni.MeniId;
            viewModel.Date = meni.Datum;
            viewModel.Food = new List<int>();
            if (meni.Hrana != null)
            {
                foreach (var hrana in meni.Hrana)
                {
                    viewModel.Food.Add(hrana.HranaId);
                }
            }

        }

        private void MapMenuVMToMenu(MeniViewModel viewModel, Meni menu)
        {
            menu.MeniId = viewModel.MenuId;
            menu.Datum = viewModel.Date;
            var hranaMeni = new List<HranaMeni>();
            if (viewModel.Food != null)
            {
                foreach (var hrana in viewModel.Food)
                {
                    hranaMeni.Add(new HranaMeni { HranaId = hrana });
                }
            }
            menu.Hrana = hranaMeni;
        }
        #endregion
    }
}