using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;
using ToDoApp.ORM;
using ToDoApp.Service;

namespace ToDoApp.Test
{
    [TestClass]
    public class ToDoServiceTest : GenericRepository<ToDoAppEntities, TODO_LIST>, IToDoRepository
    {
        private IToDoRepository _todoRepository;

        public ToDoServiceTest()
        {
            _todoRepository = Substitute.For<IToDoRepository>();
        }

        [TestMethod]
        public void ToDoCreateTest()
        {
            try
            {
                _todoRepository.CreateToDoList(new ToDoModel()
                {
                    UserId = 66,
                    CreateDate = DateTime.Now,
                    ToDoName = "testToDo"
                });
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ToDoDeleteTest()
        {
            try
            {
                _todoRepository.DeleteToDoList(new ToDoModel()
                {
                    UserId = 66,
                    CreateDate = DateTime.Now,
                    ToDoName = "testToDo"
                });
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetAllToDoListTest()
        {
            try
            {
                var list  = _todoRepository.GetAllToDoList(6);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        public void CreateToDoList(ToDoModel toDoDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteToDoList(ToDoModel toDoDTO)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<ToDoModel> GetAllToDoList(int userID)
        {
            throw new NotImplementedException();
        }

        

    }
}
