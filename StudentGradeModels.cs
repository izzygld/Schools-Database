using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoneyMustard.Web.Models
{
    public class ClassListModel
    {
        public List<ClassListTableRow> Classes { get; set; }
        public string TeacherName { get; set; }
    }

    public class GradeClassModel
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Module { get; set; }
        public DateTime StartDate { get; set; }
        public List<ParticipantModel> Participants { get; set; }
    }

    public class ClassListTableRow
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Module { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class ParticipantModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ParticpantID { get; set; }
        public int Grade { get; set; }
    }

    public class StudentGradeDetailsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<GradeTableModel> Grades { get; set; }
    }

    public class GradeTableModel
    {
        public string Coursename { get; set; }
        public string Module { get; set; }
        public int Grade { get; set; }
    }
}
