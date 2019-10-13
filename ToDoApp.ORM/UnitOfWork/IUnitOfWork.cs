using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.ORM
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository userRepository { get; }
        IToDoRepository toDoRepository { get; }
        IItemRepository itemRepository { get; }
        int Complete();
    }
}
