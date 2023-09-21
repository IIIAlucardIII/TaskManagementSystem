using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TaskManagement.Persistence;
using TaskManagement.Web.Models;
using TaskManagment.Domain;

namespace TaskManagement.Web.Commands
{
    public class CreateTaskCommand : IRequest<Unit>
    {
        public ClaimsPrincipal User { get; set; }
        public CreateTaskModel CreateTaskModel { get; set; }
    }
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Unit> 
    {
        private readonly TaskManagementDbContext _dbContext;
        private readonly UserManager<UserEntity> _userManager;

        public CreateTaskHandler(TaskManagementDbContext dbContext, UserManager<UserEntity> user)
        {
            _userManager = user;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var newEntity = new TaskEntity
            {
                Name = request.CreateTaskModel.Name,
                Description = request.CreateTaskModel.Description,
                Priority = request.CreateTaskModel.Priority,
                CreatedBy = await _userManager.GetUserAsync(request.User),
                CreatedDate = DateTime.Now,
            };
            _dbContext.Tasks.Add(newEntity);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
