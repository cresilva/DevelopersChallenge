using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.Repository
{
    public class CosmosWrapper : ICosmosWrapper
    {
        /// <inheritdoc />
        private readonly DocumentClient DocumentClient;

        /// <inheritdoc />
        public virtual string DatabaseName { get; private set; }

        /// <inheritdoc />
        public SecureString AuthKey
        {
            get
            {
                return this.DocumentClient.AuthKey;
            }
        }

        /// <inheritdoc />
        public ConnectionPolicy ConnectionPolicy
        {
            get
            {
                return this.DocumentClient.ConnectionPolicy;
            }
        }

        /// <inheritdoc />
        public ConsistencyLevel ConsistencyLevel
        {
            get
            {
                return this.DocumentClient.ConsistencyLevel;
            }
        }

        /// <inheritdoc />
        public Uri ReadEndpoint
        {
            get
            {
                return this.DocumentClient.ReadEndpoint;
            }
        }

        /// <inheritdoc />
        public Uri ServiceEndpoint
        {
            get
            {
                return this.DocumentClient.ServiceEndpoint;
            }
        }
        /// <inheritdoc />
        public object Session
        {
            get
            {
                return this.DocumentClient.Session;
            }

            set
            {
                this.DocumentClient.Session = value;
            }
        }
        /// <inheritdoc />
        public Uri WriteEndpoint
        {
            get
            {
                return this.DocumentClient.WriteEndpoint;
            }
        }

        /// <summary>
        /// Initialize this wrapper
        /// </summary>
        /// <param name="documentClient">Instance of DocumentDB</param>
        /// <param name="databaseName">Name of database</param>
        public CosmosWrapper(DocumentClient documentClient, string databaseName)
        {
            if (documentClient == null) throw new ArgumentNullException(nameof(documentClient));
            if (string.IsNullOrWhiteSpace(databaseName)) throw new ArgumentNullException(nameof(databaseName));

            this.DatabaseName = databaseName;
            this.DocumentClient = documentClient;
        }

        public virtual Task<ResourceResponse<Attachment>> CreateAttachmentAsync(Uri documentUri, object attachment, RequestOptions options)
        {
            return this.DocumentClient.CreateAttachmentAsync(documentUri, attachment, options);
        }

        public virtual Task<ResourceResponse<Attachment>> CreateAttachmentAsync(string documentLink, object attachment, RequestOptions options)
        {
            return this.DocumentClient.CreateAttachmentAsync(documentLink, attachment, options);
        }

        public virtual Task<ResourceResponse<Attachment>> CreateAttachmentAsync(Uri documentUri, Stream mediaStream, MediaOptions options, RequestOptions requestOptions)
        {
            return this.DocumentClient.CreateAttachmentAsync(documentUri, mediaStream, options, requestOptions);
        }

        public virtual Task<ResourceResponse<Attachment>> CreateAttachmentAsync(string documentLink, Stream mediaStream, MediaOptions options, RequestOptions requestOptions)
        {
            return this.DocumentClient.CreateAttachmentAsync(documentLink, mediaStream, options, requestOptions);
        }

        public IOrderedQueryable<Attachment> CreateAttachmentQuery(string documentLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery(documentLink, feedOptions);
        }

        public IOrderedQueryable<Attachment> CreateAttachmentQuery(Uri documentUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery(documentUri, feedOptions);
        }

        public IQueryable<dynamic> CreateAttachmentQuery(string documentLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery(documentLink, sqlExpression, feedOptions);
        }

        public IQueryable<dynamic> CreateAttachmentQuery(Uri documentUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery(documentUri, querySpec, feedOptions);
        }

        public IQueryable<dynamic> CreateAttachmentQuery(string documentLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery(documentLink, querySpec, feedOptions);
        }

        public IQueryable<dynamic> CreateAttachmentQuery(Uri documentUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery(documentUri, sqlExpression, feedOptions);
        }

        public IOrderedQueryable<T> CreateAttachmentQuery<T>(string documentLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery<T>(documentLink, feedOptions);
        }

        public IOrderedQueryable<T> CreateAttachmentQuery<T>(Uri documentUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery<T>(documentUri, feedOptions);
        }

        public IQueryable<T> CreateAttachmentQuery<T>(Uri documentUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery<T>(documentUri, querySpec, feedOptions);
        }

        public IQueryable<T> CreateAttachmentQuery<T>(string documentLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery<T>(documentLink, querySpec, feedOptions);
        }

        public IQueryable<T> CreateAttachmentQuery<T>(string documentLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery<T>(documentLink, sqlExpression, feedOptions);
        }

        public IQueryable<T> CreateAttachmentQuery<T>(Uri documentUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateAttachmentQuery<T>(documentUri, sqlExpression, feedOptions);
        }

        public IOrderedQueryable<Conflict> CreateConflictQuery(string collectionLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateConflictQuery(collectionLink, feedOptions);
        }

        public IOrderedQueryable<Conflict> CreateConflictQuery(Uri documentCollectionUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateConflictQuery(documentCollectionUri, feedOptions);
        }

        public IQueryable<dynamic> CreateConflictQuery(string collectionLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateConflictQuery(collectionLink, querySpec, feedOptions);
        }

        public IQueryable<dynamic> CreateConflictQuery(string collectionLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateConflictQuery(collectionLink, sqlExpression, feedOptions);
        }

        public IQueryable<dynamic> CreateConflictQuery(Uri documentCollectionUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateConflictQuery(documentCollectionUri, querySpec, feedOptions);
        }

        public IQueryable<dynamic> CreateConflictQuery(Uri documentCollectionUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateConflictQuery(documentCollectionUri, sqlExpression, feedOptions);
        }

        public virtual Task<ResourceResponse<Database>> CreateDatabaseAsync(Database database, RequestOptions options)
        {
            this.DatabaseName = database.Id;
            return this.DocumentClient.CreateDatabaseAsync(database, options);
        }

        public virtual Task<ResourceResponse<Database>> CreateDatabaseIfNotExistsAsync(Database database, RequestOptions options)
        {
            this.DatabaseName = database.Id;
            return this.DocumentClient.CreateDatabaseIfNotExistsAsync(database, options);
        }

        public IOrderedQueryable<Database> CreateDatabaseQuery(FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDatabaseQuery(feedOptions);
        }

        public IQueryable<dynamic> CreateDatabaseQuery(SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDatabaseQuery(querySpec, feedOptions);
        }

        public IQueryable<dynamic> CreateDatabaseQuery(string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDatabaseQuery(sqlExpression, feedOptions);
        }

        public virtual Task<ResourceResponse<Document>> CreateDocumentAsync(Uri documentCollectionUri, object document, RequestOptions options, bool disableAutomaticIdGeneration = false)
        {
            return this.DocumentClient.CreateDocumentAsync(documentCollectionUri, document, options, disableAutomaticIdGeneration = false);
        }

        public virtual Task<ResourceResponse<Document>> CreateDocumentAsync(string collectionLink, object document, RequestOptions options, bool disableAutomaticIdGeneration = false)
        {
            return this.DocumentClient.CreateDocumentAsync(collectionLink, document, options, disableAutomaticIdGeneration = false);
        }

        public virtual Task<ResourceResponse<DocumentCollection>> CreateDocumentCollectionAsync(Uri databaseUri, DocumentCollection documentCollection, RequestOptions options)
        {
            return this.DocumentClient.CreateDocumentCollectionAsync(databaseUri, documentCollection, options);
        }

        public virtual Task<ResourceResponse<DocumentCollection>> CreateDocumentCollectionAsync(string databaseLink, DocumentCollection documentCollection, RequestOptions options)
        {
            return this.DocumentClient.CreateDocumentCollectionAsync(databaseLink, documentCollection, options);
        }

        public virtual Task<ResourceResponse<DocumentCollection>> CreateDocumentCollectionIfNotExistsAsync(Uri databaseUri, DocumentCollection documentCollection, RequestOptions options)
        {
            return this.DocumentClient.CreateDocumentCollectionIfNotExistsAsync(databaseUri, documentCollection, options);
        }

        public IOrderedQueryable<DocumentCollection> CreateDocumentCollectionQuery(string databaseLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentCollectionQuery(databaseLink, feedOptions);
        }

        public IOrderedQueryable<DocumentCollection> CreateDocumentCollectionQuery(Uri databaseUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentCollectionQuery(databaseUri, feedOptions);
        }


        public IQueryable<dynamic> CreateDocumentCollectionQuery(string databaseLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentCollectionQuery(databaseLink, sqlExpression, feedOptions);
        }


        public IQueryable<dynamic> CreateDocumentCollectionQuery(string databaseLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentCollectionQuery(databaseLink, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateDocumentCollectionQuery(Uri databaseUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentCollectionQuery(databaseUri, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateDocumentCollectionQuery(Uri databaseUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentCollectionQuery(databaseUri, sqlExpression, feedOptions);
        }

        public IOrderedQueryable<Document> CreateDocumentQuery(string collectionLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery(collectionLink, feedOptions);
        }

        public IOrderedQueryable<Document> CreateDocumentQuery(Uri documentCollectionUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery(documentCollectionUri, feedOptions);
        }


        public IQueryable<dynamic> CreateDocumentQuery(string collectionLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery(collectionLink, sqlExpression, feedOptions);
        }


        public IQueryable<dynamic> CreateDocumentQuery(Uri documentCollectionUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery(documentCollectionUri, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateDocumentQuery(string collectionLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery(collectionLink, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateDocumentQuery(Uri documentCollectionUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery(documentCollectionUri, sqlExpression, feedOptions);
        }

        public IOrderedQueryable<T> CreateDocumentQuery<T>(string collectionLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery<T>(collectionLink, feedOptions);
        }

        public virtual IOrderedQueryable<T> CreateDocumentQuery<T>(Uri documentCollectionUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery<T>(documentCollectionUri, feedOptions);
        }

        public IQueryable<T> CreateDocumentQuery<T>(Uri documentCollectionUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery<T>(documentCollectionUri, querySpec, feedOptions);
        }

        public IQueryable<T> CreateDocumentQuery<T>(string collectionLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery<T>(collectionLink, querySpec, feedOptions);
        }

        public IQueryable<T> CreateDocumentQuery<T>(string collectionLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery<T>(collectionLink, sqlExpression, feedOptions);
        }

        public IQueryable<T> CreateDocumentQuery<T>(Uri documentCollectionUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateDocumentQuery<T>(documentCollectionUri, sqlExpression, feedOptions);
        }

        public IOrderedQueryable<Offer> CreateOfferQuery(FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateOfferQuery(feedOptions);
        }


        public IQueryable<dynamic> CreateOfferQuery(SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateOfferQuery(querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateOfferQuery(string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateOfferQuery(sqlExpression, feedOptions);
        }

        public virtual Task<ResourceResponse<Permission>> CreatePermissionAsync(Uri userUri, Permission permission, RequestOptions options)
        {
            return this.DocumentClient.CreatePermissionAsync(userUri, permission, options);
        }

        public virtual Task<ResourceResponse<Permission>> CreatePermissionAsync(string userLink, Permission permission, RequestOptions options)
        {
            return this.DocumentClient.CreatePermissionAsync(userLink, permission, options);
        }

        public IOrderedQueryable<Permission> CreatePermissionQuery(string permissionsLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreatePermissionQuery(permissionsLink, feedOptions);
        }

        public IOrderedQueryable<Permission> CreatePermissionQuery(Uri userUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreatePermissionQuery(userUri, feedOptions);
        }


        public IQueryable<dynamic> CreatePermissionQuery(Uri userUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreatePermissionQuery(userUri, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreatePermissionQuery(string permissionsLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreatePermissionQuery(permissionsLink, sqlExpression, feedOptions);
        }


        public IQueryable<dynamic> CreatePermissionQuery(string permissionsLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreatePermissionQuery(permissionsLink, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreatePermissionQuery(Uri userUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreatePermissionQuery(userUri, sqlExpression, feedOptions);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> CreateStoredProcedureAsync(Uri documentCollectionUri, StoredProcedure storedProcedure, RequestOptions options)
        {
            return this.DocumentClient.CreateStoredProcedureAsync(documentCollectionUri, storedProcedure, options);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> CreateStoredProcedureAsync(string collectionLink, StoredProcedure storedProcedure, RequestOptions options)
        {
            return this.DocumentClient.CreateStoredProcedureAsync(collectionLink, storedProcedure, options);
        }

        public IOrderedQueryable<StoredProcedure> CreateStoredProcedureQuery(string collectionLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateStoredProcedureQuery(collectionLink, feedOptions);
        }

        public IOrderedQueryable<StoredProcedure> CreateStoredProcedureQuery(Uri documentCollectionUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateStoredProcedureQuery(documentCollectionUri, feedOptions);
        }


        public IQueryable<dynamic> CreateStoredProcedureQuery(string collectionLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateStoredProcedureQuery(collectionLink, sqlExpression, feedOptions);
        }


        public IQueryable<dynamic> CreateStoredProcedureQuery(Uri documentCollectionUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateStoredProcedureQuery(documentCollectionUri, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateStoredProcedureQuery(string collectionLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateStoredProcedureQuery(collectionLink, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateStoredProcedureQuery(Uri documentCollectionUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateStoredProcedureQuery(documentCollectionUri, sqlExpression, feedOptions);
        }

        public virtual Task<ResourceResponse<Trigger>> CreateTriggerAsync(Uri documentCollectionUri, Trigger trigger, RequestOptions options)
        {
            return this.DocumentClient.CreateTriggerAsync(documentCollectionUri, trigger, options);
        }

        public virtual Task<ResourceResponse<Trigger>> CreateTriggerAsync(string collectionLink, Trigger trigger, RequestOptions options)
        {
            return this.DocumentClient.CreateTriggerAsync(collectionLink, trigger, options);
        }

        public IOrderedQueryable<Trigger> CreateTriggerQuery(string collectionLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateTriggerQuery(collectionLink, feedOptions);
        }

        public IOrderedQueryable<Trigger> CreateTriggerQuery(Uri documentCollectionUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateTriggerQuery(documentCollectionUri, feedOptions);
        }


        public IQueryable<dynamic> CreateTriggerQuery(string collectionLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateTriggerQuery(collectionLink, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateTriggerQuery(string collectionLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateTriggerQuery(collectionLink, sqlExpression, feedOptions);
        }


        public IQueryable<dynamic> CreateTriggerQuery(Uri documentCollectionUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateTriggerQuery(documentCollectionUri, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateTriggerQuery(Uri documentCollectionUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateTriggerQuery(documentCollectionUri, sqlExpression, feedOptions);
        }

        public virtual Task<ResourceResponse<User>> CreateUserAsync(Uri databaseUri, User user, RequestOptions options)
        {
            return this.DocumentClient.CreateUserAsync(databaseUri, user, options);
        }

        public virtual Task<ResourceResponse<User>> CreateUserAsync(string databaseLink, User user, RequestOptions options)
        {
            return this.DocumentClient.CreateUserAsync(databaseLink, user, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> CreateUserDefinedFunctionAsync(Uri documentCollectionUri, UserDefinedFunction function, RequestOptions options)
        {
            return this.DocumentClient.CreateUserDefinedFunctionAsync(documentCollectionUri, function, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> CreateUserDefinedFunctionAsync(string collectionLink, UserDefinedFunction function, RequestOptions options)
        {
            return this.DocumentClient.CreateUserDefinedFunctionAsync(collectionLink, function, options);
        }

        public IOrderedQueryable<UserDefinedFunction> CreateUserDefinedFunctionQuery(string collectionLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserDefinedFunctionQuery(collectionLink, feedOptions);
        }

        public IOrderedQueryable<UserDefinedFunction> CreateUserDefinedFunctionQuery(Uri documentCollectionUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserDefinedFunctionQuery(documentCollectionUri, feedOptions);
        }


        public IQueryable<dynamic> CreateUserDefinedFunctionQuery(string collectionLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserDefinedFunctionQuery(collectionLink, sqlExpression, feedOptions);
        }


        public IQueryable<dynamic> CreateUserDefinedFunctionQuery(Uri documentCollectionUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserDefinedFunctionQuery(documentCollectionUri, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateUserDefinedFunctionQuery(string collectionLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserDefinedFunctionQuery(collectionLink, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateUserDefinedFunctionQuery(Uri documentCollectionUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserDefinedFunctionQuery(documentCollectionUri, sqlExpression, feedOptions);
        }

        public IOrderedQueryable<User> CreateUserQuery(string usersLink, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserQuery(usersLink, feedOptions);
        }

        public IOrderedQueryable<User> CreateUserQuery(Uri documentCollectionUri, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserQuery(documentCollectionUri, feedOptions);
        }


        public IQueryable<dynamic> CreateUserQuery(string usersLink, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserQuery(usersLink, sqlExpression, feedOptions);
        }


        public IQueryable<dynamic> CreateUserQuery(Uri documentCollectionUri, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserQuery(documentCollectionUri, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateUserQuery(string usersLink, SqlQuerySpec querySpec, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserQuery(usersLink, querySpec, feedOptions);
        }


        public IQueryable<dynamic> CreateUserQuery(Uri documentCollectionUri, string sqlExpression, FeedOptions feedOptions)
        {
            return this.DocumentClient.CreateUserQuery(documentCollectionUri, sqlExpression, feedOptions);
        }

        public virtual Task<ResourceResponse<Attachment>> DeleteAttachmentAsync(Uri attachmentUri, RequestOptions options)
        {
            return this.DocumentClient.DeleteAttachmentAsync(attachmentUri, options);
        }

        public virtual Task<ResourceResponse<Attachment>> DeleteAttachmentAsync(string attachmentLink, RequestOptions options)
        {
            return this.DocumentClient.DeleteAttachmentAsync(attachmentLink, options);
        }

        public virtual Task<ResourceResponse<Conflict>> DeleteConflictAsync(Uri conflictUri, RequestOptions options)
        {
            return this.DocumentClient.DeleteConflictAsync(conflictUri, options);
        }

        public virtual Task<ResourceResponse<Conflict>> DeleteConflictAsync(string conflictLink, RequestOptions options)
        {
            return this.DocumentClient.DeleteConflictAsync(conflictLink, options);
        }

        public virtual Task<ResourceResponse<Database>> DeleteDatabaseAsync(Uri databaseUri, RequestOptions options)
        {
            return this.DocumentClient.DeleteDatabaseAsync(databaseUri, options);
        }

        public virtual Task<ResourceResponse<Database>> DeleteDatabaseAsync(string databaseLink, RequestOptions options)
        {
            return this.DocumentClient.DeleteDatabaseAsync(databaseLink, options);
        }

        public virtual Task<ResourceResponse<Document>> DeleteDocumentAsync(Uri documentUri, RequestOptions options)
        {
            return this.DocumentClient.DeleteDocumentAsync(documentUri, options);
        }

        public virtual Task<ResourceResponse<Document>> DeleteDocumentAsync(string documentLink, RequestOptions options)
        {
            return this.DocumentClient.DeleteDocumentAsync(documentLink, options);
        }

        public virtual Task<ResourceResponse<DocumentCollection>> DeleteDocumentCollectionAsync(Uri documentCollectionUri, RequestOptions options)
        {
            return this.DocumentClient.DeleteDocumentCollectionAsync(documentCollectionUri, options);
        }

        public virtual Task<ResourceResponse<DocumentCollection>> DeleteDocumentCollectionAsync(string documentCollectionLink, RequestOptions options)
        {
            return this.DocumentClient.DeleteDocumentCollectionAsync(documentCollectionLink, options);
        }

        public virtual Task<ResourceResponse<Permission>> DeletePermissionAsync(Uri permissionUri, RequestOptions options)
        {
            return this.DocumentClient.DeletePermissionAsync(permissionUri, options);
        }

        public virtual Task<ResourceResponse<Permission>> DeletePermissionAsync(string permissionLink, RequestOptions options)
        {
            return this.DocumentClient.DeletePermissionAsync(permissionLink, options);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> DeleteStoredProcedureAsync(Uri storedProcedureUri, RequestOptions options)
        {
            return this.DocumentClient.DeleteStoredProcedureAsync(storedProcedureUri, options);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> DeleteStoredProcedureAsync(string storedProcedureLink, RequestOptions options)
        {
            return this.DocumentClient.DeleteStoredProcedureAsync(storedProcedureLink, options);
        }

        public virtual Task<ResourceResponse<Trigger>> DeleteTriggerAsync(Uri triggerUri, RequestOptions options)
        {
            return this.DocumentClient.DeleteTriggerAsync(triggerUri, options);
        }

        public virtual Task<ResourceResponse<Trigger>> DeleteTriggerAsync(string triggerLink, RequestOptions options)
        {
            return this.DocumentClient.DeleteTriggerAsync(triggerLink, options);
        }

        public virtual Task<ResourceResponse<User>> DeleteUserAsync(Uri userUri, RequestOptions options)
        {
            return this.DocumentClient.DeleteUserAsync(userUri, options);
        }

        public virtual Task<ResourceResponse<User>> DeleteUserAsync(string userLink, RequestOptions options)
        {
            return this.DocumentClient.DeleteUserAsync(userLink, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> DeleteUserDefinedFunctionAsync(Uri functionUri, RequestOptions options)
        {
            return this.DocumentClient.DeleteUserDefinedFunctionAsync(functionUri, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> DeleteUserDefinedFunctionAsync(string functionLink, RequestOptions options)
        {
            return this.DocumentClient.DeleteUserDefinedFunctionAsync(functionLink, options);
        }

        public virtual Task<StoredProcedureResponse<TValue>> ExecuteStoredProcedureAsync<TValue>(Uri storedProcedureUri, params dynamic[] procedureParams)
        {
            return this.DocumentClient.ExecuteStoredProcedureAsync<TValue>(storedProcedureUri, procedureParams);
        }

        public virtual Task<StoredProcedureResponse<TValue>> ExecuteStoredProcedureAsync<TValue>(string storedProcedureLink, params dynamic[] procedureParams)
        {
            return this.DocumentClient.ExecuteStoredProcedureAsync<TValue>(storedProcedureLink, procedureParams);
        }

        public virtual Task<StoredProcedureResponse<TValue>> ExecuteStoredProcedureAsync<TValue>(Uri storedProcedureUri, RequestOptions options, params dynamic[] procedureParams)
        {
            return this.DocumentClient.ExecuteStoredProcedureAsync<TValue>(storedProcedureUri, options, procedureParams);
        }

        public virtual Task<StoredProcedureResponse<TValue>> ExecuteStoredProcedureAsync<TValue>(string storedProcedureLink, RequestOptions options, params dynamic[] procedureParams)
        {
            return this.DocumentClient.ExecuteStoredProcedureAsync<TValue>(storedProcedureLink, options, procedureParams);
        }

        public virtual Task<DatabaseAccount> GetDatabaseAccountAsync()
        {
            return this.DocumentClient.GetDatabaseAccountAsync();
        }

        public virtual Task<ResourceResponse<Attachment>> ReadAttachmentAsync(Uri attachmentUri, RequestOptions options)
        {
            return this.DocumentClient.ReadAttachmentAsync(attachmentUri, options);
        }

        public virtual Task<ResourceResponse<Attachment>> ReadAttachmentAsync(string attachmentLink, RequestOptions options)
        {
            return this.DocumentClient.ReadAttachmentAsync(attachmentLink, options);
        }

        public virtual Task<FeedResponse<Attachment>> ReadAttachmentFeedAsync(Uri documentUri, FeedOptions options)
        {
            return this.DocumentClient.ReadAttachmentFeedAsync(documentUri, options);
        }

        public virtual Task<FeedResponse<Attachment>> ReadAttachmentFeedAsync(string documentLink, FeedOptions options)
        {
            return this.DocumentClient.ReadAttachmentFeedAsync(documentLink, options);
        }

        public virtual Task<ResourceResponse<Conflict>> ReadConflictAsync(Uri conflictUri, RequestOptions options)
        {
            return this.DocumentClient.ReadConflictAsync(conflictUri, options);
        }

        public virtual Task<ResourceResponse<Conflict>> ReadConflictAsync(string conflictLink, RequestOptions options)
        {
            return this.DocumentClient.ReadConflictAsync(conflictLink, options);
        }

        public virtual Task<FeedResponse<Conflict>> ReadConflictFeedAsync(Uri documentCollectionUri, FeedOptions options)
        {
            return this.DocumentClient.ReadConflictFeedAsync(documentCollectionUri, options);
        }

        public virtual Task<FeedResponse<Conflict>> ReadConflictFeedAsync(string collectionLink, FeedOptions options)
        {
            return this.DocumentClient.ReadConflictFeedAsync(collectionLink, options);
        }

        public virtual Task<ResourceResponse<Database>> ReadDatabaseAsync(Uri databaseUri, RequestOptions options)
        {
            return this.DocumentClient.ReadDatabaseAsync(databaseUri, options);
        }

        public virtual Task<ResourceResponse<Database>> ReadDatabaseAsync(string databaseLink, RequestOptions options)
        {
            return this.DocumentClient.ReadDatabaseAsync(databaseLink, options);
        }

        public virtual Task<FeedResponse<Database>> ReadDatabaseFeedAsync(FeedOptions options)
        {
            return this.DocumentClient.ReadDatabaseFeedAsync(options);
        }

        public virtual Task<ResourceResponse<Document>> ReadDocumentAsync(Uri documentUri, RequestOptions options)
        {
            return this.DocumentClient.ReadDocumentAsync(documentUri, options);
        }

        public virtual Task<ResourceResponse<Document>> ReadDocumentAsync(string documentLink, RequestOptions options)
        {
            return this.DocumentClient.ReadDocumentAsync(documentLink, options);
        }

        public virtual Task<DocumentResponse<T>> ReadDocumentAsync<T>(Uri documentUri, RequestOptions options)
        {
            return this.DocumentClient.ReadDocumentAsync<T>(documentUri, options);
        }

        public virtual Task<ResourceResponse<DocumentCollection>> ReadDocumentCollectionAsync(Uri documentCollectionUri, RequestOptions options)
        {
            return this.DocumentClient.ReadDocumentCollectionAsync(documentCollectionUri, options);
        }

        public virtual Task<ResourceResponse<DocumentCollection>> ReadDocumentCollectionAsync(string documentCollectionLink, RequestOptions options)
        {
            return this.DocumentClient.ReadDocumentCollectionAsync(documentCollectionLink, options);
        }

        public virtual Task<FeedResponse<DocumentCollection>> ReadDocumentCollectionFeedAsync(Uri databaseUri, FeedOptions options)
        {
            return this.DocumentClient.ReadDocumentCollectionFeedAsync(databaseUri, options);
        }

        public virtual Task<FeedResponse<DocumentCollection>> ReadDocumentCollectionFeedAsync(string databaseLink, FeedOptions options)
        {
            return this.DocumentClient.ReadDocumentCollectionFeedAsync(databaseLink, options);
        }

        public virtual Task<FeedResponse<dynamic>> ReadDocumentFeedAsync(Uri documentCollectionUri, FeedOptions options)
        {
            return this.DocumentClient.ReadDocumentFeedAsync(documentCollectionUri, options);
        }

        public virtual Task<FeedResponse<dynamic>> ReadDocumentFeedAsync(string collectionLink, FeedOptions options)
        {
            return this.DocumentClient.ReadDocumentFeedAsync(collectionLink, options);
        }

        public virtual Task<MediaResponse> ReadMediaAsync(string mediaLink)
        {
            return this.DocumentClient.ReadMediaAsync(mediaLink);
        }

        public virtual Task<MediaResponse> ReadMediaMetadataAsync(string mediaLink)
        {
            return this.DocumentClient.ReadMediaMetadataAsync(mediaLink);
        }

        public virtual Task<ResourceResponse<Offer>> ReadOfferAsync(string offerLink)
        {
            return this.DocumentClient.ReadOfferAsync(offerLink);
        }

        public virtual Task<FeedResponse<Offer>> ReadOffersFeedAsync(FeedOptions options)
        {
            return this.DocumentClient.ReadOffersFeedAsync(options);
        }

        public virtual Task<ResourceResponse<Permission>> ReadPermissionAsync(Uri permissionUri, RequestOptions options)
        {
            return this.DocumentClient.ReadPermissionAsync(permissionUri, options);
        }

        public virtual Task<ResourceResponse<Permission>> ReadPermissionAsync(string permissionLink, RequestOptions options)
        {
            return this.DocumentClient.ReadPermissionAsync(permissionLink, options);
        }

        public virtual Task<FeedResponse<Permission>> ReadPermissionFeedAsync(Uri userUri, FeedOptions options)
        {
            return this.DocumentClient.ReadPermissionFeedAsync(userUri, options);
        }

        public virtual Task<FeedResponse<Permission>> ReadPermissionFeedAsync(string userLink, FeedOptions options)
        {
            return this.DocumentClient.ReadPermissionFeedAsync(userLink, options);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> ReadStoredProcedureAsync(Uri storedProcedureUri, RequestOptions options)
        {
            return this.DocumentClient.ReadStoredProcedureAsync(storedProcedureUri, options);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> ReadStoredProcedureAsync(string storedProcedureLink, RequestOptions options)
        {
            return this.DocumentClient.ReadStoredProcedureAsync(storedProcedureLink, options);
        }

        public virtual Task<FeedResponse<StoredProcedure>> ReadStoredProcedureFeedAsync(Uri documentCollectionUri, FeedOptions options)
        {
            return this.DocumentClient.ReadStoredProcedureFeedAsync(documentCollectionUri, options);
        }

        public virtual Task<FeedResponse<StoredProcedure>> ReadStoredProcedureFeedAsync(string collectionLink, FeedOptions options)
        {
            return this.DocumentClient.ReadStoredProcedureFeedAsync(collectionLink, options);
        }

        public virtual Task<ResourceResponse<Trigger>> ReadTriggerAsync(Uri triggerUri, RequestOptions options)
        {
            return this.DocumentClient.ReadTriggerAsync(triggerUri, options);
        }

        public virtual Task<ResourceResponse<Trigger>> ReadTriggerAsync(string triggerLink, RequestOptions options)
        {
            return this.DocumentClient.ReadTriggerAsync(triggerLink, options);
        }

        public virtual Task<FeedResponse<Trigger>> ReadTriggerFeedAsync(Uri documentCollectionUri, FeedOptions options)
        {
            return this.DocumentClient.ReadTriggerFeedAsync(documentCollectionUri, options);
        }

        public virtual Task<FeedResponse<Trigger>> ReadTriggerFeedAsync(string collectionLink, FeedOptions options)
        {
            return this.DocumentClient.ReadTriggerFeedAsync(collectionLink, options);
        }

        public virtual Task<ResourceResponse<User>> ReadUserAsync(Uri userUri, RequestOptions options)
        {
            return this.DocumentClient.ReadUserAsync(userUri, options);
        }

        public virtual Task<ResourceResponse<User>> ReadUserAsync(string userLink, RequestOptions options)
        {
            return this.DocumentClient.ReadUserAsync(userLink, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> ReadUserDefinedFunctionAsync(Uri functionUri, RequestOptions options)
        {
            return this.DocumentClient.ReadUserDefinedFunctionAsync(functionUri, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> ReadUserDefinedFunctionAsync(string functionLink, RequestOptions options)
        {
            return this.DocumentClient.ReadUserDefinedFunctionAsync(functionLink, options);
        }

        public virtual Task<FeedResponse<UserDefinedFunction>> ReadUserDefinedFunctionFeedAsync(Uri documentCollectionUri, FeedOptions options)
        {
            return this.DocumentClient.ReadUserDefinedFunctionFeedAsync(documentCollectionUri, options);
        }

        public virtual Task<FeedResponse<UserDefinedFunction>> ReadUserDefinedFunctionFeedAsync(string collectionLink, FeedOptions options)
        {
            return this.DocumentClient.ReadUserDefinedFunctionFeedAsync(collectionLink, options);
        }

        public virtual Task<FeedResponse<User>> ReadUserFeedAsync(Uri databaseUri, FeedOptions options)
        {
            return this.DocumentClient.ReadUserFeedAsync(databaseUri, options);
        }

        public virtual Task<FeedResponse<User>> ReadUserFeedAsync(string databaseLink, FeedOptions options)
        {
            return this.DocumentClient.ReadUserFeedAsync(databaseLink, options);
        }

        public virtual Task<ResourceResponse<Attachment>> ReplaceAttachmentAsync(Attachment attachment, RequestOptions options)
        {
            return this.DocumentClient.ReplaceAttachmentAsync(attachment, options);
        }

        public virtual Task<ResourceResponse<Attachment>> ReplaceAttachmentAsync(Uri attachmentUri, Attachment attachment, RequestOptions options)
        {
            return this.DocumentClient.ReplaceAttachmentAsync(attachmentUri, attachment, options);
        }

        public virtual Task<ResourceResponse<Document>> ReplaceDocumentAsync(Document document, RequestOptions options)
        {
            return this.DocumentClient.ReplaceDocumentAsync(document, options);
        }

        public virtual Task<ResourceResponse<Document>> ReplaceDocumentAsync(Uri documentUri, object document, RequestOptions options)
        {
            return this.DocumentClient.ReplaceDocumentAsync(documentUri, document, options);
        }

        public virtual Task<ResourceResponse<Document>> ReplaceDocumentAsync(string documentLink, object document, RequestOptions options)
        {
            return this.DocumentClient.ReplaceDocumentAsync(documentLink, document, options);
        }

        public virtual Task<ResourceResponse<DocumentCollection>> ReplaceDocumentCollectionAsync(DocumentCollection documentCollection, RequestOptions options)
        {
            return this.DocumentClient.ReplaceDocumentCollectionAsync(documentCollection, options);
        }

        public virtual Task<ResourceResponse<DocumentCollection>> ReplaceDocumentCollectionAsync(Uri documentCollectionUri, DocumentCollection documentCollection, RequestOptions options)
        {
            return this.DocumentClient.ReplaceDocumentCollectionAsync(documentCollectionUri, documentCollection, options);
        }

        public virtual Task<ResourceResponse<Offer>> ReplaceOfferAsync(Offer offer)
        {
            return this.DocumentClient.ReplaceOfferAsync(offer);
        }

        public virtual Task<ResourceResponse<Permission>> ReplacePermissionAsync(Permission permission, RequestOptions options)
        {
            return this.DocumentClient.ReplacePermissionAsync(permission, options);
        }

        public virtual Task<ResourceResponse<Permission>> ReplacePermissionAsync(Uri permissionUri, Permission permission, RequestOptions options)
        {
            return this.DocumentClient.ReplacePermissionAsync(permissionUri, permission, options);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> ReplaceStoredProcedureAsync(StoredProcedure storedProcedure, RequestOptions options)
        {
            return this.DocumentClient.ReplaceStoredProcedureAsync(storedProcedure, options);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> ReplaceStoredProcedureAsync(Uri storedProcedureUri, StoredProcedure storedProcedure, RequestOptions options)
        {
            return this.DocumentClient.ReplaceStoredProcedureAsync(storedProcedureUri, storedProcedure, options);
        }

        public virtual Task<ResourceResponse<Trigger>> ReplaceTriggerAsync(Trigger trigger, RequestOptions options)
        {
            return this.DocumentClient.ReplaceTriggerAsync(trigger, options);
        }

        public virtual Task<ResourceResponse<Trigger>> ReplaceTriggerAsync(Uri triggerUri, Trigger trigger, RequestOptions options)
        {
            return this.DocumentClient.ReplaceTriggerAsync(triggerUri, trigger, options);
        }

        public virtual Task<ResourceResponse<User>> ReplaceUserAsync(User user, RequestOptions options)
        {
            return this.DocumentClient.ReplaceUserAsync(user, options);
        }

        public virtual Task<ResourceResponse<User>> ReplaceUserAsync(Uri userUri, User user, RequestOptions options)
        {
            return this.DocumentClient.ReplaceUserAsync(userUri, user, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> ReplaceUserDefinedFunctionAsync(UserDefinedFunction function, RequestOptions options)
        {
            return this.DocumentClient.ReplaceUserDefinedFunctionAsync(function, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> ReplaceUserDefinedFunctionAsync(Uri userDefinedFunctionUri, UserDefinedFunction function, RequestOptions options)
        {
            return this.DocumentClient.ReplaceUserDefinedFunctionAsync(userDefinedFunctionUri, function, options);
        }

        public virtual Task<MediaResponse> UpdateMediaAsync(string mediaLink, Stream mediaStream, MediaOptions options)
        {
            return this.DocumentClient.UpdateMediaAsync(mediaLink, mediaStream, options);
        }

        public virtual Task<ResourceResponse<Attachment>> UpsertAttachmentAsync(Uri documentUri, object attachment, RequestOptions options)
        {
            return this.DocumentClient.UpsertAttachmentAsync(documentUri, attachment, options);
        }

        public virtual Task<ResourceResponse<Attachment>> UpsertAttachmentAsync(string documentLink, object attachment, RequestOptions options)
        {
            return this.DocumentClient.UpsertAttachmentAsync(documentLink, attachment, options);
        }

        public virtual Task<ResourceResponse<Attachment>> UpsertAttachmentAsync(Uri documentUri, Stream mediaStream, MediaOptions options, RequestOptions requestOptions)
        {
            return this.DocumentClient.UpsertAttachmentAsync(documentUri, mediaStream, options, requestOptions);
        }

        public virtual Task<ResourceResponse<Attachment>> UpsertAttachmentAsync(string documentLink, Stream mediaStream, MediaOptions options, RequestOptions requestOptions)
        {
            return this.DocumentClient.UpsertAttachmentAsync(documentLink, mediaStream, options, requestOptions);
        }

        public virtual Task<ResourceResponse<Document>> UpsertDocumentAsync(Uri documentCollectionUri, object document, RequestOptions options, bool disableAutomaticIdGeneration = false)
        {
            return this.DocumentClient.UpsertDocumentAsync(documentCollectionUri, document, options, disableAutomaticIdGeneration = false);
        }

        public virtual Task<ResourceResponse<Document>> UpsertDocumentAsync(string collectionLink, object document, RequestOptions options, bool disableAutomaticIdGeneration = false)
        {
            return this.DocumentClient.UpsertDocumentAsync(collectionLink, document, options, disableAutomaticIdGeneration = false);
        }

        public virtual Task<ResourceResponse<Permission>> UpsertPermissionAsync(Uri userUri, Permission permission, RequestOptions options)
        {
            return this.DocumentClient.UpsertPermissionAsync(userUri, permission, options);
        }

        public virtual Task<ResourceResponse<Permission>> UpsertPermissionAsync(string userLink, Permission permission, RequestOptions options)
        {
            return this.DocumentClient.UpsertPermissionAsync(userLink, permission, options);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> UpsertStoredProcedureAsync(Uri documentCollectionUri, StoredProcedure storedProcedure, RequestOptions options)
        {
            return this.DocumentClient.UpsertStoredProcedureAsync(documentCollectionUri, storedProcedure, options);
        }

        public virtual Task<ResourceResponse<StoredProcedure>> UpsertStoredProcedureAsync(string collectionLink, StoredProcedure storedProcedure, RequestOptions options)
        {
            return this.DocumentClient.UpsertStoredProcedureAsync(collectionLink, storedProcedure, options);
        }

        public virtual Task<ResourceResponse<Trigger>> UpsertTriggerAsync(Uri documentCollectionUri, Trigger trigger, RequestOptions options)
        {
            return this.DocumentClient.UpsertTriggerAsync(documentCollectionUri, trigger, options);
        }

        public virtual Task<ResourceResponse<Trigger>> UpsertTriggerAsync(string collectionLink, Trigger trigger, RequestOptions options)
        {
            return this.DocumentClient.UpsertTriggerAsync(collectionLink, trigger, options);
        }

        public virtual Task<ResourceResponse<User>> UpsertUserAsync(Uri databaseUri, User user, RequestOptions options)
        {
            return this.DocumentClient.UpsertUserAsync(databaseUri, user, options);
        }

        public virtual Task<ResourceResponse<User>> UpsertUserAsync(string databaseLink, User user, RequestOptions options)
        {
            return this.DocumentClient.UpsertUserAsync(databaseLink, user, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> UpsertUserDefinedFunctionAsync(Uri documentCollectionUri, UserDefinedFunction function, RequestOptions options)
        {
            return this.DocumentClient.UpsertUserDefinedFunctionAsync(documentCollectionUri, function, options);
        }

        public virtual Task<ResourceResponse<UserDefinedFunction>> UpsertUserDefinedFunctionAsync(string collectionLink, UserDefinedFunction function, RequestOptions options)
        {
            return UpsertUserDefinedFunctionAsync(collectionLink, function, options);
        }
    }
}