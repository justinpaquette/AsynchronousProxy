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
		private readonly Queue<IInvocation> _queue;

		public MemoryQueueInvocationTransporter(Queue<IInvocation> queue)
		{
			_queue = queue;
		}

		public void SendInvocation(IInvocation invocation)
		{
			_queue.Enqueue(invocation);
		}
	}
}
