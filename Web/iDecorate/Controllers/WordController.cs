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
        public IEnumerable<WordModel> Get()
        {
            return _businessWord.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public WordModel Get(string id)
        {
            return _businessWord.Find(Guid.Parse(id));
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
