using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;

namespace ToDoApp.ORM
{
    public interface IUserRepository : IGenericRepository<USER>
    {
        List<UserDTO> GetUsers();
        UserDTO Login(UserDTO userDTO);
        bool RegisterUser(UserDTO userDTO);
    }
}
