using Castle.DynamicProxy;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy.Receivers
{
	public class MemoryQueueInvocationReceiver : IInvocationReceiver
	{
		private readonly Queue<IInvocation> _queue;
		private UnityContainer _container;

		public MemoryQueueInvocationReceiver(Queue<IInvocation> queue, UnityContainer container)
		{
			_queue = queue;
			_container = container;
		}

		public void ReceiveInvocation(Castle.DynamicProxy.IInvocation invocation)
		{
			var target = _container.Resolve(invocation.TargetType);
			
			//Invoke invocation on target
		}
	}
}
