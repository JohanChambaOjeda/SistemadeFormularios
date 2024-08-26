using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeFormularios.Models
{
    internal class Job
    {
    }
}
public class Job
{
    public int JobID { get; set; }
    public string JobTitle { get; set; }
    public decimal MinSalary { get; set; }

    public decimal MaxSalary { get; set; }
}

