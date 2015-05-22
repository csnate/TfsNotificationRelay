using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCore.TfsNotificationRelay.Configuration;
using Microsoft.TeamFoundation.Server.Core;

namespace DevCore.TfsNotificationRelay.Notifications
{
    public class ShelvesetCodeReviewNotification : BaseNotification
    {
        public string Comment { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public TeamFoundationIdentity Owner { get; set; }
        //public string[] Links { get; set; } 
        public bool IsNew { get; set; }

        private string FormatAction(Configuration.BotElement bot)
        {
            return IsNew ? bot.Text.Created : bot.Text.Updated;
        }

        public override IList<string> ToMessage(BotElement bot, Func<string, string> transform)
        {
            var formatter = new
            {
                TeamProjectCollection = transform(TeamProjectCollection),
                Name = transform(Name),
                OwnerName = transform(OwnerName),
                Action = FormatAction(bot)
            };
            var lines = new List<string>();
            lines.Add(bot.Text.ShelvesetCodeReview.FormatWith(formatter));
            if (!String.IsNullOrWhiteSpace(Comment))
            {
                lines.Add(String.Format("Comment: {0}", Comment));
            }
            //if (Links.Any())
            //{
            //    lines.Add("Links:");
            //    lines.AddRange(Links);
            //}
            return lines;
        }

        public override bool IsMatch(string collection, EventRuleCollection eventRules)
        {
            var rule = eventRules
                .FirstOrDefault(a => a.Events.HasFlag(TfsEvents.ShelvesetCodeReview)
                                     && collection.IsMatchOrNoPattern(a.TeamProjectCollection)
                                     //&& Owner.MemberOf.Any(b => b.Identifier.IsMatchOrNoPattern(a.TeamProject))
                                     && Name.Contains("CodeReview_")
                );
            return rule != null && rule.Notify;
        }
    }
}
