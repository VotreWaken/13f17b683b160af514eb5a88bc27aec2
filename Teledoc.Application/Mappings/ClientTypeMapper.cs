using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.Infrastructure.Entities;


namespace Teledoc.Application.Mappings
{
    public static class ClientTypeMapper
    {
        public static ClientType ToEntity(Domain.BoundedContexts.ClientManagement.ValueObjects.Basic.ClientTypeEnum clientTypeEnum)
        {
            return new ClientType
            {
                Id = (int)clientTypeEnum,
                Name = clientTypeEnum.ToString()
            };
        }
        public static Domain.BoundedContexts.ClientManagement.ValueObjects.Basic.ClientTypeEnum ToEnum(ClientType clientTypeEntity)
        {
            if (Enum.IsDefined(typeof(Domain.BoundedContexts.ClientManagement.ValueObjects.Basic.ClientTypeEnum), clientTypeEntity.Id))
            {
                return (Domain.BoundedContexts.ClientManagement.ValueObjects.Basic.ClientTypeEnum)clientTypeEntity.Id;
            }
            throw new ArgumentException("Invalid ID for ClientTypeEnum", nameof(clientTypeEntity));
        }
    }
}
