using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic
{
    public class CheckDigit
    {
        public string Value { get; }

        public CheckDigit(string value)
        {
            if (value.Length != 2 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid check digit format", nameof(value));

            Value = value;
        }
    }
}
