﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZHDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {

        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch4",
            UserID = "sa",
            Password = "root"
        };



        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Opened...");

            string query = @"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
              FROM [dbo].[Tbl_Blog]";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Console.WriteLine("Connection Closing...");
            connection.Close();
            Console.WriteLine("Connection Closed...");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Title..." + dr["BlogTitle"]);
                Console.WriteLine("Author..." + dr["BlogAuthor"]);
                Console.WriteLine("Content..." + dr["BlogContent"]);
            }
        }

        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Create Successfull !" : "Create Failed !";
            Console.WriteLine(message);
            Console.ReadKey();
        }

        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            command.Parameters.AddWithValue("@BlogTitle", title);
            command.Parameters.AddWithValue("@BlogAuthor", author);
            command.Parameters.AddWithValue("@BlogContent", content);
            int result = command.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Fail";
            Console.WriteLine(message);
        }

        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "select * from Tbl_Blog where BlogID = @BlogID";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@BlogID", id);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Not Found !");
                return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine("ID" + dr["BlogID"]);
            Console.WriteLine("Title" + dr["BlogTitle"]);
            Console.WriteLine("Author" + dr["BlogAuthor"]);
            Console.WriteLine("Content" + dr["BlogContent"]);
            Console.ReadKey();
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"delete from Tbl_Blog where BlogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Delete Successfull !" : "Delete Failed !";
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}