using System.ComponentModel.DataAnnotations;

namespace Timesheets.DB.Services.Interfaces
{
    public interface IUserService
    {
        TokenResponse Authenticate([StringLength(100)] string user, [StringLength(100)] string password, CancellationToken tokenCancellation);


        string RefreshToken([StringLength(100)] string token, CancellationToken tokenCancellation);
    }
}
