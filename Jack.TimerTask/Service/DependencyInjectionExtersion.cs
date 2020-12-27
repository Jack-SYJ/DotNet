using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Jack.TimerTask.Service
{
    public static partial class DependencyInjectionExtersion

    {
        public static IServiceCollection AddHttpUserCenterService(this IServiceCollection services)
        {
            services.AddTransient<ILdapService, LdapService>();
            services.AddTransient<IRequestService, RequestService>();
            return services;
        }
    }
}
