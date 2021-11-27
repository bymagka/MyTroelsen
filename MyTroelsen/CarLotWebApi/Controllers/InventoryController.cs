using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using AutoLotDAL.Models;
using System.Web.Http;
using System.Web.Http.Description;
using AutoLotDAL.Repos;
using AutoMapper;
using System.Threading.Tasks;

namespace CarLotWebApi.Controllers
{
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        [HttpGet, Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet, Route("{id}")]
        public string Get (int id)
        {
            return id.ToString();
        }
         
    }
}