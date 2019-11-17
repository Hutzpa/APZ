using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class ListViewModel<T>
    {
        public IEnumerable<T> List { get; set; }

        public bool CanNext { get; set; }
        public int PageNumber { get; set; }
    }
}
