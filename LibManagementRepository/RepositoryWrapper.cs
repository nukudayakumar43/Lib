using LibManagementModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibManagementRepository
{
    public interface IRepositoryWrapper
    {
        IBookDetailRepository BookDetailRepository { get; }

        IUserDetailRepository  UserDetailRepository { get; }
        void Save();
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private LibManagementContext _repoContext;
        
        private IBookDetailRepository _book;

        private IUserDetailRepository _userDetail;
        

        public IBookDetailRepository BookDetailRepository
        {
            get
            {
                if (_book == null)
                {
                    _book = new BookDetailRepository(_repoContext);
                }

                return _book;
            }
        }

        public IUserDetailRepository UserDetailRepository
        {
            get
            {
                if (_userDetail == null)
                {
                    _userDetail = new UserDetailRepository(_repoContext);
                }

                return _userDetail;
            }
        }
        

        public RepositoryWrapper(LibManagementContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            try
            {
                _repoContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
