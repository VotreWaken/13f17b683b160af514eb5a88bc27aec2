﻿namespace Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions
{
	class DomainBusinessLogicException : DomainException
	{
		public DomainBusinessLogicException(IEnumerable<string> errors) : base(errors?.Any() == true ? string.Join(", ", errors) : "Invalid data provided.") { }
	}
}
