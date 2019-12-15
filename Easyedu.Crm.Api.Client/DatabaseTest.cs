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

        private int _courseId = 0;
        public bool PostCourseTest()
        {
            CourseDTO item = new CourseDTO()
            {
                Name = "ТестАпи Курс",
                OrganizationId = _organizationId
            };

            var response = _database.PostCourse(ref item);
            _courseId = item.CourseId;

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public bool EditCourseTest()
        {
            CourseDTO item = new CourseDTO()
            {
                Name = "ТестАпи Курс ред",
                OrganizationId = _organizationId,
                CourseId = _courseId
            };

            var response = _database.EditCourse(item);


            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public bool DeleteCourseTest()
        {
            var response = _database.DeleteCourse(_courseId);

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public bool PostLeadTest()
        {
            LeadDTO item = new LeadDTO()
            {
                //Name = "ТестАпи Заявка",
                //Comment = "Можно указать название курса",
                Phone = "+79111647627",
                CourseId = 12
            };

            var response = _database.PostLead(ref item);

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }
    }
}
