using AdSuit.DAL.Models;
using AdSuit.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdSuitProject.Controllers
{
    public class TagsController : Controller
    {
        ITagService _TagService;

        public TagsController(ITagService TagService)
        {
            _TagService = TagService;
        }
        // GET: Tags
        public ActionResult Index()
        {
            var tags = _TagService.GetAll();
            return View(tags);
        }

        // GET: Tags/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tags tags)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_TagService.GetAll().Any(x => x.TagName == tags.TagName))
                    {
                        _TagService.CreateAsync(tags);
                        TempData["SuccessMessage"] = "You saved tag successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "You have already added this tag");
                        return View(tags);
                    }

                }
                return View(tags);
                // TODO: Add insert logic here
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(tags);
            }
        }

        // GET: Tags/Edit/5
        public ActionResult Edit(int id)
        {
            var tag = _TagService.GetById(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tags tag)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _tag = _TagService.GetById(id);
                    if(_tag == null)
                    {
                        TempData["SuccessMessage"] = "Tag Couldn't Found";
                        return RedirectToAction("Index");
                    }
                    if (_TagService.GetAll().Any(x => x.TagName == tag.TagName) && tag.TagName != tag.TagName)
                    {
                        ModelState.AddModelError("Error", "There is already a tag with this tag name");
                        return View(tag);
                    }
                    else
                    {
                        _tag.TagName = tag.TagName;
                        _TagService.UpdateAsync(_tag);
                        
                        TempData["SuccessMessage"] = "You saved tag successfully";
                        return RedirectToAction("Index");
                    }

                }
                return View(tag);
                // TODO: Add insert logic here
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(tag);
            }
        }
        // POST: Tags/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int? Id = 0)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Tags tag = _TagService.GetById(Id.Value);
                    if (tag == null)
                    {
                        return HttpNotFound();
                    }
                    if (tag.EmployeeTags.Count() > 0)
                    {
                        return Json(new { Message = "You can not delete this tag, because this tag belongs some employees", State = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        _TagService.DeleteAsync(tag);
                        return Json(new { Message = "You deleted tags successfuly", State = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Message =  "Your Entered Tag Name Is Not Valid", State = false }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
