using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using PayCompute.Entity;
using PayCompute.Models;
using PayCompute.Services;
using PayCompute.Services.Implementation;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PayCompute.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly HostingEnvironment _hostingEnvironment;

        public EmployeeController(IEmployeeService employeeService, HostingEnvironment hostingEnvironment)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {

            var employees = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                ImageUrl = employee.ImageUrl,
                FullName = employee.FullName,
                Gender = employee.Gender,
                Designation = employee.Designation,
                City = employee.City,
                DateJoined = employee.DateJoined,
            }).ToList();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    MiddleName = model.MiddleName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    DateJoined = model.DateJoined,
                    Designation = model.Designation,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    Address = model.Address,
                    UnionMember = model.UnionMember,
                    City = model.City,
                    Phone = model.Phone,
                    PostCode = model.PostCode,
                };
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }

                await _employeeService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            var model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                Address = employee.Address,
                UnionMember = employee.UnionMember,
                City = employee.City,
                Phone = employee.Phone,
                PostCode = employee.PostCode,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);

                if (employee == null)
                {
                    return NotFound();
                }

                employee.EmployeeNo = model.EmployeeNo;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.MiddleName = model.MiddleName;
                employee.NationalInsuranceNo = model.NationalInsuranceNo;
                employee.Gender = model.Gender;
                employee.Email = model.Email;
                employee.DOB = model.DOB;
                employee.DateJoined = model.DateJoined;
                employee.Phone = model.Phone;
                employee.Designation = model.Designation;
                employee.PaymentMethod = model.PaymentMethod;
                employee.StudentLoan = model.StudentLoan;
                employee.UnionMember = model.UnionMember;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.PostCode = model.PostCode;

                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }
                await _employeeService.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detail (int id)
        {
            var employee = _employeeService.GetById(id);
            {
                if (employee == null)
                {
                    return NotFound();
                }
                EmployeeDetailViewModel model = new EmployeeDetailViewModel()
                {
                    Id = employee.Id,
                    EmployeeNo = employee.EmployeeNo,
                    FullName = employee.FullName,
                    Gender = employee.Gender,
                    DOB = employee.DOB,
                    DateJoined = employee.DateJoined,
                    Designation = employee.Designation,
                    NationalInsuranceNo = employee.NationalInsuranceNo,
                    Phone = employee.Phone,
                    Email = employee.Email,
                    PaymentMethod = employee.PaymentMethod,
                    StudentLoan = employee.StudentLoan,
                    UnionMember = employee.UnionMember,
                    Address = employee.Address,
                    City = employee.City,
                    ImageUrl = employee.ImageUrl,
                    PostCode = employee.PostCode
                };
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeDeleteViewModel()
            {
                Id = employee.Id,
                FullName = employee.FullName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeDeleteViewModel model)
        {
            await _employeeService.Delete(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
