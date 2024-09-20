using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			return type switch
			{
				ClientTypeEnum.LegalEntity => LegalEntity,
				ClientTypeEnum.IndividualEntrepreneur => IndividualEntrepreneur,
				_ => throw new ArgumentException("Invalid client type", nameof(type))
			};
		}

		public static ClientType FromValue(int value)
		{
			return FromEnum((ClientTypeEnum)value);
		}

		// Optionally: Override Equals and GetHashCode for proper equality checks
	}
}
