using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

public record DeleteImportOrderCommand(Guid Id) : IRequest<Result>;

public class DeleteImportOrderCommandHandler : IRequestHandler<DeleteImportOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteImportOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteImportOrderCommand request, CancellationToken cancellationToken)
    {
        var importOrder = await _context.ImportOrders
            .Include(io => io.Details)
            .FirstOrDefaultAsync(io => io.Id == request.Id, cancellationToken);

        if (importOrder == null)
            return Result.Failure("Import order not found.");

        _context.ImportOrderDetails.RemoveRange(importOrder.Details);
        _context.ImportOrders.Remove(importOrder);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
