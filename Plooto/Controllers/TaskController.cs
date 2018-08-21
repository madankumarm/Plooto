using Plooto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Plooto.Controllers
{
    public class TaskController : ApiController
    {
        static readonly IItemRepository repository = new ItemRepository();

        /// <summary>
        /// GET: api/Task
        /// </summary>
        /// <returns>
        /// Return a list of To Do tasks along with Action Result
        /// </returns>
        [HttpGet()]
        public IHttpActionResult Get()
        {
            IHttpActionResult ret = null;
            var list = repository.GetAll();
            if (list.Count<TaskItem>() > 0)
            {
                ret = Ok(list);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        /// <summary>
        /// GET: api/Task/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return a Task item along with Action Result</returns>
        public TaskItem Get(int id)
        {
            TaskItem item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        /// <summary>
        /// POST: api/Task
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Posts a task item to the To Do application list  and returns Action result </returns>
        [HttpPost()]
        public IHttpActionResult Post(TaskItem item)
        {
            IHttpActionResult ret = null;
            if (Add(item))
            {
                ret = Created<TaskItem>(Request.RequestUri +
                     item.Id.ToString(), item);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        /// <summary>
        /// Helper for the Post
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Returns true if the addition was successful else returns false</returns>
        private bool Add(TaskItem item)
        {
            try
            {
                int newId = 0;

                var list = repository.GetAll();
                newId = list.Max(t => t.Id);
                newId++;
                item.Id = newId;
                repository.Add(item);
            }
            catch (Exception ex)
            {
                return false;
            }
            // TODO: Change to ‘ false ’ to test NotFound()
            return true;
        }

        /// <summary>
        /// PUT: api/Task
        /// </summary>
        /// <param name="item"> Item is the Task object passed from the JS</param>
        /// <returns>Returns the modified item along with the Action result</returns>
        [HttpPut()]
        public IHttpActionResult Put(TaskItem item)
        {
            IHttpActionResult ret = null;
            if (Update(item))
            {
                ret = Ok(item);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        /// <summary>
        /// Helper for the Put api
        /// </summary>
        /// <param name="item">Item is teh Task object passed in from the JS</param>
        /// <returns>Returns true if the item was successfully updated in the Application To do List repository else returns false</returns>
        private bool Update(TaskItem item)
        {
            try
            {
                repository.Update(item);
            }
            catch (Exception ex)
            {
                //We have an option to log the failure here 
                return false;
            }

            return true;
        }

        /// <summary>
        /// DELETE: api/Task
        /// </summary>
        /// <param name="id">Id is the identifier for the Task item for which we will refer while updating the todo list</param>
        /// <returns>Returns true if the item was successfully deleted along with the Action Result else retruns false </returns>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult ret = null;
            if (DeleteItem(id))
            {
                ret = Ok(true);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        /// <summary>
        /// Helper for the Delete Api
        /// </summary>
        /// <param name="id">Id is the identifier for the Task item for which we will refer while updating the todo list</param>
        /// <returns>Returns true if the item was successfully deleted along with the Action Result else retruns false </returns>
        private bool DeleteItem(int id)
        {
            try
            {
                repository.Remove(id);
                return true;
            }
            catch (Exception Ex)
            {
                //Option to log the excetpions
                return false;
            }
            finally
            {
                // option to log/dispose objects 
            }
        }
    }
}
