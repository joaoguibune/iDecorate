using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iDecorate.Domain.Client.Contract;
using Newtonsoft.Json;
using iDecorate.Domain.Client.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iDecorate.Controllers
{
    [Route("api/[controller]")]
    public class WordController : Controller
    {
        private IBusinessWord _businessWord;

        public WordController(IBusinessWord businessWord)
        {
            _businessWord = businessWord;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]WordExpressionsModel value)
        {
            var word = value.toWordModel();

            _businessWord.Insert(word);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody]WordExpressionsModel value)
        {
            var word = value.toWordModel();

            _businessWord.Update(word);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _businessWord.Delete(id);
        }
    }
}
