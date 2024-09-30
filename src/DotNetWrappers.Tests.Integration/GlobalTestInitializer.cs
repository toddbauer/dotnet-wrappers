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
        var execResult = MsSqlContainer.ExecScriptAsync(File.ReadAllText("mssql-init.sql")).Result;
        if (execResult.Stdout.Contains("ORA-"))
            throw new Exception(execResult.Stdout);
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        MsSqlContainer.DisposeAsync().AsTask().Wait();
    }
}