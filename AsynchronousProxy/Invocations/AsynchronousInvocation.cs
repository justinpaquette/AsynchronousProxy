using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy.Invocations
{
	public class AsynchronousInvocation : IAsynchronousInvocation
	{
		public Type TargetType { get; set; }
		public MethodInfo Method { get; set; }
		public object[] Arguments { get; set; }
	}
}
