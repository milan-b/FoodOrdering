using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

//TODO make custom Exception for db exceptions

namespace Service
{
    public interface IOrderService
    {
        IEnumerable<Narudzba> GetAll(int menuId);
        IEnumerable<Narudzba> GetAllForUser(int userId);
        Narudzba GetByMenuId(int menuId, int userId);
        void Delete(int id);
        void Delete(Narudzba order);
        Narudzba Get(int id);
        Narudzba CreateOrUpdate(Narudzba hrana);
    }
    public class OrderService : IOrderService
    {
        private HranaContext _context;

        public OrderService(HranaContext context)
        {
            _context = context;
        }

        public IEnumerable<Narudzba> GetAll(int menuId)
        {
            return _context.Narudzbe.Where(o => o.MeniId == menuId)
                        .Include(o => o.User);
        }

        public IEnumerable<Narudzba> GetAllForUser(int userId)
        {
            return _context.Narudzbe.Where(o => o.UserId == userId);
        }


        public Narudzba CreateOrUpdate(Narudzba order)
        {
            Narudzba ret;
            if (order.NarudzbaId != 0)
            {
                _context.OrderSideDishes.RemoveRange(_context.OrderSideDishes.Where(o => o.NarudzbaId == order.NarudzbaId));
                ret = _context.Narudzbe.Update(order).Entity;
            }
            else
            {
                ret = _context.Narudzbe.Add(order).Entity;
            }
            _context.SaveChanges();
            return ret;
        }

        public Narudzba GetByMenuId(int menuId, int userId)
        {
            return _context.Narudzbe.Where(o => o.MeniId == menuId && o.UserId == userId)
                .Include(o => o.SideDishes).FirstOrDefault() ;
        }

        public Narudzba Get(int id)
        {
            return _context.Narudzbe.Find(id);
        }

        public void Delete(int id)
        {
            var order = _context.Narudzbe.Find(id);
            if (order != null)
            {
                _context.Narudzbe.Remove(order);
                _context.SaveChanges();
            }
        }

        public void Delete(Narudzba order)
        {
            if (order != null)
            {
                _context.Narudzbe.Remove(order);
                _context.SaveChanges();
            }
        }


    }
}
