﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ghostbit.Tweaker.Core
{
	/// <summary>
	/// Contains information about a tweaker object
	/// </summary>
	public class TweakerObjectInfo
	{
		/// <summary>
		/// All tweaker objects must have a unique name. This name is
		/// used to register with managers.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Optional description of this tweaker object.
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// A unique identifier for all tweakables belonging to the same instance.
		/// Zero if tweakable is a bound to a static member.
		/// </summary>
		public uint InstanceId { get; private set; }

		public TweakerObjectInfo(string name, uint instanceId = 0, string description = "")
		{
			Name = name;
			Description = description;
			InstanceId = instanceId;
		}
	}

	/// <summary>
	/// Base tweaker object class.
	/// </summary>
	public abstract class TweakerObject : ITweakerObject
	{
		protected TweakerObjectInfo Info { get; set; }

		protected readonly bool isPublic;
		protected readonly WeakReference instance;
		protected readonly Assembly assembly;

		private string shortName;

		/// <summary>
		/// The name that this tweaker object registers with.
		/// </summary>
		public string Name
		{
			get { return Info.Name; }
		}

		/// <summary>
		/// The name of this tweaker without groups included.
		/// </summary>
		public string ShortName 
		{ 
			get { return shortName; } 
		}

		/// <summary>
		/// Does this tweaker object bind to a public type of member?
		/// </summary>
		public bool IsPublic
		{
			get { return isPublic; }
		}

		/// <summary>
		///  The assembly of the type or member that this tweaker objects binds to.
		/// </summary>
		public Assembly Assembly
		{
			get { return assembly; }
		}

		/// <summary>
		/// The weak reference to the instance this tweaker object is bound to.
		/// Null if bound to a static tweaker object.
		/// </summary>
		public virtual WeakReference WeakInstance
		{
			get
			{
				return instance;
			}
		}

		/// <summary>
		/// The strong reference to the instance this tweaker object is bound to.
		/// Null if bound to a static tweaker object.
		/// </summary>
		public virtual object StrongInstance
		{
			get
			{
				if (WeakInstance == null)
				{
					return null;
				}

				object strongRef = null;
				instance.TryGetTarget(out strongRef);
				return strongRef;
			}
		}

		/// <summary>
		/// Indicates that the weak reference is still bound to a non-destroyed object and
		/// is in a valid state. All invalid tweaker object references should be nulled
		/// by objects holding a reference.
		/// </summary>
		public virtual bool IsValid
		{
			get
			{
				return WeakInstance == null || StrongInstance != null;
			}
		}

		public TweakerObject(TweakerObjectInfo info, Assembly assembly, WeakReference instance, bool isPublic)
		{
			Info = info;
			this.assembly = assembly;
			this.instance = instance;
			this.isPublic = isPublic;

			int index = Name.LastIndexOf('.');
			if(index < 0)
			{
				shortName = Name;
			}
			else
			{
				shortName = Name.Substring(index + 1);
			}
		}

		protected virtual bool CheckInstanceIsValid()
		{
			return IsValid;
		}
	}
}
