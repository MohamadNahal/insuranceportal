
namespace InsurancePortalRegistration.Exceptions;

public class CouldNotAddContactException : Exception
{
    public CouldNotAddContactException()
        : base("Could not add contact information.")
    {
    }

    public CouldNotAddContactException(Exception innerException)
        : base("Could not add contact information.", innerException)
    {
    }
}
