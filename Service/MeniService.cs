using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

//TODO make custom Exception for db exceptions

namespace Service
{
    public interface IMeniService
    {
        IEnumerable<Meni> GetAll();
        Meni GetById(int id);
        void Delete(int id);
        Meni GetByDate(DateTime date);
    }
    public class MeniService : IMeniService
    {
        private HranaContext _context;

        public MeniService(HranaContext context)
        {
            _context = context;
        }

        public IEnumerable<Meni> GetAll()
        {
            return _context.Menii;
        }

        public Meni GetById(int id)
        {
            return _context.Menii.Find(id);
        }

        public Meni GetByDate(DateTime date)
        {
            return _context.Menii
                .Include(o => o.Hrana)
                .ThenInclude(o => o.Hrana)
                .ThenInclude(o => o.Prilozi)
                .ThenInclude(o => o.Prilog)
                .Where(o => o.Datum.Date == date.Date).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var meni = _context.Menii.Find(id);
            if (meni != null)
            {
                _context.Menii.Remove(meni);
                _context.SaveChanges();
            }
        }

        
    }
}
