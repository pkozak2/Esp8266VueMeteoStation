using Esp8266VueMeteo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Services
{
    public interface IUsersService
    {
        Guid? GetUserIdByName(string userName);
    }
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Guid? GetUserIdByName(string userName)
        {
            var user = _usersRepository.GetUserByName(userName);
            if (user == null) return null;
            return user.UserId;
        }
    }
}
