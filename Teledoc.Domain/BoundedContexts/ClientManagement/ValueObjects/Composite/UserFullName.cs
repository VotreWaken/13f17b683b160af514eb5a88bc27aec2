using Teledoc.SharedKernel;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite
{
    public class UserFullName : ValueObject
    {
        public string First { get; private set; }
        public string Last { get; private set; }
		public string Patronymic { get; private set; }

		private UserFullName()
        { }

        private UserFullName(string first, string last, string patronymic)
        {
            First = first;
            Last = last;
			Patronymic = patronymic;

		}

        public static ValueObjectValidationResult Create(string first, string last, string patronymic)
        {
            List<string> businessLogicErrors = new();

            if (string.IsNullOrWhiteSpace(first)) businessLogicErrors.Add("First name is invalid.");
            if (string.IsNullOrWhiteSpace(last)) businessLogicErrors.Add("Last name is invalid.");
            if (string.IsNullOrWhiteSpace(last)) businessLogicErrors.Add("Patronymic is invalid.");

			if (businessLogicErrors?.Any() == true)
                return new ValueObjectValidationResult(null, businessLogicErrors);

            return new ValueObjectValidationResult(new UserFullName(first, last, patronymic), null);
        }

        public override string ToString() => $"{First} {Last} {Patronymic}";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
            yield return Patronymic;
		}
    }
}
