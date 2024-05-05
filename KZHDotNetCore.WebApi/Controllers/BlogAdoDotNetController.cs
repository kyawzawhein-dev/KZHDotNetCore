﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using KZHDotNetCore.Share;
using KZHDotNetCore.WebApi.Models;
using KZHDotNetCore.WebApi;

namespace KZHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly AdoDotNetService _service = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult Read()
        {
            string query = "select * from Tbl_Blog";
            var list = _service.Query<BlogDto>(query);
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            string query = "select * from Tbl_Blog where BlogID = @BlogID";
            var item = _service.QueryFirstOrDeafult<BlogDto>(query, new AdoDotNetParameter("@BlogID", id));
            if (item is null)
            {
                return NotFound("No data found.");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogDto blogDto)
        {
            String query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            int result = _service.Execute(query, new AdoDotNetParameter("@BlogTitle", blogDto.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blogDto.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blogDto.BlogContent)
                );
            string message = result > 0 ? "Create Successfull !" : "Create Failed !";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogDto blogDto)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            String query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blogDto.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogDto.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blogDto.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Update Successfull !" : "Update Failed !";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogDto blogDto)
        {
            var updates = new List<string>();
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(blogDto.BlogTitle))
            {
                updates.Add("[BlogTitle] = @BlogTitle");
                parameters.Add(new SqlParameter("@BlogTitle", blogDto.BlogTitle));
            }

            if (!string.IsNullOrEmpty(blogDto.BlogAuthor))
            {
                updates.Add("[BlogAuthor] = @BlogAuthor");
                parameters.Add(new SqlParameter("@BlogAuthor", blogDto.BlogAuthor));
            }

            if (!string.IsNullOrEmpty(blogDto.BlogContent))
            {
                updates.Add("[BlogContent] = @BlogContent");
                parameters.Add(new SqlParameter("@BlogContent", blogDto.BlogContent));
            }

            if (updates.Count == 0)
            {
                return BadRequest("No Data Not Found !.");
            }

            var query = $"UPDATE [dbo].[Tbl_Blog] SET {string.Join(", ", updates)} WHERE [BlogID] = @BlogID";
            parameters.Add(new SqlParameter("@BlogID", id));

            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters.ToArray());
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Patch successful!" : "Patch failed!";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            String query = @"delete from Tbl_Blog where BlogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Delete Successfull !" : "Delete Failed !";
            return Ok(message);
        }
    }
}