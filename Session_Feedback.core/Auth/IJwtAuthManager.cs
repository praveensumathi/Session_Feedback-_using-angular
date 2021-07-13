using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Auth
{
    public interface IJwtAuthManager
    {
        public string GenerateToken(string name);
    }
}
