using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.Repository
{
    /// <summary>
    /// Extensions contract for Azure Cosmos DB
    /// </summary>
    public interface ICosmosContracts
    {
        //
        // Summary:
        //     Creates(if doesn't exist) or gets(if already exists) a collection as an asychronous
        //     operation in the Azure DocumentDB database service.
        //
        // Parameters:
        //   databaseUri:
        //     the URI of the database to create the collection in.
        //
        //   documentCollection:
        //     The Microsoft.Azure.Documents.DocumentCollection object.
        //
        //   options:
        //     (Optional) Any Microsoft.Azure.Documents.Client.RequestOptions you wish to provide
        //     when creating a Collection. E.g. RequestOptions.OfferThroughput = 400.
        //
        // Returns:
        //     The Microsoft.Azure.Documents.DocumentCollection that was created contained within
        //     a System.Threading.Tasks.Task object representing the service response for the
        //     asynchronous operation.
        Task<ResourceResponse<DocumentCollection>> CreateDocumentCollectionIfNotExistsAsync(Uri databaseUri, DocumentCollection documentCollection, RequestOptions options = null);

        //
        // Summary:
        //     Creates(if doesn't exist) or gets(if already exists) a database resource as an
        //     asychronous operation in the Azure DocumentDB database service. You can check
        //     the status code from the response to determine whether the database was newly
        //     created(201) or existing database was returned(200)
        //
        // Parameters:
        //   database:
        //     The specification for the Microsoft.Azure.Documents.Database to create.
        //
        //   options:
        //     (Optional) The Microsoft.Azure.Documents.Client.RequestOptions for the request.
        //
        // Returns:
        //     The Microsoft.Azure.Documents.Database that was created within a task object
        //     representing the service response for the asynchronous operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     If database is not set.
        //
        //   T:System.AggregateException:
        //     Represents a consolidation of failures that occured during async processing.
        //     Look within InnerExceptions to find the actual exception(s).
        //
        //   T:Microsoft.Azure.Documents.DocumentClientException:
        //     This exception can encapsulate many different types of errors. To determine the
        //     specific error always look at the StatusCode property.
        Task<ResourceResponse<Database>> CreateDatabaseIfNotExistsAsync(Database database, RequestOptions options = null);

        //
        // Summary:
        //     Reads a Microsoft.Azure.Documents.Document as a generic type T from the Azure
        //     DocumentDB database service as an asynchronous operation.
        //
        // Parameters:
        //   documentUri:
        //     A URI to the Document resource to be read.
        //
        //   options:
        //     The request options for the request.
        //
        // Returns:
        //     A System.Threading.Tasks containing a Microsoft.Azure.Documents.Client.DocumentResponse`1
        //     which wraps a Microsoft.Azure.Documents.Document containing the read resource
        //     record.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     If documentUri is not set.
        //
        //   T:Microsoft.Azure.Documents.DocumentClientException:
        //     This exception can encapsulate many different types of errors. To determine the
        //     specific error always look at the StatusCode property. Some common codes you
        //     may get when reading a Document are: StatusCodeReason for exception 404NotFound
        //     - This means the resource you tried to read did not exist. 429TooManyRequests
        //     - This means you have exceeded the number of request units per second. Consult
        //     the DocumentClientException.RetryAfter value to see how long you should wait
        //     before retrying this operation.
        //
        // Remarks:
        //     Doing a read of a resource is the most efficient way to get a resource from the
        //     service. If you know the resource's ID, do a read instead of a query by ID.
        Task<DocumentResponse<T>> ReadDocumentAsync<T>(Uri documentUri, RequestOptions options = null);

        /// <summary>
        /// Name of database
        /// </summary>
        string DatabaseName { get; }
    }
}
