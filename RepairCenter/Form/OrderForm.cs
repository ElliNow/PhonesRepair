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
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        public void Update()
        {
            listBox1.Items.Clear();

            using (MyContext context = new MyContext())
            {
                var ls = context.Orders.ToList();

                foreach (var i in ls)
                {
                    listBox1.Items.Add(i);
                }  
            } 
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            Update();   
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var item = ((Order)listBox1.SelectedItem).Id;
                OrderView view = new OrderView(item);
                view.ShowDialog();
                this.Show();
            }
            catch
            {
                MessageBox.Show("Выберите заказ");
            }
            
        }

        //фильтр активных заказов
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            using(MyContext context = new MyContext())
            {
                var ls = context.Orders.ToList();

                if(radioButton2.Checked == true)
                {
                    listBox1.Items.Clear();
                    foreach(Order i in ls.Where(x => 
                        x.BeginTime != null && x.EndTime == null))
                    {
                        listBox1.Items.Add(i);
                    }
                }
            }
        }

        //фильтр отмененных заказов
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            using (MyContext context = new MyContext())
            {
                var ls = context.Orders.ToList();

                if (radioButton3.Checked == true)
                {
                    listBox1.Items.Clear();
                    foreach (Order i in ls.Where(x => x.BeginTime == null))
                    {
                        listBox1.Items.Add(i);
                    }
                }
                else if (radioButton3.Checked == false)
                {
                    listBox1.Items.Clear();
                    Update();
                }
            }
        }

        //фильтр завершенных заказов
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            using (MyContext context = new MyContext())
            {
                var ls = context.Orders.ToList();
                if (radioButton1.Checked == true)
                {
                    listBox1.Items.Clear();
                    foreach (Order i in ls.Where(x => x.EndTime != null))
                    {

                        listBox1.Items.Add(i);
                    }
                }
                else if (radioButton1.Checked == false)
                {
                    listBox1.Items.Clear();
                    Update();
                }
            }
        }
    }
}
