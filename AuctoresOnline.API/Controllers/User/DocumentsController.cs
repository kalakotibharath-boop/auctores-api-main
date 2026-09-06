using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Controllers.User;

/// <summary>
/// Public dynamic document / static page endpoints.
/// </summary>
[Route("api/user/documents")]
public class DocumentsController(IUserDocumentService documentService) : UserBaseController
{
    /// <summary>
    /// Returns the list of all active document pages (title and slug only).
    /// Used to build the navigation sidebar on document pages.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetAllDocuments))]
    public async Task<IActionResult> GetAllDocuments()
        => ToResult(await documentService.GetAllDocumentsAsync());

    /// <summary>
    /// Returns the full content of an active document page by its URL slug,
    /// along with the navigation list of all other active pages.
    /// Used on /doc/{slug}.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetDocument))]
    public async Task<IActionResult> GetDocument(string slug)
        => ToResult(await documentService.GetDocumentBySlugAsync(slug));
}
