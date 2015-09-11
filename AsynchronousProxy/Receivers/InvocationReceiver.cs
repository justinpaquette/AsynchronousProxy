using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy.Receivers
{
	public class InvocationReceiver : IInvocationReceiver
	{
		private readonly UnityContainer _container;

		public InvocationReceiver(UnityContainer container)
		{
			_container = container;
		}

		public async Task ReceiveInvocation(IAsynchronousInvocation invocation)
		{
			invocation.Invoke(_container);
		}
	}

	public static class IAsynchronousInvocationExtensions
	{
		public static void Invoke(this IAsynchronousInvocation invocation, UnityContainer container)
		{
			var target = container.Resolve(invocation.TargetType);

			invocation.Method.Invoke(target, invocation.Arguments);
		}
	}
}
