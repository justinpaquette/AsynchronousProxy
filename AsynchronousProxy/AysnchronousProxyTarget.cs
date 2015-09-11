using AsynchronousProxy.Invocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy
{
	public class AysnchronousProxyTarget<T>
	{
		private readonly T _target;
		private readonly Func<Task<IAsynchronousInvocation>> _receiver;

		public AysnchronousProxyTarget(T target, Func<Task<IAsynchronousInvocation>> receiver)
		{
			_target = target;
			_receiver = receiver;
		}

		public async Task Start()
		{
			while(true)
			{
				var invocation = await _receiver();

				invocation.Method.Invoke(_target, invocation.Arguments);
			}
		}
	}
}
