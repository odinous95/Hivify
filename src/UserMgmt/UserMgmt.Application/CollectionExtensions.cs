using BuildingBlocks.ApplicationPorts.Messeging;
using Microsoft.Extensions.DependencyInjection;
using UserMgmt.Application.Commands;
using UserMgmt.Application.DTOs;
using UserMgmt.Application.Quries;

namespace UserMgmt.Application;


public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddUserMgmtServices()
        {


            services.AddScoped<IQueryHandler<GetUsersQuery, IReadOnlyList<UserListItem>>, GetUsersQueryHandler>();
            services.AddScoped<ICommandHandler<RegisterUserCommand, Guid>, RegisterUserCommandHandler>();

            return services;
        }
    }
}