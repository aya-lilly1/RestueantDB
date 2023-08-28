namespace RestueantDB.Service
{
    public interface IUserServices
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}
