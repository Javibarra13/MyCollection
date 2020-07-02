﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers
{
    [Authorize(Roles = "Manager , Collector")]

    public class PaymentsController : Controller
    {
        private readonly DataContext _dataContext;

        public PaymentsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Sales
                .Include(s => s.Collector)
                .ThenInclude(c => c.User)
                .Include(s => s.Customer)
                .ThenInclude(c => c.User)
                .Include(s => s.State)
                .Include(s => s.SaleDetails)
                .Where(Extensions.IsntPaid()));
        }

        public IActionResult Paid()
        {
            return View(_dataContext.Sales
                .Include(s => s.Collector)
                .ThenInclude(c => c.User)
                .Include(s => s.Customer)
                .ThenInclude(c => c.User)
                .Include(s => s.State)
                .Include(s => s.SaleDetails)
                .Where(Extensions.IsPaid()));
        }

        public IActionResult IsntPaid()
        {
            return View(_dataContext.Sales
                .Include(s => s.Collector)
                .ThenInclude(c => c.User)
                .Include(s => s.Customer)
                .ThenInclude(c => c.User)
                .Include(s => s.State)
                .Include(s => s.SaleDetails)
                .Where(Extensions.IsntPaid()));
        }

        public IActionResult Payments()
        {
            var collector = _dataContext.Collectors.Where(s => s.User.UserName == User.Identity.Name).FirstOrDefault();
            return View(_dataContext.Sales
                .Include(s => s.Collector)
                .ThenInclude(c => c.User)
                .Include(s => s.Customer)
                .ThenInclude(c => c.User)
                .Include(s => s.State)
                .Include(s => s.SaleDetails)
                .Where(Extensions.IsntPaid())
                .Where(s => s.Customer.Collector.Id == collector.Id));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _dataContext.Sales
                .Include(s => s.House)
                .Include(s => s.Warehouse)
                .Include(s => s.Collector)
                .ThenInclude(c => c.User)
                .Include(s => s.TypePayment)
                .Include(s => s.DayPayment)
                .Include(s => s.Seller)
                .ThenInclude(s => s.User)
                .Include(s => s.Customer)
                .ThenInclude(c => c.User)
                .Include(s => s.State)
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .Include(s => s.Payments)
                .ThenInclude(p => p.Concept)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        public IActionResult Create(int? id)
        {
            var sale = _dataContext.Sales
                .Include(s => s.Collector)
                .ThenInclude(c => c.User)
                .Include(s => s.Customer)
                .ThenInclude(c => c.User)
                .FirstOrDefault(s => s.Id == id);

            var payment = new PaymentViewModel
            {
                CustomerId = sale.Customer.Id,
                CollectorId = sale.Customer.Collector.Id,
                ConceptId = 6,
                Date = DateTime.Today,
                Deposit = sale.Payment,
                SaleId = sale.Id,
            };

            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    Date = viewModel.Date.ToUniversalTime(),
                    Deposit = viewModel.Deposit,
                    Type = viewModel.Type,
                    Customer = await _dataContext.Customers.FindAsync(viewModel.CustomerId),
                    Collector = await _dataContext.Collectors.FindAsync(viewModel.CollectorId),
                    Concept = await _dataContext.Concepts.FindAsync(viewModel.ConceptId),
                    Sale = await _dataContext.Sales.FindAsync(viewModel.SaleId),
                };

                _dataContext.Payments.Add(payment);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Payments", new { id = viewModel.SaleId });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _dataContext.Payments
                .Include(p => p.Sale)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            _dataContext.Payments.Remove(payment);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Details", "Payments", new { id = payment.Sale.Id });
        }

        private bool PaymentExists(int id)
        {
            return _dataContext.Payments.Any(e => e.Id == id);
        }
    }
}