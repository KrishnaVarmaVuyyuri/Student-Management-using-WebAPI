using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI_EXAMPLE.Controllers
{
    public class DefaultController : ApiController
    {
        public List<string> strings = new List<string>()
        {"varma","krishna","vuyyuri"};
        public List<string> Get()
        {
            return strings;
        }
        public string Get(int id)
        {
            return strings[id];
        }

        public void Delete(int id)
        {
            strings.RemoveAt(id);
        }

        public void Post(string val)
        {
            strings.Add(val);
        }
    }
}
