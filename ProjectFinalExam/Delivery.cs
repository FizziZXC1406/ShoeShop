using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectFinalExam
{
    public partial class frmDelivery : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr);
        public frmDelivery()
        {
            InitializeComponent();
        }

        #region frmDelivery_Load
        private void frmDelivery_Load(object sender, EventArgs e)
        {
            datagridviewShowInvoice.Visible = false;
            datagridviewShowInvoiceDetails.Visible = false;
            lblDontHaveInvoice.Visible = false;
            btnDelivery.ForeColor = Color.Black;
            btnDelivery.FillColor = Color.White;

            string countQuery = "SELECT COUNT(*) FROM Invoice WHERE UserID = @id AND StatusInvoice = 'Confirm'";
            string selectQuery = "SELECT ID, Date, CustomerName, CustomerPhoneNumber, DeliveryAddress, TotalPayment, StatusInvoice " +
                                 "FROM Invoice WHERE UserID = @id AND StatusInvoice = 'Confirm'";

            try
            {
                connection.Open();

                SqlCommand countCMD = new SqlCommand(countQuery, connection);
                countCMD.Parameters.AddWithValue("@id", UserSession.Instance.CurrentUserID);
                int count = Convert.ToInt32(countCMD.ExecuteScalar());

                if (count > 0)
                {
                    SqlCommand selectCMD = new SqlCommand(selectQuery, connection);
                    selectCMD.Parameters.AddWithValue("@id", UserSession.Instance.CurrentUserID);
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCMD);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    datagridviewShowInvoice.AllowUserToAddRows = false;
                    datagridviewShowInvoice.DataSource = dataTable;

                    // Đặt tiêu đề cho các cột
                    datagridviewShowInvoice.Columns["Date"].HeaderText = "Date";
                    datagridviewShowInvoice.Columns["CustomerName"].HeaderText = "Customer Name";
                    datagridviewShowInvoice.Columns["CustomerPhoneNumber"].HeaderText = "Phone Number";
                    datagridviewShowInvoice.Columns["DeliveryAddress"].HeaderText = "Delivery Address";
                    datagridviewShowInvoice.Columns["TotalPayment"].HeaderText = "Total Payment";
                    datagridviewShowInvoice.Columns["StatusInvoice"].HeaderText = "Status";

                    // Ẩn cột ID
                    datagridviewShowInvoice.Columns["ID"].Visible = false;

                    // Lưu ID vào thuộc tính Tag của từng hàng
                    foreach (DataGridViewRow row in datagridviewShowInvoice.Rows)
                    {
                        row.Tag = row.Cells["ID"].Value;
                        row.Height = 60;
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                    }

                    datagridviewShowInvoice.Visible = true;
                    datagridviewShowInvoiceDetails.Visible = true;
                }
                else
                {
                    lblDontHaveInvoice.Text = "You don't have any orders yet";
                    lblDontHaveInvoice.Visible = true;
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading delivery data: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        #endregion

        #region btnReceived_Click
        private void btnReceived_Click(object sender, EventArgs e)
        {
            if (datagridviewShowInvoice.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Choose an order to confirm delivery");
                return;
            }
            try
            {
                connection.Open();
                foreach (DataGridViewRow row in datagridviewShowInvoice.SelectedRows)
                {
                    int id = Convert.ToInt32(row.Tag);
                    string updateStatusInvoiceQuery = "UPDATE Invoice SET StatusInvoice = 'Delivered' WHERE ID = @id";
                    SqlCommand updateCMD = new SqlCommand(updateStatusInvoiceQuery, connection);
                    updateCMD.Parameters.AddWithValue("@id", id);
                    updateCMD.ExecuteNonQuery();
                }
                MessageBox.Show("Received successfully <3 moah moah");
                connection.Close();
                frmDelivery_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error Received Click: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        #endregion

        #region btnViewDetails_Click
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (datagridviewShowInvoice.RowCount == 0)
            {
                MessageBox.Show("You don't have any orders yet");
                return;
            }

            if (datagridviewShowInvoice.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to view details");
                return;
            }

            int selectedRowIndex = datagridviewShowInvoice.SelectedRows[0].Index;
            int invoiceID = Convert.ToInt32(datagridviewShowInvoice.Rows[selectedRowIndex].Cells["ID"].Value.ToString());

            try
            {
                connection.Open();
                string selectQuery = "SELECT InvoiceID, ProductName, Size, Quantity, Price FROM InvoiceDetails WHERE InvoiceID = @invoiceID";
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                cmd.Parameters.AddWithValue("@invoiceID", invoiceID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                datagridviewShowInvoiceDetails.AllowUserToAddRows = false;

                datagridviewShowInvoiceDetails.DataSource = dataTable;

                datagridviewShowInvoiceDetails.Columns["InvoiceID"].HeaderText = "Invoice ID";
                datagridviewShowInvoiceDetails.Columns["ProductName"].HeaderText = "Product Name";
                datagridviewShowInvoiceDetails.Columns["Size"].HeaderText = "Size";
                datagridviewShowInvoiceDetails.Columns["Quantity"].HeaderText = "Quantity";
                datagridviewShowInvoiceDetails.Columns["Price"].HeaderText = "Price";

                foreach (DataGridViewRow row in datagridviewShowInvoiceDetails.Rows)
                {
                    row.Height = 60;
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice details: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        #endregion

        #region btnUserAccSetting_Click
        private void btnUserAccSetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmUserAccountSetting frmUserAccountSetting = new frmUserAccountSetting();
            frmUserAccountSetting.ShowDialog();
            this.Show();
        }
        #endregion

        #region btnCart_Click
        private void btnCart_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCart frmCart = new frmCart();
            frmCart.ShowDialog();
            this.Show();
        }
        #endregion

        #region btnShoe_Click
        private void btnShoe_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmShoe frmShoe = new frmShoe();
            frmShoe.ShowDialog();
            this.Close();
        }
        #endregion

        #region circlePictureBoxLogoMainPage_Click
        private void circlePictureBoxLogoMainPage_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMainPageUser frmMainPage = new frmMainPageUser();
            frmMainPage.ShowDialog();
            this.Close();
        }
        #endregion
    }
}