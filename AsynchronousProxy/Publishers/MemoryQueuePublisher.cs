using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy.Publishers
{
	public class MemoryQueuePublisher : IInvocationPublisher
	{
		private readonly ConcurrentQueue<IAsynchronousInvocation> _queue;

		public MemoryQueuePublisher(ConcurrentQueue<IAsynchronousInvocation> queue)
		{
			_queue = queue;
		}

		public async Task Publish(IAsynchronousInvocation invocation)
		{
			await Task.Run(() => _queue.Enqueue(invocation));
		}
	}
}
