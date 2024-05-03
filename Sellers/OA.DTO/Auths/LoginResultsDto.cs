using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.DTO.Auths
{
    public class LoginResultsDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
