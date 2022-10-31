using TestBackgroundThread.Models.Generated;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.HostedServices;

namespace TestBackgroundThread.App_Plugins.RecurringService
{
    public class SyncTask : RecurringHostedServiceBase
    {
        readonly IUmbracoContextFactory contextFactory;

        //Automatically start with 10 seconds delay
        public SyncTask(ILogger<SyncTask>? logger, IUmbracoContextFactory contextFactory) : base(logger, TimeSpan.FromDays(1), TimeSpan.FromSeconds(10))
        {
            this.contextFactory = contextFactory;
        }

        public override async Task PerformExecuteAsync(object? state)
        {
            //Culture is the same as default language - in my case "en-GB"
            var culture = Thread.CurrentThread.CurrentCulture;
            //Ensuring UICulture is the same a Culture (on this machine I will get CurrentUICulture = en-US)
            Thread.CurrentThread.CurrentUICulture = culture;

            using var context = contextFactory.EnsureUmbracoContext();
            //if 'Allow vary by culture' is true then homepage will be null
            //if 'Allow vary by culture' is false then homepage will not be null and it will contain name & title etc.
            var homepageDefaultLanguage = context.UmbracoContext?.Content?.GetAtRoot().OfType<HomePage1>().FirstOrDefault();
            
            var homepage = context.UmbracoContext?.Content?.GetAtRoot("*").OfType<HomePage1>().FirstOrDefault(); 
            //if 'Allow vary by culture' is true you can get homepage!=null but it's name or title will be null
            var homepagName= homepage?.Name;
            var homepageTitle= homepage?.Title;

            //Content which does not have 'Allow vary by culture' works as expected
            var shared = context.UmbracoContext?.Content?.GetAtRoot().DescendantsOrSelf<IPublishedContent>().Select(a => a.Name).ToArray();
        }
    }
}
