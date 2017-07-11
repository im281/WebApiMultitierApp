using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Azure.Documents;
using System.Net;

namespace DataAccess.Context
{
    public class BooksMongoDBcontext
    {
        /// <summary>
        /// The Azure DocumentDB endpoint for running this GetStarted sample.
        /// </summary>
        private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUrl"];

        /// <summary>
        /// The primary key for the Azure DocumentDB account.
        /// </summary>
        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

        /// <summary>
        /// The DocumentDB client instance.
        /// </summary>
        private DocumentClient client;

        public BooksMongoDBcontext(string databaseName, string collectionName)
        {
            InitializeDb(databaseName, collectionName);
        }
        private async Task InitializeDb(string databaseName, string collectionName)
        {
            // Create a new instance of the DocumentClient
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);

            await this.CreateDatabaseIfNotExists(databaseName);

            await this.CreateDocumentCollectionIfNotExists(databaseName, collectionName);
        }
        /// <summary>
        /// Create a database with the specified name if it doesn't exist. 
        /// </summary>
        /// <param name="databaseName">The name/ID of the database.</param>
        /// <returns>The Task for asynchronous execution.</returns>
        private async Task CreateDatabaseIfNotExists(string databaseName)
        {
            // Check to verify a database with the id=FamilyDB does not exist
            try
            {
                await this.client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
                this.WriteToConsoleAndPromptToContinue("Found {0}", databaseName);
            }
            catch (DocumentClientException de)
            {
                // If the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDatabaseAsync(new Database { Id = databaseName });
                    this.WriteToConsoleAndPromptToContinue("Created {0}", databaseName);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Create a collection with the specified name if it doesn't exist.
        /// </summary>
        /// <param name="databaseName">The name/ID of the database.</param>
        /// <param name="collectionName">The name/ID of the collection.</param>
        /// <returns>The Task for asynchronous execution.</returns>
        private async Task CreateDocumentCollectionIfNotExists(string databaseName, string collectionName)
        {
            try
            {
                await this.client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
                this.WriteToConsoleAndPromptToContinue("Found {0}", collectionName);
            }
            catch (DocumentClientException de)
            {
                // If the document collection does not exist, create a new collection
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    DocumentCollection collectionInfo = new DocumentCollection();
                    collectionInfo.Id = collectionName;

                    // Optionally, you can configure the indexing policy of a collection. Here we configure collections for maximum query flexibility 
                    // including string range queries. 
                    collectionInfo.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });

                    // DocumentDB collections can be reserved with throughput specified in request units/second. 1 RU is a normalized request equivalent to the read
                    // of a 1KB document.  Here we create a collection with 400 RU/s. 
                    await this.client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseName),
                        new DocumentCollection { Id = collectionName },
                        new RequestOptions { OfferThroughput = 400 });

                    this.WriteToConsoleAndPromptToContinue("Created {0}", collectionName);
                }
                else
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Write to the console, and prompt to continue.
        /// </summary>
        /// <param name="format">The string to be displayed.</param>
        /// <param name="args">Optional arguments.</param>
        private void WriteToConsoleAndPromptToContinue(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

    }
}
