using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSaSP_4
{
    public class TaskQueue
    {
        Queue<string[]> tasks = new Queue<string[]>();
        object locker = new();
        public void addTask(string[] task) {
            lock (locker) { 
                tasks.Enqueue(task);
            }
        }

        public string[] getTask()
        {
            lock (locker)
            {
                return tasks.Dequeue();
            }
        }
    }
}
