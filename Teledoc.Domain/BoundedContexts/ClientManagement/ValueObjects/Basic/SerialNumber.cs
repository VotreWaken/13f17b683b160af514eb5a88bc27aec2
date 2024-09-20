namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic
{
    public class SerialNumber
    {
        public string Value { get; }

        public SerialNumber(string value)
        {
            if (value.Length != 6 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid serial number format", nameof(value));

            Value = value;
        }
    }
}
