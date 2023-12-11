namespace WebNetLibrary.BLL.Services.Options;

public class Auth0Options
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Audience { get; set; }
    public string Authority { get; set; }
    public string UserManagementAudience { get; set; }
    public string Domain { get; set; }
}