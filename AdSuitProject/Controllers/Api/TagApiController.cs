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
    public class TagApiController : ApiController
    {
        ITagService _TagService;

        public TagApiController(ITagService TagService)
        {
            _TagService = TagService;
        }

        [HttpPost]
        [Route("api/tags/create")]
        public IHttpActionResult CreateTag(string TagName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_TagService.GetAll().Any(x => x.TagName == TagName))
                    {
                        var tag = new Tags();
                        tag.TagName = TagName;
                        _TagService.Create(tag);
                        return Ok("You saved tag successfully");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "You have already added this tag");
                        return BadRequest(ModelState);
                    }
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
        [Route("api/tags/update")]
        public IHttpActionResult UpdateTag(int id, Tags tag)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _tag = _TagService.GetById(id);
                    if (_tag == null)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Tag Doesnt Exist"));
                    }
                    if (_TagService.GetAll().Any(x => x.TagName == tag.TagName) && tag.TagName != tag.TagName)
                    {
                        ModelState.AddModelError("Error", "There is already a tag with this tag name");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        _tag.TagName = tag.TagName;
                        _TagService.Update(_tag);
                        return Ok(_tag);
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
                // TODO: Add insert logic here
            }
            catch (Exception ex)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse((HttpStatusCode)500,
                    new HttpError(ex.Message)));
            }
        }

        [HttpPost]
        [Route("api/tags/delete")]
        public IHttpActionResult Delete(int? Id = 0)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Id == null)
                    {
                        return NotFound();
                    }
                    Tags tag = _TagService.GetById(Id.Value);
                    if (tag == null)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Tag Doesnt Exist"));
                    }
                    if (tag.EmployeeTags.Count() > 0)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "You can not delete this tag, because this tag belongs some employees"));
                    }
                    else
                    {
                        _TagService.Delete(tag);
                        return Ok("Tag Removed Successfuly");
                    }
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
    }
}
