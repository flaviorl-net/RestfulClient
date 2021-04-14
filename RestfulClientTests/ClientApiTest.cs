using Restful;
using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace RestfulClientTests
{
    public class ClientApiTest : Client
    {
        public ClientApiTest() : base("http://localhost:49650/api/cliente") {}

        [Fact]
        public async void GetAllAsync_Test()
        {
            (await GetAllAsync<ClienteRetorno>())
                .Should()
                .NotBeEmpty();
        }

        [Fact]
        public async void GetAsync_Test()
        {
            var cliente = (await GetAllAsync<ClienteRetorno>()).FirstOrDefault();

            //Act
            var resp = await GetAsync<ClienteRetorno>(cliente.id.ToString());

            resp.Should().NotBeNull();

            resp.id.Should().Be(cliente.id);
            resp.nome.Should().Be(cliente.nome);
            resp.sobrenome.Should().Be(cliente.sobrenome);
            resp.dataNasc.ToShortDateString().Should().Be(cliente.dataNasc.ToShortDateString());
        }

        [Fact]
        public async void GetStreamAsync_Test()
        {
            (await GetAllStreamAsync<Cliente>())
                .Should()
                .NotBeEmpty();
        }

        [Fact]
        public async void GetByteArrayAsync_Test()
        {
            (await GetAllByteArrayAsync<Cliente>())
                .Should()
                .NotBeEmpty();
        }

        [Fact]
        public async void PostAsync_Test()
        {
            await PostAsync(new Cliente()
            {
                nome = "Flavio",
                sobrenome = "Lima",
                dataNasc = DateTime.Now
            });
        }

        [Fact]
        public async void PostAsyncReturnType_Test()
        {
            var resp  = await PostAsync<Cliente, ClienteRetorno>(new Cliente()
            {
                nome = "Flavio",
                sobrenome = "Lima",
                dataNasc = DateTime.Now
            });

            resp.id.Should().BeGreaterThan(1);
            resp.nome.Should().Be("Flavio");
            resp.sobrenome.Should().Be("Lima");
            resp.dataNasc.ToShortDateString().Should().Be(DateTime.Now.ToShortDateString());
        }

        [Fact]
        public async void PutAsync_Test()
        {
            var cliente = (await GetAllAsync<ClienteRetorno>()).FirstOrDefault();

            //Act
            await PutAsync<Cliente, ClienteRetorno>(new Cliente()
            {
                nome = "Flavio",
                sobrenome = "Ribeiro Lima",
                dataNasc = DateTime.Now
            }, cliente.id.ToString());
        }

        [Fact]
        public async void PutAsyncReturnType_Test()
        {
            var cliente = (await GetAllAsync<ClienteRetorno>()).FirstOrDefault();

            //Act
            var resp = await PutAsync<Cliente, ClienteRetorno>(new Cliente()
            {
                nome = "Flavio",
                sobrenome = "Ribeiro Lima",
                dataNasc = DateTime.Now
            }, cliente.id.ToString());

            resp.id.Should().Be(cliente.id);
            resp.nome.Should().Be("Flavio");
            resp.sobrenome.Should().Be("Ribeiro Lima");
            resp.dataNasc.ToShortDateString().Should().Be(DateTime.Now.ToShortDateString());
        }

        [Fact]
        public async void DeleteAsync_Test()
        {
            var cliente = (await GetAllAsync<ClienteRetorno>()).FirstOrDefault();

            //Act
            var resp = await DeleteAsync<ClienteRetorno>(cliente.id.ToString());

            resp.id.Should().Be(cliente.id);
            resp.nome.Should().Be(cliente.nome);
            resp.sobrenome.Should().Be(cliente.sobrenome);
            resp.dataNasc.ToShortDateString().Should().Be(cliente.dataNasc.ToShortDateString());
        }

    }
}
