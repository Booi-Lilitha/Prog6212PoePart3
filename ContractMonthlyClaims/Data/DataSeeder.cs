using ContractMonthlyClaims.Models;

namespace ContractMonthlyClaims.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Users.Any()) return;

            var users = new List<User>
            {
                new User { Username = "hradmin", Password = "Admin123!", FirstName = "System", LastName = "Admin", Email = "hr@cmcs.local", HourlyRate = 0, Role = Role.HR },
                new User { Username = "lect1", Password = "Pass123!", FirstName = "John", LastName = "Smith", Email = "john.smith@cmcs.local", HourlyRate = 50m, Role = Role.Lecturer },
                new User { Username = "lect2", Password = "Pass123!", FirstName = "Emily", LastName = "Chen", Email = "emily.chen@cmcs.local", HourlyRate = 50m, Role = Role.Lecturer },
                new User { Username = "coord1", Password = "Pass123!", FirstName = "Jane", LastName = "Miller", Email = "jane.miller@cmcs.local", HourlyRate = 0m, Role = Role.Coordinator },
                new User { Username = "man1", Password = "Pass123!", FirstName = "Sam", LastName = "Newman", Email = "sam.newman@cmcs.local", HourlyRate = 0m, Role = Role.Manager }
            };

            db.Users.AddRange(users);
            db.SaveChanges();
        }
    }
}
