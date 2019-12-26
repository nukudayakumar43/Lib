using LibManagementModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibManagementRepository
{
    public interface IRepositoryWrapper
    {
        IBookDetailRepository BookDetailRepository { get; }
        
        void Save();
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private LibManagementContext _repoContext;
        
        private IBookDetailRepository _account;

        public IBookDetailRepository BookDetailRepository
        {
            get
            {
                if (_account == null)
                {
                    _account = new BookDetailRepository(_repoContext);
                }

                return _account;
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
