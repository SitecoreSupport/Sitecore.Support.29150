namespace Sitecore.Support.Publishing.Service.Delivery
{
  using Diagnostics;
  using Events;
  using Framework.Conditions;
  using Globalization;
  using Sitecore.Publishing.Diagnostics;
  using Sitecore.Publishing.Service.Abstractions.Events;
  using System;
  using System.Linq;

  public class TargetDataCacheClearHandler : Sitecore.Publishing.Service.Delivery.TargetDataCacheClearHandler
  {
    public void ResetTranslateCache(object sender, EventArgs args)
    {
      Condition.Requires<object>(sender, "sender").IsNotNull<object>();
      Condition.Requires<EventArgs>(args, "args").IsNotNull<EventArgs>();
      Condition.Requires<object>(sender, "sender").IsNotNull<object>();
      Condition.Requires<EventArgs>(args, "args").IsNotNull<EventArgs>();
      SitecoreEventArgs sitecoreEventArgs = args as SitecoreEventArgs;
      if (((sitecoreEventArgs != null) ? sitecoreEventArgs.Parameters : null) == null || !sitecoreEventArgs.Parameters.Any<object>() || !(sitecoreEventArgs.Parameters[0] is TargetDataCacheClearEventArgs))
      {
        Log.Error("Attempted to reset the Translate cache at the end of a bulk publish, but the event arguments were not valid.", this);
        return;
      }

      try
      {
        ResetCache();
      }
      catch (Exception exception)
      {
        PublishingLog.Error("There was an error reseting Translate cache at the end of the publish job", exception);
      }
    }

    private void ResetCache()
    {
      PublishingLog.Info("Starting to reset Translate cache", null);

      Translate.ResetCache();

      PublishingLog.Info("Finished reseting Translate cache", null);
    }
  }
}