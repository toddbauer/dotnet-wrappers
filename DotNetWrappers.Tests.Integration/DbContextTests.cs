using DotNetWrappers.System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DotNetWrappers.Tests.Integration;

[TestClass]
public class DbContextTests
{
    private IQueryExecutor _queryExecutor = null!;

    private DbContext _target = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _queryExecutor = new QueryExecutor(() => new SqlConnection(GlobalTestInitializer.MsSqlContainer.GetConnectionString()));

        _target = new DbContext(_queryExecutor);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _target = null!;

        _queryExecutor = null!;
    }

    [TestMethod]
    public async Task FindAllAsync_Should_Find_All()
    {
        // Arrange

        // Act
        var results = (await _target.FindAllAsync(new TestSelectQuery(GlobalTestInitializer.MsSqlContainer.GetConnectionString()))).ToList();

        // Assert
        Assert.AreEqual(2, results.Count);
        Assert.AreEqual("Jane", results[0].FirstName);
        Assert.AreEqual("Doe", results[0].LastName);
        Assert.AreEqual("John", results[1].FirstName);
        Assert.AreEqual("Doe", results[1].LastName);
    }
}

public class TestUser
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public class TestSelectQuery(string connectionString) : SelectQueryBase<TestUser>
{
    public override CommandType CommandType => CommandType.Text;
    public override string ConnectionString { get; } = connectionString;
    public override string Sql => "SELECT * FROM TestUsers";
    public override string Where => "WHERE LastName = 'Doe'";
    public override string OrderBy => "ORDER BY FirstName";
    public override bool? BindByName => null;
    public override int? ArrayBindCount => null;

    public override IEnumerable<IDbParameterWrapper> Parameters(Func<IDbParameterWrapper> parameterFactory)
    {
        yield break;
    }

    public override TestUser MapReader(IDbDataReaderWrapper dataReader)
    {
        return new TestUser
        {
            Id = dataReader.GetGuid(0),
            FirstName = dataReader.GetString(1),
            LastName = dataReader.GetString(2)
        };
    }
}