using ClienteAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClienteAPI.Data
{
    public static class ClienteData
    {
        public static List<ClienteModel> Clientes { get; set; } = new List<ClienteModel>()
        {
            new ClienteModel(){ ID = 1, Nome = "Ana", Sobrenome = "Silva", DataNasc = DateTime.Now.AddYears(-25) },
            new ClienteModel(){ ID = 2, Nome = "Bia", Sobrenome = "Lima", DataNasc = DateTime.Now.AddYears(-27) },
            new ClienteModel(){ ID = 3, Nome = "Mel", Sobrenome = "Oliveira", DataNasc = DateTime.Now.AddYears(-28) },
            new ClienteModel(){ ID = 4, Nome = "Sol", Sobrenome = "Riberio", DataNasc = DateTime.Now.AddYears(-30) }
        };

        public static List<ClienteModel> GetAll()
        {
            return Clientes;
        }

        public static ClienteModel Get(int id)
        {
            return Clientes.FirstOrDefault(x => x.ID == id);
        }

        public static void Add(ClienteModel cliente)
        {
            cliente.ID = GetAll().Max(x => x.ID) + 1;
            Clientes.Add(cliente);
        }

        public static ClienteModel AddReturn(ClienteModel cliente)
        {
            cliente.ID = GetAll().Max(x => x.ID) + 1;
            Clientes.Add(cliente);

            return GetAll()
                .OrderByDescending(x => x.ID)
                .FirstOrDefault();
        }

        public static void Remove(ClienteModel cliente)
        {
            Clientes.Remove(cliente);
        }

        public static ClienteModel Update(int id, ClienteModel cliente)
        {
            var cli = Get(id);
            cli.Nome = cliente.Nome;
            cli.Sobrenome = cliente.Sobrenome;
            cli.DataNasc = cliente.DataNasc;

            return cli;
        }
    }
}
