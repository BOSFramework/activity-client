using System;
using System.Collections.Generic;
using System.Text;

namespace BOS.Activity.Client.ClientModels
{
    public interface IActivity
    {
        Guid Id { get; set; }
        string Subject { get; set; }
        string Target { get; set; }
        string Action { get; set; }
        string TimeStamp { get; set; }
        string Source { get; set; }
        string CorrelationId { get; set; }
        int CorrelationSequence { get; set; }
        bool Deleted { get; set; }
    }
}
