using LibManagementModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibManagementRepository
{
    public interface IBookDetailRepository : IRepository<BookDetail>
    {
    }

    public class BookDetailRepository : GenericRepository<BookDetail>, IBookDetailRepository
    {
        public BookDetailRepository(LibManagementContext context) : base(context)
        {
        }


    }
}
