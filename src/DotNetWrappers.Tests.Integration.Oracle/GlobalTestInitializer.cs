using Testcontainers.Oracle;

namespace DotNetWrappers.Tests.Integration.Oracle;

[TestClass]
public static class GlobalTestInitializer
{
    public static OracleContainer OracleContainer { private set; get; } = null!;

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        OracleContainer = new OracleBuilder().Build();
        OracleContainer.StartAsync().Wait();
        var execResult = OracleContainer.ExecScriptAsync(File.ReadAllText("oracle-init.sql")).Result;
        if (execResult.Stdout.Contains("ORA-"))
            throw new Exception(execResult.Stdout);
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        OracleContainer.DisposeAsync().AsTask().Wait();
    }
}