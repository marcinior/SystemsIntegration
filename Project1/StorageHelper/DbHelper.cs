using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StorageHelper
{
    public class DbHelper
    {
        private readonly string connectionString;
        private readonly List<PropertyInfo> ComputerFields;

        public DbHelper(string connectionString)
        {
            this.connectionString = connectionString;
            ComputerFields = typeof(Computer).GetProperties().ToList();
        }

        public List<Computer> GetComputers()
        {
            List<Computer> computers = new List<Computer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from [dbo].Computers";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Computer Computer = new Computer();
                            ComputerFields.ForEach(f =>
                            {
                                string value = reader.GetString(reader.GetOrdinal(f.Name));
                                f.SetValue(Computer, value);

                            });
                            computers.Add(Computer);
                        }
                    }
                }
            }
            return computers;
        }

        public void UpdateComputers(List<Computer> computers)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int i = 0;
                while (i < computers.Count())
                {
                    StringBuilder updateStatement = new StringBuilder();
                    updateStatement.Append("update [dbo].Computers set ");
                    ComputerFields.ForEach(p =>
                    {
                        if (!p.Name.Equals("Id"))
                            updateStatement.Append($"{p.Name} = '{p.GetValue(computers[i])}',");
                    });
                    updateStatement.Remove(updateStatement.Length - 1, 1);
                    updateStatement.Append($" where Id = {++i};");

                    using (SqlCommand command = new SqlCommand(updateStatement.ToString(), connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public int GetCountOfComputersByProducer(string producer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select count(*) as CountNumber from [dbo].Computers where [dbo].Computers.Producer = @Producer";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("Producer", SqlDbType.VarChar).Value = producer;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        return reader.GetInt32(0);
                    }
                }
            }
        }

        public List<Computer> GetComputersByDisplayType(string displayType)
        {
            List<Computer> computers = new List<Computer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from [dbo].Computers where [dbo].Computers.DisplayType = @DisplayType";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("DisplayType", SqlDbType.VarChar).Value = displayType;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Computer Computer = new Computer();
                            ComputerFields.ForEach(f =>
                            {
                                string value = reader.GetString(reader.GetOrdinal(f.Name));
                                f.SetValue(Computer, value);

                            });
                            computers.Add(Computer);
                        }
                    }
                }
            }
            return computers;
        }

        public List<string> GetProducers()
        {
            List<string> producers = new List<string>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select distinct Producer from [dbo].Computers";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producers.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return producers;
        }
    }
}
