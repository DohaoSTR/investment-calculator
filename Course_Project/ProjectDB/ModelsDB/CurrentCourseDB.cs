using System.Data.Linq.Mapping;

namespace Course_Project.ModelDB
{
    [Table(Name = "CurrentInvestmentCourse")]
    class CurrentCourseDB
    {
        [Column(Name = "Id_Account", IsPrimaryKey = true)]
        public int Id_Account { get; set; }
        [Column(Name = "Id_Operation")]
        public int Id_Operation { get; set; }
        [Column(Name = "InitialCourse", CanBeNull = true)]
        public decimal InitialCourse { get; set; }
        [Column(Name = "CurrentCourse", CanBeNull = true)]
        public decimal CurrentCourse { get; set; }
    }
}
