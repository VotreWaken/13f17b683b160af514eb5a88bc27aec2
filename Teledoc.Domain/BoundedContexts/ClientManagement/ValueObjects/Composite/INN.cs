using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite
{
    public class INN : ValueObject
	{

        public SubjectCode SubjectCode { get; }
        public TaxOfficeCode TaxOfficeCode { get; }
        public SerialNumber SerialNumber { get; }
        public CheckDigit CheckDigit { get; }

        private INN(SubjectCode subjectCode, TaxOfficeCode taxOfficeCode, SerialNumber serialNumber, CheckDigit checkDigit)
        {
            SubjectCode = subjectCode;
            TaxOfficeCode = taxOfficeCode;
            SerialNumber = serialNumber;
            CheckDigit = checkDigit;
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

			return new ValueObjectValidationResult(new INN(subjectCode, taxOfficeCode, serialNumber, checkDigit), null);
		}

		public override string ToString() => $"{SubjectCode.Value}{TaxOfficeCode.Value}{SerialNumber.Value}{CheckDigit.Value}";

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return SubjectCode;
			yield return TaxOfficeCode;
			yield return SerialNumber;
			yield return CheckDigit;
		}
	}

}
