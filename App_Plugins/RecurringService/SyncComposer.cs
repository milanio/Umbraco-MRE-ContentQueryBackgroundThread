using Umbraco.Cms.Core.Composing;

namespace TestBackgroundThread.App_Plugins.RecurringService
{
    public class SyncComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddHostedService<SyncTask>();
        }
    }
}
