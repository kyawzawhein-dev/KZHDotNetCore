using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KZHDotNetCore.ConsoleApp.Dtos;

namespace KZHDotNetCore.ConsoleApp.EFCoreExamples;

internal class EFCoreExample
{
    private readonly AppDbContext db = new AppDbContext();
    public void RunEFCore()
    {
        //read();
        //edit(8);
        //create("EFCoreCreate", "EFAuthor", "EFContent");
        //update(8,"EFCoreCreatetion", "EFAuthorUpdate", "EFContentUpdate");
        //delete(8);
    }
    private void read()
    {
        var list = db.Blogs.ToList();
        foreach (BlogDto item in list)
        {
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
    }
    private void edit(int id)
    {
        var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            Console.WriteLine("Data Not Found !");
            return;
        }

        Console.WriteLine(item.BlogId);
        Console.WriteLine(item.BlogTitle);
        Console.WriteLine(item.BlogAuthor);
        Console.WriteLine(item.BlogContent);
    }
    private void create(string title, string author, string content)
    {
        var data = new BlogDto
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        db.Blogs.Add(data);
        int result = db.SaveChanges();

        string message = result > 0 ? "Create Successfull !" : "Delete Successfull !";
        Console.WriteLine(message);
    }
    private void update(int id, string title, string author, string content)
    {
        var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            Console.WriteLine("Data Not Found !");
        }

        item.BlogTitle = title;
        item.BlogAuthor = author;
        item.BlogContent = content;
        int result = db.SaveChanges();
        string message = result > 0 ? "Update Successfull !" : "Update Successfull !";
        Console.WriteLine(message);
    }
    private void delete(int id)
    {
        var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            Console.WriteLine("Data Not Found !");
        }
        db.Blogs.Remove(item);
        int result = db.SaveChanges();
        string message = result > 0 ? "Delete Successfull !" : "Delete Successfull !";
        Console.WriteLine(message);
    }
}
