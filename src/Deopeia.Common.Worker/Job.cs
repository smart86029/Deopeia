using Coravel.Invocable;
using MediatR;

namespace Deopeia.Common.Worker;

public class Job(ISender sender, IRequest command) : IInvocable
{
    private readonly ISender _sender = sender;
    private readonly IRequest _command = command;

    public async Task Invoke()
    {
        await _sender.Send(_command);
    }
}
