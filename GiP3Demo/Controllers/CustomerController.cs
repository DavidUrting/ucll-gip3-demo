using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiP3Demo.Models;
using GiP3Demo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiP3Demo.Controllers
{
    public class CustomerController : Controller
    {
        private AdventureWorks2017Context _context;

        public CustomerController(AdventureWorks2017Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<CustomerViewModel> model = from c in _context.Customer
                                                   .Include(c => c.Person)
                                                   .ThenInclude(p => p.BusinessEntity)
                                                   .ThenInclude(be => be.BusinessEntityAddress)
                                                   .ThenInclude(bea => bea.Address)
                                                   select new CustomerViewModel()
                                                   {
                                                       FirstName = c.Person.FirstName,
                                                       LastName = c.Person.LastName,
                                                       AccountNumber = c.AccountNumber,
                                                       City = c.Person.BusinessEntity.BusinessEntityAddress.FirstOrDefault() != null ? c.Person.BusinessEntity.BusinessEntityAddress.FirstOrDefault().Address.City : "n.b."
                                                   };

            return View(model);
        }
    }
}