using Autofac;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Repository;
using Nibo.SpaceJam.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.WebAPI
{
    /// <summary>
    /// Dependency injection mapper for reposity
    /// </summary>
    public class RepositoryMappings : Module
    {
        /// <summary>
        /// Load mappings
        /// </summary>
        /// <param name="builder">Container builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ICosmosWrapper>((context, cosmosWrapper) =>
            {
                var config = context.Resolve<IConfigurationRoot>();
                return new CosmosWrapper(new DocumentClient(new Uri(config["AzureCosmosDB:AccountName"]), config["AzureCosmosDB:AccountKey"]), config["AzureCosmosDB:DatabaseName"]);
            });

            builder.RegisterType<CosmosRepository<OperationLogModel>>().As<IRepository<OperationLogModel>>();
            builder.RegisterType<CosmosRepository<TeamModel>>().As<IRepository<TeamModel>>();
            builder.RegisterType<CosmosRepository<TournamentModel>>().As<IRepository<TournamentModel>>();
            builder.RegisterType<CosmosRepository<TournamentResultModel>>().As<IRepository<TournamentResultModel>>();
        }
    }
}