using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MyCollection.Web.Data
{
    public class SeedDb
    {
            private readonly DataContext _context;

            public SeedDb(DataContext context)
            {
                _context = context;
            }

            public async Task SeedAsync()
            {
                await _context.Database.EnsureCreatedAsync();
                await CheckPropertyTypesAsync();
                await CheckCustomersAsync();
                await CheckCollectorsAsync();
                await CheckSellersAsync();
                await CheckManagersAsync();
                await CheckSupervisorsAsync();
                await CheckPropertyCollectorsAsync();
                await CheckPropertyManagersAsync();
                await CheckPropertySellersAsync();
                await CheckPropertySupervisorsAsync();
            }

        private async Task CheckPropertyTypesAsync()
            {
                if (!_context.PropertyTypes.Any())
                {
                    _context.PropertyTypes.Add(new Entities.PropertyType { Name = "Celular" });
                    _context.PropertyTypes.Add(new Entities.PropertyType { Name = "Tablet" });
                    _context.PropertyTypes.Add(new Entities.PropertyType { Name = "Laptop" });
                    _context.PropertyTypes.Add(new Entities.PropertyType { Name = "Automovil" });
                    await _context.SaveChangesAsync();
                }
            }
            private async Task CheckCustomersAsync()
            {
                if (!_context.Customers.Any())
                {
                    AddCustomer("876543", "Ramon", "Gamboa", "310 322 3221", "Calle Luna Calle Sol", "Villa Colonial", "83106", "Hermosillo", "asdasd", "Francisco Ibarra", "De los Tributos #23", "6625129365", "Claudia Sosa", "Acacia Blanca 192", "6628486267", "Bueno");
                    AddCustomer("654565", "Julian", "Martinez", "300 322 3221", "Calle 77 #22 21", "Acacia Blanca", "83177", "Hermosillo", "asdasd", "Francisco Ibarra", "De los Tributos #23", "6625129365", "Claudia Sosa", "Acacia Blanca 192", "6628486267", "Bueno");
                    AddCustomer("214231", "Carmenza", "Ruis", "350 322 3221", "Carrera 56 #22 21", "Peñasco Residencial", "83200", "Hermosillo", "asdasd", "Francisco Ibarra", "De los Tributos #23", "6625129365", "Claudia Sosa", "Acacia Blanca 192", "6628486267", "Bueno");
                    await _context.SaveChangesAsync();
                }
            }

            private void AddCustomer(
                string document,
                string firstName,
                string lastName,
                string cellPhone,
                string address,
                string neighborhood,
                string postalcode,
                string city,
                string remarks,
                string refname,
                string refaddress,
                string refphone,
                string refname2,
                string refaddress2,
                string refphone2,
                string status)
            {
                _context.Customers.Add(new Customer
                {
                    Address = address,
                    CellPhone = cellPhone,
                    Document = document,
                    FirstName = firstName,
                    LastName = lastName,
                    Neighborhood = neighborhood,
                    PostalCode = postalcode,
                    City = city,
                    Remarks = remarks,
                    RefName = refname,
                    RefAddress = refaddress,
                    RefPhone = refphone,
                    RefName2 = refname2,
                    RefAddress2 = refaddress2,
                    RefPhone2 = refphone2,
                    Status = status,
                });
            }
            private async Task CheckCollectorsAsync()
            {
                if (!_context.Collectors.Any())
                {
                    AddCollector("876543", "Ramon", "Gamboa", "234 3232", "310 322 3221", "Calle Luna Calle Sol");
                    AddCollector("654565", "Julian", "Martinez", "343 3226", "300 322 3221", "Calle 77 #22 21");
                    AddCollector("214231", "Carmenza", "Ruis", "450 4332", "350 322 3221", "Carrera 56 #22 21");
                    await _context.SaveChangesAsync();
                }
            }

            private void AddCollector(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
            {
                _context.Collectors.Add(new Collector
                {
                    Address = address,
                    CellPhone = cellPhone,
                    Document = document,
                    FirstName = firstName,
                    FixedPhone = fixedPhone,
                    LastName = lastName
                });
            }
            private async Task CheckManagersAsync()
            {
                if (!_context.Managers.Any())
                {
                    AddManager("876543", "Javier", "Ibarra", "234 3232", "310 322 3221", "Calle Luna Calle Sol");
                    AddManager("654565", "Claudia", "Sosa", "343 3226", "300 322 3221", "Calle 77 #22 21");
                    AddManager("214231", "Luis", "Ibarra", "450 4332", "350 322 3221", "Carrera 56 #22 21");
                    await _context.SaveChangesAsync();
                }
            }

            private void AddManager(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
            {
                _context.Managers.Add(new Manager
                {
                    Address = address,
                    CellPhone = cellPhone,
                    Document = document,
                    FirstName = firstName,
                    FixedPhone = fixedPhone,
                    LastName = lastName
                });
            }

            private async Task CheckSellersAsync()
            {
                if (!_context.Sellers.Any())
                {
                    AddSeller("876543", "Fryda", "Sosa", "234 3232", "310 322 3221", "Calle Luna Calle Sol");
                    AddSeller("654565", "Carmen", "Ayala", "343 3226", "300 322 3221", "Calle 77 #22 21");
                    AddSeller("214231", "Fernando", "Ayala", "450 4332", "350 322 3221", "Carrera 56 #22 21");
                    await _context.SaveChangesAsync();
                }
            }

            private void AddSeller(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
            {
                _context.Sellers.Add(new Seller
                {
                    Address = address,
                    CellPhone = cellPhone,
                    Document = document,
                    FirstName = firstName,
                    FixedPhone = fixedPhone,
                    LastName = lastName
                });
            }
            private async Task CheckSupervisorsAsync()
            {
                if (!_context.Supervisors.Any())
                {
                    AddSupervisor("876543", "Fryda", "Sosa", "234 3232", "310 322 3221", "Calle Luna Calle Sol");
                    AddSupervisor("654565", "Carmen", "Ayala", "343 3226", "300 322 3221", "Calle 77 #22 21");
                    AddSupervisor("214231", "Fernando", "Ayala", "450 4332", "350 322 3221", "Carrera 56 #22 21");
                    await _context.SaveChangesAsync();
                }
            }

            private void AddSupervisor(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
            {
                _context.Supervisors.Add(new Supervisor
                {
                    Address = address,
                    CellPhone = cellPhone,
                    Document = document,
                    FirstName = firstName,
                    FixedPhone = fixedPhone,
                    LastName = lastName
                });
            }


            private async Task CheckPropertyCollectorsAsync()
                {
                    var collector = _context.Collectors.FirstOrDefault();
                    var propertyType = _context.PropertyTypes.FirstOrDefault();
                    if (!_context.PropertyCollectors.Any())
                    {
                        AddPropertyCollector("123456", "Alcatel", "IC500", "Azul", 1500, "Se entrego totalmente nuevo y con funda", collector , propertyType);
                        AddPropertyCollector("654321", "Huawei", "P20 Lite", "Azul", 5000, "Se entrego con funda", collector, propertyType);
                        await _context.SaveChangesAsync();
                    }
                }

                private void AddPropertyCollector(
                    string serie,
                    string company,
                    string model,
                    string colour,
                    decimal price,
                    string remarks,
                    Collector collector,
                    PropertyType propertyType)
                {
                    _context.PropertyCollectors.Add(new PropertyCollector
                    {
                        Serie = serie,
                        Company = company,
                        Model = model,
                        Colour = colour,
                        Price = price,
                        IsAvailable = true,
                        Remarks = remarks,
                        PropertyType = propertyType,
                        Collector = collector,
                    });
                }

            private async Task CheckPropertyManagersAsync()
            {
                var manager = _context.Managers.FirstOrDefault();
                var propertyType = _context.PropertyTypes.FirstOrDefault();
                if (!_context.PropertyManagers.Any())
                {
                    AddPropertyManager("123456", "Alcatel", "IC500", "Azul", 1500, "Se entrego totalmente nuevo y con funda", manager, propertyType);
                    AddPropertyManager("654321", "Huawei", "P20 Lite", "Azul", 5000, "Se entrego con funda", manager, propertyType);
                    await _context.SaveChangesAsync();
                }
            }

            private void AddPropertyManager(
                string serie,
                string company,
                string model,
                string colour,
                decimal price,
                string remarks,
                Manager manager,
                PropertyType propertyType)
            {
                _context.PropertyManagers.Add(new PropertyManager
                {
                    Serie = serie,
                    Company = company,
                    Model = model,
                    Colour = colour,
                    Price = price,
                    IsAvailable = true,
                    Remarks = remarks,
                    PropertyType = propertyType,
                    Manager = manager,
                });
            }

            private async Task CheckPropertySellersAsync()
            {
                var seller = _context.Sellers.FirstOrDefault();
                var propertyType = _context.PropertyTypes.FirstOrDefault();
                if (!_context.PropertySellers.Any())
                {
                    AddPropertySeller("123456", "Alcatel", "IC500", "Azul", 1500, "Se entrego totalmente nuevo y con funda", seller, propertyType);
                    AddPropertySeller("654321", "Huawei", "P20 Lite", "Azul", 5000, "Se entrego con funda", seller, propertyType);
                    await _context.SaveChangesAsync();
                }
            }

            private void AddPropertySeller(
                string serie,
                string company,
                string model,
                string colour,
                decimal price,
                string remarks,
                Seller seller,
                PropertyType propertyType)
            {
                _context.PropertySellers.Add(new PropertySeller
                {
                    Serie = serie,
                    Company = company,
                    Model = model,
                    Colour = colour,
                    Price = price,
                    IsAvailable = true,
                    Remarks = remarks,
                    PropertyType = propertyType,
                    Seller = seller,
                });
            }
            private async Task CheckPropertySupervisorsAsync()
            {
                var supervisor = _context.Supervisors.FirstOrDefault();
                var propertyType = _context.PropertyTypes.FirstOrDefault();
                if (!_context.PropertySupervisors.Any())
                {
                    AddPropertySupervisor("123456", "Alcatel", "IC500", "Azul", 1500, "Se entrego totalmente nuevo y con funda", supervisor, propertyType);
                    AddPropertySupervisor("654321", "Huawei", "P20 Lite", "Azul", 5000, "Se entrego con funda", supervisor, propertyType);
                    await _context.SaveChangesAsync();
                }
            }

            private void AddPropertySupervisor(
                string serie,
                string company,
                string model,
                string colour,
                decimal price,
                string remarks,
                Supervisor supervisor,
                PropertyType propertyType)
            {
                _context.PropertySupervisors.Add(new PropertySupervisor
                {
                    Serie = serie,
                    Company = company,
                    Model = model,
                    Colour = colour,
                    Price = price,
                    IsAvailable = true,
                    Remarks = remarks,
                    PropertyType = propertyType,
                    Supervisor = supervisor,
                });
            }
    }
}
