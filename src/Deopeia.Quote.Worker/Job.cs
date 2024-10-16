namespace Deopeia.Quote.Worker;

internal class Job<TCommand>(ISender sender) : IInvocable
    where TCommand : IRequest, new()
{
    private readonly ISender _sender = sender;

    public async Task Invoke()
    {
        var command = new TCommand();
        await _sender.Send(command);
    }
}
