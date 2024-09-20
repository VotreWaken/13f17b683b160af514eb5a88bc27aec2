using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.SharedKernel
{
	abstract public class AggregateRoot
	{
		public int AggregateId { get; protected set; } = new int();

		protected List<string> _businessLogicErrors = new List<string>();

		protected abstract void When(IDomainEvent @event);

		public void RaiseEvent(IDomainEvent @event)
		{
			When(@event);
		}

		public IValueObject CheckAndAssign(ValueObjectValidationResult validatedValueObject)
		{
			if (validatedValueObject?.ValueObject != null)
			{
				return validatedValueObject.ValueObject;
			}
			else
			{
				if (validatedValueObject?.BusinessErrors?.Any() == true)
					_businessLogicErrors.AddRange(validatedValueObject.BusinessErrors);

				return null;
			}
		}
	}
}

