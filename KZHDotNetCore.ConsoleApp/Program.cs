using KZHDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//SqlConnectionStringBuilder sqlconnectionstringbuilder = new SqlConnectionStringBuilder();
//sqlconnectionstringbuilder.DataSource = ".";
//sqlconnectionstringbuilder.InitialCatalog = "DotNetTrainingBatch4";
//sqlconnectionstringbuilder.UserID = "sa";
//sqlconnectionstringbuilder.Password = "root";

//SqlConnection connection = new SqlConnection(sqlconnectionstringbuilder.ConnectionString);
//connection.Open();
//Console.WriteLine("Connection open.");

//string query = "select * from tbl_blog";
//SqlCommand cmd = new SqlCommand(query, connection); 
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd); 
//DataTable dt = new DataTable(); 
//sqlDataAdapter.Fill(dt); 

//connection.Close();
//Console.WriteLine("Connection close.");


//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Blog Id => " + dr["BlogId"]);
//    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
//    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
//    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
//    Console.WriteLine("-------------------------------------");
//}


Console.WriteLine("CRUD With AdoDotNets");
AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("testing create", "kyaw zaw", "kyaw@gmail.com");
//adoDotNetExample.Update(1, "testing update", "bo bo", "bobo@gmail.com");
//adoDotNetExample.Delete(1);
//adoDotNetExample.Edit(2);