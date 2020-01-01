using LibManagementModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibManagementRepository
{
    public interface IUserDetailRepository : IRepository<UserDetail>
    {
    }


    public class UserDetailRepository : GenericRepository<UserDetail>, IUserDetailRepository
    {
        public UserDetailRepository(LibManagementContext context):base(context)
        {

        }
    }
}
