using AzureAISearch.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureAISearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureAISearchController : ControllerBase
    {
        private readonly IAzureAISearchService _azureAISearchService;
        public AzureAISearchController(IAzureAISearchService azureAISearchService)
        {
            _azureAISearchService = azureAISearchService;
        }

        [HttpPost("create-index")]
        public async Task<IActionResult> CreateIndex()
        {
            await _azureAISearchService.CreateCustomIndexAsync();

            return Ok("Index created successfully.");
        }
    }
}
