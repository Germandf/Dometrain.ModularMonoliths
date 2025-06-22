using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.Endpoints;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookListTests(Fixture fixture) : TestBase<Fixture>
{
    [Fact]
    public async Task ReturnsThreeBooksAsync()
    {
        var testResult = await fixture.Client.GETAsync<ListBooks, ListBooksResponse>();
        testResult.Response.EnsureSuccessStatusCode();
        testResult.Result.Books.Count.Should().Be(3);
    }
}
