namespace InsurancePortalRegistration.Exceptions;

public class CouldNotFindAccountException : Exception
{
    public CouldNotFindAccountException()
        : base("Could not find account.")
    {
    }
}