using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemadeFormularios.Models; 

namespace SistemadeFormularios.Controllers
{
    internal class JobController
    {
        private string connectionString = "DESKTOP-DC5MS3H";

        // Método para agregar un nuevo trabajo
        public void AddJob(Job job)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Jobs (JobTitle, MinSalary, MaxSalary) VALUES (@JobTitle, @MinSalary, @MaxSalary)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                command.Parameters.AddWithValue("@MinSalary", job.MinSalary);
                command.Parameters.AddWithValue("@MaxSalary", job.MaxSalary);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Job added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding job: " + ex.Message);
                }
            }
        }

        public void UpdateJob(Job job)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Jobs SET JobTitle = @JobTitle, MinSalary = @MinSalary, MaxSalary = @MaxSalary WHERE JobId = @JobId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@JobId", job.JobID);
                command.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                command.Parameters.AddWithValue("@MinSalary", job.MinSalary);
                command.Parameters.AddWithValue("@MaxSalary", job.MaxSalary);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Job updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating job: " + ex.Message);
                }
            }
        }
    }
}

