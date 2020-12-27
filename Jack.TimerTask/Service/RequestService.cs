using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;


namespace Jack.TimerTask.Service
{
    public class RequestService : IRequestService
    {
        private readonly IHttpClientFactory _clientFactory;
        public RequestService(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }

        public async Task<TResponse> RequestPost<TRequest, TResponse>(TRequest request, string uri)
            where TRequest : class, new()
            where TResponse : class, new()
        {
            var client = _clientFactory.CreateClient();

             await Task.Run(()=> { });
            return default;

            //var json = JsonSerializer.Serialize(request, new ja { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            //using (var tracer = new Tracer(uri))
            //{
            //    var policy = Policy.Handle<Exception>().CircuitBreaker(3, TimeSpan.FromSeconds(3)); // 定义重试(重试3次间隔3秒)
            //    return await policy.Execute(async () =>
            //    {
            //        try
            //        {
            //            tracer.LogRequest(json);
            //            var response = await client.PostAsync(uri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            //            var content = await response.Content.ReadAsStringAsync();
            //            tracer.LogResponse(content);

            //            if (response.StatusCode == HttpStatusCode.OK)
            //            {
            //                return JsonConvert.DeserializeObject<TResponse>(content);
            //            }
            //            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            //            {
            //                throw new Exception("访问被拒绝，授权失败");
            //            }
            //            else
            //            {
            //                throw new Exception("远程服务器返回预期外的数据");
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            tracer.LogException(ex);
            //            throw new Exception("LDAP-接口请求失败," + ex.Message);
            //        }
            //    });
            //}
        }
    }
}
