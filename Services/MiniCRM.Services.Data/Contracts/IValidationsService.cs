namespace MiniCRM.Services.Data.Contracts
{
    public interface IValidationsService
    {
        bool IsExistUserEmail(string email);
        bool IsExistUserPhoneNumber(string phoneNumber);

    }
}
