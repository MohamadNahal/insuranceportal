namespace InsurancePortalRegistration.Exceptions;

public class CouldNotAddAccountException : Exception
{
    public CouldNotAddAccountException()
        : base("Could not add account.")
    {
    }
}