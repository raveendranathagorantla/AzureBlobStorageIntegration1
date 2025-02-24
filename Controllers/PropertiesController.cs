using AzureBlobStorageIntegration.Models;
using AzureBlobStorageIntegration.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobStorageIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly BlobStorageService _blobStorageService;

        public PropertiesController(BlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties()
        {
            var jsonData = await _blobStorageService.GetJsonDataFromBlobAsync();

            if (string.IsNullOrEmpty(jsonData))
            {
                return NotFound("Error retrieving data from blob.");
            }

            try
            {
                var propertyData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PropertyData>>(jsonData);
                return Ok(propertyData);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing data: {ex.Message}");
            }
        }
    }
}
