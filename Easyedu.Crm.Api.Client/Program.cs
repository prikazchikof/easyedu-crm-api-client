using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Easyedu.Crm.Api.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Интеграционное тестирование работы с сервером");
            Console.WriteLine("{0}Работа с Restful:", Environment.NewLine);

            DatabaseTest databaseTest = new DatabaseTest();

            Console.WriteLine("{0} - авторизация", databaseTest.AuthorizeTest());
            Console.WriteLine("{0} - получение списка организаци", databaseTest.GetOrganizationsTest());
            Console.WriteLine("{0} - создание организации", databaseTest.PostOrganizationTest());
            Console.WriteLine("{0} - создание курса", databaseTest.PostCourseTest());
            Console.WriteLine("{0} - создание заявки", databaseTest.PostLeadTest());


            Console.ReadKey();
        }
    }
}
