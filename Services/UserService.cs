using Common.DTO;
using Common.Exceptions;
using Contracts.Repositories;
using Contracts.Service;
using DataAccess;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _context;
        private readonly IUserRepository _userRepository;

        public UserService(UserContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public ResponseDTO GetAllUsers()
        {
            ResponseDTO result = new();
            try
            {
                result.Result = _userRepository.GetAllUsers();
                result.Success = true;
            }
            catch (UserExceptions userExceptions)
            {
                result.Success = false;
                result.Message = userExceptions.Message;
            }
            catch
            {
                result.Success = false;
                result.Message = "I'm sorry, an error occurred while processing your request. Please try again later or contact us for assistance. We apologize for any inconvenience this may have caused.";
            }

            return result;
        }

        public ResponseDTO AddUser(UserDTO userDTO)
        {
            ResponseDTO result = new();
            try
            {
                _userRepository.AddUser(userDTO);
                result.Success = true;
                _context.SaveChanges();
            }
            catch (UserExceptions userExceptions)
            {
                result.Success = false;
                result.Message = userExceptions.Message;
                return result;
            }
            catch
            {
                result.Success = false;
                result.Message = "I'm sorry, an error occurred while processing your request. Please try again later or contact us for assistance. We apologize for any inconvenience this may have caused.";
                return result;
            }

            return result;
        }

        public ResponseDTO DeleteUser(UserDTO userDTO)
        {
            ResponseDTO result = new();
            try
            {
                _userRepository.DeleteUser(userDTO);
                result.Success = true;
                _context.SaveChanges();
            }
            catch (UserExceptions userExceptions)
            {
                result.Success = false;
                result.Message = userExceptions.Message;
            }
            catch
            {
                result.Success = false;
                result.Message = "I'm sorry, an error occurred while processing your request. Please try again later or contact us for assistance. We apologize for any inconvenience this may have caused.";
            }

            return result;
        }



        public ResponseDTO ModifyUser(UserDTO userDTO)
        {
            ResponseDTO result = new();
            try
            {
                _userRepository.ModifyUser(userDTO);
                result.Success = true;
                _context.SaveChanges();
            }
            catch (UserExceptions userExceptions)
            {
                result.Success = false;
                result.Message = userExceptions.Message;
            }
            catch
            {
                result.Success = false;
                result.Message = "I'm sorry, an error occurred while processing your request. Please try again later or contact us for assistance. We apologize for any inconvenience this may have caused.";
            }

            return result;
        }
    }
}
