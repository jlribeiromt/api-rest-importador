using Microsoft.AspNetCore.Http;

namespace ApiRestImportador.Service.Api.Configuration
{
    public class FileUpload
    {
        public IFormFile files { get; set; }
    }
}
