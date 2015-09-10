using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy
{
	public interface IInvocationReceiver
	{
		void ReceiveInvocation(IInvocation invocation);
	}
}
