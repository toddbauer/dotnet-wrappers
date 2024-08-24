using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using DotNetWrappers.System.Data.Common;
using Moq;

namespace DotNetWrappers.Tests;

[TestClass]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")]
public class DbContextTests
{
    private Mock<IQueryExecutor> _queryExecutorMock = null!;

    // ReSharper disable once NotAccessedField.Local
    private DbContext _target = null!;
    private Mock<DbContext> _targetMock = null!;

    #region Test Initialize and Cleanup

    [TestInitialize]
    public void TestInitialize()
    {
        _queryExecutorMock = new Mock<IQueryExecutor>();

        _target = new DbContext(_queryExecutorMock.Object);
        _targetMock = new Mock<DbContext>(_queryExecutorMock.Object) {CallBase = true};
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _target = null!;
        _targetMock = null!;

        _queryExecutorMock = null!;
    }

    #endregion Test Initialize and Cleanup

    #region Constructor Tests

    [TestMethod]
    public void Constructor1_Should_Set_QueryExecutor_Property()
    {
        // Arrange

        // Act
        var result = new DbContext(_queryExecutorMock.Object);

        // Assert
        Assert.AreSame(_queryExecutorMock.Object, result.QueryExecutor);
    }

    [TestMethod]
    public void Constructor2_Should_Set_QueryExecutor_Property()
    {
        // Arrange
        var dbConnectionWrapperFactoryMock = new Mock<Func<IDbConnectionWrapper>>();

        // Act
        var result = new DbContext(dbConnectionWrapperFactoryMock.Object);

        // Assert
        Assert.IsNotNull(result.QueryExecutor);
    }

    [TestMethod]
    public void Constructor3_Should_Set_QueryExecutor_Property()
    {
        // Arrange

        // Act
        var result = new DbContext(() => new TestDbConnection());

        // Assert
        Assert.IsNotNull(result.QueryExecutor);
    }

    #endregion Constructor Tests

    #region FindAll Tests

    [TestMethod]
    public void FindAll_Should_Find_All()
    {
        // Arrange
        Func<IDbCommandWrapper, IEnumerable<string>> func = null!;
        var dbDataReaderWrapper = new Mock<IDbDataReaderWrapper>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var selectQueryMock = new Mock<ISelectQuery<string>>();
        var queryResults = new List<string>();

        _queryExecutorMock.Setup(mock => mock.ExecuteQuery(It.IsAny<ISelectQuery<string>>(), It.IsAny<Func<IDbCommandWrapper, IEnumerable<string>>>()))
            .Callback<IQuery<string>, Func<IDbCommandWrapper, IEnumerable<string>>>((_, f) => func = f)
            .Returns(queryResults);
        dbCommandWrapperMock.Setup(mock => mock.ExecuteReader()).Returns(dbDataReaderWrapper.Object);
        selectQueryMock.Setup(mock => mock.ReadAll(It.IsAny<IDbDataReaderWrapper>())).Returns(queryResults);

        // Act
        var results = _targetMock.Object.FindAll(selectQueryMock.Object);

        // Assert
        Assert.IsNotNull(func);
        Assert.AreSame(queryResults, results);
        results = func(dbCommandWrapperMock.Object);
        Assert.AreSame(queryResults, results);

        _queryExecutorMock.Verify(mock => mock.ExecuteQuery(selectQueryMock.Object, It.IsAny<Func<IDbCommandWrapper, IEnumerable<string>>>()), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.ExecuteReader(), Times.Once());
        selectQueryMock.Verify(mock => mock.ReadAll(dbDataReaderWrapper.Object), Times.Once());
    }

    #endregion FindAll Tests

    #region FindAll Tests

