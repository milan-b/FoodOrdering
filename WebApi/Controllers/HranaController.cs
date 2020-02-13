using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WebApi.Controllers
{
    public class HranaController : Controller
    {
        readonly IHranaService _hranaService;

        public HranaController(IHranaService hranaService)
        {
            _hranaService = hranaService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(_hranaService.GetAll().ToList());
        }
    }
}