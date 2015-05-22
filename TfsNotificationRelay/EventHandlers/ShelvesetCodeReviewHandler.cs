using System;
using System.Linq;
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
            //var links = notificationEventArgs.ShelvedItems != null
            //    ? notificationEventArgs.ShelvedItems.Where(a => !String.IsNullOrWhiteSpace(a)).Select(a => a).ToArray()
            //    : new string[0];
            var notification = new ShelvesetCodeReviewNotification()
            {
                //Links = links,
                IsNew = notificationEventArgs.ShelvesetNotificationType == TFVC.ShelvesetNotificationType.Create,
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
