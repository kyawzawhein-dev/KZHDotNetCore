using KZHDotNetCore.WebApi.Db;
using KZHDotNetCore.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KZHDotNetCore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    public readonly AppDbContext db;

    public BlogController()
    {
        db = new AppDbContext();
    }

    [HttpGet]
    public IActionResult read()
    {
        var data = db.Blogs.ToList();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult edit(int id)
    {
        var data = db.Blogs.FirstOrDefault(x => x.BlogID == id);
        if (data is null)
        {
            Console.WriteLine("No Data Not Found !");
        }
        return Ok(data);
    }

    [HttpPost]
    public IActionResult create(BlogDto blogDto)
    {
        db.Blogs.Add(blogDto);
        int resuslt = db.SaveChanges();
        string message = resuslt > 0 ? "Create Successfull !" : "Create Failed !";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult update(int id, BlogDto blog)
    {
        var data = db.Blogs.FirstOrDefault(x => x.BlogID == id);
        if (data is null)
        {
            Console.WriteLine("No Data Not Found !");
        }
        data.BlogTitle = blog.BlogTitle;
        data.BlogAuthor = blog.BlogAuthor;
        data.BlogContent = blog.BlogContent;
        int result = db.SaveChanges();
        string message = result > 0 ? "Update Successfull !" : "Update Failed !";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult patch(int id, BlogDto blogDto)
    {
        var data = db.Blogs.FirstOrDefault(x => x.BlogID == id);
        if (data is null)
        {
            Console.WriteLine("Data Not Found !");
        }
        if (!string.IsNullOrEmpty(blogDto.BlogTitle))
        {
            data.BlogTitle = blogDto.BlogTitle;
        }
        if (!string.IsNullOrEmpty(blogDto.BlogAuthor))
        {
            data.BlogAuthor = blogDto.BlogAuthor;
        }
        if (!string.IsNullOrEmpty(blogDto.BlogContent))
        {
            data.BlogContent = blogDto.BlogContent;
        }
        int result = db.SaveChanges();
        string message = result > 0 ? "Update Patched Successfull !" : "Update Patched Failed !";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult delete(int id)
    {
        var data = db.Blogs.FirstOrDefault(x => x.BlogID == id);
        if (data is null)
        {
            Console.WriteLine("Data Not Found !");
        }
        db.Remove(data);
        int result = db.SaveChanges();
        string message = result > 0 ? "Delete Successfull !" : "Delete Failed !";
        return Ok(message);
    }
}