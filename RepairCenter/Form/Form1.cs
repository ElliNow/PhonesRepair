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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            RequestAdd addRequest = new RequestAdd();
            addRequest.ShowDialog();
            this.Show();
            Update();
        }

        public void Update()
        {
            listBox1.Items.Clear();

            using (MyContext context = new MyContext())
            {
                var ls = context.Requests.ToList();

                foreach (var i in ls)
                {
                    listBox1.Items.Add(i);
                }

                context.SaveChanges();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrderForm order = new OrderForm();
            order.ShowDialog();
            this.Show();
            Update();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            var request = (Request)listBox1.SelectedItem;

            OrderRegistr orderRegistr = new OrderRegistr(request);
            orderRegistr.ShowDialog();
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Update();
        }
    }
}
