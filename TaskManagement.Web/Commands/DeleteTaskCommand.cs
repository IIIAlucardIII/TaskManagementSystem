using MediatR;
using System.Security.Claims;
using TaskManagement.Persistence;

namespace TaskManagement.Web.Commands
{
    public class DeleteTaskCommand : IRequest<Unit>
    {
        public ClaimsPrincipal User { get; set; }
        public Guid Id { get; set; }

    }
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, Unit>
    {
        private readonly TaskManagementDbContext _dbContext;
        public DeleteTaskHandler(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var CurrentTask = await _dbContext.Tasks.FindAsync(request.Id);
            if (CurrentTask == null)
            {
                Exception exception;
            }
            _dbContext.Tasks.Remove(CurrentTask);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
        
    }
}
