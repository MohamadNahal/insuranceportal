namespace InsurancePortalRegistration.Exceptions;

public class CouldNotFindPolicyholderException : Exception
{
    public CouldNotFindPolicyholderException()
        : base("Could not find policyholder.")
    {
    }
}