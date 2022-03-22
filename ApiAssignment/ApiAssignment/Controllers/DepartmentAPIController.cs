using ApiAssignment.Models.Database;
using ApiAssignment.Models.modelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ApiAssignment.Controllers
{
    public class DepartmentAPIController : ApiController
    {
        APITestEntities1 db = new APITestEntities1();
        [Route("api/get/departments")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {

            var data1 = (from s in db.departments select s).ToList();
            var st = new List<departmentModel>();
            foreach (var data in data1)
            {
                st.Add(new departmentModel()
                {
                    id = data.id,
                    name = data.name,
                });
            }

            string json = new JavaScriptSerializer().Serialize(st);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("api/create/department")]
        [HttpPost]
        public HttpResponseMessage Create(departmentModel departmentModel)
        {
            var dp = new department()
            {
                name = departmentModel.name
            };
            db.departments.Add(dp);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Created");
        }
        [Route("api/edit/department")]
        [HttpPost]
        public HttpResponseMessage Edit(departmentModel departmentModel)
        {
            var dp = (from s in db.departments where s.id == departmentModel.id select s).FirstOrDefault();
            db.Entry(dp).CurrentValues.SetValues(departmentModel);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Updated");
        }
        [Route("api/delete/department/{id}")]
        [HttpGet]
        public HttpResponseMessage Delete(int id)
        {
            var dp = (from s in db.departments where s.id == id select s).FirstOrDefault();
            db.departments.Remove(dp);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
        }
        [Route("api/dept/details/{id}")]
        [HttpGet]
        public HttpResponseMessage details(int id)
        {

            var data1 = (from s in db.departments where s.id == id select s).FirstOrDefault();
            //var dp = new department()
            //{
            //    name = data1.name,
            //    id = data1.id,
            //    students = data1.students

            //};

            //string json = new JavaScriptSerializer().Serialize(data1);
            return Request.CreateResponse(HttpStatusCode.OK, data1);
        }
    }
}

