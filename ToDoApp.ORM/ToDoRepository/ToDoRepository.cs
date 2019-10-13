using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;

namespace ToDoApp.ORM
{
    public class ToDoRepository : GenericRepository<ToDoAppEntities,TODO_LIST>, IToDoRepository
    {
        public static IItemRepository itemRepository = new ItemRepository();
        public ObservableCollection<ToDoModel> GetAllToDoList(int userID)
        {
            return ConverToToDoModelList(Context.TODO_LIST.Where(td => td.USER_ID == userID).ToList());
        }

        public void CreateToDoList(ToDoModel toDoDTO)
        {
            Context.TODO_LIST.Add(ConvertToToDo(toDoDTO));
            Context.SaveChanges();
        }

        public void DeleteToDoList(ToDoModel toDoDTO)
        {
            foreach (var item in toDoDTO.ItemList)
            {
                itemRepository.DeleteItem(item);
            }
            TODO_LIST _toDo = Context.TODO_LIST.SingleOrDefault(td => td.TODO_LIST_ID == toDoDTO.ToDoID);
            Delete(_toDo);
            Context.SaveChanges();
        }

        #region CONVERTERS
        //MODEL TO DTO CONVERTERS
        private ObservableCollection<ToDoModel> ConverToToDoModelList(List<TODO_LIST> todoList)
        {
            ObservableCollection<ToDoModel> toDoDTOList = new ObservableCollection<ToDoModel>();
            foreach(var todo in todoList)
            {
                ToDoModel _todoModel = new ToDoModel()
                {
                    ToDoID = todo.TODO_LIST_ID,
                    UserId = todo.USER_ID,
                    ToDoName = todo.TODO_LIST_NAME,
                    CreateDate = todo.CREATE_DATE,
                    ItemList = itemRepository.GetItemsByToDoID(todo.TODO_LIST_ID)//new ObservableCollection<Item>()
                };
                toDoDTOList.Add(_todoModel);
            }
            return toDoDTOList;
        }

        private TODO_LIST ConvertToToDo(ToDoModel toDoModel)
        {
            TODO_LIST _toDo = new TODO_LIST()
            {
                TODO_LIST_ID = toDoModel.ToDoID,
                TODO_LIST_NAME = toDoModel.ToDoName,
                USER_ID = toDoModel.UserId,
                ITEMs = new ObservableCollection<ITEM>(),
                CREATE_DATE = DateTime.Now,
                USER = Context.USERs.Where(u => u.USER_ID == toDoModel.UserId).SingleOrDefault()
            };
            return _toDo;
        }

        #endregion
    }
}
