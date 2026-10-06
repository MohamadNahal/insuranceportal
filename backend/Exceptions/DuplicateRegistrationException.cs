namespace InsurancePortalRegistration.Exceptions;

public class DuplicateRegistrationException : Exception
{
    public DuplicateRegistrationException()
        : base("Username already exists.")
    {
    }
}