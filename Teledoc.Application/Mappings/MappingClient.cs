using AutoMapper;
using Teledoc.Application.Commands;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Basic;
using Teledoc.Domain.BoundedContexts.ClientManagement.ValueObjects.Composite;
namespace Teledoc.Application.Mappings
{
	public class MappingClient : Profile
	{
		public MappingClient()
		{
			CreateMap<Client, Teledoc.Infrastructure.Entities.Client>()
				.ForMember(dest => dest.INN, opt => opt.MapFrom(src => src.INN.ToString()))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.ClientTypeId, opt => opt.MapFrom(src => (int)src.ClientType.Value))
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.ClientType, opt => opt.Ignore());

			CreateMap<Teledoc.Infrastructure.Entities.Client, Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates.Client>()
				.ForMember(dest => dest.INN, opt => opt.MapFrom(src => INN.Create(src.INN)))
				.ForMember(dest => dest.ClientType, opt => opt.MapFrom(src => ClientType.FromValue(src.ClientTypeId)))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

			CreateMap<FounderCreateCommand, Teledoc.Infrastructure.Entities.Founder>()
				.ForMember(dest => dest.INN, opt => opt.MapFrom(src => src.INN))
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
		}

	}
}
