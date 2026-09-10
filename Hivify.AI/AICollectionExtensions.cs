using Hivify.AI.Agents;
using Hivify.AI.Services.FeedGenerator;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using OllamaSharp;





public static class AICollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddHivifyAIServices()
        {
            services.AddSingleton<IChatClient>(sp =>
            {
                return new OllamaApiClient(
                    new Uri("http://localhost:11434"),
                    "phi4-mini");
            });
            services.AddScoped<IHivifyAgent, HivifyAgent>();

            services.AddScoped<IFeedGenerator, FeedGenerator>();


            return services;
        }
    }
}


