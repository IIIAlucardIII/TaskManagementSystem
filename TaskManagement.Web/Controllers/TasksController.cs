using AutoMapper;
using FluentValidation;
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

        public TasksController(TaskManagementDbContext dbContext, UserManager<UserEntity> user, IMapper mapper)
        {
            _userManager = user;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult Index(string searchValue)
        {
            string search = null;
            if (!string.IsNullOrEmpty(searchValue))
            {
                search = searchValue.ToLower();
            }

            var userName = _userManager.GetUserName(User);
            var userTasks = _dbContext
                .Tasks
                .Where(t => t.CreatedBy.Email == userName && 
                    (string.IsNullOrEmpty(search) ||

                    t.Name.ToLower().Contains(search) ||
                    t.Description.ToLower().Contains(search))).OrderByDescending(d => d.EditedAt)
                .ToList();

            var taskModels = _mapper.Map<List<TaskModel>>(userTasks);
            return View(taskModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromForm]CreateTaskModel model)
        {
            var userName = _userManager.GetUserName(User);
            var newEntity = new TaskEntity
            {
                Name = model.Name,
                Description = model.Description,
                Priority = model.Priority,
                CreatedBy = await _userManager.FindByNameAsync(userName),
                CreatedDate = DateTime.Now,
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

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTask(EditTaskModel model)
        {
            var task = await _dbContext.Tasks.FindAsync(model.Id);
            if (task == null)
            {
                return NotFound();
            }

            _mapper.Map(model, task);

            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return LocalRedirect("/tasks");
        }
        public IActionResult Search(SearchTaskModel searchTask)
        {
            var searchResults = searchTask; 
            return View("SearchResults", searchResults);
        }
    }
}
