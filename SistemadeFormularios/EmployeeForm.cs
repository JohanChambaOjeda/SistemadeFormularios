using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemadeFormularios
{
    public partial class EmployeeForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-DC5MS3H;Initial Catalog=companiadb4;Integrated Security=True";

        public EmployeeForm()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT EmployeeID, FirstName, LastName, JobID, Salary, HireDate FROM Employees";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewEmployees.DataSource = dt;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                !decimal.TryParse(txtSalary.Text, out decimal salary) ||
                !DateTime.TryParse(txtHireDate.Text, out DateTime hireDate))
            {
                MessageBox.Show("Por favor, completa todos los campos correctamente.");
                return;
            }

            string query = "INSERT INTO Employees (FirstName, LastName, JobID, Salary, HireDate) VALUES (@FirstName, @LastName, @JobID, @Salary, @HireDate)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@JobID", Convert.ToInt32(cmbJobID.SelectedItem));
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    cmd.Parameters.AddWithValue("@HireDate", hireDate);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Empleado guardado correctamente.");
            LoadEmployees();
            ClearForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                !decimal.TryParse(txtSalary.Text, out decimal salary) ||
                !DateTime.TryParse(txtHireDate.Text, out DateTime hireDate) ||
                !int.TryParse(txtEmployeeID.Text, out int employeeID))
            {
                MessageBox.Show("Por favor, completa todos los campos correctamente.");
                return;
            }

            string query = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, JobID = @JobID, Salary = @Salary, HireDate = @HireDate WHERE EmployeeID = @EmployeeID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@JobID", Convert.ToInt32(cmbJobID.SelectedItem)); 
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    cmd.Parameters.AddWithValue("@HireDate", hireDate);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Empleado actualizado correctamente.");
            LoadEmployees();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtEmployeeID.Text, out int employeeID))
            {
                MessageBox.Show("Por favor, ingrese un ID de empleado válido.");
                return;
            }

            string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Empleado eliminado correctamente.");
            LoadEmployees();
            ClearForm();
        }

        private void ClearForm()
        {
            txtEmployeeID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtSalary.Clear();
            txtHireDate.Clear();
            cmbJobID.SelectedIndex = -1; 
        }

        private void dataGridViewEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.CurrentRow != null)
            {
                txtEmployeeID.Text = dataGridViewEmployees.CurrentRow.Cells["EmployeeID"].Value.ToString();
                txtFirstName.Text = dataGridViewEmployees.CurrentRow.Cells["FirstName"].Value.ToString();
                txtLastName.Text = dataGridViewEmployees.CurrentRow.Cells["LastName"].Value.ToString();
                txtSalary.Text = dataGridViewEmployees.CurrentRow.Cells["Salary"].Value.ToString();
                txtHireDate.Text = dataGridViewEmployees.CurrentRow.Cells["HireDate"].Value.ToString();
                cmbJobID.SelectedItem = dataGridViewEmployees.CurrentRow.Cells["JobID"].Value.ToString(); 
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

