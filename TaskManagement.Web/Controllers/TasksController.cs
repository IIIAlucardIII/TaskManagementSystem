using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Persistence;
using TaskManagement.Web.Models;
using TaskManagment.Domain;

namespace TaskManagement.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly TaskManagementDbContext _dbContext;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        public TasksController(TaskManagementDbContext dbContext, UserManager<UserEntity> user, IMapper mapper = null)
        {
            _userManager = user;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var userName = _userManager.GetUserName(User);
            var userTasks = _dbContext.Tasks.Where(t => t.CreatedBy.Email == userName).ToList();

            var taskModels = _mapper.Map<List<TaskModel>>(userTasks);
            return View(taskModels);
        }
        public async Task<IActionResult> CreateTask(TaskModel model)
        {
            var userName = _userManager.GetUserName(User);
            var newEntity = new TaskEntity
            {
                Name = model.Name,
                Description = model.Description,
                Priority = model.Priority,
                CreatedBy = await _userManager.FindByNameAsync(userName),
                CreatedDate = DateTime.Now,
                Status = model.Status,
            };
            _dbContext.Tasks.Add(newEntity);
            await _dbContext.SaveChangesAsync();
            return LocalRedirect("/tasks");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var CurrentTask =await _dbContext.Tasks.FindAsync(id);
            if (CurrentTask == null)
            {
                return NotFound();
            }
            _dbContext.Tasks.Remove(CurrentTask);
            await _dbContext.SaveChangesAsync();
            return LocalRedirect("/tasks");
        }
    }
}
