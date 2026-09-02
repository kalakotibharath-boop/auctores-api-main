namespace AuctoresOnline.API.Helpers;

public class FileUploadHelper(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
{
    private readonly string _basePath = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

    private static readonly HashSet<string> ImageExtensions =
        new(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };

    private static readonly HashSet<string> PdfExtensions =
        new(StringComparer.OrdinalIgnoreCase) { ".pdf" };

    public async Task<string> UploadImageAsync(IFormFile file, string folder)
    {
        var ext = Path.GetExtension(file.FileName);
        if (!ImageExtensions.Contains(ext))
            throw new InvalidOperationException($"Invalid image type: {ext}");

        return await SaveFileAsync(file, folder, ext);
    }

    public async Task<string> UploadPdfAsync(IFormFile file, string folder)
    {
        var ext = Path.GetExtension(file.FileName);
        if (!PdfExtensions.Contains(ext))
            throw new InvalidOperationException("Only PDF files are allowed.");

        return await SaveFileAsync(file, folder, ext);
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folder)
    {
        var ext = Path.GetExtension(file.FileName);
        return await SaveFileAsync(file, folder, ext);
    }

    public void DeleteFile(string folder, string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) return;
        var path = Path.Combine(_basePath, "uploads", folder, fileName);
        if (File.Exists(path))
            File.Delete(path);
    }

    public string GetFileUrl(string folder, string fileName)
    {
        var request = httpContextAccessor.HttpContext!.Request;
        return $"{request.Scheme}://{request.Host}/uploads/{folder}/{fileName}";
    }

    private async Task<string> SaveFileAsync(IFormFile file, string folder, string ext)
    {
        var uploadDir = Path.Combine(_basePath, "uploads", folder);
        Directory.CreateDirectory(uploadDir);

        var fileName = $"{Guid.NewGuid():N}{ext}";
        var filePath = Path.Combine(uploadDir, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    }
}
