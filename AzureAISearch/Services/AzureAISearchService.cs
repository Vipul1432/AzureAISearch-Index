using Azure;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using AzureAISearch.Interfaces;

namespace AzureAISearch.Services
{
    public class AzureAISearchService : IAzureAISearchService
    {
        private readonly SearchIndexClient _indexClient;
        public AzureAISearchService()
        {
            var credential = new AzureKeyCredential("api-key");
            Uri endpoint = new Uri("service-url");
            _indexClient = new SearchIndexClient(endpoint, credential);
        }
        public async Task CreateCustomIndexAsync()
        {
            string indexName = "employee-index";

            var index = new SearchIndex(indexName)
            {
                Fields =
                {
                    // Employee fields
                    new SimpleField("employee_id", SearchFieldDataType.String) { IsKey = true, IsFilterable = true, IsSortable = true },
                    new SearchableField("first_name") { IsFilterable = true, IsSortable = true, IsFacetable = true },
                    new SearchableField("last_name") { IsFilterable = true, IsSortable = true, IsFacetable = true },
                    new SearchableField("email") { IsFilterable = true, IsFacetable = true },
                    new SimpleField("date_of_birth", SearchFieldDataType.DateTimeOffset) { IsFilterable = true, IsSortable = true },
                    new SimpleField("hire_date", SearchFieldDataType.DateTimeOffset) { IsFilterable = true, IsSortable = true },
                    new SearchableField("position") { IsFilterable = true, IsFacetable = true },
                    new SearchableField("department") { IsFilterable = true, IsFacetable = true },
                    new SearchableField("manager") { IsFilterable = true, IsFacetable = true },

                    // Deprtment fields
                     new ComplexField("dept_records")
                     {
                         Fields =
                         {
                             new SimpleField("dept_id", SearchFieldDataType.String) { IsKey = true, IsFilterable = true },
                             new SearchableField("dept_name") { IsFilterable = true, IsFacetable = true },
                             new SimpleField("joining_date", SearchFieldDataType.DateTimeOffset) { IsFilterable = true, IsSortable = true },
                             new SearchableField("details") { IsFilterable = true }
                         }
                     }
                }
            };

            try
            {
                var test = await _indexClient.CreateIndexAsync(index);
                Console.WriteLine($"Index '{indexName}' created successfully.");
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error creating index: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An unexpected error occured : {ex.Message}");
            }
        }
    }
}
