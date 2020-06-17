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
            await CheckHousesAsync();
            await CheckSublinesAsync();
            await CheckConceptsAsync();
            await CheckLinesAsync();
            await CheckProvidersAsync();
            await CheckStatesAsync();
            await CheckProductsAsync();
            await CheckWarehousesAsync();
            await CheckInventoriesAsync();
            await CheckMovementsAsync();
            await CheckTypePaymentsAsync();
            await CheckDayPaymentsAsync();
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
        private async Task CheckCollectorsAsync(User user)
        {
            if (!_context.Collectors.Any())
            {
                _context.Collectors.Add(new Collector { User = user });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckTypePaymentsAsync()
        {
            if (!_context.TypePayments.Any())
            {
                _context.TypePayments.Add(new Entities.TypePayment { Name = "Mensual"});
                _context.TypePayments.Add(new Entities.TypePayment { Name = "Semanal"});
                _context.TypePayments.Add(new Entities.TypePayment { Name = "Quincenal" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckDayPaymentsAsync()
        {
            if (!_context.DayPayments.Any())
            {
                _context.DayPayments.Add(new Entities.DayPayment { Name = "Domingo" });
                _context.DayPayments.Add(new Entities.DayPayment { Name = "Lunes" });
                _context.DayPayments.Add(new Entities.DayPayment { Name = "Martes" });
                _context.DayPayments.Add(new Entities.DayPayment { Name = "Miercoles" });
                _context.DayPayments.Add(new Entities.DayPayment { Name = "Jueves" });
                _context.DayPayments.Add(new Entities.DayPayment { Name = "Viernes" });
                _context.DayPayments.Add(new Entities.DayPayment { Name = "Sabado" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckHousesAsync()
        {
            if (!_context.Houses.Any())
            {
                _context.Houses.Add(new Entities.House { Name = "Comercial Hermosillo", Address = "Acacia Blanca 192", City = "Hermosillo", Neighborhood = "Villa Colonial", Phone = "6625129365", Contact = "Francsico Ibarra" });
                _context.Houses.Add(new Entities.House { Name = "Comercial Obregon", Address = "De los Tributos 23", City = "Obregon", Neighborhood = "California Residencial", Phone = "6625129365", Contact = "Javier Ayala" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckMovementsAsync()
        {
            if (!_context.Movements.Any())
            {
                _context.Movements.Add(new Entities.Movement { Name = "Venta", Type = "Pendiente", Costing = "Recuperación", IsAvailable = true });
                _context.Movements.Add(new Entities.Movement { Name = "Compra", Type = "Pendiente", Costing = "Inversión", IsAvailable = false });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckWarehousesAsync()
        {
            if (!_context.Warehouses.Any())
            {
                _context.Warehouses.Add(new Entities.Warehouse { Name = "Bodega Principal", Address = "Avenida Central 798", City = "Hermosillo", Neighborhood = "Villa Central", Phone = "6625129365", IsMain = true, IsAvailable = true, Contact = "Claudia Sosa" });
                _context.Warehouses.Add(new Entities.Warehouse { Name = "Bodega Norte", Address = "Calle Norte 225", City = "Hermosillo", Neighborhood = "California Norte", Phone = "6625129365", IsMain = true, IsAvailable = false, Contact = "Fryda Sosa" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckLinesAsync()
        {
            if (!_context.Lines.Any())
            {
                _context.Lines.Add(new Entities.Line { Name = "Cocinas"});
                _context.Lines.Add(new Entities.Line { Name = "Recamaras"});
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckSublinesAsync()
        {
            var line = _context.Lines.FirstOrDefault();
            if (!_context.Sublines.Any())
            {
                _context.Sublines.Add(new Entities.Subline { Name = "Buros", Line = line });
                _context.Sublines.Add(new Entities.Subline { Name = "Bases", Line = line });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckProductsAsync()
        {
            var line = _context.Lines.FirstOrDefault();
            var subline = _context.Sublines.FirstOrDefault();
            var provider = _context.Providers.FirstOrDefault();
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Entities.Product { Code = "000001", Barcode = "110011", Name = "Base Matrimonial", PurchaseUnit = "Pza", Sale = "Pzas", Factor = "0", IVA = 16M, Location = "Hermosillo", Remarks = "Producto Nuevo", Price = 1000M, Price2 = 2000M, Price3 = 3000M, Price4 = 4000M, Price5 = 5000M, ReorderPoint = 5M, LastCost = 500M, IsAvailable = true, Line = line, Subline = subline, Provider = provider });
                _context.Products.Add(new Entities.Product { Code = "000002", Barcode = "220022", Name = "Base King Size", PurchaseUnit= "Pza", Sale = "Pzas", Factor = "0", IVA = 16M, Location = "Hermosillo", Remarks = "Producto Nuevo", Price = 1000M, Price2 = 2000M, Price3 = 3000M, Price4 = 4000M, Price5 = 5000M, ReorderPoint = 10M, LastCost = 500M, IsAvailable= true, Line = line, Subline = subline, Provider = provider });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckInventoriesAsync()
        {
            var warehouse = _context.Warehouses.FirstOrDefault();
            var product = _context.Products.FirstOrDefault();
            if (!_context.Inventories.Any())
            {
                _context.Inventories.Add(new Entities.Inventory { Stock = 20M, Product = product, Warehouse = warehouse });
                _context.Inventories.Add(new Entities.Inventory { Stock = 10M, Product = product, Warehouse = warehouse });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckConceptsAsync()
        {
            if (!_context.Concepts.Any())
            {
                _context.Concepts.Add(new Entities.Concept { Name = "Abono", Type = "Abono", IsAvailable = true });
                _context.Concepts.Add(new Entities.Concept { Name = "Compra", Type = "Cargo", IsAvailable = false });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProvidersAsync()
        {
            if (!_context.Providers.Any())
            {
                _context.Providers.Add(new Entities.Provider { Name = "Muebles Dico", Address = "Blvd. Acacia 148", Neighborhood = "California", PostalCode = "83155", City = "Hermosillo", RFC = "IAAF940713CM4", Contact = "Javier Ayala", Phone = "6625129365", UserName = "jibarra@hotmail.com", Remarks = "Nuevo", IsAvailable = true});
                _context.Providers.Add(new Entities.Provider { Name = "Muebles Luna", Address = "Blvd. Morelos 844", Neighborhood = "Morelos", PostalCode = "84148", City = "Hermosillo", RFC = "IOOF789512MC8", Contact = "Francisco Ibarra", Phone = "6628486267", UserName = "fibarra@hotmail.com", Remarks = "Nuevo", IsAvailable = true });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCustomersAsync(User user)
        {
            var collector = _context.Collectors.FirstOrDefault();
            var house = _context.Houses.FirstOrDefault();
            if (!_context.Customers.Any())
            {
                AddCustomer("Hermosillo", "Villa Colonial", "83106", "Claudia Sosa", "Acacia Blanca 192", "6625129365",  "Javier Ibarra", "Acacia Blanca 192", "6628486267", "Bueno", "Nuevo", user, house, collector);
                await _context.SaveChangesAsync();
            }
        }

        private void AddCustomer(
            string city,
            string neighborhood,
            string postalCode,
            string refName,
            string refAddress,
            string refPhone,
            string refName2,
            string refAddress2,
            string refPhone2,
            string status,
            string remarks,
            User user,
            House house,
            Collector collector)
        {
            _context.Customers.Add(new Customer
            {
                City = city,
                Neighborhood = neighborhood,
                PostalCode = postalCode,
                RefName = refName,
                RefAddress = refAddress,
                RefPhone = refPhone,
                RefName2 = refName2,
                RefAddress2 = refAddress2,
                RefPhone2 = refPhone2,
                Status = status,
                Remarks = remarks,
                User = user,
                House = house,
                Collector = collector
            });
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

        private async Task CheckStatesAsync()
        {
            if (!_context.States.Any())
            {
                _context.States.Add(new Entities.State { Name = "Created" });
                _context.States.Add(new Entities.State { Name = "Invoiced" });
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
                AddPropertyCollector("123456", "Alcatel", "IC500", "Azul", 1500M, "Se entrego totalmente nuevo y con funda", collector, propertyType);
                AddPropertyCollector("654321", "Huawei", "P20 Lite", "Azul", 5000M, "Se entrego con funda", collector, propertyType);
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
                AddPropertyManager("123456", "Alcatel", "IC500", "Azul", 1500M, "Se entrego totalmente nuevo y con funda", manager, propertyType);
                AddPropertyManager("654321", "Huawei", "P20 Lite", "Azul", 5000M, "Se entrego con funda", manager, propertyType);
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
                AddPropertySeller("123456", "Alcatel", "IC500", "Azul", 1500M, "Se entrego totalmente nuevo y con funda", seller, propertyType);
                AddPropertySeller("654321", "Huawei", "P20 Lite", "Azul", 5000M, "Se entrego con funda", seller, propertyType);
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
                AddPropertySupervisor("123456", "Alcatel", "IC500", "Azul", 1500M, "Se entrego totalmente nuevo y con funda", supervisor, propertyType);
                AddPropertySupervisor("654321", "Huawei", "P20 Lite", "Azul", 5000M, "Se entrego con funda", supervisor, propertyType);
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
