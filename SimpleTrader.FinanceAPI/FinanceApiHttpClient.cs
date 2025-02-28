using Newtonsoft.Json;

namespace SimpleTrader.FinanceAPI
{
    public class FinanceApiHttpClient : HttpClient
    {
        public FinanceApiHttpClient()
        {
            BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            using HttpResponseMessage response = await GetAsync(uri + "?apikey=UrPfNJeuuJJltcQPYjZBzpW6ew77vJwf");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                throw new Exception("Error Occured in API Call");
            }
        }

    }
}
