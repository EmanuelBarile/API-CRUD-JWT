using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUserRepository
    {
        IList<UserDTO> GetAllUsers();
        void AddUser(UserDTO userDTO);
        void DeleteUser(UserDTO userDTO);
        void ModifyUser(UserDTO userDTO);
    }
}
