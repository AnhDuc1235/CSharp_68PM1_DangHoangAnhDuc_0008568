using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class ucQLLH : UserControl
    {
        databaseDataContext db = new databaseDataContext();
        public ucQLLH()
        {
            InitializeComponent();
        }

        private void ucQLLH_Load(object sender, EventArgs e)
        {
            List<dbo_tbl_lophoc> dslh = db.dbo_tbl_lophocs.ToList();
            dataGridView1.DataSource = dslh;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string maid = textBox1.Text;
            string malop = textBox2.Text;
            string tenlop = textBox3.Text;
            string dateTime = dateTimePicker1.Text;
            string ghichu = richTextBox1.Text;
            dbo_tbl_lophoc lophoc = new dbo_tbl_lophoc();
            lophoc.maid = maid;
            lophoc.ngay = DateTime.Parse(dateTime);
            lophoc.ghichu = ghichu;
            lophoc.tenlop = tenlop;
            lophoc.malop = malop;

            try
            {
                db.dbo_tbl_lophocs.InsertOnSubmit(lophoc);
                db.SubmitChanges();
                LoadData();
                //loadDSLHCBX();
                MessageBox.Show("thành công");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }


        }

        public void LoadData()
        {
            List<dbo_tbl_lophoc> dslh = db.dbo_tbl_lophocs.ToList();
            dataGridView1.DataSource = dslh;
        }

        //ở dưới là ví dụ làm cái dropdownlist   
        //public void loadDSLHCBX()
        //{
        //    List<dbo_tbl_lophoc> dslh = db.dbo_tbl_lophocs.ToList();
        //    comboBox1.DataSource = dslh;
        //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
