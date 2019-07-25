using System;
using System.Data.Linq;
using System.Windows.Forms;
using Course_Project.ModelDB;

namespace Course_Project.Models
{
    class DataBase
    {
        public  DataContext Db { get; private set; }
        public Table<MoneyCountDB> MoneyCountDBs { get; private set; }
        public Table<LogPassDB> LogPassDBs { get; private set; }
        public Table<CurrentCourseDB> СurrentCourseDBs { get; private set; }
        public Table<CurrentDB> CurrentDBs { get; private set; }
        public Table<CompletedDB> CompletedDBs { get; private set; }
        public void ConnectionString()
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\KursBase.mdf;Integrated Security=True;Connect Timeout=30;User Instance=False;MultipleActiveResultSets=True";
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + @"C:\Users\muzal\OneDrive\Рабочий стол\Course_Project\Course_Project\KursBase.mdf;Integrated Security=True;User Instance=False;";
            Db = new DataContext(connectionString);

            MoneyCountDBs = Db.GetTable<MoneyCountDB>();
            LogPassDBs = Db.GetTable<LogPassDB>();
            СurrentCourseDBs = Db.GetTable<CurrentCourseDB>();
            CurrentDBs = Db.GetTable<CurrentDB>();
            CompletedDBs = Db.GetTable<CompletedDB>();
        }
    }
}
