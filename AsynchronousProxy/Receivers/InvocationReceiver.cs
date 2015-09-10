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

		public void ReceiveInvocation(Castle.DynamicProxy.IInvocation invocation)
		{
			var target = _container.Resolve(invocation.TargetType);
			
			Invoke(invocation, target); //Something like this
		}
	}
}
