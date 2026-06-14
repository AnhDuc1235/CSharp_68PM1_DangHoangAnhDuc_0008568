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
            List<dbo_tbl_sinhvien> dssv = db.dbo_tbl_sinhviens.ToList();
            dataGridView1.DataSource = dssv;
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
