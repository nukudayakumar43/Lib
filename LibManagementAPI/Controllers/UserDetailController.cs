using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibManagementRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserDetailController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public UserDetailController(IRepositoryWrapper repoWrapper)
        {
            _repositoryWrapper = repoWrapper;

        }

        // GET: api/<controller>
        [HttpGet]
        public bool Get(string userName, string password)
        {
            bool isExist = _repositoryWrapper.UserDetailRepository.GetFirst(p => p.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)
                        && p.Password.Equals(password, StringComparison.InvariantCultureIgnoreCase)) != null;

            return isExist;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
