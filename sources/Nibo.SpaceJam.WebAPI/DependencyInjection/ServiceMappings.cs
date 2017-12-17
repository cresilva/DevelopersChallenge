using Autofac;
using Nibo.SpaceJam.Services;
using Nibo.SpaceJam.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.WebAPI
{
    /// <summary>
    /// Dependency injection mapper for service
    /// </summary>
    public class ServiceMappings : Module
    {
        /// <summary>
        /// Load mappings
        /// </summary>
        /// <param name="builder">Container builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OperationLogService>().As<IOperationLogService>();
            builder.RegisterType<TeamService>().As<ITeamService>();
            builder.RegisterType<TournamentService>().As<ITournamentService>();
            builder.RegisterType<TournamentResultService>().As<ITournamentResultService>();
        }
    }
}