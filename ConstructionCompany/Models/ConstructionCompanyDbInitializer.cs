using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace ConstructionCompany.Models
{
    public class ConstructionCompanyDbInitializer: CreateDatabaseIfNotExists<ConstructionCompanyContext>
    {
        protected override void Seed(ConstructionCompanyContext db)
        {
            //задание пути к файлу с текстом T-SQL инструкции
            string readPath = HttpContext.Current.Server.MapPath("~") + "/Scripts/ConstructionCompany.sql";

            //считывание текста SQL инструкции из внешнего текстового файла
            string SQLstring = "";
            try
            {
                using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
                {
                    SQLstring = sr.ReadToEnd();
                }
                //Выполнение T-SQL инструкции
                db.Database.ExecuteSqlCommand(SQLstring);
                base.Seed(db);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}