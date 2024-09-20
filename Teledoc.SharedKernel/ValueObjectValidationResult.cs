using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.SharedKernel
{
	public class ValueObjectValidationResult
	{
		public IValueObject ValueObject { get; private set; }
		public List<string> BusinessErrors { get; private set; }

		public ValueObjectValidationResult(IValueObject valueObject, List<string> businessErrors)
		{
			ValueObject = valueObject;
			BusinessErrors = businessErrors;
		}
	}
}
