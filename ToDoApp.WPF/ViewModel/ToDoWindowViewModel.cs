using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;
using ToDoApp.Service;

namespace ToDoApp.WPF
{
    public class ToDoWindowViewModel : INotifyPropertyChanged
    {
        public static ToDoListOperation toDoListOperation = new ToDoListOperation();
        public static ItemOperation itemOperation = new ItemOperation();
        private string _selectedToDoName;
        public string SelectedToDoName
        {
            get
            {
                return _selectedToDoName;
            }
            set
            {
                if (_selectedToDoName == value)
                    return;
                _selectedToDoName = value;
                OnPropertyChanged("SelectedToDoName");
            }
        }
        private ObservableCollection<ToDoModel> _toDoList;
        public ObservableCollection<ToDoModel> ToDoList
        {
            get
            {
                return _toDoList;
            }
            set
            {
                _toDoList = value;
            }
        }

        private ObservableCollection<Item> _itemList;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public ObservableCollection<Item> ItemList
        {
            get
            {
                return _itemList;
            }
            set
            {
                _itemList = value;
            }
        }

        public ToDoWindowViewModel()
        {
            _toDoList = new ObservableCollection<ToDoModel>();
            _itemList = new ObservableCollection<Item>();
        }

        public void CreateToDo(string toDoName,int userID)
        {
            ToDoModel _toDo = new ToDoModel()
            {
                UserId = userID,
                ToDoName = toDoName,
                ItemList = new ObservableCollection<Item>()
            };
            toDoListOperation.CreateToDoList(_toDo);
            ToDoList.Add(_toDo);
        }

        public void DeleteToDo(object selectedToDo)
        {
            ToDoModel _selectedToDo = (ToDoModel)selectedToDo;
            toDoListOperation.DeleteToDoList(_selectedToDo);
        }

        public void AddItem(Item _item)
        {
            itemOperation.CreateItem(_item);
            ItemList.Add(_item);
        }

        public void DeleteItem(object selectedItem)
        {
            Item _item = (Item)selectedItem;
            itemOperation.DeleteItem(_item);
        }

        public void UpdateItem(Item _item)
        {
            itemOperation.UpdateItem(_item);
        }

        public ObservableCollection<Item> GetItemsByToDo(object selectedToDo)
        {
            if(selectedToDo != null)
            {
                ToDoModel _selectedToDo = (ToDoModel)selectedToDo;
                var itemList = itemOperation.GetItemsByToDoID(_selectedToDo.ToDoID);
                return itemList;
            }else
            {
                return null;
            }
        }

        public ObservableCollection<ToDoModel> GetAllToDoListByUser(int userID)
        {
            ToDoList = toDoListOperation.GetAllToDoListForUser(userID);
            return ToDoList;
        }

        public ObservableCollection<Item> FilterItems(int Status, object selectedToDo)
        {
            ObservableCollection<Item> filteredList = new ObservableCollection<Item>();
            ToDoModel todoDTO = (ToDoModel)selectedToDo;
            List<Item> list = new List<Item>();
            if (Status == 0)
            {
                list = ToDoList
                    .Where(td => td.ToDoID == todoDTO.ToDoID)
                    .FirstOrDefault()
                    .ItemList
                    .ToList();
            }
            else
            {
                list = ToDoList
                    .Where(td => td.ToDoID == todoDTO.ToDoID)
                    .FirstOrDefault()
                    .ItemList
                    .Where(x => x.Status == Status)
                    .ToList();
            }
            
            foreach (var item in list)
            {
                filteredList.Add(item);
            }
            return filteredList;
        }
    }
}
