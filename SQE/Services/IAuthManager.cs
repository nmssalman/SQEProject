using SQE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDTO loginUserDTO);
        Task<string> CreateToken();
    }
}
