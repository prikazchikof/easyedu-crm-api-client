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
            List<OrganizationDTO> shedule = new List<OrganizationDTO>();

            var response = _database.GetOrganizations(out shedule);

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public bool PostOrganizationTest()
        {
           OrganizationDTO shedule = new OrganizationDTO();
            shedule.Name = "ТестАпи";

            var response = _database.PostOrganization(ref shedule);

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }
    }
}
