using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Repository.Abstractions;
using Nibo.SpaceJam.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.SpaceJam.Services
{
    /// <summary>
    /// Implementation of operation log service features
    /// </summary>
    public class OperationLogService : ServiceBase<OperationLogModel>, IOperationLogService
    {
        /// <inheritdoc />
        public OperationLogService(IRepository<OperationLogModel> repository) : base(repository)
        {

        }
    }
}
