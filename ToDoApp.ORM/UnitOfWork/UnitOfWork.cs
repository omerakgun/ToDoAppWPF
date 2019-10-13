using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.ORM
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoAppEntities _context;
        public IUserRepository userRepository { get; private set; }
        public IToDoRepository toDoRepository { get; private set; }
        public IItemRepository itemRepository { get; private set; }
        public UnitOfWork(ToDoAppEntities context)
        {
            _context = context;
            userRepository = new UserRepository();
            toDoRepository = new ToDoRepository();
            itemRepository = new ItemRepository();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
