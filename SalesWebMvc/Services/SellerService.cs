using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;
    
       public SellerService(SalesWebMvcContext context)
       {
            _context = context;
       }  

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {    
            _context.Add(obj);//faz a adição
            _context.SaveChanges();//salva o insert, ou a alteração no BD

        }
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        public void Remove (int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if(!_context.Seller.Any(x => x.Id == obj.Id )) //Se não existir algum id no meu DB
            {
                throw new NotFoundException("Id not found");
            }

            try 
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e) //pode ocorrer uma exceção do entity framework, no momento de realizar o update do dados (nivel de acesso a dados)
            {
                throw new DbConcurrencyException(e.Message);//exceção nivel de serviço, segragando a camadas, a camaada de serviço é con
            }
                    
        }
    }
}
