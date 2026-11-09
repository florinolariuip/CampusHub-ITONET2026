using CampusHub.Domain.Common;

namespace CampusHub.Common.CQRS;

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}