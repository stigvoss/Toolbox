using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Toolbox.Threading.Tasks.Extensions
{
    public static class TaskFactoryExtensions
    {
        public static List<Task> StartNew(this TaskFactory factory, Action action, int count, CancellationToken token)
        {
            var tasks = new List<Task>();

            for (int i = 0; i < count; i++)
            {
                var task = factory.StartNew(action, token);
                tasks.Add(task);
            }

            return tasks;
        }
    }
}
