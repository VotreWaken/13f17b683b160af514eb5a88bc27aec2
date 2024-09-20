namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic
{
    public class TaxOfficeCode
    {
        public string Value { get; }

        public TaxOfficeCode(string value)
        {
            if (value.Length != 2 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid tax office code format", nameof(value));

            Value = value;
        }
    }
}
