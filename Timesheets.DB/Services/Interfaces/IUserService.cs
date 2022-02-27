namespace Timesheets.DB.Services.Interfaces
{
    public interface IUserService
    {
        TokenResponse Authenticate(string user, string password, CancellationToken tokenCancellation);


        string RefreshToken(string token, CancellationToken tokenCancellation);
    }
}
