﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.SharedKernel
{
	public interface IDomainEvent
	{
		int EventId { get; }
	}
}
