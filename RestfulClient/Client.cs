using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newton = Newtonsoft.Json;

namespace Restful
{
    public class Client
    {
        protected virtual HttpClient HttpClient { get; set; } = new HttpClient();

        private string URLApi { get; set; }

        public Client(string urlAPI)
        {
            URLApi = urlAPI;
            ConfigureHttpClient();
        }

        protected virtual void ConfigureHttpClient()
        {
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<T>> GetAllStreamAsync<T>() =>
            await JsonSerializer.DeserializeAsync<List<T>>(await HttpClient.GetStreamAsync(URLApi));

        public async Task<T> GetStreamAsync<T>(string param = "") =>
            await JsonSerializer.DeserializeAsync<T>(await HttpClient.GetStreamAsync($"{URLApi}/{param}"));

        public async Task<IEnumerable<T>> GetAllAsync<T>() =>
            Newton.JsonConvert.DeserializeObject<List<T>>(
                (await HttpClient.GetAsync(URLApi))
                .Content
                .ReadAsStringAsync()
                .Result);

        public async Task<T> GetAsync<T>(string param = "") =>
            Newton.JsonConvert.DeserializeObject<T>(
                (await HttpClient.GetAsync($"{URLApi}/{param}"))
                .Content
                .ReadAsStringAsync()
                .Result);

        public async Task<IEnumerable<T>> GetAllByteArrayAsync<T>() =>
            JsonSerializer.Deserialize<List<T>>(await HttpClient.GetByteArrayAsync(URLApi));

        public async Task<T> GetByteArrayAsync<T>(string param = "") =>
            JsonSerializer.Deserialize<T>(await HttpClient.GetByteArrayAsync($"{URLApi}/{param}"));


        public async Task PostAsync<T>(T model)
        {
            var stringPayload = Newton.JsonConvert.SerializeObject(model);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            await HttpClient.PostAsync(URLApi, content);
        }

        public async Task<TReturn> PostAsync<T, TReturn>(T model)
        {
            var stringPayload = Newton.JsonConvert.SerializeObject(model);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(URLApi, content);
            
            TReturn returnType = default;

            if (response.Content != null)
            {
                returnType = Newton.JsonConvert.DeserializeObject<TReturn>(await response.Content.ReadAsStringAsync());
            }

            return returnType;
        }

        public async Task PutAsync<T>(T model)
        {
            var stringPayload = Newton.JsonConvert.SerializeObject(model);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            await HttpClient.PutAsync(URLApi, content);
        }

        public async Task<TReturn> PutAsync<T, TReturn>(T model, string param = "")
        {
            var stringPayload = Newton.JsonConvert.SerializeObject(model);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync($"{URLApi}/{param}", content);

            TReturn returnType = default;

            if (response.Content != null)
            {
                returnType = Newton.JsonConvert.DeserializeObject<TReturn>(await response.Content.ReadAsStringAsync());
            }

            return returnType;
        }

        public async Task<TReturn> DeleteAsync<TReturn>(string param = "")
        {
            var response = await HttpClient.DeleteAsync($"{URLApi}/{param}");

            TReturn returnType = default;

            if (response.Content != null)
            {
                returnType = Newton.JsonConvert.DeserializeObject<TReturn>(await response.Content.ReadAsStringAsync());
            }

            return returnType;
        }
    }
}
