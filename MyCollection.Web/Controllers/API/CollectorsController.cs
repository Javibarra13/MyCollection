﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Common.Models;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyCollection.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CollectorsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CollectorsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetCollectorByEmail")]
        public async Task<IActionResult> GetCollectorByEmailAsync(EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var collector = await _dataContext.Collectors
                .Include(c => c.User)
                .Include(c => c.PropertyCollectors)
                .ThenInclude(pc => pc.PropertyType)
                .Include(c => c.PropertyCollectors)
                .ThenInclude(pc => pc.PropertyCollectorImages)
                .Include(c => c.Customers)
                .ThenInclude(c => c.House)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Collector)
                .Include(c => c.Customers)
                .ThenInclude(c => c.CustomerImages)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Sales)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Sales)
                .ThenInclude(s => s.TypePayment)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Sales)
                .ThenInclude(s => s.DayPayment)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Sales)
                .ThenInclude(s => s.Seller)
                .ThenInclude(s => s.User)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Sales)
                .ThenInclude(s => s.Collector)
                .ThenInclude(s => s.User)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Sales)
                .ThenInclude(s => s.Customer)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Sales)
                .ThenInclude(s => s.SaleDetails)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Sales)
                .ThenInclude(s => s.SaleDetails)
                .ThenInclude(s => s.Product)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Sales)
                .ThenInclude(s => s.SaleDetails)
                .ThenInclude(s => s.Sale)
                .Include(c => c.Customers)
                .ThenInclude(c => c.Payments)
                .ThenInclude(p => p.Concept)
                .Include(c => c.Sales)
                .FirstOrDefaultAsync(c => c.User.Email.ToLower() == request.Email.ToLower());

            if (collector == null)
            {
                return NotFound();
            }

            var response = new CollectorResponse
            {
                Id = collector.Id,
                FirstName = collector.User.FirstName,
                LastName = collector.User.LastName,
                Address = collector.User.Address,
                Document = collector.User.Document,
                Email = collector.User.Email,
                PhoneNumber = collector.User.PhoneNumber,
                PropertyCollectors = collector.PropertyCollectors?.Select(pc => new PropertyCollectorResponse
                {
                    Id = pc.Id,
                    Serie = pc.Serie,
                    Company = pc.Company,
                    Model = pc.Model,
                    Colour = pc.Colour,
                    Price = pc.Price,
                    IsAvailable = pc.IsAvailable,
                    Remarks = pc.Remarks,
                    PropertyType = pc.PropertyType.Name,
                    PropertyCollectorImages = pc.PropertyCollectorImages?.Select(pci => new PropertyCollectorImageResponse
                    {
                        Id = pci.Id,
                        ImageUrl = pci.ImageFullPath
                    }).ToList()
                }).ToList(),
                Customers = collector.Customers?.Select(c => new CustomerResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Neighborhood = c.Neighborhood,
                    City = c.City,
                    PhoneNumber = c.PhoneNumber,
                    PostalCode = c.PostalCode,
                    Remarks = c.Remarks,
                    RefName = c.RefName,
                    RefAddress = c.RefAddress,
                    RefPhone = c.RefPhone,
                    RefName2 = c.RefName2,
                    RefAddress2 = c.RefAddress2,
                    RefPhone2 = c.RefPhone2,
                    House = c.House.Id,
                    Collector = c.Collector.Id,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    CustomerImages = c.CustomerImages?.Select(ci => new CustomerImageResponse
                    {
                        Id = ci.Id,
                        ImageUrl = ci.ImageFullPath
                    }).ToList(),
                    Sales = c.Sales?.Select(s => new SaleResponse
                    {
                        Id = s.Id,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        Payment = s.Payment,
                        Deposit = s.Deposit,
                        Remarks = s.Remarks,
                        TypePayment = s.TypePayment.Name,
                        DayPayment = s.DayPayment.Name,
                        Seller = s.Seller.User.FullName,
                        Collector = s.Collector.Id,
                        Customer = s.Customer.Id,
                        Pending = s.Pending,
                        SaleDetails = s.SaleDetails?.Select(sd => new SaleDetailResponse
                        {
                            Id = sd.Id,
                            Name = sd.Name,
                            Price = sd.Price,
                            Quantity = sd.Quantity,
                            Sale = sd.Sale.Id,
                            Product = sd.Product.Id
                        }).ToList(),
                        Payments = s.Payments?.Select(p => new PaymentResponse
                        {
                            Id = p.Id,
                            Collector = p.Collector.Id,
                            Sale = p.Sale.Id,
                            Customer = p.Customer.Id,
                            Concept = p.Concept.Name,
                            Type = p.Type,
                            Date = p.Date,
                            Deposit = p.Deposit,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude
                        }).ToList(),
                    }).Where(s => s.Pending == false).ToList(),
                }).ToList(),
                Sales = collector.Sales?.Select(s => new SaleResponse
                {
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Payment = s.Payment,
                    Deposit = s.Deposit,
                    Remarks = s.Remarks,
                    TypePayment = s.TypePayment.Name,
                    DayPayment = s.DayPayment.Name,
                    Seller = s.Seller.User.FullName,
                    Collector = s.Collector.Id,
                    Customer = s.Customer.Id,
                    Pending = s.Pending,
                    SaleDetails = s.SaleDetails?.Select(sd => new SaleDetailResponse
                    {
                        Id = sd.Id,
                        Name = sd.Name,
                        Price = sd.Price,
                        Quantity = sd.Quantity,
                        Sale = sd.Sale.Id,
                        Product = sd.Product.Id
                    }).ToList(),
                    Payments = s.Payments?.Select(p => new PaymentResponse
                    {
                        Id = p.Id,
                        Collector = p.Collector.Id,
                        Sale = p.Sale.Id,
                        Customer = p.Customer.Id,
                        Concept = p.Concept.Name,
                        Type = p.Type,
                        Date = p.Date,
                        Deposit = p.Deposit,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude
                    }).ToList(),
                }).Where(s => s.Pending == false).ToList(),
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetAvailbleCustomers")]
        public async Task<IActionResult> GetAvailbleCustomers()
        {
            var customers = await _dataContext.Customers
                .Include(c => c.House)
                .Include(c => c.CustomerImages)
                .Include(c => c.Collector)
                .ToListAsync();

            var response = new List<CustomerResponse>(customers.Select(c => new CustomerResponse
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Neighborhood = c.Neighborhood,
                City = c.City,
                PhoneNumber = c.PhoneNumber,
                PostalCode = c.PostalCode,
                Remarks = c.Remarks,
                RefName = c.RefName,
                RefAddress = c.RefAddress,
                RefPhone = c.RefPhone,
                RefName2 = c.RefName2,
                RefAddress2 = c.RefAddress2,
                RefPhone2 = c.RefPhone2,
                House = c.House.Id,
                Collector = c.Collector.Id,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                CustomerImages = new List<CustomerImageResponse>(c.CustomerImages.Select(ci => new CustomerImageResponse
                {
                    Id = ci.Id,
                    ImageUrl = ci.ImageFullPath
                }).ToList()),
            }).ToList());

            return Ok(response);
        }

    }

}
