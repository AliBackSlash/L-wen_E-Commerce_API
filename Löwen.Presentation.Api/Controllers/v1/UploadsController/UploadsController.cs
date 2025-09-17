namespace Löwen.Presentation.Api.Controllers.v1.UploadsController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Uploads")]
    public class UploadsController(ISender _sender, IFileService fileService) : ControllerBase
    {
 
        
        [HttpPost("upload-product-images")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadProductImages()
        {
            throw new NotImplementedException();
        }

        [HttpPost("upload-category-image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadCategoryImage()
        {
            throw new NotImplementedException();
        }

    }
}
