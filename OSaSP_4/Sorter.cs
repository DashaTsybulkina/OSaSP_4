using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSaSP_4
{
    public static class Sorter
    {
        public static string[] sort(string[] lines, int countOfThreads)
        {
            if (lines.Length == 0) { 
                return new string[0];
            }
            var taskQueue = new TaskQueue();
            splitTask(lines,countOfThreads, taskQueue);
            createThreads(countOfThreads, taskQueue);
            var result = mergeTask(taskQueue, countOfThreads);
            return result;
        }

        private static void splitTask(string[] lines, int countOfThreads, TaskQueue taskQueue) { 
            var linesCount = lines.Length;

            var correctCountOfThread = linesCount > countOfThreads? countOfThreads: linesCount;
            int countTaskInThreads = linesCount / correctCountOfThread + (linesCount % correctCountOfThread == 0 ? 0 : 1);
            var i = 0;
            var countRemainThread = correctCountOfThread;
            while (countRemainThread > 0) {
                var task = new string[countTaskInThreads];
                for (var j = 0; j < countTaskInThreads; j++) { 
                    task[j] = lines[i++];
                }
                taskQueue.addTask(task);
                if (countRemainThread != 1)
                {
                    countTaskInThreads = (linesCount - i) / (--countRemainThread) + ((linesCount - i) % countRemainThread == 0 ? 0 : 1);
                }
                else
                {
                    --countRemainThread;
                }
            }

        }

        private static string[] mergeTwo(string[] arr1, string[] arr2) { 
            var size1 = arr1.Length;
            var size2 = arr2.Length;

            var result = new List<string>();
            int i = 0, j = 0;
            while (i < size1 && j< size2) {
                if (arr1[i].CompareTo(arr2[j]) <= 0)
                {
                    result.Add(arr1[i++]);
                }
                else
                {
                    result.Add(arr2[j++]);
                }
            }
            while (i < size1) {
                result.Add(arr1[i++]);
            }
            while (j < size2)
            {
                result.Add(arr2[j++]);
            }
            return result.ToArray();
        }
        private static string[] mergeTask(TaskQueue taskQueue, int count) {
            var mergeString = new List<string>();
            mergeString.AddRange(taskQueue.getTask());
            for (var i = 0; i < count-1; i++) {
                mergeString = mergeTwo(mergeString.ToArray(), taskQueue.getTask()).ToList();
            }
            var result  = mergeString.ToArray();
            return result;
        }
        private static void threadSorting(string[] task, TaskQueue taskQueue)
        {
            Array.Sort(task);
            taskQueue.addTask(task);   
        }

        private static void createThreads(int countOfThreads, TaskQueue taskQueue)
        {
            var threads = new Thread[countOfThreads];
            for (var i = 0; i < countOfThreads; i++) {
                var task = taskQueue.getTask();
                Thread thread = new Thread(()=>threadSorting(task, taskQueue));
                thread.Start();
                threads[i] = thread;            }
            for (var i = 0; i < countOfThreads; i++) {
                if (!(threads[i].ThreadState == ThreadState.Stopped)) {
                    threads[i].Join();
                }
            }
        }
    }
}
