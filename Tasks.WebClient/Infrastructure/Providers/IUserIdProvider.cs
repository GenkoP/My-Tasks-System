namespace Tasks.WebClient.Infrastructure.Providers
{
    public interface ICurrentUserIdProvider
    {
        string GetUserId();
    }
}