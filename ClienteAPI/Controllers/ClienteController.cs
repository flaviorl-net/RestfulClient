using ClienteAPI.Data;
using ClienteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ClienteAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        [HttpGet]
        public IEnumerable<ClienteModel> Get()
        {
            return ClienteData.GetAll();
        }

        [HttpGet("{id}")]
        public ClienteModel Get(int id)
        {
            return ClienteData.Get(id);
        }

        [HttpPost]
        public ClienteModel Post([FromBody] ClienteModel cliente)
        {
            return ClienteData.AddReturn(cliente);
        }

        [HttpPut("{id}")]
        public ClienteModel Put(int id, [FromBody] ClienteModel cliente)
        {
            return ClienteData.Update(id, cliente);
        }

        [HttpDelete("{id}")]
        public ClienteModel Delete(int id)
        {
            var cliente = Get(id);
            if (cliente == null)
                return null;

            ClienteData.Remove(cliente);
            return cliente;
        }
    }
}
