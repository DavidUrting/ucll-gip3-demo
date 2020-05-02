using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiP3Demo.Models;
using GiP3Demo.ViewModels.Customer;
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
            IEnumerable<CustomerViewModel> model = (from c in _context.Customer
                                                   .Include(c => c.Person)
                                                   .ThenInclude(p => p.BusinessEntity)
                                                   .ThenInclude(be => be.BusinessEntityAddress)
                                                   .ThenInclude(bea => bea.Address)
                                                   where c.Person.LastName != null
                                                   orderby c.Person.LastName
                                                   select new CustomerViewModel()
                                                   {
                                                       FirstName = c.Person.FirstName,
                                                       LastName = c.Person.LastName,
                                                       AccountNumber = c.AccountNumber,
                                                       City = c.Person.BusinessEntity.BusinessEntityAddress.FirstOrDefault() != null ? c.Person.BusinessEntity.BusinessEntityAddress.FirstOrDefault().Address.City : "n.b."
                                                   }).Take(20);

            return View(model);
        }

        public IActionResult Details(string accountNumber)
        {
            CustomerDetailsViewModel model = (from c in _context.Customer
                                       .Include(c => c.Person)
                                       .ThenInclude(p => p.BusinessEntity)
                                       .ThenInclude(be => be.BusinessEntityAddress)
                                       .ThenInclude(bea => bea.Address)
                                        where c.AccountNumber == accountNumber
                                        select new CustomerDetailsViewModel()
                                        {
                                            FirstName = c.Person.FirstName,
                                            LastName = c.Person.LastName,
                                            AccountNumber = c.AccountNumber,
                                            City = c.Person.BusinessEntity.BusinessEntityAddress.FirstOrDefault() != null ? c.Person.BusinessEntity.BusinessEntityAddress.FirstOrDefault().Address.City : "n.b.",
                                            Street = c.Person.BusinessEntity.BusinessEntityAddress.FirstOrDefault() != null ? c.Person.BusinessEntity.BusinessEntityAddress.FirstOrDefault().Address.AddressLine1 : "n.b."
                                        }).FirstOrDefault();
            if (model == null)
            {
                return NotFound();
            } else
            {
                return View(model);
            }
        }
    }
}