using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Entity
{
    public class Item
    {
        public int ItemID { get; set; }
        public int ToDoID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<DateTime> Deadline { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
    }

    public enum ItemStatus
    {
        All = 0,
        Started = 1,
        Finished = 2,
        Expired = 3
    }
}
