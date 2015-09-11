﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProxy
{
	public class AysnchronousProxyTarget<T>
	{
		private readonly T _target;
		private readonly IInvocationReceiver _receiver;

		public AysnchronousProxyTarget(T target, IInvocationReceiver receiver)
		{
			_target = target;
			_receiver = receiver;
		}

		public async Task Start()
		{
			while(true)
			{
				
			}
		}
	}
}