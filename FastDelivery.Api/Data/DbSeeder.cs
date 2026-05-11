using FastDelivery.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastDelivery.Api.Data
{
    public class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            db.Database.Migrate();

            if (db.Users.Any() || db.Clients.Any() || db.Orders.Any())
                return;

            var adminId = Guid.NewGuid();
            var user1Id = Guid.NewGuid();
            var passwordHasher = new PasswordHasher<User>();

            var users = new List<User>
            {
                new User { Id = adminId, Name = "Admin", Email = "admin@example.com" , Role = "Driver"},
                new User { Id = user1Id, Name = "User1", Email = "user1@example.com" , Role = "Driver"}
            };
            users[0].Password = BCrypt.Net.BCrypt.HashPassword("123456");
            users[1].Password = BCrypt.Net.BCrypt.HashPassword("123456");
            db.Users.AddRange(users);
            db.SaveChanges();

            var clients = new List<Client>
            { 
                new Client{ Name = "Cliente 1", Address = "Edificio Ocampo, Constitución, Centro Monterrey, N.L.", Phone = "8111111111"},
                new Client{ Name = "Cliente 2", Address = "Av. Universidad 1000, San Nicolás, N.L.", Phone = "8112223333"},
                new Client{ Name = "Cliente 3", Address = "Col. Centro, Monterrey, N.L.", Phone = "8113334444"}
            };
            db.Clients.AddRange(clients);
            db.SaveChanges();

            var orders = new List<Order>
            {
                new Order{ ClientId = 1, DriverId = adminId, Status = "En Camino", OrderNumber = "1234"},
                new Order{ ClientId = 2, DriverId = user1Id, Status = "Pendiente", OrderNumber = "1235"},
                new Order{ ClientId = 3, DriverId = adminId, Status = "Entregado", OrderNumber = "1236"}
            };
            db.Orders.AddRange(orders);
            db.SaveChanges(); db.SaveChanges();
        }
    }
}
