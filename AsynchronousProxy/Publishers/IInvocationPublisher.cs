using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy.Publishers
{
	public interface IInvocationPublisher
	{
		void Publish(IAsynchronousInvocation invocation);
	}
}
