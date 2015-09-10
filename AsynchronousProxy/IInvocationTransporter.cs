using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy
{
	public interface IInvocationTransporter
	{
		void SendInvocation(IInvocation invocation);
	}
}
