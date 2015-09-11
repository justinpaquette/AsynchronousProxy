using AsynchronousProxy;
using AsynchronousProxy.Invocations;
using AsynchronousProxy.Receivers;
using AsynchronousProxy.Transporters;
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
			var queue = new ConcurrentQueue<AsynchronousInvocation>();

			Task.WaitAll(new[]
			{
				Invoker(queue),
				Receiver(queue)
			});
		}

		public static async Task Invoker(ConcurrentQueue<AsynchronousInvocation> queue)
		{
			var proxy = new AsynchronousProxy<ISampleService>(invocation =>
			{
				queue.Enqueue(invocation);
			});

			while(true)
			{
				proxy.Object.Test();

				await Task.Delay(TimeSpan.FromSeconds(2));
			}
		}

		public static async Task Receiver(ConcurrentQueue<AsynchronousInvocation> queue)
		{
			var service = new SampleService();

			var receiver = new AysnchronousProxyTarget<ISampleService>(
				service,
				() => Task.Run(() =>
				{
					while (true)
					{
						var invocation = default(AsynchronousInvocation);
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
