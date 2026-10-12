namespace InsurancePortalRegistration.DTOs;

public class LoginResponse
{
    public string Message { get; set; } = "Login successful.";
    public string Username { get; set; } = "";
    public Guid PolicyholderId { get; set; }
}
