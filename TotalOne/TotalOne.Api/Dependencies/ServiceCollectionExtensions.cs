using FluentValidation;

using MediatR;

using Polly;

using TotalOne.Api.Mutations;
using TotalOne.Api.Queries;
using TotalOne.Application;
using TotalOne.Application.Behaviors;
using TotalOne.Application.Services;
using TotalOne.Infrastructure;
using TotalOne.Infrastructure.Http;

namespace TotalOne.Api.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTotalOneServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<ITotalOneContext, TotalOneContext>()
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(ApplicationAssembly.Get()))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddValidatorsFromAssembly(ApplicationAssembly.Get(), includeInternalTypes: true)
            ;
    }

    public static IServiceCollection AddExternalServices(this IServiceCollection services, string arthouseBaseUri)
    {

        services
            .AddHttpClient<IArthouseApiHttpService, ArthouseApiHttpService>(client =>
            {
                client.BaseAddress = new Uri(arthouseBaseUri);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));

        return services;
    }

    public static IServiceCollection AddGraphQlQueriesAndMutations(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<PingQuery>()
            .AddTypeExtension<BusinessPartnerQuery>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<BusinessPartnerMutation>();

        return services;
    }


}
