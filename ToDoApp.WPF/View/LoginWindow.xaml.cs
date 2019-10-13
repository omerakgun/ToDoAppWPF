using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using ToDoApp.Service;

namespace ToDoApp.WPF.View
{
    /// <summary>
    /// LoginWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (!CheckDatabaseExist())
            {
                GenerateDatabase();
            }
        }

        private void GenerateDatabase()
        {
            List<string> cmds = new List<string>();
            if (File.Exists(Directory.GetCurrentDirectory() + "\\ToDoDbScript.sql"))
            {
                TextReader tr = new StreamReader(Directory.GetCurrentDirectory() + "\\ToDoDbScript.sql");
                string line = "";
                string cmd = "";
                while((line = tr.ReadLine()) != null)
                {
                    if(line.Trim().ToUpper() == "GO")
                    {
                        cmds.Add(cmd);
                        cmd = "";
                    }else
                    {
                        cmd += line + "\r\n";
                    }
                }
                if(cmd.Length > 0)
                {
                    cmds.Add(cmd);
                    cmd = "";
                }
                tr.Close();
            }
            if(cmds.Count > 0)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True");
                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();
                for(int i = 0; i < cmds.Count; i++)
                {
                    command.CommandText = cmds[i];
                    command.ExecuteNonQuery();
                }
            }
        }

        private bool CheckDatabaseExist()
        {
            SqlConnection connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=ToDoApp;Trusted_Connection=True");
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var responseUser = UserOperation.Login(
                new UserDTO()
                {
                    UserName = UserName.Text,
                    Password = Password.Password.ToString().BasicCrypt()
                });

            if (responseUser != null)
            {
                ToDoWindow toDoWindow = new ToDoWindow() {DataContext=responseUser };
                toDoWindow.DataContext = responseUser;
                toDoWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Hatalı Giriş !");
                Password.Clear();
            }
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            if(UserName.Text.Trim().Equals("") || Password.Password.ToString().Trim().Equals(""))
            {
                MessageBox.Show("Please Enter UserName and Password...");
                
            }else
            {
                var result = UserOperation.Register(new UserDTO()
                {
                    UserName = UserName.Text,
                    Password = Password.Password.ToString().BasicCrypt()
                });
                if (result)
                {
                    UserName.Clear();
                    Password.Clear();
                    MessageBox.Show("Register User Success...");
                }
                else
                {
                    MessageBox.Show("Register User Failed !");
                    Password.Clear();
                }
            }
        }
    }
}
