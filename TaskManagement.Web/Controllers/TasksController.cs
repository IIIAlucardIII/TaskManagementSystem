using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Commands;
using TaskManagement.Web.Models;
using TaskManagement.Web.Queryes;
using TaskManagment.Domain.Enums;

namespace TaskManagement.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IMediator _mediatR;

        public TasksController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }
        public async Task<IActionResult>  Index(string searchValue, Status? status)
        {
          var result = await _mediatR.Send(new GetTasksQuery
          {
              SearchValue = searchValue,
              TaskStatus = status,
              User = User
          });
          return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask([FromForm]CreateTaskModel model)
        {
            await _mediatR.Send(new CreateTaskCommand
            {
                CreateTaskModel = model,
                User = User
            });
            return LocalRedirect("/tasks");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
           await _mediatR.Send(new DeleteTaskCommand 
           {
               User = User,
               Id = id
           });
           return LocalRedirect("/tasks");
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTask(EditTaskModel model)
        {
            await _mediatR.Send(new EditTaskCommand
            {
                EditTaskModel = model,
                User = User
            });
            return LocalRedirect("/tasks");
        }
    }
}
