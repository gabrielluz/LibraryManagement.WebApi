using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManager.Models;

namespace LibraryManager.Repositories
{
    public class UsersRepository
    {
        private readonly static Dictionary<int, User> _inMemoryDataBase = CreateInitialUsersDictionary();

        public IEnumerable<User> GetAll() 
        {
            return _inMemoryDataBase.Values.ToList();
        }

        public User GetUser(int id) 
        {
            if (_inMemoryDataBase.TryGetValue(id, out var user))
                return user;
            
            throw new ArgumentException($"User with id { id } not found.");
        }

        public User AddUser(User user) 
        {
            if (user == null)
                throw new ArgumentNullException("User must not be null.");

            var nextId = _inMemoryDataBase.Keys.Max() + 1;
            user.Id = nextId;
            _inMemoryDataBase.Add(nextId, user);
            return _inMemoryDataBase.GetValueOrDefault(nextId);
        }

        public User UpdateUser(int id, User user) 
        {
            if (user == null)
                throw new ArgumentNullException("User must not be null.");

            if (!_inMemoryDataBase.TryGetValue(id, out var existingUser)) 
                throw new ArgumentException($"User with id { user.Id } not found.");

            existingUser.Description = user.Description;
            existingUser.Email = user.Email;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.SecretKey = user.SecretKey;
            _inMemoryDataBase.Remove(id);
            _inMemoryDataBase.Add(id, existingUser);
            return existingUser;
        }

        public void Delete(int id) 
        {
            if (_inMemoryDataBase.Remove(id)) 
                throw new ArgumentException($"User with id { id } not found.");
        }

        private static Dictionary<int, User> CreateInitialUsersDictionary()
        {
            var users = new List<User>
            { 
                new User() 
                { 
                    Id = 1, 
                    Description = "Desc1", 
                    Email = "gabriel@csg.com", 
                    FirstName = "Gabriel", 
                    LastName = "Luz",
                    SecretKey = "abcd1234" 
                },
                new User()
                {
                    Id = 2,
                    Description = "Desc2",
                    Email = "marcelo@csg.com",
                    FirstName = "Marcelo",
                    LastName = "Paglione",
                    SecretKey = "dsaqwe123"
                },
                new User()
                {
                    Id = 3,
                    Description = "Desc3",
                    Email = "daniel@csg.com",
                    FirstName = "Daniel",
                    LastName = "Frossard",
                    SecretKey = "dddssaaaqwe"
                }
            };
            return users.ToDictionary(u => u.Id);
        }
    }
}