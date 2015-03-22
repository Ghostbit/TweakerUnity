﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghostbit.Tweaker.Core
{
	public interface ITweakerFactory
	{
		T Create<T>(params object[] constructorArgs);
	}
}
