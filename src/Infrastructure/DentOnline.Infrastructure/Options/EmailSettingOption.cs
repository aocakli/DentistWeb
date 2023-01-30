namespace DentOnline.Infrastructure.Options;

public class EmailSettingOption
{
    public string Host { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ushort Port { get; set; }
    public bool EnableSsl { get; set; }

    public string DisplayName { get; set; }
}