using DevCore.TfsNotificationRelay.Notifications;
using Microsoft.TeamFoundation.Framework.Server;
using TFVC = Microsoft.TeamFoundation.VersionControl.Server;

namespace DevCore.TfsNotificationRelay.EventHandlers
{
    class ShelvesetCodeReviewHandler : BaseHandler<TFVC.ShelvesetNotification>
    {
        protected override INotification CreateNotification(TeamFoundationRequestContext requestContext,
            TFVC.ShelvesetNotification notificationEventArgs, int maxLines)
        {
            var notification = new ShelvesetCodeReviewNotification()
            {
                TeamProjectCollection = requestContext.ServiceHost.Name,
                Comment = notificationEventArgs.Comment,
                Name = notificationEventArgs.ShelvesetName,
                Owner = notificationEventArgs.ShelvesetOwner,
                OwnerName = notificationEventArgs.ShelvesetOwner.DisplayName
            };
            
            return notification;
        }
    }
}
