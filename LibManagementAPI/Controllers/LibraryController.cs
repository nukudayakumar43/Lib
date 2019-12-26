using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibManagementModel;
using LibManagementRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LibManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LibraryController : ControllerBase
    {
        IEnumerable<BookDetail> bookDetails;

        private IRepositoryWrapper _repoWrapper;

        public LibraryController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<BookDetail> Get()
        {
            return _repoWrapper.BookDetailRepository.GetAll();
        }

        [HttpGet]
        [Route("BookSearch")]
        public IEnumerable<BookDetail> Get(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _repoWrapper.BookDetailRepository.GetAll();
            }

            return _repoWrapper.BookDetailRepository.GetMany(p => p.AuthorName.Contains(search) || p.BookName.Contains(search));
        }

        [HttpGet("{id}")]
        public ActionResult<BookDetail> Get(int id)
        {
            return _repoWrapper.BookDetailRepository.Get(p => p.BookDetailId == id);
        }

        [HttpPost]
        public void Post([FromBody] BookDetail value)
        {
            _repoWrapper.BookDetailRepository.Insert(value);
            _repoWrapper.Save();
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repoWrapper.BookDetailRepository.Delete(id);
            _repoWrapper.Save();
        }
    }
}
