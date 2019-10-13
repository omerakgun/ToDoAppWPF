using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;

namespace ToDoApp.ORM
{
    public class ItemRepository : GenericRepository<ToDoAppEntities , ITEM> , IItemRepository
    {
        public ObservableCollection<Item> GetItemsByToDoID(int ToDoID)
        {
            ObservableCollection<Item> itemList = new ObservableCollection<Item>();
            itemList = ConvertItemModelListToItemDTOList(Context.ITEMs.Where(item => item.TODO_LIST_ID == ToDoID).ToList());
            return itemList;
        }

        public void CreateItem(Item itemDTO)
        {
            Context.ITEMs.Add(ConvertToItem(itemDTO));
            Context.SaveChanges();
        }

        public void DeleteItem(Item itemDTO)
        {
            ITEM _item = Context.ITEMs.SingleOrDefault(item => item.ITEM_ID == itemDTO.ItemID);
            Delete(_item);
            Context.SaveChanges();
        }

        public void UpdateItem(Item itemDTO)
        {
            Edit(ConvertToItem(itemDTO));
            Context.SaveChanges();
        }

        #region CONVERTERS
        //MODEL TO DTO CONVERTERS
        private ObservableCollection<Item> ConvertItemModelListToItemDTOList(List<ITEM> itemList)
        {
            ObservableCollection<Item> itemDtoList = new ObservableCollection<Item>();
            foreach (var item in itemList)
            {
                Item _itemModel = new Item()
                {
                    ItemID = item.ITEM_ID,
                    ItemName = item.ITEM_NAME,
                    ItemDescription = item.ITEM_DESCRIPTION,
                    ToDoID = item.TODO_LIST_ID,
                    Deadline = item.DEADLINE,
                    CreateDate = item.CREATE_DATE
                };
                if (item.DEADLINE < DateTime.Now)
                {
                    _itemModel.Status = 3;
                    item.STATUS = 3;
                    Edit(item);
                    Context.SaveChanges();
                } else
                {
                    _itemModel.Status = item.STATUS;
                }
                
                itemDtoList.Add(_itemModel);
            }
            return itemDtoList;
        }
        private ITEM ConvertToItem(Item itemDTO)
        {
            ITEM _toDo = new ITEM()
            {
                ITEM_ID = itemDTO.ItemID,
                ITEM_NAME = itemDTO.ItemName,
                ITEM_DESCRIPTION = itemDTO.ItemDescription,
                STATUS = (int)ItemStatus.Started,
                TODO_LIST_ID = itemDTO.ToDoID,
                DEADLINE = itemDTO.Deadline,
                CREATE_DATE = itemDTO.CreateDate
            };
            if (itemDTO.ItemID > 0)
            {
                _toDo.STATUS = itemDTO.Status;
            }
                return _toDo;
        }

        #endregion
    }
}
