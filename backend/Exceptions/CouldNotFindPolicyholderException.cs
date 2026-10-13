namespace InsurancePortalRegistration.Exceptions;

public class CouldNotFindPolicyholderException : Exception
{
    public CouldNotFindPolicyholderException()
        : base("Could not find policyholder.")
    {
    }

    public CouldNotFindPolicyholderException(Exception innerException)
        : base("Could not find policyholder.", innerException)
    {
    }
}
