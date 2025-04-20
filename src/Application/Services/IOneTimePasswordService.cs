namespace Application.Services;

public interface IOneTimePasswordService
{
    //string GenerateSecureOtp(int length);
    string CreateAndStoreOtp(string email);
    bool ValidateOtp(string email, string requestCode);

}