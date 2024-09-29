using Testcontainers.MsSql;

namespace DotNetWrappers.Tests.Integration;

[TestClass]
public static class GlobalTestInitializer
{
    public static MsSqlContainer MsSqlContainer { private set; get; } = null!;

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        MsSqlContainer = new MsSqlBuilder().Build();
        MsSqlContainer.StartAsync().Wait();
        MsSqlContainer.ExecScriptAsync(File.ReadAllText("mssql-init.sql")).Wait();
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        MsSqlContainer.DisposeAsync().AsTask().Wait();
    }
}