using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vimachem.Models.Domain;

namespace Vimachem.Tests.Data
{
    public class TestDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
    }
}
