
namespace InsurancePortalRegistration.Exceptions;

public class CouldNotFindAccountException : Exception
{
    public CouldNotFindAccountException()
        : base("Could not find account.")
    {
    }

    public CouldNotFindAccountException(Exception innerException)
        : base("Could not find account.", innerException)
    {
    }
}
