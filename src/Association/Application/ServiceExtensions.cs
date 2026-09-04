using Association.Application.Commands.AddAssociation;
using Association.Application.Commands.AddStaffMember;
using Association.Application.Commands.RemoveStaffMember;
using Association.Application.Commands.UpdateStaffMemberRole;
using Association.Application.Contracts;
using Association.Application.Queries.GetAssociation;
using Association.Application.Queries.GetAssociations;
using Association.Domain.Associations;
using Association.Domain.Members;
using BuildingBlocks.ApplicationPorts.Messeging;
using Microsoft.Extensions.DependencyInjection;

namespace Association.Application;

public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAssociationServices()
        {
            services.AddScoped<ICommandHandler<AddStaffMemberCommand, MemberID>, AddStaffMemberCommandHandler>();
            services.AddScoped<ICommandHandler<AddAssociationCommand, AssociationID>, CreateAssociationCommandHandler>();
            services.AddScoped<IQueryHandler<GetAssociationsQuery, IReadOnlyList<AssociationListItem>>, GetAssociationsQueryHandler>();
            services.AddScoped<IQueryHandler<GetAssociationQuery, AssociationListItem>, GetAssociationQueryHandler>();
            services.AddScoped<ICommandHandler<RemoveStaffMemberCommand, bool>, RemoveStaffMemberCommandHandler>();
            services.AddScoped<
    ICommandHandler<UpdateStaffMemberRoleCommand, bool>, UpdateStaffMemberRoleCommandHandler>();

            return services;
        }
    }
}





