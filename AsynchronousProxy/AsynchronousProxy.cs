using AsynchronousProxy.Invocations;
using AsynchronousProxy.Publishers;
using AsyncProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy
{
	public  class AsynchronousProxy<T> where T : class
	{
		private IInvocationPublisher _publisher;

		public AsynchronousProxy(Action<IAsynchronousInvocation> onInvocation)
		{
			Object = Proxy.CreateProxy<T>(async invocation =>
			{
				await Task.Delay(1);
				onInvocation(invocation.ToAsynchronousInvocation(typeof(T)));

				return default(T);
			});
		}

		public AsynchronousProxy(IInvocationPublisher publisher)
		{
			_publisher = publisher;

			Object = Proxy.CreateProxy<T>(async invocation =>
			{
				await _publisher.Publish(invocation.ToAsynchronousInvocation(typeof(T)));

				return default(T);
			});
		}

		public T Object { get; set; }
	}

	public static class IInvocationExtensions
	{
		public static AsynchronousInvocation ToAsynchronousInvocation(this Invocation invocation, Type type)
		{
			return new AsynchronousInvocation()
			{
				Arguments = invocation.Arguments,
				Method = invocation.Method,
				TargetType = type
			};
		}
	}
}
