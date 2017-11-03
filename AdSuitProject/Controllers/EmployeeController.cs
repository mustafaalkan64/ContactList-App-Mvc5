using AdSuit.Service.Interfaces;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;

namespace AdSuitProject.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeService _EmployeeService;
        IContactTypeService _ContactTypeService;
        ITagService _TagService;
        IEmployeeContactTypesService _EmployeeContactTypesService;
        IEmployeeHistoryService _EmployeeHistoryService;
        IEmployeeTagsService _EmployeeTagsService;


        public EmployeeController(IEmployeeService EmployeeService, 
            IContactTypeService ContactTypeService,
            ITagService TagService,
            IEmployeeContactTypesService EmployeeContactTypesService,
            IEmployeeHistoryService EmployeeHistoryService,
            IEmployeeTagsService EmployeeTagsService
            )
        {
            _EmployeeService = EmployeeService;
            _ContactTypeService = ContactTypeService;
            _TagService = TagService;
            _EmployeeContactTypesService = EmployeeContactTypesService;
            _EmployeeHistoryService = EmployeeHistoryService;
            _EmployeeTagsService = EmployeeTagsService;
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var query = _EmployeeService.GetQueryableAll();
            var totalCount = query.Count();

            #region Filtering  
            // Apply filters for searching  
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Name.Contains(value) ||
                                         p.Surname.Contains(value) ||
                                         p.Tags.Contains(value)
                                   );
            }

            var filteredCount = query.Count();

            #endregion Filtering  

            #region Sorting  
            // Sorting  
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            query = query.OrderBy(orderByString == string.Empty ? "Name asc" : orderByString);

            #endregion Sorting  

            // Paging  
            query = query.Skip(requestModel.Start).Take(requestModel.Length);


            var data = query.Select(employee => new
            {
                ID = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Tags = employee.Tags,
                CreateDate = employee.CreateDate
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var ContactTypes = _ContactTypeService.GetAll();
            ViewBag.ContactTypes = ContactTypes.Select(a => new SelectListItem { Text = a.ContactType, Value = a.Name.ToString() }).AsEnumerable().ToList();
            var Tags = _TagService.GetAll();
            ViewBag.Tags = Tags.Select(a => new SelectListItem { Text = a.TagName, Value = a.Id.ToString() }).AsEnumerable().ToList();
            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = _EmployeeService.GetById(id);

            if (employee == null)
                return HttpNotFound();

            employee.EmployeeTags = employee.EmployeeTags.Where(x => x.EmployeeHistories_Id == null).ToList();
            employee.EmployeeProperties = employee.EmployeeProperties.Where(x => x.EmployeeHistories_Id == null).ToList();

            var ContactTypes = _ContactTypeService.GetAll().ToList();
            var Tags = _TagService.GetAll().ToList();
            foreach (var i in employee.EmployeeProperties)
            {
                if(ContactTypes.Any(a => a.Name == i.ContactType))
                {
                    ContactTypes.Remove(ContactTypes.FirstOrDefault(a => a.Name == i.ContactType));
                }
            }

            foreach (var i in employee.EmployeeTags)
            {
                if (Tags.Any(a => a.Id == i.TagId))
                {
                    Tags.Remove(Tags.FirstOrDefault(a => a.Id == i.TagId));
                }
            }

            ViewBag.ContactTypes = ContactTypes.Select(a => new SelectListItem { Text = a.ContactType, Value = a.Name.ToString() }).AsEnumerable().ToList(); 
            ViewBag.Tags = Tags.Select(a => new SelectListItem { Text = a.TagName, Value = a.Id.ToString() }).AsEnumerable().ToList();
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
