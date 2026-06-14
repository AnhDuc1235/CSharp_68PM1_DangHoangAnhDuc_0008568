using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class ucQLSV : UserControl
    {
        databaseDataContext db = new databaseDataContext();

        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPage = 0;
        private string SelectedMalop;

        public ucQLSV()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int mssv = int.Parse(textBox1.Text);

            var sinhvien = db.dbo_tbl_sinhviens.FirstOrDefault(s => s.MSSV == mssv);
            if (sinhvien != null)
            {
                sinhvien.Hoten = textBox2.Text;
                sinhvien.ngaysinh = DateTime.Parse(dateTimePicker1.Text);
                sinhvien.gioitinh = comboBox1.Text;
                sinhvien.lop = comboBox2.SelectedValue.ToString();
                db.SubmitChanges();
                MessageBox.Show("Cập nhật thành công");
                LoadData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int mssv = int.Parse(textBox1.Text);

            var sinhvien = db.dbo_tbl_sinhviens.FirstOrDefault(s => s.MSSV == mssv);
            if (sinhvien != null)
            {
                db.dbo_tbl_sinhviens.DeleteOnSubmit(sinhvien);
                db.SubmitChanges();
                MessageBox.Show("Xóa thành công");
                LoadData();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["MSSV"].Value?.ToString().Trim();
                textBox2.Text = row.Cells["Hoten"].Value?.ToString().Trim();
                dateTimePicker1.Text = row.Cells["ngaysinh"].Value?.ToString();
                comboBox1.Text = row.Cells["gioitinh"].Value?.ToString().Trim();
                comboBox2.Text = row.Cells["lop"].Value?.ToString().Trim();
            }
        }

        private void ucQLSV_Load(object sender, EventArgs e)
        {
            LoadData();
            loadDSSVCBX();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string masv = textBox1.Text;
            string hoten = textBox2.Text;
            string dateTime = dateTimePicker1.Text;
            dbo_tbl_sinhvien sinhvien = new dbo_tbl_sinhvien();
            sinhvien.MSSV = int.Parse(masv);
            sinhvien.Hoten = hoten;
            sinhvien.ngaysinh = DateTime.Parse(dateTime);
            sinhvien.gioitinh = comboBox1.Text;
            sinhvien.lop = comboBox2.SelectedValue.ToString();

            try
            {
                db.dbo_tbl_sinhviens.InsertOnSubmit(sinhvien);
                db.SubmitChanges();
                LoadData();
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

            var query = db.dbo_tbl_sinhviens.AsQueryable();

            if (!string.IsNullOrEmpty(SelectedMalop))
            {
                query = query.Where(x => x.lop == SelectedMalop);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x =>
                    x.Hoten.Contains(keyword) ||
                    x.lop.Contains(keyword) ||
                    x.MSSV.ToString().Contains(keyword));
            }

            totalPage = (int)Math.Ceiling((double)query.Count() / pageSize);
            if (totalPage == 0) totalPage = 1;
            if (currentPage > totalPage) currentPage = totalPage;
            if (currentPage < 1) currentPage = 1;

            dataGridView1.DataSource = query
                .OrderBy(x => x.MSSV)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            button7.Enabled = currentPage > 1;
            button9.Enabled = currentPage < totalPage;
            button6.Enabled = currentPage != 1;
            button8.Enabled = currentPage != totalPage;

            label8.Text = $"Trang {currentPage}/{totalPage}";
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

        private void button6_Click(object sender, EventArgs e)
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
        public void loadDSSVCBX()
        {
            comboBox1.Items.Add("Nam");
            comboBox1.Items.Add("Nữ");
            comboBox1.SelectedIndex = 0;

            List<dbo_tbl_lophoc> dslh = db.dbo_tbl_lophocs.ToList();
            comboBox2.DisplayMember = "malop";
            comboBox2.ValueMember = "malop";
            comboBox2.DataSource = dslh;
        }
    }
}
