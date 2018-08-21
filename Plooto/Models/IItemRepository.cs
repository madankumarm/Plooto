using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plooto.Models
{
    interface IItemRepository
    {
        List<TaskItem> GetAll();
        TaskItem Get(int id);
        TaskItem Add(TaskItem item);
        void Remove(int id);
        bool Update(TaskItem item);
    }
}
