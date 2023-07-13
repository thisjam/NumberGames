using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace designMode.observer
{



    public class TaskNotify : ITaskNotify
    {
        public event Func<dynamic, dynamic> action;
        public dynamic UpDate(dynamic data)
        {
           return  action(data);
        }
    }
}
