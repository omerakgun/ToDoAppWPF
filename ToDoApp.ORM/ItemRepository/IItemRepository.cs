using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;

namespace ToDoApp.ORM
{
    public interface IItemRepository : IGenericRepository<ITEM>
    {
        ObservableCollection<Item> GetItemsByToDoID(int ToDoID);
        void CreateItem(Item itemDTO);
        void DeleteItem(Item itemDTO);
        void UpdateItem(Item itemDTO);
    }
}
