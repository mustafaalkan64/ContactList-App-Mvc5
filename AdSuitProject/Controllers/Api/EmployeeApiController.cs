using AdSuit.DAL.Models;
using AdSuit.Service.Interfaces;
using AdSuitProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AdSuitProject.Controllers.Api
{
    public class EmployeeApiController : ApiController
    {
        IEmployeeService _EmployeeService;
        IContactTypeService _ContactTypeService;
        ITagService _TagService;
        IEmployeeContactTypesService _EmployeeContactTypesService;
        IEmployeeHistoryService _EmployeeHistoryService;
        IEmployeeTagsService _EmployeeTagsService;


        public EmployeeApiController(IEmployeeService EmployeeService,
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

        [HttpPost]
        [Route("api/employees/create")]
        public IHttpActionResult CreateEmployee([FromBody]EmployeeContactsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Employee employee = new Employee();
                    employee.Tags = String.Join(",", model.tags.Select(x => x.TagName));
                    employee.CreateDate = DateTime.Now;
                    employee.Name = model.employee.Name;
                    employee.Surname = model.employee.Surname;
                    employee.Deleted = false;
                    employee.UpdateDate = DateTime.Now;
                    _EmployeeService.Create(employee);

                    var employeeId = employee.Id;
                    foreach(var i in model.contactTypes)
                    {
                        var employeeContactType = new EmployeeProperties();
                        employeeContactType.EmployeeId = employeeId;
                        employeeContactType.ContactType = i.ContactTypeName;
                        employeeContactType.Value = i.ContactTypeValue;
                        _EmployeeContactTypesService.Create(employeeContactType);
                    }
                    foreach (var i in model.tags)
                    {
                        var employeeTags = new EmployeeTags();
                        employeeTags.EmployeeId = employeeId;
                        employeeTags.TagId = i.TagId;
                        _EmployeeTagsService.Create(employeeTags);
                    }
                    return Ok("Successfully Employee Created! ");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse((HttpStatusCode)500,
                    new HttpError(ex.Message)));
            }
            
        }

        [HttpPost]
        [Route("api/employees/update")]
        public IHttpActionResult UpdateEmployee(int? id, [FromBody]EmployeeContactsViewModel model)
        {
            try
            {
                if(id == null)
                    return NotFound();
                
                var employee = _EmployeeService.GetById(id);

                if (employee == null)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    #region Update Employee
                    employee.Tags = String.Join(",", model.tags.Select(x => x.TagName));
                    employee.Name = model.employee.Name;
                    employee.Surname = model.employee.Surname;
                    employee.UpdateDate = DateTime.Now;
                    _EmployeeService.Update(employee);
                    var employeeId = id.Value;
                    #endregion

                    #region Create Employee History
                    EmployeeHistories histories = new EmployeeHistories();
                    histories.Name = employee.Name;
                    histories.Surname = employee.Surname;
                    histories.UpdatedEmployeeId = employeeId;
                    histories.Status = "Update";
                    histories.CreateDate = DateTime.Now;
                    _EmployeeHistoryService.Create(histories);
                    var EmployeeProperties = employee.EmployeeProperties;
                    var EmployeeTags = employee.EmployeeTags;
 
                    foreach (var i in EmployeeProperties.ToList())
                    {
                        _EmployeeContactTypesService.Delete(i);
                        var employeeContactType = new EmployeeProperties();
                        employeeContactType.EmployeeId = employee.Id;
                        employeeContactType.ContactType = i.ContactType;
                        employeeContactType.Value = i.Value;
                        employeeContactType.EmployeeHistories_Id = histories.Id;
                        _EmployeeContactTypesService.Create(employeeContactType);
                    }
                    foreach (var i in EmployeeTags.ToList())
                    {
                        _EmployeeTagsService.Delete(i);
                        var employeeTags = new EmployeeTags();
                        employeeTags.EmployeeId = employee.Id;
                        employeeTags.TagId = i.TagId;
                        employeeTags.EmployeeHistories_Id = histories.Id;
                        _EmployeeTagsService.Create(employeeTags);
                    }
                    #endregion

                    #region Update Contacts and Tags
                    foreach (var i in model.contactTypes)
                    {
                        var employeeContactType = new EmployeeProperties();
                        employeeContactType.EmployeeId = employeeId;
                        employeeContactType.ContactType = i.ContactTypeName;
                        employeeContactType.Value = i.ContactTypeValue;
                        _EmployeeContactTypesService.Create(employeeContactType);
                    }
                    foreach (var i in model.tags)
                    {
                        var employeeTags = new EmployeeTags();
                        employeeTags.EmployeeId = employeeId;
                        employeeTags.TagId = i.TagId;
                        _EmployeeTagsService.Create(employeeTags);
                    }
                    #endregion

                    return Ok("Employee Updated Successfuly");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse((HttpStatusCode)500,
                    new HttpError(ex.Message)));
            }

        }

        [HttpPost]
        [Route("api/employees/delete")]
        public IHttpActionResult DeleteEmployee(int id= 0)
        {
            try
            {
                Employee employee = _EmployeeService.GetById(id);
                if (employee == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Employee Doesnt Exist"));
                }
                else
                {
                    #region Create Employee History
                    EmployeeHistories histories = new EmployeeHistories();
                    histories.Name = employee.Name;
                    histories.Surname = employee.Surname;
                    histories.UpdatedEmployeeId = employee.Id;
                    histories.Status = "Deleted";
                    histories.CreateDate = DateTime.Now;
                    histories.UpdateDate = DateTime.Now;
                    _EmployeeHistoryService.Create(histories);

                    #endregion

                    #region Create Contact Types nad Tags Belongs to History Table
                    var EmployeeProperties = employee.EmployeeProperties;
                    var EmployeeTags = employee.EmployeeTags;
                    foreach (var i in EmployeeProperties.ToList())
                    {
                        var employeeContactType = new EmployeeProperties();
                        employeeContactType.EmployeeId = employee.Id;
                        employeeContactType.ContactType = i.ContactType;
                        employeeContactType.Value = i.Value;
                        employeeContactType.EmployeeHistories_Id = histories.Id;
                        _EmployeeContactTypesService.Create(employeeContactType);
                    }
                    foreach (var i in EmployeeTags.ToList())
                    {
                        var employeeTags = new EmployeeTags();
                        employeeTags.EmployeeId = employee.Id;
                        employeeTags.TagId = i.TagId;
                        employeeTags.EmployeeHistories_Id = histories.Id;
                        _EmployeeTagsService.Create(employeeTags);
                    }
                    #endregion

                    #region Update Employee's status to Deleted
                    employee.Deleted = true;
                    employee.DeletedDate = DateTime.Now;
                    employee.UpdateDate = DateTime.Now;
                    _EmployeeService.Update(employee);
                    #endregion

                    return Ok("Deleted Successfuly");
                } 

            }
            catch (Exception ex)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse((HttpStatusCode)500,
                    new HttpError(ex.Message)));
            }

        }


        [HttpGet]
        [Route("api/employees/details")]
        public IHttpActionResult EmployeeDetails(int id = 0)
        {
            try
            {
                Employee employee = _EmployeeService.GetById(id);
                if (employee == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Employee Doesnt Exist"));
                }
                else
                {
                    employee.EmployeeProperties = employee.EmployeeProperties.Where(x => x.EmployeeHistories_Id == null).ToList();
                    employee.EmployeeTags = employee.EmployeeTags.Where(x => x.EmployeeHistories_Id == null).ToList();
                    return Ok(employee);
                }

            }
            catch (Exception ex)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse((HttpStatusCode)500,
                    new HttpError(ex.Message)));
            }

        }
    }
}
