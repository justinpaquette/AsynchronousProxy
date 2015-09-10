using AsynchronousProxy;
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
			var queue = new Queue<IInvocation>();

			var transporter = new MemoryQueueInvocationTransporter(queue);
			var proxy = new AsynchronousProxy<ISampleService>(transporter);

			proxy.Object.Test();


		}
	}
}
