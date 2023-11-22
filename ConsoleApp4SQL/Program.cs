using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=NIJATAZIZ\\SQLEXPRESS;Initial Catalog=P236;Integrated Security=True";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            Console.WriteLine("1.Create User");
            Console.WriteLine("2.Update User");
            Console.WriteLine("3.Delete User");
            Console.WriteLine("4.Show All Users");
            Console.WriteLine("5. Exit");

            while (true)
            {
                Console.Write("Select: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewUser(connection);
                        break;
                    case "2":
                        UpdateUser(connection);
                        break;
                    case "3":
                        DeleteUser(connection);
                        break;
                    case "4":
                        PrintAllUsers(connection);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }

    static void CreateNewUser(SqlConnection connection)
    {
        Console.WriteLine("Create New User:");

        Console.Write("Name: ");
        string ad = Console.ReadLine();

        Console.Write("Surname: ");
        string soyad = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        string insertQuery = $@"
            insert into Users (Ad, Soyad, Email)
            values ('{ad}', '{soyad}', '{email}');
        ";

        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
        {
            insertCommand.ExecuteNonQuery();
            Console.WriteLine("New User was created");
        }
    }

    static void UpdateUser(SqlConnection connection)
    {
        Console.WriteLine("Enter user id which edited:");

        Console.Write("id: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("New Name: ");
        string name = Console.ReadLine();

        Console.Write("New Surname: ");
        string surname = Console.ReadLine();

        string updateQuery = $@"
            update Users
            set Ad = '{name}', Soyad = '{surname}'
            where Id = {id};
        ";

        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
        {
            updateCommand.ExecuteNonQuery();
            Console.WriteLine(" User was edited");
        }
    }

    static void DeleteUser(SqlConnection connection)
    {
        Console.WriteLine("Enter user id which deleted:");

        Console.Write("id: ");
        int id = int.Parse(Console.ReadLine());

        string deleteQuery = $@"
            delete from Users
            where Id = {id};
        ";

        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
        {
            deleteCommand.ExecuteNonQuery();
            Console.WriteLine("User was deleted");
        }
    }

    static void PrintAllUsers(SqlConnection connection)
    {
        string selectQuery = "select * from Users";

        using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
        {
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                Console.WriteLine("Users:");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string ad = reader.GetString(1);
                    string soyad = reader.GetString(2);
                    string email = reader.GetString(3);

                    Console.WriteLine($"ID: {id}, Name: {ad}, Surname: {soyad}, Email: {email}");
                }
            }
        }
    }
}