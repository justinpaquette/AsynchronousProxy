using AsynchronousProxy;
using AsynchronousProxy.Receivers;
using AsynchronousProxy.Transporters;
using AsynchronousProxyDemo.Test;
using Castle.DynamicProxy;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxyDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			var container = new UnityContainer();
			container.RegisterType<ISampleService, SampleService>();

			var queue = new Queue<IAsynchronousInvocation>();

			var transporter = new MemoryQueueInvocationTransporter(queue);
			var proxy = new AsynchronousProxy<ISampleService>(transporter);

			Task.WaitAll(new[]
			{
				CreateInvocations(proxy.Object),
				ProcessQueue(queue, container)
			});
		}

		public static async Task ProcessQueue(Queue<IAsynchronousInvocation> queue, UnityContainer container)
		{
			var receiver = new InvocationReceiver(container);

			while(true)
			{
				if(queue.Count > 0)
				{
					var invocation = queue.Dequeue();

					if(invocation != null)
					{
						receiver.ReceiveInvocation(invocation);
					}
				}
				else
				{
					await Task.Delay(TimeSpan.FromMilliseconds(500));
				}
			}
		}

		public static async Task CreateInvocations(ISampleService service)
		{
			while(true)
			{
				service.Test();
				await Task.Delay(TimeSpan.FromSeconds(1));
			}
		}
	}
}
