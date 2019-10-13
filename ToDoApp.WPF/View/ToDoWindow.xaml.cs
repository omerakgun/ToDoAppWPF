using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoApp.Entity;

namespace ToDoApp.WPF.View
{
    /// <summary>
    /// ToDoWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class ToDoWindow : Window
    {
        public UserDTO userDTO { get; set; }
        ToDoWindowViewModel toDoWindowViewModel;
        private GridViewColumnHeader lastHeaderClicked = null;
        private ListSortDirection lastDirection = ListSortDirection.Ascending;

        public ToDoWindow()
        {
            InitializeComponent();
            toDoWindowViewModel = new ToDoWindowViewModel();
            ToDoLV.ItemsSource = toDoWindowViewModel.ToDoList;
            ItemLV.ItemsSource = toDoWindowViewModel.ItemList;
            SelectedToDoName.Content = toDoWindowViewModel.SelectedToDoName;
        }

        private void ToDoWindowLoaded(object sender, RoutedEventArgs e)
        {
            userDTO = (UserDTO)this.DataContext;
            ItemStatusCmb.ItemsSource =  Enum.GetValues(typeof(ItemStatus)).Cast<ItemStatus>();
            GetAllToDoList(userDTO.UserId);
        }
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void GetAllToDoList(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
            //ToDoListNameTxt.Text = "new text";
        }
        private void GetAllToDoList(int userID)
        {
            ToDoLV.ItemsSource = null;
            ToDoLV.ItemsSource = toDoWindowViewModel.GetAllToDoListByUser(userDTO.UserId);
        }

        private void AddToDoList(object sender, RoutedEventArgs e)
        {
            toDoWindowViewModel.CreateToDo(ToDoNameTxt.Text, userDTO.UserId);
            GetAllToDoList(userDTO.UserId);
            ToDoNameTxt.Clear();
        }

        private void DeleteToDoList(object sender, RoutedEventArgs e)
        {
            var selected = ToDoLV.SelectedItem;
            toDoWindowViewModel.DeleteToDo(selected);
            GetAllToDoList(userDTO.UserId);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            AddItem(false);
        }
        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            AddItem(true);
        }

        public void AddItem(bool isUpdate)
        {
            var _selectedToDo = (ToDoModel)ToDoLV.SelectedItem;
            var _selectedItem = (Item)ItemLV.SelectedItem;
            if (_selectedToDo == null)
            {
                MessageBox.Show("Select ToDo");
                return;
            }
            if(ItemNameTxt.Text.Trim() == "" || ItemDescriptionTxt.Text.Trim() == "" || DeadLine.SelectedDate == null)
            {
                MessageBox.Show("Please Enter Item Properties");
                return;
            }
            Item _item = new Item()
            {
                ItemName = ItemNameTxt.Text,
                ItemDescription = ItemDescriptionTxt.Text,
                Status = (int)ItemStatus.Started,
                CreateDate = DateTime.Now,
                Deadline = DeadLine.SelectedDate,
                ToDoID = _selectedToDo.ToDoID
            };
            if (isUpdate)
            {
                if (ItemStatusCmb.SelectedValue == null)
                {
                    MessageBox.Show("Select Status to Update Item");
                    return;
                }
                _item.Status = (int)ItemStatusCmb.SelectedValue;
                _item.ItemID = _selectedItem.ItemID;
                toDoWindowViewModel.UpdateItem(_item);
            }else
            {
                toDoWindowViewModel.AddItem(_item);
            }
            ItemTextClear();
            ToDoChanged();
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            var _selectedItem = ItemLV.SelectedItem;
            if(_selectedItem == null)
            {
                MessageBox.Show("Select An Item To Delete");
                return;
            }
            toDoWindowViewModel.DeleteItem(_selectedItem);
            ToDoChanged();
        }

        private void ToDoChanged(object sender, SelectionChangedEventArgs e)
        {
            ToDoChanged();
        }

        private void ToDoChanged()
        {
            var selected = ToDoLV.SelectedItem;
            ItemLV.ItemsSource = null;
            ItemLV.ItemsSource = toDoWindowViewModel.GetItemsByToDo(selected);
        }

        private void ItemTextClear()
        {
            ItemNameTxt.Clear();
            ItemDescriptionTxt.Clear();
        }

        private void SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Item)ItemLV.SelectedItem;
            if (selected != null)
            {
                ItemNameTxt.Text = selected.ItemName;
                ItemDescriptionTxt.Text = selected.ItemDescription;
                ItemStatusCmb.SelectedItem = (ItemStatus)selected.Status;
                DeadLine.SelectedDate = selected.Deadline;
            }
        }

        //Sort functions for Item List View
        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is GridViewColumnHeader ch)) return;
            var dir = ListSortDirection.Ascending;
            if (ch == lastHeaderClicked && lastDirection == ListSortDirection.Ascending)
                dir = ListSortDirection.Descending;
            sort(ch, dir);
            lastHeaderClicked = ch;
            lastDirection = dir;
        }

        private void sort(GridViewColumnHeader ch, ListSortDirection dir)
        {
            var bn = (ch.Column.DisplayMemberBinding as Binding)?.Path.Path;
            bn = bn ?? ch.Column.Header as string;
            var dv = CollectionViewSource.GetDefaultView(ItemLV.ItemsSource);
            dv.SortDescriptions.Clear();
            var sd = new SortDescription(bn, dir);
            dv.SortDescriptions.Add(sd);
            dv.Refresh();
        }

        private void ItemFilter(object sender, RoutedEventArgs e)
        {
            if(ItemStatusCmb.SelectedValue == null)
            {
                MessageBox.Show("Select Antatus To FilterItem");
                return;
            } else if(ToDoLV.SelectedItem == null) {
                MessageBox.Show("Select A ToDo List To FilterItem");
                return;
            }
            var status = (int)ItemStatusCmb.SelectedValue;
            var sortedList = toDoWindowViewModel.FilterItems(status, ToDoLV.SelectedItem);
            ItemLV.ItemsSource = null;
            ItemLV.ItemsSource = sortedList;
        }
    }
}
