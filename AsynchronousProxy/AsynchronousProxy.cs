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
		private IInvocationTransporter _transporter;

		public AsynchronousProxy(IInvocationTransporter transporter)
		{
			_transporter = transporter;

			var interceptor = new AsynchronousInterceptor(invocation => _transporter.SendInvocation(invocation));

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
}
