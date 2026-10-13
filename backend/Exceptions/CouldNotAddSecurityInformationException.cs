
namespace InsurancePortalRegistration.Exceptions;

public class CouldNotAddSecurityInformationException : Exception
{
    public CouldNotAddSecurityInformationException()
        : base("Could not add security information.")
    {
    }

    public CouldNotAddSecurityInformationException(Exception innerException)
        : base("Could not add security information.", innerException)
    {
    }
}
