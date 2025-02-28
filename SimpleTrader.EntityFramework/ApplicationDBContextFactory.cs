using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleTrader.EntityFramework
{
    public class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[]? args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();

            optionsBuilder.UseSqlServer("Data Source=ZANZARUKIYA\\SQLEXPRESS;Initial Catalog=simple_trader;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

            return new ApplicationDBContext(optionsBuilder.Options);
        }
    }
}
