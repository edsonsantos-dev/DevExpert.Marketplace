namespace DevExpert.Marketplace.Core.Domain.User;

public interface IUserContext
{
    Guid GetUserId();
    bool IsAuthenticated();
}