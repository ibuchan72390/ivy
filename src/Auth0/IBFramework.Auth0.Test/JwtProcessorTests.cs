namespace IBFramework.Auth0.Test
{
    /*
     * To set up a test for this class, please use the tool at
     * http://jwt.io
     * 
     * This now just got a lot more complicated because we had to switch to RS256 from HS256.
     * This means we're now going to be using an asymmetric key loaded from a remote resource.
     * This will be leveraging a number of pieces of the ASP.NET System.Security namespace.
     * I'm not going to say I understand it all, but we can at least mock it out into some 
     * understandable components where we can verify attribute assignments occur correctly.
     */
    //[Collection(CollectionDefinitionNames.DatabaseCollection)]
    //public class JwtProcessorTests : ApiDataTestBase
    //{
    //    #region Variables & Constants

    //    private readonly IJwtProcessor _sut;

    //    private readonly Mock<IAuth0ConfigurationProvider> _mockConfigProvider;

    //    private const string clientSecret = "secret";

    //    #endregion

    //    #region SetUp & TearDown

    //    public JwtProcessorTests()
    //    {
    //        JWT.JsonWebToken.JsonSerializer = new JwtSerializer();

    //        var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

    //        base.ConfigureContainer(containerGen);

    //        _mockConfigProvider = new Mock<IAuth0ConfigurationProvider>();

    //        _mockConfigProvider.Setup(x => x.ClientSecret).Returns(clientSecret);

    //        containerGen.RegisterInstance<IAuth0ConfigurationProvider>(_mockConfigProvider.Object);

    //        var container = containerGen.GenerateContainer();

    //        _sut = container.Resolve<IJwtProcessor>();
    //    }

    //    #endregion

    //    #region Tests

    //    [Fact]
    //    public void Decode_Jwt_Decodes_As_Expected_With_Client_Secret()
    //    {
    //        const string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ";

    //        var result = _sut.ReturnsAsync(token);

    //        Assert.Equal("{\"sub\":\"1234567890\",\"name\":\"John Doe\",\"admin\":true}", result);
    //    }

    //    #endregion

    //}
}
