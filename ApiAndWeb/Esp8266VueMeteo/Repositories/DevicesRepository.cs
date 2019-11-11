using Esp8266VueMeteo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Repositories
{
    public interface IDevicesRepository
    {
        List<Devices> AuthorizeByEspId(string espId);
    }
    public class DevicesRepository : IDevicesRepository
    {
        private readonly MeteoDbContext _context;
        public DevicesRepository(MeteoDbContext context)
        {
            _context = context;
        }

        public List<Devices> AuthorizeByEspId(string espId)
        {
            return _context.Devices.Where(d => espId.Contains(d.Esp8266Id) && d.IsActive).ToList();
        }
    }
}
