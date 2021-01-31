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
    public partial class RepairTypeAdd : Form
    {
        public RepairTypeAdd()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
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

        public void Delete(object sender, EventArgs e)
        {
            using (MyContext context = new MyContext())
            {
                var repair = (RepairType)listBox1.SelectedItem;
                
                context.RepairTypes.Remove(context.RepairTypes.Find(repair.Id));
                context.SaveChanges();
            }

            Update();
        }

        public void Add(object sender, EventArgs e)
        {
            try
            {
                RepairType repair = new RepairType();
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Не все поля были заполнены");
                    return;
                }
                repair.Name = textBox1.Text;
                repair.LeadTime = Convert.ToInt32(textBox2.Text);
                repair.Price = Convert.ToDecimal(textBox3.Text);

                using (MyContext context = new MyContext())
                {
                    if (context.RepairTypes.Any(rep => rep.Name == repair.Name))
                    {
                        MessageBox.Show("Уже существует");
                    }
                    else
                    {
                        context.RepairTypes.Add(repair);
                        context.SaveChanges();
                        MessageBox.Show("Ремонт добавлен");
                    }
                }

                Update();
            }
            catch
            {
                return;
            }    
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var repairType = (RepairType)listBox1.SelectedItem;
                textBox1.Text = repairType.Name;
                textBox2.Text = Convert.ToString(repairType.LeadTime);
                textBox3.Text = Convert.ToString(repairType.Price);
            }
        }
    }
}
