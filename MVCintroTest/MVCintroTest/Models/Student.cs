using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCintroTest.Models
{
    public class Student
    {
        [Required]
        public string Name { get; set; }

        public string ID { get; set; }

        public string Dob { get; set; }

        public string Email { get; set; }

    }
}