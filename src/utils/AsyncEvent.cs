using System;
using System.Threading.Tasks;

namespace TiercelFoundry.GDUtils
{
    public static class AsyncEvent
    {
        public static async Task AwaitAction(Action action)
        {
            var tcs = new TaskCompletionSource();
            action += () =>
            {
                tcs.SetResult();
            };
            await tcs.Task;
        }

        public static async Task Action(Func<Task> result)
        {
            await result();
        }
    }
}

