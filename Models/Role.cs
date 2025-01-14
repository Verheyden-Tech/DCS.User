using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLibrary
{
    public class Role
    {
        public Role() { }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }
    }
}
