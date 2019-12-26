using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace LibManagementModel
{
    public class LibManagementContext : DbContext
    {
        public LibManagementContext(DbContextOptions<LibManagementContext> options)
            : base(options)
        {
        }

        public DbSet<LibManagementModel.UserDetail> UserDetails { get; set; }
        public DbSet<LibManagementModel.BookDetail> BookDetails { get; set; }

       
    }

}
