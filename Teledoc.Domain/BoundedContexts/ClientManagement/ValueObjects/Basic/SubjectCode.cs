namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic
{
    public class SubjectCode
    {
        public string Value { get; }

        public SubjectCode(string value)
        {
            if (value.Length != 2 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid subject code format", nameof(value));

            Value = value;
        }
    }
}
