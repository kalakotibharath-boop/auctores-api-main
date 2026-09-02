namespace AuctoresOnline.API.Services.Interfaces.User;

public interface IUserSitemapService
{
    Task<IEnumerable<string>> GetSitemapUrlsAsync(string baseUrl);
}
