using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyThongTinSinhVien1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (cmbChuyenNganh.Items.Count > 0)
                cmbChuyenNganh.SelectedIndex = 0;


            radNu.Checked = true;

            CapNhatThongKe();
        }

        private void grpThongTin_Enter(object sender, EventArgs e)
        {
     
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnThemSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaSinhVien.Text) ||
                    string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(txtDiemTB.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtDiemTB.Text.Trim(),
                        NumberStyles.Any, CultureInfo.InvariantCulture, out double diem))
                {
                    MessageBox.Show("Điểm phải là số!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (diem < 0 || diem > 10)
                {
                    MessageBox.Show("Điểm phải trong khoảng 0 - 10!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string maSV = txtMaSinhVien.Text.Trim();
                int rowIndex = TimDongTheoMaSV(maSV);

                if (rowIndex == -1)
                {
             
                    int newRow = dgvSinhVien.Rows.Add();
                    CapNhatDong(newRow);
                    MessageBox.Show("Thêm mới dữ liệu thành công!");
                }
                else
                {

                    CapNhatDong(rowIndex);
                    MessageBox.Show("Cập nhật dữ liệu thành công!");
                }

                CapNhatThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvSinhVien.Rows[e.RowIndex];

                txtMaSinhVien.Text = row.Cells["colMaSV"].Value?.ToString();
                txtHoTen.Text = row.Cells["colHoTen"].Value?.ToString();
                txtDiemTB.Text = row.Cells["colDiemTB"].Value?.ToString();
                cmbChuyenNganh.Text = row.Cells["colChuyenNganh"].Value?.ToString();

                string gt = row.Cells["colGioiTinh"].Value?.ToString();
                radNam.Checked = (gt == "Nam");
                radNu.Checked = (gt == "Nữ");
            }
        }

        private int TimDongTheoMaSV(string maSV)
        {
            for (int i = 0; i < dgvSinhVien.Rows.Count; i++)
            {
                if (dgvSinhVien.Rows[i].Cells["colMaSV"].Value?.ToString() == maSV)
                {
                    return i;
                }
            }
            return -1;
        }

        private void CapNhatDong(int rowIndex)
        {
            dgvSinhVien.Rows[rowIndex].Cells["colMaSV"].Value = txtMaSinhVien.Text.Trim();
            dgvSinhVien.Rows[rowIndex].Cells["colHoTen"].Value = txtHoTen.Text.Trim();
            dgvSinhVien.Rows[rowIndex].Cells["colGioiTinh"].Value = radNam.Checked ? "Nam" : "Nữ";
            dgvSinhVien.Rows[rowIndex].Cells["colDiemTB"].Value =
                double.Parse(txtDiemTB.Text.Trim(), CultureInfo.InvariantCulture).ToString("0.00");
            dgvSinhVien.Rows[rowIndex].Cells["colChuyenNganh"].Value = cmbChuyenNganh.Text;
        }

        // 🔹 Đếm lại Nam/Nữ
        private void CapNhatThongKe()
        {
            int nam = 0, nu = 0;
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                string gt = row.Cells["colGioiTinh"].Value?.ToString();
                if (gt == "Nam") nam++;
                if (gt == "Nữ") nu++;
            }

            lblTongNam.Text = $"Tổng SV Nam: {nam}";
            lblTongNu.Text = $"Tổng SV Nữ: {nu}";
        }
    }
}