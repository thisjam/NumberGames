using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace designMode.observer
{
    public static class Taskmanager
    {
        public static TaskNotifyList TaskNotifyList = null;
        public static TaskNotify TaskNotify = null;
        static Taskmanager()
        {
            if (TaskNotifyList == null)
            {
                TaskNotifyList = new TaskNotifyList();
                TaskNotify = new TaskNotify();
                TaskNotifyList.AddTask(TaskNotify);
            }

        }




    }
}
