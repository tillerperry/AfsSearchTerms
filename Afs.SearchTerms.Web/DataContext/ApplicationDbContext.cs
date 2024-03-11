using System.Reflection;
using Afs.SearchTerms.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Afs.SearchTerms.Web.DataContext
{
    public class ApplicationDbContext : DbContext
    {

        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public  DbSet<TranslationSearch> TranslationSearch => Set<TranslationSearch>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    

    }
    
 
}