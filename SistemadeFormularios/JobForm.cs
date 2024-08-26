using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SistemadeFormularios
{
    public partial class JobForm : Form
    {
      
        private string connectionString = "Data Source=DESKTOP-DC5MS3H;Initial Catalog=companiadb4;Integrated Security=True";

        public JobForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Recoge los datos del formulario
            string jobTitle = txtJobTitle.Text;
            decimal minSalary;
            decimal maxSalary;

            if (string.IsNullOrWhiteSpace(jobTitle) ||
                !decimal.TryParse(txtMinSalary.Text, out minSalary) ||
                !decimal.TryParse(txtMaxSalary.Text, out maxSalary))
            {
                MessageBox.Show("Por favor, completa todos los campos correctamente.");
                return;
            }

          
            Job newJob = new Job
            {
                JobTitle = jobTitle,
                MinSalary = minSalary,
                MaxSalary = maxSalary
            };

            
            SaveJobToDatabase(newJob);
        }

        private void SaveJobToDatabase(Job job)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Jobs (JobTitle, MinSalary, MaxSalary) VALUES (@JobTitle, @MinSalary, @MaxSalary)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                    cmd.Parameters.AddWithValue("@MinSalary", job.MinSalary);
                    cmd.Parameters.AddWithValue("@MaxSalary", job.MaxSalary);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("El trabajo ha sido guardado correctamente.");
            ClearForm();
        }

        private void ClearForm()
        {
            txtJobTitle.Clear();
            txtMinSalary.Clear();
            txtMaxSalary.Clear();
        }

        private void JobForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
