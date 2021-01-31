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
    public partial class DetailRegistr : Form
    {

        public DetailRegistr()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            listBox1.Items.Clear();

            using(MyContext context = new MyContext())
            {
                var ls = context.Details.ToList();

                foreach (var i in ls)
                {
                    listBox1.Items.Add(i);
                }

                context.SaveChanges();
            }  
        }

        public void Delete(object sender, EventArgs e)
        {
            using (MyContext context = new MyContext())
            {
                var detail = (Detail)listBox1.SelectedItem;
                context.Details.Remove(context.Details.Find(detail.Id));
                context.SaveChanges();
            }

            Update();
        }

        public void Add(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Не все поля были заполнены");
                return;
            }

            Detail detail = new Detail();
            try
            {
                detail.Name = textBox1.Text;
                detail.Provider = textBox2.Text;
                detail.Price = Convert.ToDecimal(textBox3.Text);
                detail.WaitTime = Convert.ToInt32(textBox4.Text);
            }
            catch
            {

            }

            if (checkBox1.Checked) detail.InStock = true;
            else detail.InStock = false;

            using (MyContext context = new MyContext())
            {
                if (context.Details.Any(d => d.Name == detail.Name))
                {
                    MessageBox.Show("Уже существует");
                }
                else
                {
                    context.Details.Add(detail);
                    context.SaveChanges();
                }
            }

            Update();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var detail = (Detail)listBox1.SelectedItem;
                textBox1.Text = detail.Name;
                textBox2.Text = detail.Provider;
                textBox3.Text = Convert.ToString(detail.Price);
                textBox4.Text = Convert.ToString(detail.WaitTime);

                if (detail.InStock)
                {
                    checkBox1.Checked = true;
                }
                else checkBox1.Checked = false;
            }
        }
    }
}
