namespace EventSubscription.DLSService.Context
{
    public interface IDbaContextProvider
    {
        IDbaContext CreateNewContext();
    }
}
