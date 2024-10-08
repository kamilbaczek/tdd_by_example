using Testcontainers.PostgreSql;

namespace ZTM.IntegrationTests.Common;

public sealed class DatabaseContainer : IAsyncLifetime
{
    private PostgreSqlContainer? _container;

    public string? ConnectionString { get; set; }

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder().WithDatabase("ztm").WithUsername("admin").WithPassword("$3cureP@ssw0rd").Build();
        await _container.StartAsync();
        ConnectionString = _container.GetConnectionString();
    }

    public async Task DisposeAsync() => await _container.StopAsync();
}