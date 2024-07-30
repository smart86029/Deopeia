using System.Reflection;

namespace Deopeia.Common.Infrastructure.Events;

internal class EventBusOptions
{
    public string SubscriptionClientName { get; set; } =
        Assembly.GetEntryAssembly()!.GetName().Name!;

    public int RetryCount { get; set; } = 10;
}
