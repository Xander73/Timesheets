using System.ComponentModel.DataAnnotations;

namespace Timesheets.DB.Services.Interfaces
{
    public interface IUserService
    {
        TokenResponse Authenticate([MinLength(3), StringLength(50)] string user, [MinLength(3), StringLength(50)] string password, CancellationToken tokenCancellation);


        string RefreshToken([MinLength(150), StringLength(200)] string token, CancellationToken tokenCancellation);
    }
}
