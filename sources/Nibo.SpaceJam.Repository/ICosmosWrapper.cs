using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.SpaceJam.Repository
{
    /// <summary>
    /// Wrapper for Azure Cosmos DB
    /// </summary>
    public interface ICosmosWrapper : ICosmosContracts, IDocumentClient
    {

    }
}
