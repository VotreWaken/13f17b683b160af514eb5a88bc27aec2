using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Application.QueryObjects;
using Teledoc.Infrastructure.Entities;

namespace Teledoc.Application.Mappings
{
	public class ClientEntityToDTOMapper
	{
		public static ClientInfo ToDto(Client client)
		{
			return new ClientInfo
			{
				Id = client.Id,
				INN = client.INN.ToString(),
				Name = client.Name,
				ClientType = client.ClientTypeId.ToString(),
				CreatedAt = client.CreatedAt.ToShortDateString(),
				UpdatedAt = client.UpdatedAt.ToShortDateString(),
				Founders = client.Founders.Select(f => new FounderInfo
				{
					Id = f.Id,
					Name = f.FullName,
					INN = f.INN.ToString(),
					UpdatedAt = f.UpdatedAt.ToShortDateString(),
					CreatedAt = f.CreatedAt.ToShortDateString(),
				}).ToList()
			};
		}
	}
}
