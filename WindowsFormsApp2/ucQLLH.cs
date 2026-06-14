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

        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPage = 0;

        public ucQLLH()
        {
            InitializeComponent();
        }

        private void ucQLLH_Load(object sender, EventArgs e)
        {
            LoadData();
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
            string keyword = maskedTextBox1.Text.Trim();

            var query = db.dbo_tbl_lophocs.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x =>
                    x.malop.Contains(keyword) ||
                    x.tenlop.Contains(keyword) ||
                    x.maid.Contains(keyword));
            }

            totalPage = (int)Math.Ceiling((double)query.Count() / pageSize);
            if (totalPage == 0) totalPage = 1;
            if (currentPage > totalPage) currentPage = totalPage;
            if (currentPage < 1) currentPage = 1;

            dataGridView1.DataSource = query
                .OrderBy(x => x.id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            button7.Enabled = currentPage > 1;
            button9.Enabled = currentPage < totalPage;
            button10.Enabled = currentPage != 1;
            button8.Enabled = currentPage != totalPage;

            label6.Text = $"Trang {currentPage}/{totalPage}";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["maid"].Value?.ToString().Trim();
                textBox2.Text = row.Cells["malop"].Value?.ToString().Trim();
                textBox3.Text = row.Cells["tenlop"].Value?.ToString().Trim();
                dateTimePicker1.Text = row.Cells["ngay"].Value?.ToString();
                richTextBox1.Text = row.Cells["ghichu"].Value?.ToString().Trim();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string malop = textBox2.Text;

            var lophoc = db.dbo_tbl_lophocs.FirstOrDefault(s => s.malop == malop);
            if (lophoc != null)
            {
                lophoc.maid = textBox1.Text;
                lophoc.tenlop = textBox3.Text;
                lophoc.ngay = DateTime.Parse(dateTimePicker1.Text);
                lophoc.ghichu = richTextBox1.Text;
                db.SubmitChanges();
                MessageBox.Show("Cập nhật thành công");
                LoadData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string malop = textBox2.Text;

            var lophoc = db.dbo_tbl_lophocs.FirstOrDefault(s => s.malop == malop);
            if (lophoc != null)
            {
                db.dbo_tbl_lophocs.DeleteOnSubmit(lophoc);
                db.SubmitChanges();
                MessageBox.Show("Xóa thành công");
                currentPage = 1;
                LoadData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Clear();
            currentPage = 1;
            LoadData();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentPage--;
            LoadData();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            currentPage = totalPage;
            LoadData();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadData();
        }
    }
}
