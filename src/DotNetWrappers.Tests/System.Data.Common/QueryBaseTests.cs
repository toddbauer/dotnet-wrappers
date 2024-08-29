using System.Data;
using System.Diagnostics.CodeAnalysis;
using DotNetWrappers.System.Data.Common;
using Moq;

namespace DotNetWrappers.Tests.System.Data.Common;

[TestClass]
[ExcludeFromCodeCoverage]
public class QueryBaseTests
{
    // ReSharper disable once NotAccessedField.Local
    private QueryBaseTest _target = null!;
    private Mock<QueryBaseTest> _targetMock = null!;

    #region Test Initialize and Cleanup

    [TestInitialize]
    public void TestInitialize()
    {
        _target = new QueryBaseTest();
        _targetMock = new Mock<QueryBaseTest> {CallBase = true};
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _targetMock = null!;
        _target = null!;
    }

    #endregion Test Initialize and Cleanup

    #region Property Tests

    [TestMethod]
    public void CommandTimeout_Getter_Should_Return_Null()
    {
        // Arrange

        // Act
        var result = _targetMock.Object.CommandTimeout;

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void BindByName_Getter_Should_Return_True()
    {
        // Arrange

        // Act
        var result = _targetMock.Object.BindByName;

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ArrayBindCount_Getter_Should_Return_0()
    {
        // Arrange

        // Act
        var result = _targetMock.Object.ArrayBindCount;

        // Assert
        Assert.AreEqual(0, result);
    }

    #endregion Property Tests

    #region CreateCommandTest Tests

    [TestMethod]
    public void CreateCommandText_Should_Create_Command_Text()
    {
        // Arrange

        // Act
        var result = _targetMock.Object.CreateCommandText();

        // Assert
        Assert.AreEqual($"{_targetMock.Object.Sql} {_targetMock.Object.Where} {_targetMock.Object.OrderBy}".Trim(), result);
    }

    #endregion CreateCommandTest Tests

    #region CreateParameters Tests

    [TestMethod]
    public void CreateParameters_When_DbType_Null_Should_Create_Parameters()
    {
        // Arrange
        const string parameterName = "parameterName";
        const string parameterValue = "parameterValue";
        var dbType = (DbType?)null!;
        var parameterFactoryMock = new Mock<Func<IDbParameterWrapper>>();
        var dbParameterWrapperMock = new Mock<IDbParameterWrapper>();

        parameterFactoryMock.Setup(mock => mock()).Returns(dbParameterWrapperMock.Object);
        dbParameterWrapperMock.Setup(mock => mock.ParameterName);
        dbParameterWrapperMock.Setup(mock => mock.Value);
        dbParameterWrapperMock.Setup(mock => mock.DbType);

        // Act
        var result = _targetMock.Object.CreateParameter(parameterFactoryMock.Object, parameterName, parameterValue, dbType);

        // Assert
        Assert.AreSame(dbParameterWrapperMock.Object, result);

        parameterFactoryMock.Verify(mock => mock(), Times.Once());
        dbParameterWrapperMock.VerifySet(mock => mock.ParameterName = parameterName, Times.Once());
        dbParameterWrapperMock.VerifySet(mock => mock.Value = parameterValue, Times.Once());
        dbParameterWrapperMock.VerifySet(mock => mock.DbType = It.IsAny<DbType>(), Times.Never());
    }

    [TestMethod]
    public void CreateParameters_When_DbType_Not_Null_Should_Create_Parameters()
    {
        // Arrange
        const string parameterName = "parameterName";
        const string parameterValue = "parameterValue";
        const DbType dbType = DbType.String;
        var parameterFactoryMock = new Mock<Func<IDbParameterWrapper>>();
        var dbParameterWrapperMock = new Mock<IDbParameterWrapper>();

        parameterFactoryMock.Setup(mock => mock()).Returns(dbParameterWrapperMock.Object);
        dbParameterWrapperMock.Setup(mock => mock.ParameterName);
        dbParameterWrapperMock.Setup(mock => mock.Value);
        dbParameterWrapperMock.Setup(mock => mock.DbType);

        // Act
        var result = _targetMock.Object.CreateParameter(parameterFactoryMock.Object, parameterName, parameterValue, dbType);

        // Assert
        Assert.AreSame(dbParameterWrapperMock.Object, result);

        parameterFactoryMock.Verify(mock => mock(), Times.Once());
        dbParameterWrapperMock.VerifySet(mock => mock.ParameterName = parameterName, Times.Once());
        dbParameterWrapperMock.VerifySet(mock => mock.Value = parameterValue, Times.Once());
        dbParameterWrapperMock.VerifySet(mock => mock.DbType = dbType, Times.Once());
    }

    #endregion CreateParameters Tests
}

[ExcludeFromCodeCoverage]
public class QueryBaseTest : QueryBase<string>
{
    public override CommandType CommandType => CommandType.Text;
    public override string ConnectionString => "ConnectionString";
    public override string Sql => "Sql";
    public override string Where => "Where";
    public override string OrderBy => "OrderBy ";
    public override IEnumerable<IDbParameterWrapper> Parameters(Func<IDbParameterWrapper> parameterFactory)
    {
        throw new NotImplementedException();
    }
}