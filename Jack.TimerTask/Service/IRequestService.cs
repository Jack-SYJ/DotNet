using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jack.TimerTask.Service
{
    public interface IRequestService
    {
        Task<TResponse> RequestPost<TRequest, TResponse>(TRequest request, string uri) where TRequest : class, new() where TResponse : class, new();
    }
}
