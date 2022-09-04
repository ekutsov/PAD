namespace PAD.Identity.Domain.Settings;

public class DbConnectSettings
{
    public string Host { get; set; }

    public string Port { get; set; }

    public string Database { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string GetConnectionString() =>
        $"Host={this.Host};Port={this.Port};Database={this.Database};Username={this.Username};Password={this.Password};";
}