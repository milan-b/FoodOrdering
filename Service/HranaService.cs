using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

//TODO make custom Exception for db exceptions

namespace Service
{
    public interface IHranaService
    {
        IEnumerable<Hrana> GetAll();
        IEnumerable<Prilog> GetAllSideDishes();
        Hrana GetById(int id);
        int CreateSideDish(string name);
        Prilog GetSideDish(int id);
        void Delete(int id);
        Hrana CreateOrUpdate(Hrana hrana);
        void SetRate(int userId, int foodId, int mark);
    }
    public class HranaService : IHranaService
    {
        private HranaContext _context;

        public HranaService(HranaContext context)
        {
            _context = context;
        }

        public IEnumerable<Hrana> GetAll()
        {
            return _context.Hrana
                        .Include(o => o.Ocjene)
                        .Include(o => o.Prilozi);
                        //.ThenInclude(o => o.Prilog);
        }

        public IEnumerable<Prilog> GetAllSideDishes()
        {
            return _context.Prilozi;
        }

        public Hrana CreateOrUpdate(Hrana hrana)
        {
            Hrana ret;
            if (hrana.HranaId != 0)
            {
                _context.HranaPrilozi.RemoveRange(_context.HranaPrilozi.Where(o => o.HranaId == hrana.HranaId));
                ret = _context.Hrana.Update(hrana).Entity;
            }
            else
            {
                ret = _context.Hrana.Add(hrana).Entity;
            }
            _context.SaveChanges();
            return ret;
        }
        public int CreateSideDish(string name)
        {
            var sideDish = _context.Prilozi.Add(new Prilog { Naziv = name });
            _context.SaveChanges();
            return sideDish.Entity.PrilogId;
        }

        public Prilog GetSideDish(int id)
        {
            return _context.Prilozi.Find(id);
        }

        public void SetRate(int userId, int foodId, int mark)
        {
            var rate = _context.Ocjene.Where(o => o.UserId == userId && o.HranaId == foodId).FirstOrDefault();
            if (rate == null)
            {
                var newRate = new Ocjena
                {
                    UserId = userId,
                    HranaId = foodId,
                    Vrijednost = mark
                };
                _context.Ocjene.Add(newRate);
            }
            else
            {
                rate.Vrijednost = mark;
                _context.Ocjene.Update(rate);
            }
            _context.SaveChanges();
        }


        public Hrana GetById(int id)
        {
            return _context.Hrana.Find(id);
        }

        public void Delete(int id)
        {
            var hrana = _context.Hrana.Find(id);
            if (hrana != null)
            {
                _context.Hrana.Remove(hrana);
                _context.SaveChanges();
            }
        }


    }
}
