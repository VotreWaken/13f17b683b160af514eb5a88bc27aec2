using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite
{
    public class JuristicINN : ValueObject
	{
        public SubjectCode SubjectCode { get; }
        public TaxOfficeCode TaxOfficeCode { get; }
        public SerialNumber SerialNumber { get; }
        public CheckDigit CheckDigit { get; }

        private JuristicINN(SubjectCode subjectCode, TaxOfficeCode taxOfficeCode, SerialNumber serialNumber, CheckDigit checkDigit)
        {
            SubjectCode = subjectCode;
            TaxOfficeCode = taxOfficeCode;
            SerialNumber = serialNumber;
            CheckDigit = checkDigit;
        }

		public static ValueObjectValidationResult Create(string inn)
		{
			List<string> errors = new();

			if (inn.Length != 10 || !inn.All(char.IsDigit))
			{
				errors.Add("Invalid Legal INN format. INN must be 10 digits long.");
			}

			if (errors.Any())
				return new ValueObjectValidationResult(null, errors);

			var subjectCode = new SubjectCode(inn.Substring(0, 2));
			var taxOfficeCode = new TaxOfficeCode(inn.Substring(2, 2));
			var serialNumber = new SerialNumber(inn.Substring(4, 5));
			var checkDigit = new CheckDigit(inn.Substring(9, 1));

			return new ValueObjectValidationResult(new JuristicINN(subjectCode, taxOfficeCode, serialNumber, checkDigit), null);
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return SubjectCode;
			yield return TaxOfficeCode;
			yield return SerialNumber;
			yield return CheckDigit;
		}
	}
}
