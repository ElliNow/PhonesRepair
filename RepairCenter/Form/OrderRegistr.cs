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
    public partial class OrderRegistr : Form
    {
        public Request request;
        public OrderRegistr(Request request)
        {
            InitializeComponent();
            UpdateRepairTypes();
            UpdateDetails();
            UpdateEmployees();

            this.request = request;
            textBox1.Text = request.ClientName;
            textBox2.Text = request.ClientPhone;
            textBox3.Text = request.Device;
            richTextBox1.Text = request.Description;
        }

        public void UpdateRepairTypes()
        {
            listBox1.Items.Clear();

            using (MyContext context = new MyContext())
            {
                var ls = context.RepairTypes.ToList();

                foreach (var i in ls)
                {
                    listBox1.Items.Add(i);
                }

                context.SaveChanges();
            }
        }

        public void UpdateDetails()
        {
            listBox2.Items.Clear();

            using (MyContext context = new MyContext())
            {
                var ls = context.Details.ToList();

                foreach (var i in ls)
                {
                    listBox2.Items.Add(i);
                }

                context.SaveChanges();
            }
        }

        public void UpdateEmployees()
        {
            listBox3.Items.Clear();

            using (MyContext context = new MyContext())
            {
                var ls = context.Employees.ToList();

                foreach (var i in ls)
                {
                    listBox3.Items.Add(i);
                }

                context.SaveChanges();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            RepairTypeAdd repair = new RepairTypeAdd();
            repair.ShowDialog();
            this.Show();
            UpdateRepairTypes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Order order = new Order();
                order.RepairTypeId = ((RepairType)listBox1.SelectedItem).Id;
                order.EmployeeId = ((Employee)listBox3.SelectedItem).Id;
                order.RequestId = request.Id;
                order.BeginTime = DateTime.Today;

                using (MyContext context = new MyContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                    foreach (Detail i in listBox4.Items)
                    {
                        context.OrderDetail.Add(new OrderDetail { OrderId = order.Id, DetailId = i.Id });

                    }
                    context.SaveChanges();
                }
                this.Close();
                throw new Exception();
            }
            catch
            {
                MessageBox.Show("Не все поля были выбраны");
            }
            
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            DetailRegistr detail = new DetailRegistr();
            detail.ShowDialog();
            this.Show();
            UpdateDetails();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeRegistr empl = new EmployeeRegistr();
            empl.ShowDialog();
            this.Show();
            UpdateEmployees();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var item = (Detail)listBox2.SelectedItem;
            listBox4.Items.Add(item);
            listBox2.Items.Remove(item); 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var item = (Detail)listBox4.SelectedItem;
            listBox2.Items.Add(item);
            listBox4.Items.Remove(item);
        }
    }
}
