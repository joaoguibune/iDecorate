using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iDecorate.Domain.Client.Contract;
using iDecorate.Domain.Client.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iDecorate.Controllers.Mobile
{
    [Route("api/[controller]")]
    public class TopicMobileController : Controller
    {
        private IBusinessTopic _businessTopic;

        public TopicMobileController(IBusinessTopic businessTopic)
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
        public TopicModel Get(string id)
        {
            return _businessTopic.Find(Guid.Parse(id));
        }

        // POST api/values
        [HttpPost]
        public IEnumerable<TopicModel> Post([FromBody]TopicModel value)
        {
            _businessTopic.Insert(value);

            return _businessTopic.GetAll();
        }

        // PUT api/values/5
        [HttpPut]
        public IEnumerable<TopicModel> Put([FromBody]TopicModel value)
        {
            _businessTopic.Update(value);

            return _businessTopic.GetAll();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IEnumerable<TopicModel> Delete(Guid id)
        {
            _businessTopic.Delete(id);

            return _businessTopic.GetAll();
        }
    }
}
