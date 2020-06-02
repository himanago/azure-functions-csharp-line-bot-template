using LineBotTemplate.Configurations;
using LineDC.Messaging;
using LineDC.Messaging.Webhooks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(LineBotTemplate.Startup))]
namespace LineBotTemplate
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", true)
                .AddEnvironmentVariables()
                .Build();

            var settings = config.GetSection(nameof(LineBotSettings)).Get<LineBotSettings>();

            builder.Services
                .AddSingleton(settings)
                .AddSingleton<ILineMessagingClient>(_ => LineMessagingClient.Create(settings.ChannelAccessToken))
                .AddSingleton<IWebhookApplication, LineBotApp>();
        }
    }
}
