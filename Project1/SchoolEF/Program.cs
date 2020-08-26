using System;
using System.Linq;

namespace SchoolEF
{
    class Program
    {
        static void Main()
        {
            // Database accessor. This opens the database automatically
            var school = new SchoolEntities();

            // This accesses the ClassMaster table
            foreach (var classMaster in school.ClassMasters)
            {
                Console.WriteLine("ClassId: {0}\nClassName: {1}\nClassDescription: {2}\nClassPrice: {3}\n",
                    classMaster.ClassId, classMaster.ClassName, classMaster.ClassDescription, classMaster.ClassPrice);
            }

            foreach (var user in school.Users)
            {
                Console.WriteLine(user.UserName);
                foreach (var classMaster in user.ClassMasters)
                {
                    Console.WriteLine("ClassId: {0}  Name: {1}", classMaster.ClassId, classMaster.ClassName);
                }

                Console.WriteLine("--");

                var classIds = school.RetrieveClassesForStudent(user.UserId).ToArray();

                foreach (var item in classIds)
                {
                    var classMaster = school.ClassMasters.FirstOrDefault(t => t.ClassId == item.ClassId);

                    Console.WriteLine("ClassId: {0}  Name: {1}", classMaster.ClassId, classMaster.ClassName);
                }
            }

            Console.Write("Done.");
            Console.ReadLine();
        }
    }
}