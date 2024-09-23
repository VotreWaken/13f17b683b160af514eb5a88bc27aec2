using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Application.QueryObjects;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.ClientType;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;

namespace Teledoc.Application.Mapper
{
	public static class ClientEntityToDTOMapper
	{
		public static void RegisterMappings()
		{
			// INN -> string
			 TypeAdapterConfig<INN, string>.NewConfig()
				.MapWith(src => src.Value != null ? src.Value : string.Empty);

			// ClientType -> string
			TypeAdapterConfig<ClientType, string>.NewConfig()
				.MapWith(src => src != null ? src.Name : string.Empty);

			// Founder -> FounderInfo
			TypeAdapterConfig<Founder, FounderInfo>.NewConfig()
				.Map(dest => dest.INN, src => src.INN.Value != null ? src.INN.Value.ToString() : string.Empty)
				.Map(dest => dest.Name, src => src.FullName.Value)
				.Map(dest => dest.CreatedAt, src => src.CreatedAt.ToShortDateString())
				.Map(dest => dest.UpdatedAt, src => src.UpdatedAt.ToShortDateString());

			// Client -> ClientInfo
			TypeAdapterConfig<Client, ClientInfo>.NewConfig()
				.Map(dest => dest.INN, src => src.INN.Value != null ? src.INN.Value.ToString() : string.Empty)
				.Map(dest => dest.ClientType, src => ClientType.FromValue(src.ClientTypeId).ToString())
				.Map(dest => dest.CreatedAt, src => src.CreatedAt.ToShortDateString())
				.Map(dest => dest.UpdatedAt, src => src.UpdatedAt.ToShortDateString())
				.Map(dest => dest.Founders, src => src.Founders.Adapt<List<FounderInfo>>());
		}
	}
}
