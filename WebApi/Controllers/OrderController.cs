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
using WebApi.Services;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Authorize]
    //[ApiController]
    public class OrderController : Controller
    {
        readonly IOrderService _orderService;
        readonly IEmailService _emailService;
        readonly IMeniService _meniService;
        readonly private IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper, IEmailService emailService, IMeniService meniService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _emailService = emailService;
            _meniService = meniService;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Cook)]
        [HttpGet]
        public IActionResult GetAll(int menuId)
        {
            var orders = _orderService.GetAll(menuId).ToList();
            var ordersVM = orders.Select(o => MapOrderToOrderVM(o));
            return Ok(ordersVM);
        }

        [HttpGet]
        public IActionResult GetAllForUser()
        {
            var orders = _orderService.GetAllForUser(Convert.ToInt32(User.Identity.Name)).ToList();
            var menusWithOrders = orders.Select(o => o.MeniId);
            return Ok(menusWithOrders);
        }

        [HttpGet]
        public IActionResult MailTest()
        {
            _emailService.SendEmailAsync("bojic.job@gmail.com", "Test email", "Ovo je prva poruka! \n Radiii !!!!");
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int menuId)
        {
            OrderViewModel orderVM = null;
            var order = _orderService.GetByMenuId(menuId, Convert.ToInt32(User.Identity.Name));
            if (order != null)
            {
                orderVM = MapOrderToOrderVM(order);
            }
            return Ok(orderVM);
        }

        [HttpPost]
        public IActionResult CreateOrUpdate([FromBody] OrderViewModel viewModel)
        {
            IActionResult result;
            if (!ModelState.IsValid)
            {
                result = ValidationProblem("Nisu popunjena sva obavezna polja.");
            }
            else
            {
                var meni = _meniService.GetById(viewModel.MenuId);
                if (meni.Datum.Subtract(DateTime.Now).TotalHours < 10)
                {
                    result = ValidationProblem("Vrijeme za narudžbu je isteklo. Naručiti možete do 14h, dan ranije.");
                }
                else
                {
                    var order = new Narudzba();
                    MapOrderVMToOrder(viewModel, order);
                    order = _orderService.CreateOrUpdate(order);
                    result = Ok(order.NarudzbaId);
                }
            }

            return result;
        }

        [HttpPost]
        public IActionResult Delete([FromBody] int orderId)
        {
            IActionResult ret;
            var order = _orderService.Get(orderId);
            if (User.IsInRole(Roles.Admin) || order.UserId == Convert.ToInt32(User.Identity.Name))
            {
                _orderService.Delete(order);
                ret = Ok();
            }
            else
            {
                ret = ValidationProblem("Nemate pravo da izvršite ovu akciju.");
            }
            return ret;
        }

        #region Mappers

        private OrderViewModel MapOrderToOrderVM(Narudzba order)
        {
            var orderVM = new OrderViewModel();
            MapOrderToOrderVM(order, orderVM);
            return orderVM;
        }

        private void MapOrderToOrderVM(Narudzba order, OrderViewModel viewModel)
        {
            viewModel.MenuId = order.MeniId;
            viewModel.FoodId = order.HranaId;
            viewModel.OrderId = order.NarudzbaId;
            viewModel.TimeId = order.TimeId;
            viewModel.LocationId = order.LocationId;
            viewModel.SideDishes = order.SideDishes.Select(o => o.PrilogId).ToList();
            viewModel.User = new UserViewModel { UserId = order.UserId, Email = order.User.Email };
        }

        private void MapOrderVMToOrder(OrderViewModel viewModel, Narudzba order)
        {
            order.MeniId = viewModel.MenuId;
            order.HranaId = viewModel.FoodId;
            order.NarudzbaId = viewModel.OrderId;
            order.TimeId = viewModel.TimeId;
            order.LocationId = viewModel.LocationId;
            order.UserId = Convert.ToInt32(User.Identity.Name);
            order.SideDishes = viewModel.SideDishes.Select(o => new OrderSideDish { PrilogId = o }).ToList();
        }

        #endregion

    }
}