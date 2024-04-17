using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET: Students
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var newStudent = new Students
            {
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                Email = viewModel.Email,
                Subscribed = viewModel.Subscribed
            };

            await _dbContext.AddAsync(newStudent);
            await _dbContext.SaveChangesAsync();

            return View();
        }

        // GET: Students List
        public async Task<IActionResult> List()
        {
            var students = await _dbContext.Students.ToListAsync();
            return View(students);
        }

        // GET: Students Edit
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Students viewModel)
        {
            var student = await _dbContext.Students.FindAsync(viewModel.Id);

            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.Phone = viewModel.Phone;
                student.Email = viewModel.Email;
                student.Subscribed = viewModel.Subscribed;

                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Students viewModel)
        {
            var student = await _dbContext.Students.FindAsync(viewModel.Id);

            if (student is not null)
            {
                //Removing and Discarding
                _ = _dbContext.Students.Remove(student);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }



    }

}
