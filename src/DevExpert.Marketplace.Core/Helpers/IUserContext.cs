namespace DevExpert.Marketplace.Core.Helpers;

public interface IUserContext
{
    Guid GetUserId();
    bool IsAuthenticated();
}