namespace DevExpert.Marketplace.Business.Interfaces;

public interface IUserContext
{
    Guid GetUserId();
    bool IsAuthenticated();
}