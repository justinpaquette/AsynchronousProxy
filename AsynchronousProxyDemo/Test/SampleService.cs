using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxyDemo.Test
{
	public class SampleService : ISampleService
	{
		public void Test()
		{
			Console.WriteLine("hello");
		}

		public async Task TestTask()
		{
			Console.WriteLine("hellotask");
		}
	}
}
