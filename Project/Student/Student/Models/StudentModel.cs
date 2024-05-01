using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Student.Models
{
    public class StudentModel
    {
        public int s_id { get; set; }
        [Required]
        public string s_name { get; set; }
        public int? s_Age { get; set; }
        public int? s_Address_id { get; set; }
        [Required]
        public int s_SubjectID { get; set; }
        public string opsection { get; set; }
        public List<StudentList> Students { get; set; }
    }

    public class StudentList
    {
        public int s_id { get; set; }
        public string s_name { get; set; }
        public int? s_Age { get; set; }
        public string s_Address { get; set; }
        public string s_Subject { get; set; }
    }
}