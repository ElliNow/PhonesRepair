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
    public partial class RequestAdd : Form
    {
        public RequestAdd()
        {
            InitializeComponent();
         
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            if (string.IsNullOrWhiteSpace(richTextBox1.Text) ||
                string.IsNullOrWhiteSpace(richTextBox2.Text) ||
                string.IsNullOrWhiteSpace(richTextBox3.Text) ||
                string.IsNullOrWhiteSpace(richTextBox4.Text))
            {
                MessageBox.Show("Не все поля были заполнены");
                return;
            }

            Request request = new Request();
            request.ClientName = richTextBox1.Text;
            request.ClientPhone = richTextBox2.Text;
            request.Device = richTextBox3.Text;
            request.Description = richTextBox4.Text;

            using (MyContext context = new MyContext())
            {
                context.Requests.Add(request);
                context.SaveChanges();
                MessageBox.Show("Заявка добавлена");
            }

            this.Close();
            
        }   
    }
}
