
namespace InsurancePortalRegistration.Exceptions;

public class CouldNotAddAccountException : Exception
{
    public CouldNotAddAccountException()
        : base("Could not add account.")
    {
    }

    public CouldNotAddAccountException(Exception innerException)
        : base("Could not add account.", innerException)
    {
    }
}
