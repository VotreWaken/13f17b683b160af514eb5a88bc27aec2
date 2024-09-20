using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.SharedKernel
{
	internal interface IAggregateRoot
	{
		public int AggregateId { get; }
	}
}
