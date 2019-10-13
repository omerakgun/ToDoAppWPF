using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;

namespace ToDoApp.ORM
{
    public interface IToDoRepository : IGenericRepository<TODO_LIST>
    {
        ObservableCollection<ToDoModel> GetAllToDoList(int userID);
        void CreateToDoList(ToDoModel toDoDTO);
        void DeleteToDoList(ToDoModel toDoDTO);
    }
}
