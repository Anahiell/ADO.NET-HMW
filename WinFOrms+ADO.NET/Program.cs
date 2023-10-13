using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFOrms_ADO.NET
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region create
            string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();

                // проверка существования базы данных
                string checkDbExistsQuery = "SELECT COUNT(*) FROM sys.databases WHERE name = 'SalesDB'";
                using (SqlCommand checkDbExistsCmd = new SqlCommand(checkDbExistsQuery, sqlCon))
                {
                    int dbCount = (int)checkDbExistsCmd.ExecuteScalar();
                    if (dbCount == 0)
                    {
                        string createDbQuery = "CREATE DATABASE SalesDB";
                        using (SqlCommand createDbCmd = new SqlCommand(createDbQuery, sqlCon))
                        {
                            createDbCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SalesDB;Integrated Security=True";

            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();

                // проверка существования таблиц
                string checkTableExistsQuery = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Customers'";
                using (SqlCommand checkTableExistsCmd = new SqlCommand(checkTableExistsQuery, sqlCon))
                {
                    int tableCount = (int)checkTableExistsCmd.ExecuteScalar();
                    if (tableCount == 0)
                    {
                        // табл Customers
                        string createCustomersTableQuery = "CREATE TABLE Customers (Id INT PRIMARY KEY, FirstName NVARCHAR(50), LastName NVARCHAR(50))";
                        using (SqlCommand createCustomersTableCmd = new SqlCommand(createCustomersTableQuery, sqlCon))
                        {
                            createCustomersTableCmd.ExecuteNonQuery();
                        }
                    }
                }

                checkTableExistsQuery = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Sellers'";
                using (SqlCommand checkTableExistsCmd = new SqlCommand(checkTableExistsQuery, sqlCon))
                {
                    int tableCount = (int)checkTableExistsCmd.ExecuteScalar();
                    if (tableCount == 0)
                    {
                        // табл Sellers
                        string createSellersTableQuery = "CREATE TABLE Sellers (Id INT PRIMARY KEY, FirstName NVARCHAR(50), LastName NVARCHAR(50))";
                        using (SqlCommand createSellersTableCmd = new SqlCommand(createSellersTableQuery, sqlCon))
                        {
                            createSellersTableCmd.ExecuteNonQuery();
                        }
                    }
                }

                checkTableExistsQuery = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Sales'";
                using (SqlCommand checkTableExistsCmd = new SqlCommand(checkTableExistsQuery, sqlCon))
                {
                    int tableCount = (int)checkTableExistsCmd.ExecuteScalar();
                    if (tableCount == 0)
                    {
                        //табл Sales
                        string createSalesTableQuery = "CREATE TABLE Sales (Id INT PRIMARY KEY, CustomerId INT, SellerId INT, SaleAmount DECIMAL(10, 2), SaleDate DATETIME)";
                        using (SqlCommand createSalesTableCmd = new SqlCommand(createSalesTableQuery, sqlCon))
                        {
                            createSalesTableCmd.ExecuteNonQuery();
                        }
                    }
                }
                #endregion
                #region insert
                    string checkCustomersQuery = "SELECT COUNT(*) FROM Customers";
                    using (SqlCommand checkCustomersCmd = new SqlCommand(checkCustomersQuery, sqlCon))
                    {
                        int customerCount = (int)checkCustomersCmd.ExecuteScalar();
                        if (customerCount == 0)
                        {
                        //если запесей нет
                        string insertCustomersQuery = "INSERT INTO Customers (Id, FirstName, LastName) VALUES (1, 'Ivan', 'Ivanov'), (2, 'Petr', 'Petrov'), (3, 'Annah', 'Sidorova')";
                            using (SqlCommand insertCustomersCmd = new SqlCommand(insertCustomersQuery, sqlCon))
                            {
                                insertCustomersCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    string checkSellersQuery = "SELECT COUNT(*) FROM Sellers";
                    using (SqlCommand checkSellersCmd = new SqlCommand(checkSellersQuery, sqlCon))
                    {
                        int sellerCount = (int)checkSellersCmd.ExecuteScalar();
                        if (sellerCount == 0)
                        {
                        //если запесей нет
                        string insertSellersQuery = "INSERT INTO Sellers (Id, FirstName, LastName) VALUES (1, 'Serhei', 'Serg'), (2, 'Elen', 'White'), (3, 'Mikle', 'Beile')";
                            using (SqlCommand insertSellersCmd = new SqlCommand(insertSellersQuery, sqlCon))
                            {
                                insertSellersCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    string checkSalesQuery = "SELECT COUNT(*) FROM Sales";
                    using (SqlCommand checkSalesCmd = new SqlCommand(checkSalesQuery, sqlCon))
                    {
                        int salesCount = (int)checkSalesCmd.ExecuteScalar();
                        if (salesCount == 0)
                        {
                            //если запесей нет
                            string insertSalesQuery = "INSERT INTO Sales (Id, CustomerId, SellerId, SaleAmount, SaleDate) VALUES (1, 1, 1, 100.00, '2023-10-13'), (2, 2, 2, 150.00, '2023-10-13'), (3, 3, 3, 200.00, '2023-10-13')";
                            using (SqlCommand insertSalesCmd = new SqlCommand(insertSalesQuery, sqlCon))
                            {
                                insertSalesCmd.ExecuteNonQuery();
                            }
                        }
                    }
                
            
            }
            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
