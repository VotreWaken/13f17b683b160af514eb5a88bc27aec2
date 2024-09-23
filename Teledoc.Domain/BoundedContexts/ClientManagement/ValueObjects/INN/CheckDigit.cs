namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.INN
{
    public class CheckDigit
    {
        public string Value { get; }

		public CheckDigit() { }
		public CheckDigit(string value)
        {
            if (value.Length != 2 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid check digit format", nameof(value));

            Value = value;
        }
    }
}
