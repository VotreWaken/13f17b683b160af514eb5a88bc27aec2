using Teledoc.Infrastructure.Entities;

namespace Teledoc.Application
{
	public static class ClientTypeMapper
	{
		public static ClientType ToEntity(Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic.ClientTypeEnum clientTypeEnum)
		{
			return new ClientType
			{
				Id = (int)clientTypeEnum,
				Name = clientTypeEnum.ToString()
			};
		}
		public static Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic.ClientTypeEnum ToEnum(ClientType clientTypeEntity)
		{
			if (Enum.IsDefined(typeof(Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic.ClientTypeEnum), clientTypeEntity.Id))
			{
				return (Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic.ClientTypeEnum)clientTypeEntity.Id;
			}
			throw new ArgumentException("Invalid ID for ClientTypeEnum", nameof(clientTypeEntity));
		}
	}
}