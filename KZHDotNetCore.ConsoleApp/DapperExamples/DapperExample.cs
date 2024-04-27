using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KZHDotNetCore.ConsoleApp.Dtos;
using KZHDotNetCore.ConsoleApp.Services;

namespace KZHDotNetCore.ConsoleApp.DapperExamples;

internal class DapperExample
{
    public void Run()
    {
        Read();
        //Edit(6);
        //Create("DCreate", "Dauthor", "DContent");
        //Update(6, "DCreate", "Dauthor", "DContent");
        //Delete(6);
    }
    private void Read()
    {
        using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        List<BlogDto> lst = db.Query<BlogDto>("select * from Tbl_Blog").ToList();
        foreach (BlogDto item in lst)
        {
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("--------------------------------");
        }
    }
    private void Edit(int id)
    {
        using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        var item = db.Query<BlogDto>("select * from Tbl_Blog where BlogId=@BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
        if (item is null)
        {
            Console.WriteLine("Data Not Found !");
            return;
        }
        Console.WriteLine(item.BlogId);
        Console.WriteLine(item.BlogTitle);
        Console.WriteLine(item.BlogAuthor);
        Console.WriteLine(item.BlogContent);
        Console.WriteLine("--------------------------------");
    }
    private void Create(string title, string author, string content)
    {
        var data = new BlogDto
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
        int result = db.Execute(query, data);
        string message = result > 0 ? "Create Successfull !" : "Create Error !";
        Console.WriteLine(message);
        Console.ReadKey();
    }
    private void Update(int id, string title, string author, string content)
    {
        var item = new BlogDto
        {
            BlogId = id,
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId;";
        var result = db.Execute(query, item);
        string message = result > 0 ? "Update Successfull !" : "Update Error !";
        Console.WriteLine(message);
        Console.ReadKey();
    }
    private void Delete(int id)
    {
        var data = new BlogDto
        {
            BlogId = id
        };
        using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId=@BlogId";
        int result = db.Execute(query, data);
        string message = result > 0 ? "Delete Successfull !" : "Delete Error !";
        Console.WriteLine(message);
        Console.ReadKey();
    }
}
