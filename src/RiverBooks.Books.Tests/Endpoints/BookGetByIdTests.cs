using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.Endpoints;
using RiverBooks.Books.UseCases;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookGetByIdTests(Fixture fixture) : TestBase<Fixture>
{
    [Theory]
    [InlineData("d1b2c3f4-5e6f-4a8b-9c0d-e1f2a3b4c5d6", "The Fellowship of the Ring")]
    [InlineData("a1b2c3d4-e5f6-4a8b-9c0d-e1f2a3b4c5d6", "The Two Towers")]
    [InlineData("f1e2d3c4-b5a6-4b8c-9d0e-f1a2b3c4d5e6", "The Return of the King")]
    public async Task ReturnsExpectedBookGivenIdAsync(string id, string expectedTitle)
    {
        var guid = Guid.Parse(id);
        var request = new GetBookByIdRequest(guid);
        var testResult = await fixture.Client.GETAsync<GetBookById, GetBookByIdRequest, BookDto>(request);
        testResult.Response.EnsureSuccessStatusCode();
        testResult.Result.Title.Should().Be(expectedTitle);
    }
}
