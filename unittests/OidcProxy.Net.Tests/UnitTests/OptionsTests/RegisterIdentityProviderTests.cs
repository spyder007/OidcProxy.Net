using FluentAssertions;
using OidcProxy.Net.IdentityProviders;
using OidcProxy.Net.ModuleInitializers;

namespace OidcProxy.Net.Tests.OptionsTests;

public class RegisterIdentityProviderTests
{
    [Fact]
    public void WhenRegisteringEndpointNameTwice_ShouldThrowNotSupportedException()
    {
        // Arrange
        var sut = new ProxyOptions();
        sut.RegisterIdentityProvider<TestIdp1, TestConfig>(new TestConfig(), "test");
        
        // Act
        var actual = () => sut.RegisterIdentityProvider<TestIdp2, TestConfig2>(new TestConfig2(), "test");

        // Assert
        actual.Should().Throw<NotSupportedException>();
    }
    
    [Fact]
    public void WhenRegisteringTypeTwice_ShouldThrowNotSupportedException()
    {
        // Arrange
        var sut = new ProxyOptions();
        sut.RegisterIdentityProvider<TestIdp1, TestConfig>(new TestConfig(), "test1");
        
        // Act
        Action actual = () => sut.RegisterIdentityProvider<TestIdp1, TestConfig2>(new TestConfig2(), "test2");

        // Assert
        actual.Should().Throw<NotSupportedException>();
    }

    [Fact]
    public void WhenRegisteringOptionsTypeTwice_ShouldThrowNotSupportedException()
    {
        // Arrange
        var sut = new ProxyOptions();
        sut.RegisterIdentityProvider<TestIdp1, TestConfig>(new TestConfig(), "test1");
        
        // Act
        Action actual = () => sut.RegisterIdentityProvider<TestIdp2, TestConfig>(new TestConfig(), "test2");

        // Assert
        actual.Should().Throw<NotSupportedException>();
    }
    
    private class TestConfig
    {
    }

    private class TestConfig2
    {
    }
    
    private class TestIdp2 : TestIdp1
    {
    }

    private class TestIdp1 : IIdentityProvider
    {
        public Task<AuthorizeRequest> GetAuthorizeUrlAsync(string redirectUri)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponse> GetTokenAsync(string redirectUri, string code, string? codeVerifier, string traceIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken, string traceIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task RevokeAsync(string token, string traceIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<Uri> GetEndSessionEndpointAsync(string? idToken, string baseAddress)
        {
            throw new NotImplementedException();
        }
    }
}