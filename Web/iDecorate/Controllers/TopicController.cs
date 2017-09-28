using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iDecorate.Domain.Client.Contract;
using iDecorate.Domain.Client.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iDecorate.Controllers
{
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        private IBusinessTopic _businessTopic;

        public TopicController(IBusinessTopic businessTopic)
        {
            _businessTopic = businessTopic;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TopicModel> Get()
        {
            IEnumerable<TopicModel> topics = _businessTopic.GetAll();

            return topics;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]TopicModel value)
        {
            _businessTopic.Insert(value);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody]TopicModel value)
        {
            _businessTopic.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _businessTopic.Delete(id);
        }
    }
}
