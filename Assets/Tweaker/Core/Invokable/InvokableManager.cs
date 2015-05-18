﻿using System.Collections.Generic;
using System;
using Tweaker.AssemblyScanner;
using System.Reflection;
using System.Diagnostics;

namespace Tweaker.Core
{
	public class InvokableManager :
		IInvokableManager
	{
		private BaseTweakerManager<IInvokable> baseManager;
		public InvokableManager(IScanner scanner)
		{
			baseManager = new BaseTweakerManager<IInvokable>(scanner);
			if (scanner != null)
			{
				scanner.AddProcessor(new InvokableProcessor());
			}
		}

		public IInvokable RegisterInvokable(InvokableInfo info, Delegate del)
		{
			var invokable = InvokableFactory.MakeInvokable(info, del);
			RegisterInvokable(invokable);
			return invokable;
		}

		public IInvokable RegisterInvokable(InvokableInfo info, MethodInfo methodInfo, object instance = null)
		{
			var invokable = InvokableFactory.MakeInvokable(info, methodInfo, instance);
			RegisterInvokable(invokable);
			return invokable;
		}

		public IInvokable RegisterInvokable(InvokableInfo info, EventInfo eventInfo, object instance = null)
		{
			var invokable = InvokableFactory.MakeInvokable(info, eventInfo, instance);
			RegisterInvokable(invokable);
			return invokable;
		}

		public void RegisterInvokable(IInvokable invokable)
		{
			// Transfer ownership from previous manager to this manager.
			if (invokable.Manager != null && invokable.Manager != this)
			{
				invokable.Manager.UnregisterInvokable(invokable);
			}
			invokable.Manager = this;

			baseManager.RegisterObject(invokable);
		}

		public void UnregisterInvokable(IInvokable invokable)
		{
			baseManager.UnregisterObject(invokable);
		}

		public void UnregisterInvokable(string name)
		{
			baseManager.UnregisterObject(name);
		}

		public TweakerDictionary<IInvokable> GetInvokables(SearchOptions options = null)
		{
			return baseManager.GetObjects(options);
		}

		public IInvokable GetInvokable(SearchOptions options = null)
		{
			return baseManager.GetObject(options);
		}

		public IInvokable GetInvokable(string name)
		{
			return baseManager.GetObject(name);
		}

		public object Invoke(IInvokable invokable, params object[] args)
		{
			return invokable.Invoke(args);
		}

		public object Invoke(string name, params object[] args)
		{
			IInvokable invokable = baseManager.GetObject(name);
			if (invokable == null)
			{
				throw new NotFoundException(name);
			}

			return Invoke(invokable, args);
		}
	}
}