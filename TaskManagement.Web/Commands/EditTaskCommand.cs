using AutoMapper;
using MediatR;
using System.Security.Claims;
using TaskManagement.Persistence;
using TaskManagement.Web.Models;

namespace TaskManagement.Web.Commands
{
    public class EditTaskCommand : IRequest<Unit>
    {
        public ClaimsPrincipal User { get; set; }
        public EditTaskModel EditTaskModel { get; set; }
    }
    public class EditTaskHandler : IRequestHandler<EditTaskCommand, Unit>
    {
        private readonly TaskManagementDbContext _dbContext;

        private readonly IMapper _mapper;
        public EditTaskHandler(TaskManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _dbContext.Tasks.FindAsync(request.EditTaskModel.Id);
            if (task == null)
            {
                Exception exception;
            }

            _mapper.Map(request.EditTaskModel, task);

            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }

}
