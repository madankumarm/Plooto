using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plooto.Models
{
    /// <summary>
    /// Itemrepository is to simulate the Database instance and implements interface IItemRepository
    /// </summary>
    public class ItemRepository : IItemRepository
    {
        private List<TaskItem> taskItems = new List<TaskItem>();
        private int _nextId = 1;

        /// <summary>
        /// Initializing the object with dummy data
        /// </summary>
        public ItemRepository()
        {
            Add(new TaskItem() { Complete = false, Description = "Buy Groceries" });
            Add(new TaskItem() { Complete = false, Description = "Wash car" });
            Add(new TaskItem() { Complete = false, Description = "Fix the garage lights" });
        }

        //Add Item to the Repository
        public TaskItem Add(TaskItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Item is null");
            }
            item.Id = _nextId++;
            taskItems.Add(item);
            return item;
        }

        //Returning the Found items
        public TaskItem Get(int id)
        {
            return taskItems.Find(i => i.Id == id);
        }

        //GetAll method to retrieve taskitems ordered by ID
        public List<TaskItem> GetAll()
        {

            return  taskItems.OrderBy(x => x.Id).ToList<TaskItem>() ;
        }

        //Remove items based on ID value
        public void Remove(int id)
        {
            taskItems.RemoveAll(i => i.Id == id);
        }

        //Update Itesm using the Reference object
        public bool Update(TaskItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Cannot update empty/null Item");
            }
            int index = taskItems.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                throw new ArgumentNullException("Item not found");
            }
            taskItems.RemoveAt(index);
            taskItems.Add(item);
            return true;
        }

    }
}