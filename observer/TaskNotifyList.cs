using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace designMode.observer
{
    public  class TaskNotifyList
    {
        private List<TaskNotify> taskList = new List<TaskNotify>();
        
        public TaskNotify AddTask(TaskNotify taskNotify)
        {
            taskList.Add(taskNotify);
            return taskNotify;
        }

        public void RemoveTask(TaskNotify taskNotify)
        {
            taskList.Remove(taskNotify);
        }
        public void Clear() { taskList.Clear(); }
        


        public void Notify(dynamic data)
        {
            foreach (var task in taskList)
            {
                task.UpDate(data);
               
            }

        }

        
    }
}
