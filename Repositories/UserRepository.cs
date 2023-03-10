using Common.DTO;
using Common.Exceptions;
using Contracts.Repositories;
using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserContext _context { get; set; }
        public UserRepository(UserContext context)
        {
            _context = context;
        }
      
        public IList<UserDTO> GetAllUsers()
        {
            return _context.Users.Where(x => x.IsActive).Select(x => new UserDTO()
            {

                UserName = x.UserName,
                Password = x.Password,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Role = x.Role,
            }).ToList();
        }

        public void AddUser(UserDTO userDTO)
        {

            if (_context.Users.Any(x => x.UserName == userDTO.UserName))
            {
                throw new UserExceptions("UserName is already taken.");
            }
            _context.Users.Add(new DataAccess.Models.User()
            {
                UserName = userDTO.UserName,
                Password = userDTO.Password,
                Name = userDTO.Name,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                Role = userDTO.Role,
                IsActive = true
            });

        }

        public void DeleteUser(UserDTO userDTO)
        {

            var deleteUser = _context.Users.FirstOrDefault(x => x.UserName == userDTO.UserName && x.IsActive)
                ?? throw new UserExceptions("UserName not found.");

            deleteUser.IsActive = false;
            _context.Users.Update(deleteUser);

        }

        public void ModifyUser(UserDTO userDTO)
        {
            var modifyUser = _context.Users.FirstOrDefault(x => x.UserName == userDTO.UserName && x.IsActive)
                ?? throw new UserExceptions("UserName not found.");

            if (userDTO.UserName != modifyUser.UserName)
            {
                modifyUser.UserName = userDTO.UserName;
            }

            if (userDTO.Password != modifyUser.Password)
            {
                modifyUser.Password = userDTO.Password;
            }

            if (userDTO.Name != modifyUser.Name)
            {
                modifyUser.Name = userDTO.Name;
            }

            if (userDTO.Email != modifyUser.Email)
            {
                modifyUser.Email = userDTO.Email;
            }

            if (userDTO.Phone != modifyUser.Phone)
            {
                modifyUser.Phone = userDTO.Phone;
            }

            if (userDTO.Role != modifyUser.Role)
            {
                modifyUser.Role = userDTO.Role;
            }

            _context.Users.Update(modifyUser);
        }



    }
}
