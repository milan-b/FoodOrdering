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
    public class MeniController : Controller
    {
        readonly IMeniService _meniService;
        private IMapper _mapper;

        public MeniController(IMeniService meniService, IMapper mapper)
        {
            _meniService = meniService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetMeni(DateTime date)
        {
            var meni = _meniService.GetByDate(date);
            var meniViewModel = _mapper.Map<MeniViewModel>(meni);
            //var meniViewModel = new MeniViewModel();
            //MapMeniTOMeniVM(meni, meniViewModel);
            return Ok(meniViewModel);
        }


        #region Mappers
        //private void MapMeniTOMeniVM(Meni meni, MeniViewModel viewModel)
        //{
        //    viewModel.MeniId = meni.MeniId;
        //    viewModel.Hrana = new List<HranaViewModel>();
        //    foreach (var hrana in meni.Hrana)
        //    {
        //        viewModel.Hrana.Add(_mapper.Map<HranaViewModel>(hrana.Hrana));
        //    }

        //}
        #endregion
    }
}