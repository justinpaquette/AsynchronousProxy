﻿using AsynchronousProxy.Invocations;
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

		public AsynchronousProxy(Action<AsynchronousInvocation> onInvocation)
		{
			var interceptor = new AsynchronousInterceptor(invocation => 
				onInvocation(invocation.ToAsynchronousInvocation(typeof(T)))
			);

			var generator = new ProxyGenerator();
			Object = generator.CreateInterfaceProxyWithoutTarget<T>(interceptor);
		}

		public AsynchronousProxy(IInvocationPublisher publisher)
		{
			var interceptor = new AsynchronousInterceptor(invocation => 
				publisher.Publish(invocation.ToAsynchronousInvocation(typeof(T)))
			);

			var generator = new ProxyGenerator();
			Object = generator.CreateInterfaceProxyWithoutTarget<T>(interceptor);
		}

		public T Object { get; set; }
	}

	internal class AsynchronousInterceptor : IInterceptor
	{
		private readonly Action<IInvocation> _onIntercept;

		public AsynchronousInterceptor(Action<IInvocation> onIntercept)
		{
			_onIntercept = onIntercept;
		}

		public void Intercept(IInvocation invocation)
		{
			_onIntercept(invocation);
		}
	}

	public static class IInvocationExtensions
	{
		public static AsynchronousInvocation ToAsynchronousInvocation(this IInvocation invocation, Type type)
		{
			return new AsynchronousInvocation()
			{
				TargetType = type,
				Method = invocation.Method,
				Arguments = invocation.Arguments
			};
		}
	}
}
