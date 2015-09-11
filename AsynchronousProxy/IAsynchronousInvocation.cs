using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy
{
	public interface IAsynchronousInvocation
	{
		Type TargetType { get; }
		MethodInfo Method { get; }
		object[] Arguments { get; }
	}
}
