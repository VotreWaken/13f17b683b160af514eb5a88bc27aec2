using System.Collections.Generic;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.INN;
using Teledoc.SharedKernel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite
{
    public class INN : ValueObject
	{

        public SubjectCode SubjectCode { get; private set; } = default!;
		public TaxOfficeCode TaxOfficeCode { get; private set; } = default!;
		public SerialNumber SerialNumber { get; private set; } = default!;
		public CheckDigit CheckDigit { get; private set; } = default!;
		public string Value { get; private set; }

        private INN(SubjectCode subjectCode, TaxOfficeCode taxOfficeCode, SerialNumber serialNumber, CheckDigit checkDigit)
        {
            SubjectCode = subjectCode;
            TaxOfficeCode = taxOfficeCode;
            SerialNumber = serialNumber;
            CheckDigit = checkDigit;

			Value = $"{SubjectCode.Value}{TaxOfficeCode.Value}{SerialNumber.Value}{CheckDigit.Value}";
		}
		
		public INN()
		{

		}

		public INN(string value)
		{
			Value = value;

			SubjectCode = new SubjectCode(value.Substring(0, 2));
			TaxOfficeCode = new TaxOfficeCode(value.Substring(2, 2));
			SerialNumber = new SerialNumber(value.Substring(4, 6));
			CheckDigit = new CheckDigit(value.Substring(10, 2));
		}

		public static ValueObjectValidationResult Create(string inn)
		{
			List<string> errors = new();

			if (inn.Length != 12 || !inn.All(char.IsDigit))
			{
				errors.Add("Invalid INN format. INN must be 12 digits long.");
			}

			if (errors.Any())
				return new ValueObjectValidationResult(null, errors);

			var subjectCode = new SubjectCode(inn.Substring(0, 2));
			var taxOfficeCode = new TaxOfficeCode(inn.Substring(2, 2));
			var serialNumber = new SerialNumber(inn.Substring(4, 6));
			var checkDigit = new CheckDigit(inn.Substring(10, 2));

			return new ValueObjectValidationResult(new INN(subjectCode, taxOfficeCode, serialNumber, checkDigit), errors);
		}

		public override string ToString() => $"{SubjectCode?.Value}{TaxOfficeCode?.Value}{SerialNumber?.Value}{CheckDigit?.Value}";

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return SubjectCode;
			yield return TaxOfficeCode;
			yield return SerialNumber;
			yield return CheckDigit;
		}
	}

}
