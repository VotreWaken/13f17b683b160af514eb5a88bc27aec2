using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
using Teledoc.Domain.BoundedContexts.ClientManagement.Exceptions;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.ClientType.Enums;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.INN;
using Teledoc.SharedKernel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.ClientType
{

    public class ClientType : ValueObject
	{
        public ClientTypeEnum Value { get ; private set; }
        public string Name { get; private set; }
		public int ClientTypeIdValue { get { return (int)Value; } set { ClientTypeIdValue = (int)Value; } }

		public ClientType(ClientTypeEnum value)
		{
			Value = value;
		}

		public ClientType()
		{
		}

		private ClientType(ClientTypeEnum value, string description)
        {
			Value = value;
			Name = description;
        }

        public static ClientType LegalEntity => new ClientType(ClientTypeEnum.LegalEntity, "Legal Entity");
        public static ClientType IndividualEntrepreneur => new ClientType(ClientTypeEnum.IndividualEntrepreneur, "Individual Entrepreneur");
		public static ClientType FromEnum(ClientTypeEnum type)
        {
			return type switch
            {
                ClientTypeEnum.LegalEntity => LegalEntity,
                ClientTypeEnum.IndividualEntrepreneur => IndividualEntrepreneur,
                _ => throw new ArgumentException("Invalid client type", nameof(type))
            };
        }
		public static ClientType FromString(string type)
        {
			List<string> errors = new();

			if (string.IsNullOrEmpty(type))
			{
				errors.Add("Invalid Client Type format. Client Type must be LegalEntity or IndividualEntrepreneur");
			}

			if (errors.Any())
				throw new ArgumentException(null, nameof(type));

			return type switch
            {
                "LegalEntity" => LegalEntity,
                "IndividualEntrepreneur" => IndividualEntrepreneur,
                _ => throw new DomainException("Invalid client type string")
            };
        }

		public override string ToString()
		{
			return Value switch
			{
				ClientTypeEnum.LegalEntity => "LegalEntity",
				ClientTypeEnum.IndividualEntrepreneur => "IndividualEntrepreneur",
				_ => throw new DomainException("Invalid client type")
			};
		}

		public static ValueObjectValidationResult ValidateFoundersForClientType(ClientType clientType, IEnumerable<Founder> founders)
		{
			List<string> businessLogicErrors = new();
			if (clientType.Value == ClientTypeEnum.IndividualEntrepreneur && founders.Count() > 1)
			{
				businessLogicErrors.Add("Individual Entrepreneur cannot have more than one founder.");
				throw new DomainException("Individual Entrepreneur cannot have more than one founder.");
			}

			if (clientType.Value == ClientTypeEnum.LegalEntity && !founders.Any())
			{
				businessLogicErrors.Add("Legal Entity must have at least one founder.");
				throw new DomainException("Legal Entity must have at least one founder.");
			}
			return new ValueObjectValidationResult(new ClientType(clientType.Value, clientType.Name), null);
		}
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
			yield return Name;
		}

		public static ClientType FromValue(int value)
        {
            ClientTypeEnum val = (ClientTypeEnum)value;
            return FromEnum((ClientTypeEnum)value);
        }
    }
}
