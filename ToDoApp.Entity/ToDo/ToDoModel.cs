using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Entity
{
    public class ToDoModel
    {
        public int ToDoID { get; set; }
        public int UserId { get; set; }
        public string ToDoName { get; set; }
        public DateTime CreateDate { get; set; }
        public ObservableCollection<Item> ItemList { get; set; }
    }
}
