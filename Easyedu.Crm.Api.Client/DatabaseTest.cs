using NewInterfaceTest.Db.Models.DTOs;
using NewInterfaceTest.Models.DTOs;
using System.Collections.Generic;
using System.Net;

namespace Easyedu.Crm.Api.Client
{
    class DatabaseTest
    {
        private Database _database;
        private string _password;

        public DatabaseTest()
        {
            _database = new Database();
        }

        public bool AuthorizeTest()
        {
            var isAuthorized = _database.Authorize("info@courseburg.ru", "CourseBurg24");

            return isAuthorized;
        }

        public bool AuthorizeKeyTest()
        {
            var isAuthorized = _database.Authorize(_database.AccessToken);

            return isAuthorized;
        }

        public bool ChangePasswordTest()
        {
            var response = _database.ChangePassword(_password, "123456");

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public bool GetOrganizationsTest()
        {
            List<OrganizationDTO> item = new List<OrganizationDTO>();

            var response = _database.GetOrganizations(out item);

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }

        private int _organizationId = 0;
        public bool PostOrganizationTest()
        {
           OrganizationDTO item = new OrganizationDTO();
            item.Name = "ТестАпи";

            var response = _database.PostOrganization(ref item);

            _organizationId = item.OrganizationId;

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public bool PostCourseTest()
        {
            CourseDTO item = new CourseDTO()
            {
                Name = "ТестАпи Курс",
                OrganizationId = _organizationId
            };
            item.Name = "ТестАпи Курс";

            var response = _database.PostCourse(ref item);


            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }
    }
}
