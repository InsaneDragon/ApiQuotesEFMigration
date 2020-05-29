using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using CrudApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Data.Sqlite;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CrudApi.Controllers
{
    [ApiController]
    [Route("crud/")]
    public class CrudOperationsController : ControllerBase
    {
        [HttpGet]
        [Route("Read")]
        public ActionResult<List<Quote>> Read()
        {
            try
            {
                using (var context = new SqliteConnection("Data source=myDb.db"))
                {
                    return context.Query<Quote>("select * from Quotes").ToList();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("Create")]

        public ActionResult Create(string Text, string Author)
        {
            try
            {
                using (var context = new SqliteConnection("Data source=myDb.db"))
                {
                    context.Query($"insert into Quotes(Text,Author,InsertDate)values('{Text}','{Author}','{DateTime.Now}')");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var context = new SqliteConnection("Data source=myDb.db"))
                {
                    context.Query($"delete from Quotes where Id={id}");
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpGet]
        [Route("Update")]
        public ActionResult Update(string Text, string Author,int id)
        {
            try
            {
                using (var context = new SqliteConnection("Data source=myDb.db"))
                {
                    var UpdatedQuote = context.Query<Quote>($"select * from Quotes where Id={id}").FirstOrDefault();
                    if (UpdatedQuote != null)
                    {
                        UpdatedQuote.Text = Text ?? UpdatedQuote.Text;
                        UpdatedQuote.Author = Text ?? UpdatedQuote.Author;
                        context.Query($"update Quotes set Text='{UpdatedQuote.Text}',Author='{UpdatedQuote.Author}' where Id={UpdatedQuote.Id}");
                    }
                    else
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}