using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Net;
using System.Text;
using WebApi.Helpers;
using static WebApi.Helpers.MultipartRequestHelper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IUploadService _fileBlobsService;
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        private readonly long _fileSizeLimit = 2097152;
        private readonly string[] _permittedExtensions = { ".xlsx", ".pdf" };

        public DocumentController(IUploadService fileBlobsService)
        {
            _fileBlobsService = fileBlobsService;
        }

        [HttpGet("getAll")]
        public IActionResult getAllDocument()
        {
            return Ok(_fileBlobsService.GetAllDocument());
        }

        [HttpPost("uploadFile")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> PostFile()
        {
            if (!IsMultipartContentType(Request.ContentType!))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            var formAccumulator = new KeyValueAccumulator();
            var trustedFileNameForDisplay = string.Empty;
            var untrustedFileNameForStorage = string.Empty;
            var streamedFileContent = Array.Empty<byte>();

            var boundary = GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (HasFileContentDisposition(contentDisposition))
                    {
                        untrustedFileNameForStorage = contentDisposition.FileName.Value;
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        trustedFileNameForDisplay = WebUtility.HtmlEncode(contentDisposition.FileName.Value);

                        streamedFileContent = await FileHelpers.ProcessStreamedFile(section, contentDisposition,
                                ModelState, _permittedExtensions, _fileSizeLimit);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }
                    }
                    else if (HasFormDataContentDisposition(contentDisposition))
                    {
                        // Don't limit the key name length because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name).Value;
                        var encoding = GetEncoding(section);

                        if (encoding == null)
                        {
                            ModelState.AddModelError("File", $"The request couldn't be processed (Error 2).");

                            return BadRequest(ModelState);
                        }

                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by 
                            // MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();

                            if (string.Equals(value, "undefined",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                value = string.Empty;
                            }

                            formAccumulator.Append(key, value);

                            if (formAccumulator.ValueCount >
                                _defaultFormOptions.ValueCountLimit)
                            {
                                // Form key count limit of 
                                // _defaultFormOptions.ValueCountLimit 
                                // is exceeded.
                                ModelState.AddModelError("File",
                                    $"The request couldn't be processed (Error 3).");
                                return BadRequest(ModelState);
                            }
                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            // Bind form data to the model
            var formData = new FormData();
            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);
            var bindingSuccessful = await TryUpdateModelAsync(formData, prefix: "", valueProvider: formValueProvider);

            if (!bindingSuccessful)
            {
                ModelState.AddModelError("File","The request couldn't be processed (Error 5).");
                return BadRequest(ModelState);
            }

            var fileBlob = new FileBlob()
            {
                Content = streamedFileContent,
                Name = untrustedFileNameForStorage!,
                Size = streamedFileContent.Length
            };

            _fileBlobsService.UploadFiles(fileBlob);

            return Ok();
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader =
                MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            // UTF-7 is insecure and shouldn't be honored. UTF-8 succeeds in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }

            return mediaType.Encoding!;
        }
    }
}
