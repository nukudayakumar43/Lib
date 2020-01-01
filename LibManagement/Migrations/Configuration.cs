using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibManagementModel;
namespace LibManagement.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LibInitializer
    {
        private LibManagementContext _context;

        public LibInitializer(LibManagementContext context)
        {
            _context = context;
        }



        public async Task Seed()
        {
            if (!_context.UserDetails.Any())
            {
                _context.AddRange(_userDetails);
                await _context.SaveChangesAsync();
            }
            

            if (!_context.BookDetails.Any())
            {
                _context.AddRange(_bookDetail);
                await _context.SaveChangesAsync();
            }
        }

        

        List<BookDetail> _bookDetail = new List<BookDetail>
        {
            new BookDetail()
            {
                AuthorName = "Author1",
                BookCategory = "Internet",
                BookName ="ASP.NEt",
                Edition = "1.0",
                CreatedDate = DateTime.UtcNow,
                Price = 120
                
            }
        };

        List<UserDetail> _userDetails = new List<UserDetail>
        {
            new UserDetail()
            {
                EmailID="test@gmail.com",
                UserName = "Admin",
                Password ="password"
            },
            new UserDetail()
            {
                EmailID ="test2@gmail.com",
                UserName = "sam",
                Password ="sam@123"

            },
            new UserDetail()
            {
                EmailID="test3@gmail.com",
                UserName = "user1",
                Password ="user1@123"
            }
        };
    }
}

