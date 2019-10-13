using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;

namespace ToDoApp.ORM
{
    public class UserRepository : GenericRepository<ToDoAppEntities, USER>, IUserRepository
    {
        public List<UserDTO> GetUsers()
        {
            var userList = Context
                .USERs
                .ToList();
                return ConvertUserListToUserDTOList(userList);
        }

        public UserDTO Login(UserDTO userDTO)
        {
            var user = Context
                .USERs
                .Where(u =>
                    u.USER_NAME == userDTO.UserName &&
                    u.USER_PASSWORD == userDTO.Password)
                .FirstOrDefault();
            return ConvertUserToUserDTO(user);
        }
        public bool RegisterUser(UserDTO userDTO)
        {
            try
            {
                var user = ConvertUserDTOToUser(userDTO);
                if (Context.USERs.Where(u => u.USER_NAME == user.USER_NAME).FirstOrDefault() == null)
                {
                    Context.USERs.Add(user);
                    Context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        //MODEL TO DTO CONVERTERS
        private UserDTO ConvertUserToUserDTO(USER user)
        {
            if (user != null)
            {
                UserDTO userDTO = new UserDTO()
                {
                    UserId = user.USER_ID,
                    UserName = user.USER_NAME

                };
                return userDTO;
            } else
            {
                return null;
            }
        }

        private List<UserDTO> ConvertUserListToUserDTOList(List<USER> userList)
        {
            List<UserDTO> userDTOList = new List<UserDTO>();
            foreach(var user in userList)
            {
                UserDTO userDTO = new UserDTO()
                {
                    UserId = user.USER_ID,
                    UserName = user.USER_NAME
                };
                userDTOList.Add(userDTO);
            }
            return userDTOList;
        }

        //DTO TO MODEL CONVERTERS
        private USER ConvertUserDTOToUser(UserDTO userDTO)
        {
            if (userDTO != null)
            {
                USER user = new USER()
                {
                    USER_NAME = userDTO.UserName,
                    USER_PASSWORD = userDTO.Password,
                    CREATE_DATE = DateTime.Now
                };
                return user;
            } else
            {
                return null;
            }
        }

        private List<USER> ConvertUserDTOListToUser(List<UserDTO> userDTOList)
        {
            List<USER> userList = new List<USER>();
            foreach (var userDTO in userDTOList)
            {
                USER user = new USER()
                {
                    USER_ID = userDTO.UserId,
                    USER_NAME = userDTO.UserName,
                };
                userList.Add(user);
            }
            return userList;
        }
    }
}
