using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data.Entities;

namespace MyCollection.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Collector> Collectors { get; set; }

        public DbSet<CustomerImage> CustomerImages { get; set; }

        public DbSet<House> Houses { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<PropertyCollector> PropertyCollectors { get; set; }

        public DbSet<PropertyCollectorImage> PropertyCollectorImages { get; set; }

        public DbSet<PropertyManager> PropertyManagers { get; set; }

        public DbSet<PropertyManagerImage> PropertyManagerImages { get; set; }

        public DbSet<PropertySeller> PropertySellers { get; set; }

        public DbSet<PropertySellerImage> PropertySellerImages { get; set; }

        public DbSet<PropertySupervisor> PropertySupervisors { get; set; }

        public DbSet<PropertySupervisorImage> PropertySupervisorImages { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Supervisor> Supervisors { get; set; }

        public DbSet<Concept> Concepts { get; set; }

        public DbSet<Line> Lines { get; set; }

        public DbSet<Subline> Sublines { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Movement> Movements { get; set; }

        public DbSet<TypePayment> TypePayments { get; set; }

        public DbSet<DayPayment> DayPayments { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        public DbSet<PurchaseDetailTmp> PurchaseDetailTmps { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderTmp> OrderTmps { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderDetailTmp> OrderDetailTmps { get; set; }

        public DbSet<State> States { get; set; }
    }

}
