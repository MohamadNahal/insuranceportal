namespace InsurancePortalRegistration.Exceptions;

public class CouldNotAddSecurityInformationException : Exception
{
    public CouldNotAddSecurityInformationException()
        : base("Could not add security information.")
    {
    }
}