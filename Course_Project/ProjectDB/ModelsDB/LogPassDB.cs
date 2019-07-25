using System.Data.Linq.Mapping;

namespace Course_Project.ModelDB
{
    [Table(Name = "LogPass")]
    class LogPassDB
    {
        [Column(Name = "Id_Account", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id_Account { get; set; }
        [Column(Name = "Login", CanBeNull = true)]
        public string Login { get; set; }
        [Column(Name = "Password", CanBeNull = true)]
        public string Password { get; set; }
    }
}