    [TestMethod]
    public async Task FindAllAsync_Should_Find_All()
    {
        // Arrange
        Func<IDbCommandWrapper, Task<IEnumerable<string>>> funcAsync = null!;
        var dbDataReaderWrapper = new Mock<IDbDataReaderWrapper>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var selectQueryMock = new Mock<ISelectQuery<string>>();
        var queryResults = new List<string>();

        _queryExecutorMock.Setup(mock => mock.ExecuteQueryAsync(It.IsAny<ISelectQuery<string>>(), It.IsAny<Func<IDbCommandWrapper, Task<IEnumerable<string>>>>()))
            .Callback<IQuery<string>, Func<IDbCommandWrapper, Task<IEnumerable<string>>>>((_, f) => funcAsync = f)
            .ReturnsAsync(queryResults);
        dbCommandWrapperMock.Setup(mock => mock.ExecuteReaderAsync()).ReturnsAsync(dbDataReaderWrapper.Object);
        selectQueryMock.Setup(mock => mock.ReadAllAsync(It.IsAny<IDbDataReaderWrapper>())).ReturnsAsync(queryResults);

        // Act
        var results = await _targetMock.Object.FindAllAsync(selectQueryMock.Object);

        // Assert
        Assert.IsNotNull(funcAsync);
        Assert.AreSame(queryResults, results);
        results = await funcAsync(dbCommandWrapperMock.Object);
        Assert.AreSame(queryResults, results);

        _queryExecutorMock.Verify(mock => mock.ExecuteQueryAsync(selectQueryMock.Object, It.IsAny<Func<IDbCommandWrapper, Task<IEnumerable<string>>>>()), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.ExecuteReaderAsync(), Times.Once());
        selectQueryMock.Verify(mock => mock.ReadAllAsync(dbDataReaderWrapper.Object), Times.Once());
    }

    #endregion FindAllAsync Tests

    #region FindOne Tests

    [TestMethod]
    public void FindOne_Should_Find_One()
    {
        // Arrange
        Func<IDbCommandWrapper, string> func = null!;
        var dbDataReaderWrapper = new Mock<IDbDataReaderWrapper>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var selectQueryMock = new Mock<ISelectQuery<string>>();
        const string queryResult = "queryResult";

        _queryExecutorMock.Setup(mock => mock.ExecuteQuery(It.IsAny<ISelectQuery<string>>(), It.IsAny<Func<IDbCommandWrapper, string>>()))
            .Callback<IQuery<string>, Func<IDbCommandWrapper, string>>((_, f) => func = f)
            .Returns(queryResult);
        dbCommandWrapperMock.Setup(mock => mock.ExecuteReader()).Returns(dbDataReaderWrapper.Object);
        selectQueryMock.Setup(mock => mock.ReadOne(It.IsAny<IDbDataReaderWrapper>())).Returns(queryResult);

        // Act
        var result = _targetMock.Object.FindOne(selectQueryMock.Object);

        // Assert
        Assert.IsNotNull(func);
        Assert.AreSame(queryResult, result);
        result = func(dbCommandWrapperMock.Object);
        Assert.AreSame(queryResult, result);

        _queryExecutorMock.Verify(mock => mock.ExecuteQuery(selectQueryMock.Object, It.IsAny<Func<IDbCommandWrapper, string>>()), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.ExecuteReader(), Times.Once());
        selectQueryMock.Verify(mock => mock.ReadOne(dbDataReaderWrapper.Object), Times.Once());
    }

    #endregion FindOne Tests

    #region FindOneAsync Tests

