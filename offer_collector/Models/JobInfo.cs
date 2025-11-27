using System.Collections.Concurrent;

namespace Offer_collector.Models
{
    record JobInfo(
     string JobId,
     int TotalBatches,
     ConcurrentBag<List<string>> AiBatches,
     string Status,
     string? ErrorMessage
 );
}
