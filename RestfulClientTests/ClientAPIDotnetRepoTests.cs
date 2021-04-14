using FluentAssertions;
using Restful;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace RestfulClientTests
{
    public class ClientAPIDotnetRepoTests : Client
    {
        public ClientAPIDotnetRepoTests() : base("https://api.github.com/orgs/dotnet/repos")
        {
            ConfigureHttpClient();
        }

        protected override void ConfigureHttpClient()
        {
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            HttpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        [Fact]
        public async Task GetAllAsync_Test()
        {
            (await GetAllAsync<Repository>())
                .Should()
                .NotBeEmpty();
        }

        [Fact]
        public async Task GetAllByteArrayAsync_Test()
        {
            (await GetAllByteArrayAsync<Repository>())
                .Should()
                .NotBeEmpty();
        }

        [Fact]
        public async Task GetAllStreamAsync_Test()
        {
            (await GetAllStreamAsync<Repository>())
                .Should()
                .NotBeEmpty();
        }

    }
}
