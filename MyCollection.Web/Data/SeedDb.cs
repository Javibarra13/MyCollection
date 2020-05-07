using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Helpers;
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
            private readonly IUserHelper _userHelper;

            public SeedDb(
                DataContext context,
                IUserHelper userHelper)
            {
                _context = context;
                _userHelper = userHelper;
            }

            public async Task SeedAsync()
            {
                await _context.Database.EnsureCreatedAsync();
                await CheckRoles();
                var manager = await CheckUserAsync("1010", "Javier", "Ibarra", "frajavi3@gmail.com", "6625129365", "Acacia Blanca 192", "Manager");
                var customer = await CheckUserAsync("5050", "Javier", "Ayala", "javibarra@gmail.com", "6625129365", "Maytorena 6", "Customer");
                var seller = await CheckUserAsync("2020", "Juan", "Zuluaga", "jzuluaga55@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", "Seller");
                var collector = await CheckUserAsync("2020", "Francisco", "Ayala", "frajavi3@hotmail.com", "6625129365", "De los Tributos 23", "Collector");
                var supervisor = await CheckUserAsync("2020", "Claudia", "Sosa", "clau201569@gmail.com", "6628486267", "Acacia Blanca 192", "Supervisor");
                await CheckPropertyTypesAsync();
                await CheckCustomersAsync(customer);
                await CheckManagersAsync(manager);
                await CheckSellersAsync(seller);
                await CheckCollectorsAsync(collector);
                await CheckSupervisorsAsync(supervisor);
                await CheckPropertyCollectorsAsync();
                await CheckPropertyManagersAsync();
                await CheckPropertySellersAsync();
                await CheckPropertySupervisorsAsync();
            }

            private async Task CheckCustomersAsync(User user)
            {
                if (!_context.Customers.Any())
                {
                    _context.Customers.Add(new Customer { User = user });
                    await _context.SaveChangesAsync();
                }
            }

            private async Task CheckManagersAsync(User user)
            {
                if (!_context.Managers.Any())
                {
                    _context.Managers.Add(new Manager { User = user });
                    await _context.SaveChangesAsync();
                }
            }

            private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string role)
            {
                var user = await _userHelper.GetUserByEmailAsync(email);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        UserName = email,
                        PhoneNumber = phone,
                        Address = address,
                        Document = document
                    };

                    await _userHelper.AddUserAsync(user, "123456");
                    await _userHelper.AddUserToRoleAsync(user, role);
                }

                return user;
            }


            private async Task CheckRoles()
            {
                await _userHelper.CheckRoleAsync("Manager");
                await _userHelper.CheckRoleAsync("Customer");
                await _userHelper.CheckRoleAsync("Seller");
                await _userHelper.CheckRoleAsync("Collector");
                await _userHelper.CheckRoleAsync("Supervisor");
            }

            private async Task CheckSellersAsync(User user)
            {
                if (!_context.Sellers.Any())
                {
                    _context.Sellers.Add(new Seller { User = user });
                    await _context.SaveChangesAsync();
                }
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

            private async Task CheckCollectorsAsync(User user)
            {
                if (!_context.Collectors.Any())
                {
                    _context.Collectors.Add(new Collector { User = user });
                    await _context.SaveChangesAsync();
                }
            }

            private async Task CheckSupervisorsAsync(User user)
            {
                if (!_context.Supervisors.Any())
                {
                    _context.Supervisors.Add(new Supervisor { User = user });
                    await _context.SaveChangesAsync();
                }
            }

            private async Task CheckPropertyCollectorsAsync()
            {
                var collector = _context.Collectors.FirstOrDefault();
                var propertyType = _context.PropertyTypes.FirstOrDefault();
                if (!_context.PropertyCollectors.Any())
                {
                    AddPropertyCollector("123456", "Alcatel", "IC500", "Azul", 1500, "Se entrego totalmente nuevo y con funda", collector, propertyType);
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