    [TestMethod]
    public async Task FindOneAsync_Should_Find_One()
    {
        // Arrange
        Func<IDbCommandWrapper, Task<string>> funcAsync = null!;
        var dbDataReaderWrapper = new Mock<IDbDataReaderWrapper>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var selectQueryMock = new Mock<ISelectQuery<string>>();
        const string queryResult = "queryResult";

        _queryExecutorMock.Setup(mock => mock.ExecuteQueryAsync(It.IsAny<ISelectQuery<string>>(), It.IsAny<Func<IDbCommandWrapper, Task<string>>>()))
            .Callback<IQuery<string>, Func<IDbCommandWrapper, Task<string>>>((_, f) => funcAsync = f)
            .ReturnsAsync(queryResult);
        dbCommandWrapperMock.Setup(mock => mock.ExecuteReaderAsync()).ReturnsAsync(dbDataReaderWrapper.Object);
        selectQueryMock.Setup(mock => mock.ReadOneAsync(It.IsAny<IDbDataReaderWrapper>())).ReturnsAsync(queryResult);

        // Act
        var result = await _targetMock.Object.FindOneAsync(selectQueryMock.Object);

        // Assert
        Assert.IsNotNull(funcAsync);
        Assert.AreSame(queryResult, result);
        result = await funcAsync(dbCommandWrapperMock.Object);
        Assert.AreSame(queryResult, result);

        _queryExecutorMock.Verify(mock => mock.ExecuteQueryAsync(selectQueryMock.Object, It.IsAny<Func<IDbCommandWrapper, Task<string>>>()), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.ExecuteReaderAsync(), Times.Once());
        selectQueryMock.Verify(mock => mock.ReadOneAsync(dbDataReaderWrapper.Object), Times.Once());
    }

    #endregion FindOneAsync Tests

    #region Modify Tests

    [TestMethod]
    public void Modify_Should_Modify()
    {
        // Arrange
        Func<IDbCommandWrapper, int> func = null!;
        var modifyQueryMock = new Mock<IModifyQuery<int>>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        const int queryResult = 1;

        _queryExecutorMock.Setup(mock => mock.ExecuteQuery(It.IsAny<IModifyQuery<int>>(), It.IsAny<Func<IDbCommandWrapper, int>>()))
            .Callback<IQuery<int>, Func<IDbCommandWrapper, int>>((_, f) => func = f)
            .Returns(queryResult);
        dbCommandWrapperMock.Setup(mock => mock.ExecuteNonQuery()).Returns(queryResult);
        
        // Act
        var result = _targetMock.Object.Modify(modifyQueryMock.Object);

        // Assert
        Assert.AreEqual(queryResult, result);
        result = func(dbCommandWrapperMock.Object);
        Assert.AreEqual(queryResult, result);

        _queryExecutorMock.Verify(mock => mock.ExecuteQuery(modifyQueryMock.Object, It.IsAny<Func<IDbCommandWrapper, int>>()), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.ExecuteNonQuery(), Times.Once());
    }

    #endregion Modify Tests

    #region ModifyAsync Tests

    [TestMethod]
    public async Task ModifyAsync_Should_Modify()
    {
        // Arrange
        Func<IDbCommandWrapper, Task<int>> funcAsync = null!;
        var modifyQueryMock = new Mock<IModifyQuery<int>>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        const int queryResult = 1;

        _queryExecutorMock.Setup(mock => mock.ExecuteQueryAsync(It.IsAny<IModifyQuery<int>>(), It.IsAny<Func<IDbCommandWrapper, Task<int>>>()))
            .Callback<IQuery<int>, Func<IDbCommandWrapper, Task<int>>>((_, f) => funcAsync = f)
            .ReturnsAsync(queryResult);
        dbCommandWrapperMock.Setup(mock => mock.ExecuteNonQueryAsync()).ReturnsAsync(queryResult);
        
        // Act
        var result = await _targetMock.Object.ModifyAsync(modifyQueryMock.Object);

        // Assert
        Assert.AreEqual(queryResult, result);
        result = await funcAsync(dbCommandWrapperMock.Object);
        Assert.AreEqual(queryResult, result);

        _queryExecutorMock.Verify(mock => mock.ExecuteQueryAsync(modifyQueryMock.Object, It.IsAny<Func<IDbCommandWrapper, Task<int>>>()), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.ExecuteNonQueryAsync(), Times.Once());
    }

    #endregion ModifyAsync Tests

    #region EvaluateScalar Tests

