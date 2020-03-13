using Esp8266VueMeteo.Database;
using Esp8266VueMeteo.Database.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Repositories
{
    public interface IUsersRepository
    {
        Users GetUserByName(string userName);
    }
    public class UsersRepository : IUsersRepository
    {
        private readonly ILogger<UsersRepository> _logger;
        private readonly MeteoDbContext _context;
        public UsersRepository(MeteoDbContext context, ILogger<UsersRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public Users GetUserByName(string userName)
        {
            return _context.Users.FirstOrDefault(w => w.Username == userName);
        }
    }
}
