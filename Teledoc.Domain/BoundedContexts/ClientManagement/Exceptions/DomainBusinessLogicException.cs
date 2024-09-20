using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions
{
	class DomainBusinessLogicException : DomainException
	{
		public DomainBusinessLogicException(IEnumerable<string> errors) : base(errors?.Any() == true ? string.Join(", ", errors) : "Invalid data provided.") { }
	}
}
