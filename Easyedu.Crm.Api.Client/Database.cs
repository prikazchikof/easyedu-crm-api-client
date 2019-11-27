using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NewInterfaceTest.Models.Db;
using NewInterfaceTest.Models.DTOs;
using NewInterfaceTest.Db.Models.DTOs;

namespace Easyedu.Crm.Api.Client
{
    /// <summary>
    /// Класс для доступа к данным.
    /// </summary>
    public class Database
    {
        private string _appPath = "http://localhost:49876/";


        private string _accessToken;

        /// <summary>
        /// Получает ключ доступа к API.
        /// </summary>
        /// <value>
        /// Ключ доступа.
        /// </value>
        public string AccessToken
        {
            get { return _accessToken; }
            private set { _accessToken = value; }
        }

        /// <summary>
        /// Авторизация.
        /// </summary>
        /// <param name="userName">Логин пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>True, если авторизация прошла успешно.</returns>
        public bool Authorize(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", userName ),
                    new KeyValuePair<string, string> ( "Password", password )
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(_appPath + "/Token", content).Result;

                if (response.StatusCode != HttpStatusCode.OK)
                    return false;

                var result = response.Content.ReadAsStringAsync().Result;
                // Десериализация полученного JSON-объекта
                Dictionary<string, string> tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                _accessToken = tokenDictionary["access_token"];
                return true;
            }
        }

        /// <summary>
        /// Авторизация по ключу доступа.
        /// </summary>
        /// <param name="accessToken">Ключ доступа.</param>
        /// <returns>True, если авторизация прошла успешно.</returns>
        public bool Authorize(string accessToken)
        {
            using (var client = createClient(accessToken))
            {
                var response = client.GetAsync(_appPath + "/api/Account/UserInfo").Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    _accessToken = accessToken;
                    return true;
                }
                
                    return false;
            }
        }

        /// <summary>
        /// Удаление текущей сессии.
        /// </summary>
        public void Logout()
        {
            using (var client = createClient(_accessToken))
            {
                var response = client.GetAsync(_appPath + "/api/Account/Logout").Result;
                _accessToken = "";
            }
        }

        /// <summary>
        /// Изменение пароля.
        /// </summary>
        /// <param name="oldPassword">Старый пароль.</param>
        /// <param name="newPassword">Новый пароль.</param>
        /// <returns></returns>
        public HttpResponseMessage ChangePassword(string oldPassword, string newPassword)
        {
            var changePasswordModel = new
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmPassword = newPassword
            };
            using (var client = createClient(_accessToken))
            {
                var response = client.PostAsJsonAsync(_appPath + "/api/Account/ChangePassword", changePasswordModel).Result;

                return response;
            }
        }



        public HttpResponseMessage GetOrganizations(out List<OrganizationDTO> notification)
        {
            using (var client = createClient(_accessToken))
            {
                var response = client.GetAsync(_appPath + "/api/Organizations").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                notification = JsonConvert.DeserializeObject<List<OrganizationDTO>>(result);

                return response;
            }
        }

        public HttpResponseMessage PostOrganization(ref OrganizationDTO notification)
        {
            using (var client = createClient(_accessToken))
            {
                var response = client.PostAsJsonAsync(_appPath + "/api/Organizations", notification).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                notification = JsonConvert.DeserializeObject<OrganizationDTO>(result);

                return response;
            }
        }

        public HttpResponseMessage PostCourse(ref CourseDTO notification)
        {
            using (var client = createClient(_accessToken))
            {
                var response = client.PostAsJsonAsync(_appPath + "/api/Courses", notification).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                notification = JsonConvert.DeserializeObject<CourseDTO>(result);

                return response;
            }
        }


        /// <summary>
        /// HTTP-клиент с ключом доступа.
        /// </summary>
        /// <param name="accessToken">Ключ доступа.</param>
        /// <returns>HTTP-клиент.</returns>
        private HttpClient createClient(string accessToken = "")
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return client;
        }
    }  
}
