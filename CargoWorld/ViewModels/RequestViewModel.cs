using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class RequestViewModel 
    {
        public Request Request { get; set; }
        //необходимо для третьего реквеста(пользователь для компании)
        public IEnumerable<Group> Groups { get; set; }
    }
}
