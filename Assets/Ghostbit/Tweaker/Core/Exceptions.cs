﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;

namespace Ghostbit.Tweaker.Core
{
	public class NameAlreadyRegisteredException : Exception, ISerializable
	{
		public NameAlreadyRegisteredException(string name)
			: base("The name '" + name + "' is already registered.")
		{
		}
	}

	public class InstanceAlreadyRegisteredException : Exception, ISerializable
	{
		public InstanceAlreadyRegisteredException(ITweakerObject obj)
			: base("The instance of type '" + obj.GetType().Name + "' with name '" + obj.Name + "' is already registered.")
		{
		}
	}

	public class NotFoundException : Exception, ISerializable
	{
		public NotFoundException(string name)
			: base("The name '" + name + "' is not currently in use.")
		{
		}
	}

	public class InvokeException : Exception, ISerializable
	{
		public InvokeException(string name, object[] args, Exception inner)
			: base("Invocation of '" + name + "(" + args + ")' failed. See inner exception.", inner)
		{
		}
	}

	// TODO: does .NET include this functionality somewhere?
	internal class Printer
	{
		public static string PrintObjectArray(object[] objects)
		{
			if(objects == null || objects.Length == 0)
			{
				return "";
			}


			object obj = objects[0];
			StringBuilder str = new StringBuilder(objects[0] != null ? objects[0].ToString() : "");
			for(var i = 1; i < objects.Length; ++i)
			{
				str.Append(",");
				str.Append(objects[i] != null ? objects[i].ToString() : "");
			}
			return str.ToString();
		}

		public static string PrintTypeArray(Type[] types)
		{
			if (types == null || types.Length == 0)
			{
				return "";
			}

			StringBuilder str = new StringBuilder(types[0] != null ? types[0].FullName : "");
			for (var i = 1; i < types.Length; ++i)
			{
				str.Append(",");
				str.Append(types[i] != null ? types[i].FullName : "");
			}
			return str.ToString();
		}
	}

	public class InvokeArgNumberException : Exception, ISerializable
	{
		public InvokeArgNumberException(string name, object[] args, Type[] expectedArgTypes)
			: base(string.Format("Invokation of '{0}'){1}) failed. {2} args were provided but {3} args were expected. The expected types are: [{4}]",
			name, Printer.PrintObjectArray(args), args.Length, expectedArgTypes.Length, Printer.PrintTypeArray(expectedArgTypes)))
		{
		}
	}

	public class InvokeArgTypeException : Exception, ISerializable
	{
		public InvokeArgTypeException(string name, object[] args, Type[] argTypes, Type[] expectedArgTypes, string extraMessage = "")
			: base(string.Format("Invokation of '{0}({1})' failed. The expected arg types are [{2}] but [{3}] was provided. {4}",
			name, Printer.PrintObjectArray(args), Printer.PrintTypeArray(expectedArgTypes), Printer.PrintTypeArray(argTypes), extraMessage))
		{

		}
	}

	public class TweakerObjectInvalidException : Exception, ISerializable
	{
		public TweakerObjectInvalidException(ITweakerObject obj)
			: base("The invokable named '" + obj.Name + "' is no longer valid and should be unregistered or uncached.")
		{
		}
	}

	public class TweakableSetException : Exception, ISerializable
	{
		public TweakableSetException(string name, object value, Exception inner)
			: base("Failed to set tweakable '" + name + "' to value '" + value.ToString() + "'. See inner exception.", inner)
		{
		}

		public TweakableSetException(string name, string message)
			: base("Failed to set tweakable '" + name + "'. " + message)
		{
		}
	}

	public class TweakableGetException : Exception, ISerializable
	{
		public TweakableGetException(string name, Exception inner)
			: base("Failed to get tweakable '" + name + "'. See inner exception.", inner)
		{
		}

		public TweakableGetException(string name, string message)
			: base("Failed to get tweakable '" + name + "'. " + message)
		{
		}
	}

	public class StepTweakableInvalidException : Exception, ISerializable
	{
		public StepTweakableInvalidException(string name, string message)
			: base("The step tweakable named '" + name + "' is invalid: " + message)
		{

		}

		public StepTweakableInvalidException(string name, string message, Exception inner)
			: base("The step tweakable named '" + name + "' is invalid: " + message, inner)
		{

		}
	}

	public class ProcessorException : Exception, ISerializable
	{
		public ProcessorException(string msg)
			: base(msg)
		{
		}
	}
}