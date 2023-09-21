using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Claims;
using TaskManagement.Persistence;
using TaskManagement.Web.Models;
using TaskManagment.Domain;

namespace TaskManagement.Web.Queryes
{
    public class GetTasksQuery : IRequest<List<TaskModel>>
    {
        public ClaimsPrincipal User { get; set; }
        public string SearchValue { get; set; }

    }
    public class GetTasksHandler : IRequestHandler<GetTasksQuery, List<TaskModel>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly TaskManagementDbContext _dbContext;
        private readonly IMediator _mediatR;
        private readonly IMapper _mapper;
        public GetTasksHandler(UserManager<UserEntity> userManager, IMediator mediatR, TaskManagementDbContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _mediatR = mediatR;
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
            var userTasks = await _dbContext
                .Tasks
                .Where(t => t.CreatedBy.Email == userName &&
                    (string.IsNullOrEmpty(search) ||

                    t.Name.ToLower().Contains(search) ||
                    t.Description.ToLower().Contains(search))).OrderByDescending(d => d.EditedAt)
                .ToListAsync();

            var taskModels = _mapper.Map<List<TaskModel>>(userTasks);
            return taskModels;
        }
    }
}
