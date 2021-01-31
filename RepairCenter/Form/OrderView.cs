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
    public partial class OrderView : Form
    {
        private int orderId;
        
        public OrderView(int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
        }

        private void OrderView_Load(object sender, EventArgs e)
        {

            UpdateRepairTypes();
            UpdateEmployees();
            UpdateDetails();

            using (MyContext context = new MyContext())
            {
                var order = context.Orders.Find(orderId);
                textBox1.Text = order.Request.ClientName;
                textBox2.Text = order.Request.ClientPhone;
                textBox3.Text = order.Request.Device;
                richTextBox1.Text = order.Request.Description;
                textBox4.Text =  order.BeginTime.ToString();
                textBox5.Text = order.EndTime.ToString();
                //цена
                var ls = context.OrderDetail.ToList();
                List<Detail> details = new List<Detail>();
                foreach(OrderDetail i in ls)
                {
                    if (i.OrderId == orderId)
                    {
                        details.Add(i.Detail);  
                    }     
                }

                foreach (var i in details)
                {
                    order.Price = order.RepairType.Price + i.Price;

                }
                textBox6.Text = order.Price.ToString();

                if (order.EndTime != null) button1.Enabled = false;
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MyContext context = new MyContext())
            {
                var order = context.Orders.Find(orderId);
                
                order.EndTime = DateTime.Now;
                textBox5.Text = order.EndTime.ToString();
                context.SaveChanges();
            }   
        }

        //Изменение
        private void button2_Click(object sender, EventArgs e)
        {
            using(MyContext context = new MyContext())
            {
                var order = context.Orders.Find(orderId);

                order.RepairTypeId = ((RepairType)listBox1.SelectedItem).Id;
                order.EmployeeId = ((Employee)listBox3.SelectedItem).Id;

                var ls = context.OrderDetail.Where(x => x.OrderId == order.Id).ToList();
                context.OrderDetail.RemoveRange(ls);

                foreach (Detail i in listBox4.Items)
                {
                    context.OrderDetail.Add(new OrderDetail { OrderId = order.Id, DetailId = i.Id });
                }
                context.SaveChanges();
                MessageBox.Show("Заказ изменен");
            }
        }

        public void UpdateRepairTypes()
        {
            listBox1.Items.Clear();
            using (MyContext context = new MyContext())
            {
                var order = context.Orders.Find(orderId);

                var ls = context.RepairTypes.ToList();
                foreach (var i in ls)
                {
                    listBox1.Items.Add(i);
                    if (i.Id == order.RepairTypeId)
                    {
                        listBox1.SelectedItem =
                            listBox1.Items[listBox1.Items.Count - 1];
                    }
                }
            }
        }

        public void UpdateDetails()
        {
            listBox2.Items.Clear();

            using (MyContext context = new MyContext())
            {
                var order = context.Orders.Find(orderId);
                var ls = context.Details.ToList();
                foreach (var i in ls)
                {
                    bool isUsed =
                        context.OrderDetail.Any(od =>
                        od.OrderId == order.Id &&
                        od.DetailId == i.Id);
                    if (isUsed)
                    {
                        listBox4.Items.Add(i);
                    }
                    else listBox2.Items.Add(i);
                }
            }
        }

        public void UpdateEmployees()
        {
            listBox3.Items.Clear();

            using (MyContext context = new MyContext())
            {
                var ls = context.Employees.ToList();
                var order = context.Orders.Find(orderId);

                foreach (var i in ls)
                {
                    listBox3.Items.Add(i);
                    if (i.Id == order.EmployeeId)
                    {
                        listBox3.SelectedItem =
                            listBox3.Items[listBox3.Items.Count - 1];
                    }
                }
            }
        }

        //Добавление вида ремонта
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            RepairTypeAdd addRepair = new RepairTypeAdd();
            addRepair.ShowDialog();
            this.Show();
        }

        //Добавление мастера
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeRegistr empl = new EmployeeRegistr();
            empl.ShowDialog();
            this.Show();
        }

        //Добавление детали
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            DetailRegistr addDetail = new DetailRegistr();
            addDetail.ShowDialog();
            this.Show();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var item = (Detail)listBox4.SelectedItem;
                listBox2.Items.Add(item);
                listBox4.Items.Remove(item);
            }
            catch
            {
                MessageBox.Show("Выберите деталь");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var item = (Detail)listBox2.SelectedItem;
            listBox4.Items.Add(item);
            listBox2.Items.Remove(item);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (MyContext context = new MyContext())
            {
                var order = context.Orders.Find(orderId);
                order.BeginTime = null;
                context.SaveChanges();
            }
            this.Close();
        }
    }
}
