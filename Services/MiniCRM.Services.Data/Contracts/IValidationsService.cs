namespace MiniCRM.Services.Data.Contracts
{
    public interface IValidationsService
    {
        bool IsValidUserEmail(string email);
        bool IsValidUserPhoneNumber(string phoneNumber);
    }
}
