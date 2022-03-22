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
    public class StudentAPIController : ApiController
    {
        APITestEntities1 db = new APITestEntities1();
        [Route("api/get/students")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            
            var data1 = (from s in db.students select s).ToList();
            var st = new List<studentModel>();
            foreach (var data in data1)
            {
                st.Add(new studentModel()
                {
                    id = data.id,
                    name = data.name,
                    dept_id = data.dept_id,
                });
            }

            string json = new JavaScriptSerializer().Serialize(st);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("api/create/student")]
        [HttpPost]
        public HttpResponseMessage Create(studentModel studentModel)
        {
            var student = new student()
            {
                name = studentModel.name,
                dept_id = studentModel.dept_id
            };
            db.students.Add(student);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Created");
        }
        [Route("api/edit/student")]
        [HttpPost]
        public HttpResponseMessage Edit(studentModel studentModel)
        {
            var student = (from s in db.students where s.id == studentModel.id select s).FirstOrDefault();
            db.Entry(student).CurrentValues.SetValues(studentModel);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Updated");
        }
        [Route("api/delete/student/{id}")]
        [HttpGet]
        public HttpResponseMessage Delete(int id)
        {
            var student = (from s in db.students where s.id == id select s).FirstOrDefault();
            db.students.Remove(student);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
        }
    }
}