    [TestMethod]
    public void EvaluateScalar_Should_Evaluate_Scalar()
    {
        // Arrange
        Func<IDbCommandWrapper, object?> func = null!;
        var selectQueryMock = new Mock<ISelectQuery<string>>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var predicateMock = new Mock<Func<object, int>>();
        const int scalarResult = 1;

        _queryExecutorMock.Setup(mock => mock.ExecuteQuery(It.IsAny<ISelectQuery<string>>(), It.IsAny<Func<IDbCommandWrapper, object?>>()))
            .Callback<IQuery<string>, Func<IDbCommandWrapper, object?>>((_, f) => func = f)
            .Returns(scalarResult);
        predicateMock.Setup(mock => mock(It.IsAny<object>())).Returns(scalarResult);
        dbCommandWrapperMock.Setup(mock => mock.ExecuteScalar()).Returns(scalarResult);

        // Act
        var result = _targetMock.Object.EvaluateScalar(selectQueryMock.Object, predicateMock.Object);

        // Assert
        Assert.AreEqual(scalarResult, result);
        Assert.IsNotNull(func);
        func(dbCommandWrapperMock.Object);

        _queryExecutorMock.Verify(mock => mock.ExecuteQuery(selectQueryMock.Object, It.IsAny<Func<IDbCommandWrapper, object?>>()), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.ExecuteScalar(), Times.Once());
        predicateMock.Verify(mock => mock(scalarResult), Times.Once());
    }

    #endregion EvaluateScalar Tests

    #region EvaluateScalarAsync Tests

    [TestMethod]
    public async Task EvaluateScalarAsync_Should_Evaluate_Scalar()
    {
        // Arrange
        Func<IDbCommandWrapper, Task<object?>> funcAsync = null!;
        var selectQueryMock = new Mock<ISelectQuery<string>>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var predicateMock = new Mock<Func<object, int>>();
        const int scalarResult = 1;

        _queryExecutorMock.Setup(mock => mock.ExecuteQueryAsync(It.IsAny<ISelectQuery<string>>(), It.IsAny<Func<IDbCommandWrapper, Task<object?>>>()))
            .Callback<IQuery<string>, Func<IDbCommandWrapper, Task<object?>>>((_, f) => funcAsync = f)
            .ReturnsAsync(scalarResult);
        predicateMock.Setup(mock => mock(It.IsAny<object>())).Returns(scalarResult);
        dbCommandWrapperMock.Setup(mock => mock.ExecuteScalarAsync()).ReturnsAsync(scalarResult);

        // Act
        var result = await _targetMock.Object.EvaluateScalarAsync(selectQueryMock.Object, predicateMock.Object);

        // Assert
        Assert.AreEqual(scalarResult, result);
        Assert.IsNotNull(funcAsync);
        await funcAsync(dbCommandWrapperMock.Object);

        _queryExecutorMock.Verify(mock => mock.ExecuteQueryAsync(selectQueryMock.Object, It.IsAny<Func<IDbCommandWrapper, Task<object?>>>()), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.ExecuteScalarAsync(), Times.Once());
        predicateMock.Verify(mock => mock(scalarResult), Times.Once());
    }

    #endregion EvaluateScalarAsync Tests
}

[ExcludeFromCodeCoverage]
public class TestDbConnection : DbConnection
{
    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
        throw new NotImplementedException();
    }

    public override void ChangeDatabase(string databaseName)
    {
        throw new NotImplementedException();
    }

    public override void Close()
    {
        throw new NotImplementedException();
    }

    public override void Open()
    {
        throw new NotImplementedException();
    }

    [AllowNull] public override string ConnectionString { get; set; }
    public override string Database => null!;
    public override ConnectionState State => default;
    public override string DataSource => null!;
    public override string ServerVersion => null!;

    protected override DbCommand CreateDbCommand()
    {
        throw new NotImplementedException();
    }
}