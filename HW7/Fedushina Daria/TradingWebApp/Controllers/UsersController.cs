using System;
using System.Linq;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using System.Web.Http;
using TradingApp.Core.Repositories;
using System.Web;
using System.Collections.Specialized;

namespace TradingWebApi.Controllers
{
    
    public class UsersController : ApiController
    {
        private readonly IUserTableRepository usersService;
        

        public UsersController() { }
        public UsersController(IUserTableRepository usersService)
        {
            this.usersService = usersService;
        }

        // GET api/users
        //[HttpGet]
        public string Get() 
        {
            string stringToreturn = String.Empty;
            String currurl = Request.RequestUri.ToString();
            String querystring;

            // Check to make sure some query string variables
            // exist and if not return full list of users.
            int iqs = currurl.IndexOf('?');
            if (iqs == -1)
            {
                
                var users = usersService.GetAll();

                stringToreturn= Newtonsoft.Json.JsonConvert.SerializeObject(users); 
            }
            // If query string variables exist, put them in
            // a string.
            else if (iqs >= 0)
            {
                querystring = (iqs < currurl.Length - 1) ? currurl.Substring(iqs + 1) : String.Empty;
                // Parse the query string variables into a NameValueCollection.
                NameValueCollection qscoll = HttpUtility.ParseQueryString(querystring);
                if (qscoll.AllKeys.Contains("page")&& qscoll.AllKeys.Contains("top"))
                {
                    int page;
                    bool ifPageIsInt = int.TryParse(qscoll.Get("page"), out page);
                    int top;
                    bool ifTopIsInt = int.TryParse(qscoll.Get("top"), out top);
                    var obj =  usersService.GetAll().Skip((page - 1) * top).Take(top).ToList();
                    stringToreturn= Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                }
            }
            return stringToreturn;
        }

      
        // POST api/users
        //[HttpPost]
        [ActionName("add")]
        public string Post([FromBody] UserRegistrationInfo value)
        {
            UserEntity user = new UserEntity()
            {
                Name = value.Name,
                Surname = value.Surname,
                PhoneNumber = value.PhoneNumber
            };
            usersService.Add(user);
            usersService.SaveChanges();
            int id = usersService.GetId(value);
            return Newtonsoft.Json.JsonConvert.SerializeObject(usersService.Get(id));
        }

        // PUT api/users/5
        //[HttpPut("{id}")]
        [ActionName("update")]
        public bool Put([FromBody] UserEntity value)
        {
            return usersService.Update(value);

        }

        // DELETE api/users/5
        //[HttpDelete("{id}")]
        [ActionName("remove")]
        public void Delete([FromBody]int id)
        {
            usersService.Delete(id);
        }

           
    }
}
