using Plooto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plooto.DM
{
    public sealed class TodoList
    {
        private static List<TaskItem> taskItem = null;
        private static readonly object padlock = new object();

        TodoList()
        {
        }

        public static List<TaskItem> TaskItem
        {
            get
            {
                lock (padlock)
                {
                    if (taskItem == null)
                    {
                        taskItem = new List<TaskItem>();
                    }
                    return taskItem;
                }
            }
        }
        public void Add(TaskItem item)
        {
            TaskItem.Add(item);
        }
        public void Delete(int id)
        {
            taskItem.Remove(taskItem.SingleOrDefault(x => x.Id == id));
        }
    }
}