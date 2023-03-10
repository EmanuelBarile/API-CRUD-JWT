using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IUserService
    {
        ResponseDTO GetAllUsers();
        ResponseDTO AddUser(UserDTO userDTO);
        ResponseDTO DeleteUser(UserDTO userDTO);
        ResponseDTO ModifyUser(UserDTO userDTO);
    }
}
