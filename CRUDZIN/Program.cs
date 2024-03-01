using CRUDZIN;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string connectionString = "Server=127.0.0.1;Database=db;User ID=root;Password=12345678;";

app.MapGet("/products/{id}", (int id) => {
    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        try
        {
            conn.Open();
            string Query = "select * from products where id = @value1;";
            MySqlCommand command = new MySqlCommand(Query, conn);

            command.Parameters.AddWithValue("@value1", id);
            MySqlDataReader reader = command.ExecuteReader();
       
        }
        finally
        {
            conn.Close();
        }
    }
});
app.MapPost("/products", (Product product) => 
{
    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        try
        {
            conn.Open();
            string Query = "insert into products (name, price) values (@value1,@value2);";
            MySqlCommand command = new MySqlCommand(Query, conn);

            command.Parameters.AddWithValue("@value1", product.Name);
            command.Parameters.AddWithValue("@value2", product.Price);
            command.ExecuteNonQuery();

        } 
        finally
        {
            conn.Close();
        }
   
    }
});

app.MapPatch("/products/{id}", (int id, Product product) =>
{
    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        try
        {
            conn.Open();
            string Query = "update products set name = @value1, price = @value2 where id = @value3;";
            MySqlCommand command = new MySqlCommand(Query, conn);

            command.Parameters.AddWithValue("@value1", product.Name);
            command.Parameters.AddWithValue("@value2", product.Price);
            command.Parameters.AddWithValue("@value3", id);
            command.ExecuteNonQuery();

        }
        finally
        {
            conn.Close();
        }

    }


    app.MapDelete("/products/{id}", (int id, Product product) =>
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string Query = "delete from products where id = @value3;";
                MySqlCommand command = new MySqlCommand(Query, conn);

                command.Parameters.AddWithValue("@value3", id);
                command.ExecuteNonQuery();

            }
            finally
            {
                conn.Close();
            }
        }
    });

    app.Run();

});
