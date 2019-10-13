using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;
using ToDoApp.ORM;

namespace ToDoApp.Service
{
    public static class UserOperation
    {
        static IUserRepository userRepository = new UserRepository();
        public static UserDTO Login(UserDTO userDTO)
        {
            using (UnitOfWork uow = new UnitOfWork(new ToDoAppEntities()))
            {
                return uow.userRepository.Login(userDTO);
            }
        }
        public static bool Register(UserDTO userDTO)
        {
            using (UnitOfWork uow = new UnitOfWork(new ToDoAppEntities()))
            {
                return uow.userRepository.RegisterUser(userDTO);
            }
        }

        public static List<UserDTO> GetUsers()
        {
            using (UnitOfWork uow = new UnitOfWork(new ToDoAppEntities()))
            {
                return uow.userRepository.GetUsers();
            }
        }
    }
}
