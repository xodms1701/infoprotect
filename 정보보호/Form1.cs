using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 정보보호
{
    public partial class Form1 : Form
    {
        
        private Engine eng;
        public Form1()
        {
            InitializeComponent();
            eng = new Engine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resultLable.Text = "";
            String Key = "";
            String Context = "";
            try
            {
                Key = tKey.Text;
            }
            catch
            {
                MessageBox.Show("키가 이상합니다.");
                tKey.Text = "";
                return;
            }
            try
            {
                Context = tContext.Text;
            }
            catch
            {
                MessageBox.Show("평문이 이상합니다.");
                tContext.Text = "";
                return;
            }
            eng.Einit(Key, Context);
            resultLable.Text = eng.GetContext();
            eng.RemoveResult();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            resultLable.Text = "";
            String Key = "";
            String Context = "";
            try
            {
                Key = tKey.Text;
            }
            catch
            {
                MessageBox.Show("키가 이상합니다.");
                tKey.Text = "";
                return;
            }
            try
            {
                Context = tContext.Text;
            }
            catch
            {
                MessageBox.Show("평문이 이상합니다.");
                tContext.Text = "";
                return;
            }
            eng.Binit(Key, Context);
            resultLable.Text = eng.GetContext();
            eng.RemoveResult();
        }
    }
}
