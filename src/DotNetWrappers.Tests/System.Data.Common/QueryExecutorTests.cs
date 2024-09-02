using DotNetWrappers.System.Data.Common;
using Moq;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace DotNetWrappers.Tests.System.Data.Common;

[TestClass]
[ExcludeFromCodeCoverage]
public class QueryExecutorTests
{
    private Mock<Func<IDbConnectionWrapper>> _dbConnectionWrapperFactoryMock = null!;

    // ReSharper disable once NotAccessedField.Local
    private QueryExecutor _target = null!;
    private Mock<QueryExecutor> _targetMock = null!;

    #region Test Initialize and Cleanup

    [TestInitialize]
    public void Initialize()
    {
        _dbConnectionWrapperFactoryMock = new Mock<Func<IDbConnectionWrapper>>();

        _target = new QueryExecutor(_dbConnectionWrapperFactoryMock.Object);
        _targetMock = new Mock<QueryExecutor>(_dbConnectionWrapperFactoryMock.Object) { CallBase = true };
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dbConnectionWrapperFactoryMock = null!;

        _target = null!;
        _targetMock = null!;
    }

    #endregion Test Initialize and Cleanup

    #region Constructor Tests

    [TestMethod]
    public void Constructor1_Should_Set_DbConnectionWrapperFactory_Property()
    {
        // Arrange

        // Act
        var result = new QueryExecutor(_dbConnectionWrapperFactoryMock.Object);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreSame(_dbConnectionWrapperFactoryMock.Object, result.DbConnectionWrapperFactory);
    }

