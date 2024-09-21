namespace Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic
{
	public enum ClientTypeEnum
	{
		IndividualEntrepreneur = 1,
		LegalEntity = 2,
	}

	public class ClientType
	{
		public ClientTypeEnum Value { get; private set; }

		private ClientType(ClientTypeEnum value, string description)
		{
			Value = value;
		}

		public static ClientType LegalEntity => new ClientType(ClientTypeEnum.LegalEntity, "Legal Entity");
		public static ClientType IndividualEntrepreneur => new ClientType(ClientTypeEnum.IndividualEntrepreneur, "Individual Entrepreneur");

		public static ClientType FromEnum(ClientTypeEnum type)
		{
			Console.WriteLine($"Type: {type}");
			return type switch
			{
				ClientTypeEnum.LegalEntity => LegalEntity,
				ClientTypeEnum.IndividualEntrepreneur => IndividualEntrepreneur,
				_ => throw new ArgumentException("Invalid client type", nameof(type))
			};
		}

		public static ClientType FromString(string type)
		{
			Console.WriteLine($"From String: {type} ");

			if (string.IsNullOrEmpty(type))
			{
				throw new ArgumentException("Client type cannot be null or empty.");
			}

			return type switch
			{
				"LegalEntity" => LegalEntity,
				"IndividualEntrepreneur" => IndividualEntrepreneur,
				_ => throw new ArgumentException("Invalid client type string", nameof(type))
			};
		}

		public static ClientType FromValue(int value)
		{
			Console.WriteLine($"Mapping ClientTypeEnum: {value}");
			ClientTypeEnum val = (ClientTypeEnum)value;
			Console.WriteLine($"Val: {val}");
			return FromEnum((ClientTypeEnum)value);
		}

		// Optionally: Override Equals and GetHashCode for proper equality checks
	}
}
