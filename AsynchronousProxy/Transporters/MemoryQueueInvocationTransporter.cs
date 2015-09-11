using Castle.DynamicProxy;
using Castle.DynamicProxy.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy.Transporters
{
	public class MemoryQueueInvocationTransporter : IInvocationTransporter
	{
		private readonly Queue<IAsynchronousInvocation> _queue;

		public MemoryQueueInvocationTransporter(Queue<IAsynchronousInvocation> queue)
		{
			_queue = queue;
		}

		public void SendInvocation(IAsynchronousInvocation invocation)
		{
			_queue.Enqueue(invocation);
		}
	}
}
