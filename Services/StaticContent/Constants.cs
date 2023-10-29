using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.StaticContent
{
    public class Constants
    {
        public readonly static string CompanyEmail = Environment.GetEnvironmentVariable("CompanyEmail", EnvironmentVariableTarget.Machine);
        public readonly static string CompanyPassword = Environment.GetEnvironmentVariable("CompanyPassword", EnvironmentVariableTarget.Machine);


        public readonly static string ISSUER = "https://localhost:7136/";
        public readonly static string AUDIENCE = "https://localhost:7136/";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
          new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9F75B0643BD607815115ED832C886DB72C6F367EFEED5042063F88E1598FD893"));
       
    }
}
