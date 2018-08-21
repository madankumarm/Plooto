using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plooto.Models
{
    /// <summary>
    /// Task Model for our To Do application
    /// </summary>
    public class TaskItem
    {
        public int Id { get; set; } = 0;
        public bool Complete { get; set; } = false;
        
        //Currently not used in the application but can be a future enhancement
        public DateTime Target { get; set; } = DateTime.Now.AddDays(1);
        public string Description { get; set; } = "New Task";
    }
}