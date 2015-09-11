using AsynchronousProxy;
using AsynchronousProxy.Invocations;
using AsynchronousProxy.Publishers;
using AsynchronousProxy.Receivers;
using AsynchronousProxyDemo.Test;
using Castle.DynamicProxy;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
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
			var queue = new ConcurrentQueue<IAsynchronousInvocation>();

			Task.WaitAll(new[]
			{
				Invoker(queue),
				Receiver(queue)
			});
		}

		public static async Task Invoker(ConcurrentQueue<IAsynchronousInvocation> queue)
		{
			var proxy = new AsynchronousProxy<ISampleService>(new MemoryQueuePublisher(queue));

			var service = proxy.Object;

			while(true)
			{
				await service.TestTask();
				await Task.Delay(TimeSpan.FromSeconds(1));
			}
		}

		public static async Task Receiver(ConcurrentQueue<IAsynchronousInvocation> queue)
		{
			var service = new SampleService();

			var receiver = new AysnchronousProxyTarget<ISampleService>(
				service,
				() => Task.Run(() =>
				{
					while (true)
					{
						var invocation = default(IAsynchronousInvocation);
						if (queue.TryDequeue(out invocation))
						{
							return invocation;
						}
					}
				})
			);

			await receiver.Start();
		}
	}
}