    [TestMethod]
    public void Constructor2_Should_Set_DbConnectionWrapperFactory_Property()
    {
        // Arrange
        var dbConnectionFactoryMock = new Mock<Func<DbConnection>>();

        // Act
        var result = new QueryExecutor(dbConnectionFactoryMock.Object);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.DbConnectionWrapperFactory);
    }

    #endregion Constructor Tests

    #region ExecuteQuery Tests

    [TestMethod]
    public void ExecuteQuery_When_CommandTimeout_Non_Null_BindByName_True_ArrayBindCount_Non_Null_Should_Execute_Query()
    {
        // Arrange
        const int commandTimeout = 1;
        const bool bindByName = true;
        const string commandText = "commandText";
        const CommandType commandType = CommandType.Text;
        const int arrayBindCount = 2;
        const string funcReturnValue = "funcReturnValue";
        var dbConnectionWrapperMock = new Mock<IDbConnectionWrapper>();
        var queryMock = new Mock<IQuery<string>>();
        var dbCommandWrapperFuncMock = new Mock<Func<IDbCommandWrapper, string>>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var dbParameterMock1 = new Mock<IDbParameterWrapper>();
        var dbParameterMock2 = new Mock<IDbParameterWrapper>();
        var dbParameterMocks = new[] { dbParameterMock1.Object, dbParameterMock2.Object };
        var dbParameterMock3 = new Mock<IDbParameterWrapper>();
        Func<IDbParameterWrapper> dbParameterWrapperFunc = null!;

        _dbConnectionWrapperFactoryMock.Setup(mock => mock()).Returns(dbConnectionWrapperMock.Object);
        dbConnectionWrapperMock.Setup(mock => mock.Open());
        dbConnectionWrapperMock.Setup(mock => mock.CreateCommand()).Returns(dbCommandWrapperMock.Object);
        queryMock.SetupGet(mock => mock.CommandTimeout).Returns(commandTimeout);
        dbCommandWrapperMock.SetupGet(mock => mock.CommandTimeout);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandTimeout = It.IsAny<int>());
        queryMock.SetupGet(mock => mock.BindByName).Returns(bindByName);
        dbCommandWrapperMock.SetupSet(mock => mock.BindByName = It.IsAny<bool>());
        queryMock.Setup(mock => mock.CreateCommandText()).Returns(commandText);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandText = It.IsAny<string>());
        queryMock.SetupGet(mock => mock.CommandType).Returns(commandType);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandType = It.IsAny<CommandType>());
        queryMock.SetupGet(mock => mock.ArrayBindCount).Returns(arrayBindCount);
        dbCommandWrapperMock.SetupSet(mock => mock.ArrayBindCount = It.IsAny<int>());
        queryMock.Setup(mock => mock.Parameters(It.IsAny<Func<IDbParameterWrapper>>()))
            .Callback<Func<IDbParameterWrapper>>(func => dbParameterWrapperFunc = func)
            .Returns(dbParameterMocks);
        dbCommandWrapperMock.Setup(mock => mock.CreateParameter()).Returns(dbParameterMock3.Object);
        _targetMock.Setup(mock => mock.BindParameters(It.IsAny<IEnumerable<IDbParameterWrapper>>(), It.IsAny<IDbCommandWrapper>()));
        dbCommandWrapperFuncMock.Setup(mock => mock(It.IsAny<IDbCommandWrapper>())).Returns(funcReturnValue);

        // Act
        var result = _targetMock.Object.ExecuteQuery(queryMock.Object, dbCommandWrapperFuncMock.Object);

        // Assert
        Assert.AreEqual(funcReturnValue, result);

        _dbConnectionWrapperFactoryMock.Verify(mock => mock(), Times.Once());
        dbConnectionWrapperMock.Verify(mock => mock.Open(), Times.Once());
        dbConnectionWrapperMock.Verify(mock => mock.CreateCommand(), Times.Once());
        queryMock.VerifyGet(mock => mock.CommandTimeout, Times.Once());
        dbCommandWrapperMock.VerifyGet(mock => mock.CommandTimeout, Times.Never());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandTimeout = commandTimeout, Times.Once());
        queryMock.VerifyGet(mock => mock.BindByName, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.BindByName = bindByName, Times.Once());
        queryMock.Verify(mock => mock.CreateCommandText(), Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandText = commandText, Times.Once());
        queryMock.VerifyGet(mock => mock.CommandType, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandType = commandType, Times.Once());
        queryMock.VerifyGet(mock => mock.ArrayBindCount, Times.Exactly(2));
        dbCommandWrapperMock.VerifySet(mock => mock.ArrayBindCount = arrayBindCount, Times.Once());
        queryMock.Verify(mock => mock.Parameters(It.IsAny<Func<IDbParameterWrapper>>()), Times.Once());
        _targetMock.Verify(mock => mock.BindParameters(dbParameterMocks, dbCommandWrapperMock.Object), Times.Once());
        dbCommandWrapperFuncMock.Verify(mock => mock(dbCommandWrapperMock.Object), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.CreateParameter(), Times.Never());
        dbParameterWrapperFunc();
        dbCommandWrapperMock.Verify(mock => mock.CreateParameter(), Times.Once());
    }

    [TestMethod]
    public void ExecuteQuery_When_CommandTimeout_Null_BindByName_False_ArrayBindCount_Null_Should_Execute_Query()
    {
        // Arrange
        const int commandTimeout = 1;
        const string commandText = "commandText";
        const bool bindByName = false;
        const CommandType commandType = CommandType.Text;
        const string funcReturnValue = "funcReturnValue";
        var dbConnectionWrapperMock = new Mock<IDbConnectionWrapper>();
        var queryMock = new Mock<IQuery<string>>();
        var dbCommandWrapperFuncMock = new Mock<Func<IDbCommandWrapper, string>>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var dbParameterMock1 = new Mock<IDbParameterWrapper>();
        var dbParameterMock2 = new Mock<IDbParameterWrapper>();
        var dbParameterMocks = new[] { dbParameterMock1.Object, dbParameterMock2.Object };
        var dbParameterMock3 = new Mock<IDbParameterWrapper>();
        Func<IDbParameterWrapper> dbParameterWrapperFunc = null!;

        _dbConnectionWrapperFactoryMock.Setup(mock => mock()).Returns(dbConnectionWrapperMock.Object);
        dbConnectionWrapperMock.Setup(mock => mock.Open());
        dbConnectionWrapperMock.Setup(mock => mock.CreateCommand()).Returns(dbCommandWrapperMock.Object);
        queryMock.SetupGet(mock => mock.CommandTimeout).Returns(null as int?);
        dbCommandWrapperMock.SetupGet(mock => mock.CommandTimeout).Returns(commandTimeout);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandTimeout = It.IsAny<int>());
        queryMock.SetupGet(mock => mock.BindByName).Returns(bindByName);
        dbCommandWrapperMock.SetupSet(mock => mock.BindByName = It.IsAny<bool>());
        queryMock.Setup(mock => mock.CreateCommandText()).Returns(commandText);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandText = It.IsAny<string>());
        queryMock.SetupGet(mock => mock.CommandType).Returns(commandType);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandType = It.IsAny<CommandType>());
        queryMock.SetupGet(mock => mock.ArrayBindCount).Returns(null as int?);
        dbCommandWrapperMock.SetupSet(mock => mock.ArrayBindCount = It.IsAny<int>());
        queryMock.Setup(mock => mock.Parameters(It.IsAny<Func<IDbParameterWrapper>>()))
            .Callback<Func<IDbParameterWrapper>>(func => dbParameterWrapperFunc = func)
            .Returns(dbParameterMocks);
        dbCommandWrapperMock.Setup(mock => mock.CreateParameter()).Returns(dbParameterMock3.Object);
        _targetMock.Setup(mock => mock.BindParameters(It.IsAny<IEnumerable<IDbParameterWrapper>>(), It.IsAny<IDbCommandWrapper>()));
        dbCommandWrapperFuncMock.Setup(mock => mock(It.IsAny<IDbCommandWrapper>())).Returns(funcReturnValue);

        // Act
        var result = _targetMock.Object.ExecuteQuery(queryMock.Object, dbCommandWrapperFuncMock.Object);

        // Assert
        Assert.AreEqual(funcReturnValue, result);

        _dbConnectionWrapperFactoryMock.Verify(mock => mock(), Times.Once());
        dbConnectionWrapperMock.Verify(mock => mock.Open(), Times.Once());
        dbConnectionWrapperMock.Verify(mock => mock.CreateCommand(), Times.Once());
        queryMock.VerifyGet(mock => mock.CommandTimeout, Times.Once());
        dbCommandWrapperMock.VerifyGet(mock => mock.CommandTimeout, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandTimeout = commandTimeout, Times.Once());
        queryMock.VerifyGet(mock => mock.BindByName, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.BindByName = It.IsAny<bool>(), Times.Never());
        queryMock.Verify(mock => mock.CreateCommandText(), Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandText = commandText, Times.Once());
        queryMock.VerifyGet(mock => mock.CommandType, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandType = commandType, Times.Once());
        queryMock.VerifyGet(mock => mock.ArrayBindCount, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.ArrayBindCount = It.IsAny<int>(), Times.Never());
        queryMock.Verify(mock => mock.Parameters(It.IsAny<Func<IDbParameterWrapper>>()), Times.Once());
        _targetMock.Verify(mock => mock.BindParameters(dbParameterMocks, dbCommandWrapperMock.Object), Times.Once());
        dbCommandWrapperFuncMock.Verify(mock => mock(dbCommandWrapperMock.Object), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.CreateParameter(), Times.Never());
        dbParameterWrapperFunc();
        dbCommandWrapperMock.Verify(mock => mock.CreateParameter(), Times.Once());
    }

    #endregion ExecuteQuery Tests

    #region ExecuteQueryAsync Tests

    [TestMethod]
    public async Task ExecuteQueryAsync_When_CommandTimeout_Non_Null_BindByName_True_ArrayBindCount_Non_Null_Should_Execute_Query()
    {
        // Arrange
        const int commandTimeout = 1;
        const bool bindByName = true;
        const string commandText = "commandText";
        const CommandType commandType = CommandType.Text;
        const int arrayBindCount = 2;
        const string funcReturnValue = "funcReturnValue";
        var dbConnectionWrapperMock = new Mock<IDbConnectionWrapper>();
        var queryMock = new Mock<IQuery<string>>();
        var dbCommandWrapperFuncAsyncMock = new Mock<Func<IDbCommandWrapper, Task<string>>>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var dbParameterMock1 = new Mock<IDbParameterWrapper>();
        var dbParameterMock2 = new Mock<IDbParameterWrapper>();
        var dbParameterMocks = new[] { dbParameterMock1.Object, dbParameterMock2.Object };
        var dbParameterMock3 = new Mock<IDbParameterWrapper>();
        Func<IDbParameterWrapper> dbParameterWrapperFunc = null!;

        _dbConnectionWrapperFactoryMock.Setup(mock => mock()).Returns(dbConnectionWrapperMock.Object);
        dbConnectionWrapperMock.Setup(mock => mock.OpenAsync()).Returns(Task.CompletedTask);
        dbConnectionWrapperMock.Setup(mock => mock.CreateCommand()).Returns(dbCommandWrapperMock.Object);
        queryMock.SetupGet(mock => mock.CommandTimeout).Returns(commandTimeout);
        dbCommandWrapperMock.SetupGet(mock => mock.CommandTimeout);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandTimeout = It.IsAny<int>());
        queryMock.SetupGet(mock => mock.BindByName).Returns(bindByName);
        dbCommandWrapperMock.SetupSet(mock => mock.BindByName = It.IsAny<bool>());
        queryMock.Setup(mock => mock.CreateCommandText()).Returns(commandText);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandText = It.IsAny<string>());
        queryMock.SetupGet(mock => mock.CommandType).Returns(commandType);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandType = It.IsAny<CommandType>());
        queryMock.SetupGet(mock => mock.ArrayBindCount).Returns(arrayBindCount);
        dbCommandWrapperMock.SetupSet(mock => mock.ArrayBindCount = It.IsAny<int>());
        queryMock.Setup(mock => mock.Parameters(It.IsAny<Func<IDbParameterWrapper>>()))
            .Callback<Func<IDbParameterWrapper>>(func => dbParameterWrapperFunc = func)
            .Returns(dbParameterMocks);
        dbCommandWrapperMock.Setup(mock => mock.CreateParameter()).Returns(dbParameterMock3.Object);
        _targetMock.Setup(mock => mock.BindParameters(It.IsAny<IEnumerable<IDbParameterWrapper>>(), It.IsAny<IDbCommandWrapper>()));
        dbCommandWrapperFuncAsyncMock.Setup(mock => mock(It.IsAny<IDbCommandWrapper>())).ReturnsAsync(funcReturnValue);

        // Act
        var result = await _targetMock.Object.ExecuteQueryAsync(queryMock.Object, dbCommandWrapperFuncAsyncMock.Object);

        // Assert
        Assert.AreEqual(funcReturnValue, result);

        _dbConnectionWrapperFactoryMock.Verify(mock => mock(), Times.Once());
        dbConnectionWrapperMock.Verify(mock => mock.OpenAsync(), Times.Once());
        dbConnectionWrapperMock.Verify(mock => mock.CreateCommand(), Times.Once());
        queryMock.VerifyGet(mock => mock.CommandTimeout, Times.Once());
        dbCommandWrapperMock.VerifyGet(mock => mock.CommandTimeout, Times.Never());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandTimeout = commandTimeout, Times.Once());
        queryMock.VerifyGet(mock => mock.BindByName, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.BindByName = bindByName, Times.Once());
        queryMock.Verify(mock => mock.CreateCommandText(), Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandText = commandText, Times.Once());
        queryMock.VerifyGet(mock => mock.CommandType, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandType = commandType, Times.Once());
        queryMock.VerifyGet(mock => mock.ArrayBindCount, Times.Exactly(2));
        dbCommandWrapperMock.VerifySet(mock => mock.ArrayBindCount = arrayBindCount, Times.Once());
        queryMock.Verify(mock => mock.Parameters(It.IsAny<Func<IDbParameterWrapper>>()), Times.Once());
        _targetMock.Verify(mock => mock.BindParameters(dbParameterMocks, dbCommandWrapperMock.Object), Times.Once());
        dbCommandWrapperFuncAsyncMock.Verify(mock => mock(dbCommandWrapperMock.Object), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.CreateParameter(), Times.Never());
        dbParameterWrapperFunc();
        dbCommandWrapperMock.Verify(mock => mock.CreateParameter(), Times.Once());
    }

    [TestMethod]
    public async Task ExecuteQueryAsync_When_CommandTimeout_Null_BindByName_False_ArrayBindCount_Null_Should_Execute_Query()
    {
        // Arrange
        const int commandTimeout = 1;
        const string commandText = "commandText";
        const bool bindByName = false;
        const CommandType commandType = CommandType.Text;
        const string funcReturnValue = "funcReturnValue";
        var dbConnectionWrapperMock = new Mock<IDbConnectionWrapper>();
        var queryMock = new Mock<IQuery<string>>();
        var dbCommandWrapperFuncAsyncMock = new Mock<Func<IDbCommandWrapper, Task<string>>>();
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var dbParameterMock1 = new Mock<IDbParameterWrapper>();
        var dbParameterMock2 = new Mock<IDbParameterWrapper>();
        var dbParameterMocks = new[] { dbParameterMock1.Object, dbParameterMock2.Object };
        var dbParameterMock3 = new Mock<IDbParameterWrapper>();
        Func<IDbParameterWrapper> dbParameterWrapperFunc = null!;

        _dbConnectionWrapperFactoryMock.Setup(mock => mock()).Returns(dbConnectionWrapperMock.Object);
        dbConnectionWrapperMock.Setup(mock => mock.OpenAsync()).Returns(Task.CompletedTask);
        dbConnectionWrapperMock.Setup(mock => mock.CreateCommand()).Returns(dbCommandWrapperMock.Object);
        queryMock.SetupGet(mock => mock.CommandTimeout).Returns(null as int?);
        dbCommandWrapperMock.SetupGet(mock => mock.CommandTimeout).Returns(commandTimeout);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandTimeout = It.IsAny<int>());
        queryMock.SetupGet(mock => mock.BindByName).Returns(bindByName);
        dbCommandWrapperMock.SetupSet(mock => mock.BindByName = It.IsAny<bool>());
        queryMock.Setup(mock => mock.CreateCommandText()).Returns(commandText);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandText = It.IsAny<string>());
        queryMock.SetupGet(mock => mock.CommandType).Returns(commandType);
        dbCommandWrapperMock.SetupSet(mock => mock.CommandType = It.IsAny<CommandType>());
        queryMock.SetupGet(mock => mock.ArrayBindCount).Returns(null as int?);
        dbCommandWrapperMock.SetupSet(mock => mock.ArrayBindCount = It.IsAny<int>());
        queryMock.Setup(mock => mock.Parameters(It.IsAny<Func<IDbParameterWrapper>>()))
            .Callback<Func<IDbParameterWrapper>>(func => dbParameterWrapperFunc = func)
            .Returns(dbParameterMocks);
        dbCommandWrapperMock.Setup(mock => mock.CreateParameter()).Returns(dbParameterMock3.Object);
        _targetMock.Setup(mock => mock.BindParameters(It.IsAny<IEnumerable<IDbParameterWrapper>>(), It.IsAny<IDbCommandWrapper>()));
        dbCommandWrapperFuncAsyncMock.Setup(mock => mock(It.IsAny<IDbCommandWrapper>())).ReturnsAsync(funcReturnValue);

        // Act
        var result = await _targetMock.Object.ExecuteQueryAsync(queryMock.Object, dbCommandWrapperFuncAsyncMock.Object);

        // Assert
        Assert.AreEqual(funcReturnValue, result);

        _dbConnectionWrapperFactoryMock.Verify(mock => mock(), Times.Once());
        dbConnectionWrapperMock.Verify(mock => mock.OpenAsync(), Times.Once());
        dbConnectionWrapperMock.Verify(mock => mock.CreateCommand(), Times.Once());
        queryMock.VerifyGet(mock => mock.CommandTimeout, Times.Once());
        dbCommandWrapperMock.VerifyGet(mock => mock.CommandTimeout, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandTimeout = commandTimeout, Times.Once());
        queryMock.VerifyGet(mock => mock.BindByName, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.BindByName = It.IsAny<bool>(), Times.Never());
        queryMock.Verify(mock => mock.CreateCommandText(), Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandText = commandText, Times.Once());
        queryMock.VerifyGet(mock => mock.CommandType, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.CommandType = commandType, Times.Once());
        queryMock.VerifyGet(mock => mock.ArrayBindCount, Times.Once());
        dbCommandWrapperMock.VerifySet(mock => mock.ArrayBindCount = It.IsAny<int>(), Times.Never());
        queryMock.Verify(mock => mock.Parameters(It.IsAny<Func<IDbParameterWrapper>>()), Times.Once());
        _targetMock.Verify(mock => mock.BindParameters(dbParameterMocks, dbCommandWrapperMock.Object), Times.Once());
        dbCommandWrapperFuncAsyncMock.Verify(mock => mock(dbCommandWrapperMock.Object), Times.Once());
        dbCommandWrapperMock.Verify(mock => mock.CreateParameter(), Times.Never());
        dbParameterWrapperFunc();
        dbCommandWrapperMock.Verify(mock => mock.CreateParameter(), Times.Once());
    }

    #endregion ExecuteQueryAsync Tests

    #region BindParameters Tests

    [TestMethod]
    public void BindParameters_When_DbDataParameters_Null_Should_Not_Add_Parameters()
    {
        // Arrange
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var dbDataParametersMock = new Mock<IDbParameterCollectionWrapper>();
        dbCommandWrapperMock.SetupGet(mock => mock.Parameters).Returns(dbDataParametersMock.Object);
        dbDataParametersMock.Setup(mock => mock.Add(It.IsAny<IDbParameterWrapper>()));

        // Act
        _targetMock.Object.BindParameters(null!, dbCommandWrapperMock.Object);

        // Assert
        dbCommandWrapperMock.VerifyGet(mock => mock.Parameters, Times.Never());
        dbDataParametersMock.Verify(mock => mock.Add(It.IsAny<IDbParameterWrapper>()), Times.Never());
    }

    [TestMethod]
    public void BindParameters_When_DbDataParameters_Not_Null_Should_Add_Parameters()
    {
        // Arrange
        var dbParameterWrapperMock1 = new Mock<IDbParameterWrapper>();
        var dbParameterWrapperMock2 = new Mock<IDbParameterWrapper>();
        var dbDataParameters = new List<IDbParameterWrapper> { dbParameterWrapperMock1.Object, dbParameterWrapperMock2.Object };
        var dbCommandWrapperMock = new Mock<IDbCommandWrapper>();
        var dbDataParametersMock = new Mock<IDbParameterCollectionWrapper>();
        dbCommandWrapperMock.SetupGet(mock => mock.Parameters).Returns(dbDataParametersMock.Object);
        dbDataParametersMock.Setup(mock => mock.Add(It.IsAny<IDbParameterWrapper>()));

        // Act
        _targetMock.Object.BindParameters(dbDataParameters, dbCommandWrapperMock.Object);

        // Assert
        dbCommandWrapperMock.VerifyGet(mock => mock.Parameters, Times.Exactly(2));
        dbDataParametersMock.Verify(mock => mock.Add(It.IsAny<IDbParameterWrapper>()), Times.Exactly(2));
        dbDataParametersMock.Verify(mock => mock.Add(dbParameterWrapperMock1.Object), Times.Once());
        dbDataParametersMock.Verify(mock => mock.Add(dbParameterWrapperMock2.Object), Times.Once());
    }

    #endregion BindParameters Tests
}