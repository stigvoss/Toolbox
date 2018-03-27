using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toolbox.Threading.Tasks.Extensions
{
    public static class TaskFactoryExtensions
    {
        public static List<Task> StartNew(this TaskFactory factory, Action action, int count, CancellationToken token)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < count; i++)
            {
                Task task = factory.StartNew(action, token);
                tasks.Add(task);
            }

            return tasks;
        }
    }
}
