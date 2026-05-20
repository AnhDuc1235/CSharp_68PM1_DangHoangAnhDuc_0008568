using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
<<<<<<< HEAD
=======
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
>>>>>>> b6ac06088fe2f3618f60005dfad24cae287ce768

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
<<<<<<< HEAD
=======

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            if (username == "0008568@st.huce.edu.vn" && password == "0008568")
            {
                MessageBox.Show("đăng nhập thành công");
            }
            else
            {
                MessageBox.Show("đăng nhập thất bại");
            }
        }
>>>>>>> b6ac06088fe2f3618f60005dfad24cae287ce768
    }
}
