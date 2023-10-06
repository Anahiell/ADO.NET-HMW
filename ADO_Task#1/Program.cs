using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ADO_Task_1
{
    internal class Program
    {


        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDTB"].ConnectionString;
            Console.WriteLine("Choese task:\n(1-6)");
            int x;
            x = Int32.Parse(Console.ReadLine());
            while (x != 0)
            {
                switch (x)
                {
                    case 1:
                        {
                            First_and_last(connectionString);
                            break;
                        }
                    case 2:
                        {
                            GetAuthorsOfBook(3, connectionString);
                            break;
                        }
                    case 3:
                        {
                            GetAvailableBooks(connectionString);
                            break;
                        }
                    case 4:
                        {
                            GetBooksBorrowedByUser(5, connectionString);
                            break;
                        }
                    case 5:
                        {
                            GetBooksBorrowedLast2Weeks(connectionString);
                            break;
                        }
                    case 6:
                        {
                            GetBooksBorrowedLastYear(5, connectionString);
                            break;
                        }
                    default:
                        break;
                }
                Console.WriteLine("Choese task:\n(1-6)");
                x = Int32.Parse(Console.ReadLine());
            }
        }
        public static void GetDebtors(SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                string query = "SELECT S.[first_name], S.[last_name], B.[name], SC.[date_out] " +
                                "FROM [Library].[dbo].[S_Cards] AS SC " +
                                "INNER JOIN [Library].[dbo].[Student] AS S ON SC.[id_student] = S.[id] " +
                                "INNER JOIN [Library].[dbo].[Book] AS B ON SC.[id_book] = B.[id] " +
                                "WHERE SC.[date_in] IS NULL";

                SqlCommand command = new SqlCommand(query, connection, transaction);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Студент: {reader["first_name"]} {reader["last_name"]}");
                    Console.WriteLine($"Книга: {reader["name"]}");
                    Console.WriteLine($"Дата взятия: {reader["date_out"]}");
                    Console.WriteLine();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при выполнении операции GetDebtors: " + ex.Message);
                throw; 
            }
        }
        public static void GetAuthorsOfBook(int bookId, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT A.[first_name], A.[last_name] FROM [Library].[dbo].[Author] AS A " +
                               "INNER JOIN [Library].[dbo].[Book] AS BA ON A.[id] = BA.[id_author] " +
                               "WHERE BA.[id] = @bookId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@bookId", bookId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["first_name"] + " " + reader["last_name"]);
                }
            }
        }
        public static void GetAvailableBooks(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM [Library].[dbo].[Book] WHERE [quantity] > 0";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("Книга:" + reader["name"] + "\tКол-во:" + reader["quantity"]);
                }
            }
        }
        public static void GetBooksBorrowedByUser(int userId, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT B.[name] FROM [Library].[dbo].[S_Cards] AS SC " +
                               "INNER JOIN [Library].[dbo].[Book] AS B ON SC.[id_book] = B.[id] " +
                               "WHERE SC.[id_student] = @userId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Обработка данных книг, взятых пользователем
                    Console.WriteLine(reader["name"] + ";");
                }
            }
        }
        public static void GetBooksBorrowedLast2Weeks(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                DateTime twoWeeksAgo = DateTime.Now.AddDays(-14);
                string formattedDate = twoWeeksAgo.ToString("yyyy-MM-dd");

                string query = "SELECT SC.[id_book], B.[name] FROM [Library].[dbo].[S_Cards] AS SC " +
                               "INNER JOIN [Library].[dbo].[Book] AS B ON SC.[id_book] = B.[id] " +
                               "WHERE SC.[date_out] >= @twoWeeksAgo";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@twoWeeksAgo", formattedDate);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID книги: {reader["id_book"]}");
                    Console.WriteLine($"Название книги: {reader["name"]}");
                    Console.WriteLine("==============================================");
                }
            }
        }
        public static void ResetDebts(SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                // S_Cards
                string querySCards = "UPDATE [Library].[dbo].[S_Cards] SET [date_out] = GETDATE() WHERE [date_out] IS NULL";
                SqlCommand commandSCards = new SqlCommand(querySCards, connection, transaction);

                int rowsAffectedSCards = commandSCards.ExecuteNonQuery();

                // T_Cards
                string queryTCards = "UPDATE [Library].[dbo].[T_Cards] SET [date_out] = GETDATE() WHERE [date_out] IS NULL";
                SqlCommand commandTCards = new SqlCommand(queryTCards, connection, transaction);

                int rowsAffectedTCards = commandTCards.ExecuteNonQuery();

                if (rowsAffectedSCards > 0 || rowsAffectedTCards > 0)
                {
                    Console.WriteLine("Дата 'date_out' успешно обновлена для записей в таблицах 'S_Cards' и 'T_Cards'.");
                }
                else
                {
                    Console.WriteLine("Нет данных для обновления даты 'date_out'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при выполнении операции ResetDebts: " + ex.Message);
                throw; 
            }
        }
        public static void GetBooksBorrowedLastYear(int userId, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                DateTime lastYear = DateTime.Now.AddYears(-1);
                string formattedDate = lastYear.ToString("yyyy-MM-dd");

                string query = "SELECT COUNT(*) FROM [Library].[dbo].[S_Cards] " +
                               "WHERE [id_student] = @userId AND [date_out] >= @lastYear";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@lastYear", formattedDate);

                int count = (int)command.ExecuteScalar();

                Console.WriteLine($"Количество книг, взятых пользователем {userId} за последний год: {count}");
            }
        }
        public static void First_and_last(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    GetDebtors(connection, transaction);

                    // проверяем, есть ли должники
                    if (Check(connection, transaction))
                    {
                        //  есть должники, выполняем сброс задолженностей
                        ResetDebts(connection, transaction);

                        // подтверждаем транзакцию
                        transaction.Commit();

                        Console.WriteLine("Транзакция успешно завершена.");
                    }
                    else
                    {
                        // нет должников, отменяем транзакцию
                        transaction.Rollback();

                        Console.WriteLine("Нет должников. Транзакция отменена.");
                    }
                }
                catch (Exception ex)
                {
                    //  откатываем транзакцию
                    transaction.Rollback();
                    Console.WriteLine("Произошла ошибка. Транзакция отменена.");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static bool Check(SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM [Library].[dbo].[S_Cards]";
                SqlCommand command = new SqlCommand(query, connection, transaction);
                int debtorCount = (int)command.ExecuteScalar();

                return debtorCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при выполнении операции Check: " + ex.Message);
                throw; 
            }
        }
    }
}
