using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{    
    public class TeamRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class TeamResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

