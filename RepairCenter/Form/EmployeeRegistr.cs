using RepairCenter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepairCenter
{
    public partial class EmployeeRegistr : Form
    {
        public EmployeeRegistr()
        {
            InitializeComponent();
            Update();
        }

        public void Add(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Не все поля были заполнены");
                return;
            }

            Employee employee = new Employee();
            employee.Name = textBox1.Text;
            employee.Experience = textBox2.Text;

            using (MyContext context = new MyContext())
            {
                if (context.Employees.Any(empl => empl.Name == employee.Name))
                {
                    MessageBox.Show("Уже существует");
                }
                else
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                }
            }

            Update();
        }

        public void Update()
        {
            listBox1.Items.Clear();
            using(MyContext context = new MyContext())
            {
                var ls = context.Employees.ToList();

                foreach (var i in ls)
                {
                    listBox1.Items.Add(i);
                }

                context.SaveChanges();
            }   
        }

        public void Delete(object sender, EventArgs e)
        {
            using(MyContext context = new MyContext())
            {
                var empl = (Employee)listBox1.SelectedItem;
                context.Employees.Remove(context.Employees.Find(empl.Id));
                context.SaveChanges();
            }
            
            Update();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var empl = (Employee)listBox1.SelectedItem;
                textBox1.Text = empl.Name;
                textBox2.Text = empl.Experience;
            }
            
        }
    }
}
