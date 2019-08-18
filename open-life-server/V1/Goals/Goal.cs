using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace open_life_server.V1.Goals
{
    public class Goal
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
