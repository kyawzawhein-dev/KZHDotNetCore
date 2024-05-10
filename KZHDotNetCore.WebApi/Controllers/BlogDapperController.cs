using Dapper;
using KZHDotNetCore.WebApi.Models;
using KZHDotNetCore.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using KZHDotNetCore.Share;

namespace KZHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly DapperService dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult Read()
        {
            var items = dapperService.Query<BlogDto>("select * from Tbl_Blog;");
            return Ok(items);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("Data Not Found !");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogDto blogDto)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            int result = dapperService.Execute(query, blogDto);
            string message = result > 0 ? "Create Successfull !" : "Create Failed !";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogDto dto)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId;";
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("Data Not Found");
            }
            dto.BlogID = id;
            int result = dapperService.Execute(query, dto);
            string message = result > 0 ? "Update Successfull !" : "Update Failed !";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogDto blogDto)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("Data Not Found");
            }
            string conditon = string.Empty;
            if (!string.IsNullOrEmpty(blogDto.BlogTitle))
            {
                conditon += "[BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blogDto.BlogAuthor))
            {
                conditon += "[BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blogDto.BlogContent))
            {
                conditon += "[BlogContent] = @BlogContent, ";
            }
            if (conditon.Length == 0)
            {
                return NotFound("No Data Found !");
            }
            conditon = conditon.Substring(0, conditon.Length - 2);
            blogDto.BlogID = id;
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditon}
 WHERE BlogId = @BlogId;";
            int result = dapperService.Execute(query, blogDto);
            string message = result > 0 ? "Patch Successfull !" : "Patch Failed !";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId=@BlogId";
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("Data Not Found");
            }
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogDto { BlogID = id });
            string message = result > 0 ? "Delete Successfull !" : "Delete Failed !";
            return Ok(message);
        }
        private BlogDto FindById(int id)
        {
            var item = dapperService.QueryFirstOrDefault<BlogDto>("select * from Tbl_Blog where BlogId=@BlogId", new BlogDto { BlogID = id });
            return item;
        }
    }
}