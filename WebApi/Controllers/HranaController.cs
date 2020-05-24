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
    [Authorize]
    //[ApiController]
    public class HranaController : Controller
    {
        readonly IHranaService _hranaService;
        readonly private IMapper _mapper;

        public HranaController(IHranaService hranaService, IMapper mapper)
        {
            _hranaService = hranaService;
            _mapper = mapper;
        }

        
        [HttpGet]
        public IActionResult GetAll()
        {
            var food = _hranaService.GetAll().ToList();
            var viewModel = food.Select(o => new HranaViewModel
            {
                HranaId = o.HranaId,
                Stalna = o.Stalna,
                Naziv = o.Naziv,
                Prilozi = o.Prilozi.Select(o1 => new PrilogViewModel
                {
                    PrilogId = o1.PrilogId,
                    Varijanata = o1.Varijanta
                }).ToList(),
                Rating = o.Ocjene.Count() > 0 ? o.Ocjene.Select(o1 => o1.Vrijednost).Average() : 0,
                NumberOfComments = o.Komentari.Count()
            }) ;
            return Ok(viewModel);
        }

        
        [HttpGet]
        public IActionResult GetAllSideDishes()
        {
            var prilozi = _hranaService.GetAllSideDishes().ToList();
            var viewModel = _mapper.Map<List<PrilogViewModel>>(prilozi);
            return Ok(viewModel);
        }

        [HttpPost]
        public IActionResult Rate([FromBody] RateViewModel viewModel)
        {
            IActionResult result;
            if (!ModelState.IsValid)
            {
                result = ValidationProblem("Nevalidan zahtjev.");
            }
            else
            {
                _hranaService.SetRate(Convert.ToInt32(User.Identity.Name), viewModel.FoodId, viewModel.Mark);
                result = Ok();
            }

            return result;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Cook)]
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

        [HttpPost]
        public IActionResult SetComment([FromBody] CommentViewModel viewModel)
        {
            IActionResult result;
            if (!ModelState.IsValid)
            {
                result = ValidationProblem("Nevalidan zahtjev.");
            }
            else
            {
                _hranaService.SetComment(Convert.ToInt32(User.Identity.Name), viewModel.FoodId, viewModel.Content, viewModel.Image);
                result = Ok();
            }

            return result;
        }

        [HttpGet]
        public IActionResult GetComments(int foodId)
        {
            var comments = _hranaService.GetComments(foodId);
            var viewModel = comments.Select(o => MapCommentToCommentVM(o)).ToList();
            return Ok(viewModel);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Cook)]
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

        #region Mappers

        private CommentViewModel MapCommentToCommentVM(Komentar comment)
        {
            var commentVM = new CommentViewModel();
            MapCommentToCommentVM(comment, commentVM);
            return commentVM;
        }

        private void MapCommentToCommentVM(Komentar comment, CommentViewModel viewModel)
        {
            viewModel.User = comment.User.Email;
            viewModel.Time = comment.Time;
            viewModel.Content = comment.Comment;
            viewModel.Image = comment.Slika;
            
        }

        #endregion
    }
}