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
    public class ToDoListOperation
    {
        static IToDoRepository toDoRepository = new ToDoRepository();
        public ObservableCollection<ToDoModel> GetAllToDoListForUser(int userID)
        {
            return toDoRepository.GetAllToDoList(userID);
        }

        public void CreateToDoList(ToDoModel todoDTO)
        {
            toDoRepository.CreateToDoList(todoDTO);
        }

        public void DeleteToDoList(ToDoModel todoDTO)
        {
            toDoRepository.DeleteToDoList(todoDTO);
        }
    }
}
