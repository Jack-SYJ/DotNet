using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Jack.TimerTask.Extentions
{
    public static class ConfigurationExtention
    {
        public static IConfigurationBuilder AddJsonFileEx(this IConfigurationBuilder builder, string path)
        {
            return builder.AddJsonFileEx(builder.GetFileProvider(), path, optional: false, reloadOnChange: false);
        }

        public static IConfigurationBuilder AddJsonFileEx(this IConfigurationBuilder builder, string path, bool optional)
        {
            return builder.AddJsonFileEx(builder.GetFileProvider(), path, optional, reloadOnChange: false);
        }

        public static IConfigurationBuilder AddJsonFileEx(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return builder.AddJsonFileEx(builder.GetFileProvider(), path, optional, reloadOnChange);
        }


        public static IConfigurationBuilder AddJsonFileEx(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
        {
            if (provider == null && Path.IsPathRooted(path))
            {
                provider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                path = Path.GetFileName(path);
            }

            JsonConfigurationSource source = new JsonConfigurationSource
            {
                FileProvider = provider,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };
            builder.Add(source);
            return builder;
        }
    }
}
