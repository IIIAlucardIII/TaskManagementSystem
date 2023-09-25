using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagement.Persistence;
using TaskManagement.Web.Models;
using TaskManagment.Domain;
using TaskManagment.Domain.Enums;

namespace TaskManagement.Web.Queryes
{
    public class GetTasksQuery : IRequest<List<TaskModel>>
    {
        public ClaimsPrincipal User { get; set; }
        public string SearchValue { get; set; }
        public Status? TaskStatus { get; set; }

    }
    public class GetTasksHandler : IRequestHandler<GetTasksQuery, List<TaskModel>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly TaskManagementDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetTasksHandler(UserManager<UserEntity> userManager, TaskManagementDbContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<TaskModel>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            string search = null;
            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                search = request.SearchValue.ToLower();
            }

            var userName = _userManager.GetUserName(request.User);
            var userTasksQuery = _dbContext
                .Tasks
                .Where(t => t.CreatedBy.Email == userName &&
                    (string.IsNullOrEmpty(search) ||
                    t.Name.ToLower().Contains(search) ||
                    t.Description.ToLower().Contains(search)));

            if (request.TaskStatus != null)
            {
                    var status = request.TaskStatus;
                    userTasksQuery = userTasksQuery.Where(t => t.Status == status);
               
            }
            
            var userTasks = await userTasksQuery
                .OrderByDescending(d => d.EditedAt)
                .ToListAsync();

            var taskModels = _mapper.Map<List<TaskModel>>(userTasks);
            return taskModels;
        }

    }
}
