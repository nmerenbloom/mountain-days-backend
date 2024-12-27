using Microsoft.EntityFrameworkCore;
using DotNetPracticeApp.Models;

namespace DotNetPracticeApp.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<MountainDay> MountainDays { get; set; } 

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) 
        { 
        }
    }
}
