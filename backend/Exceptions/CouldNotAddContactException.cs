namespace InsurancePortalRegistration.Exceptions;

public class CouldNotAddContactException : Exception
{
    public CouldNotAddContactException()
        : base("Could not add contact information.")
    {
    }
}