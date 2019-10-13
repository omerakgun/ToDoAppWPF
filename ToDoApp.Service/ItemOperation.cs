using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;
using ToDoApp.ORM;

namespace ToDoApp.Service
{
    public class ItemOperation
    {
        IItemRepository itemRepository = new ItemRepository();
        public void CreateItem(Item itemDTO)
        {
            itemRepository.CreateItem(itemDTO);
        }

        public ObservableCollection<Item> GetItemsByToDoID(int toDoID)
        {
            return itemRepository.GetItemsByToDoID(toDoID);
        }

        public void DeleteItem(Item itemDTO)
        {
            itemRepository.DeleteItem(itemDTO);
        }

        public void UpdateItem(Item itemDTO)
        {
            itemRepository.UpdateItem(itemDTO);
        }
    }
}
