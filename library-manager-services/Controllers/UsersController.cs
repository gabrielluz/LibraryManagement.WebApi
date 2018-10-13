using System;
using System.Collections.Generic;
using LibraryManager.Models;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository _usersRepository;

        public UsersController() : base() => _usersRepository = new UsersRepository();

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get() => new ActionResult<IEnumerable<User>>(_usersRepository.GetAll());

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id) => _usersRepository.GetUser(id);

        [HttpPost]
        public User Post([FromBody] User user) => _usersRepository.AddUser(user);

        [HttpPut("{id}")]
        public User Put(int id, [FromBody] User user) => _usersRepository.UpdateUser(id, user);

        [HttpDelete("{id}")]
        public void Delete(int id) => _usersRepository.Delete(id);
    }
}