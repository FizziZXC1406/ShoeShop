using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace ProjectFinalExam
{
    public partial class frmMainPageAdmin : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr);
        public frmMainPageAdmin()
        {
            InitializeComponent();
            tabcontrolManagement.SelectedIndexChanged += tabcontrolManagement_SelectedIndexChanged;
        }

        #region AdminMainPage_Load
        private void AdminMainPage_Load(object sender, EventArgs e)
        {
            tooltipLoadData.SetToolTip(btnLoad,
                "Load Data");
            tooltipViewDetails.SetToolTip(btnViewDetails,
                "View Details");
        }
        #endregion

        #region tabcontrolManagement_SelectedIndexChanged
        private void tabcontrolManagement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabcontrolManagement.SelectedTab == tabpageInvoiceManagement)
            {
                LoadInvoiceData();
                LoadInvoiceDetailsData();
            }
        }
        #endregion

        #region LoadInvoiceData
        private void LoadInvoiceData()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                string InvoiceQuery = "SELECT * FROM Invoice";
                SqlCommand InvoiceCMD = new SqlCommand(InvoiceQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(InvoiceCMD);
                DataTable dataTable = new DataTable();
                adapter = new SqlDataAdapter(InvoiceCMD);
                adapter.Fill(dataTable);

                datagridviewInvoice.DataSource = dataTable;

                datagridviewInvoice.Columns["ID"].HeaderText = "ID";
                datagridviewInvoice.Columns["UserID"].HeaderText = "User ID";
                datagridviewInvoice.Columns["Date"].HeaderText = "Date";
                datagridviewInvoice.Columns["CustomerName"].HeaderText = "Customer Name";
                datagridviewInvoice.Columns["CustomerPhoneNumber"].HeaderText = "Phone Number";
                datagridviewInvoice.Columns["DeliveryAddress"].HeaderText = "Delivery Address";
                datagridviewInvoice.Columns["TotalPayment"].HeaderText = "Total Payment";
                datagridviewInvoice.Columns["StatusInvoice"].HeaderText = "Status";

                foreach (DataGridViewRow row in datagridviewInvoice.Rows)
                {
                    row.Height = 60;
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Invoice: " + ex.Message);
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

        #region LoadInvoiceDetailsData
        private void LoadInvoiceDetailsData()
        {
            try
            {
                string InvoiceDetailsQuery = "SELECT * FROM InvoiceDetails";
                SqlCommand InvoiceDetailsCMD = new SqlCommand(InvoiceDetailsQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(InvoiceDetailsCMD);
                DataTable dataTable = new DataTable();
                adapter = new SqlDataAdapter(InvoiceDetailsCMD);
                adapter.Fill(dataTable);

                datagridviewInvoiceDetails.DataSource = dataTable;

                datagridviewInvoiceDetails.Columns["InvoiceDetailsID"].HeaderText = "ID";
                datagridviewInvoiceDetails.Columns["InvoiceID"].HeaderText = "Invoice ID";
                datagridviewInvoiceDetails.Columns["ProductID"].HeaderText = "Product ID";
                datagridviewInvoiceDetails.Columns["SizeID"].HeaderText = "Size ID";
                datagridviewInvoiceDetails.Columns["ProductName"].HeaderText = "Product Name";
                datagridviewInvoiceDetails.Columns["Quantity"].HeaderText = "Quantity";
                datagridviewInvoiceDetails.Columns["Price"].HeaderText = "Price";

                foreach (DataGridViewRow row in datagridviewInvoiceDetails.Rows)
                {
                    row.Height = 40;
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Load Invoice Details: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region LoadProductData
        private void LoadProductData()
        {
            datagridviewProduct.DataSource = null;
            try
            {
                string ProductQuery = "SELECT ProductID, Name, Price FROM Product";
                connection.Open();
                SqlCommand ProductCMD = new SqlCommand(ProductQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(ProductCMD);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                datagridviewProduct.Columns.Clear();

                datagridviewProduct.DataSource = dataTable;

                foreach (DataGridViewRow row in datagridviewProduct.Rows)
                {
                    row.Height = 80;
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }
                datagridviewProduct.AllowUserToAddRows = false;

                // Thêm sự kiện CellClick để xử lý việc hiển thị ảnh
                datagridviewProduct.CellClick += DatagridviewProduct_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Load Product: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void DatagridviewProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo rằng dòng được chọn hợp lệ
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Lấy ProductID từ dòng được chọn
                    string productId = Convert.ToString(datagridviewProduct.Rows[e.RowIndex].Cells["ProductID"].Value);

                    // Truy vấn để lấy ảnh từ cơ sở dữ liệu
                    string ImageQuery = "SELECT Image FROM Product WHERE ProductID = @ProductID";
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(ImageQuery, connection);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    byte[] imageData = (byte[])cmd.ExecuteScalar();

                    if (imageData != null)
                    {
                        // Chuyển đổi dữ liệu ảnh từ byte[] sang Image và hiển thị trong PictureBox
                        MemoryStream ms = new MemoryStream(imageData);
                        pictureboxShowImage.Image = Image.FromStream(ms);
                    }
                    else
                    {
                        // Nếu không có ảnh, đặt PictureBox là null
                        pictureboxShowImage.Image = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        #endregion

        #region LoadProductSizeData
        private void LoadProductSizeData()
        {
            try
            {
                string ProductSizeQuery = "SELECT * FROM ProductSize";
                connection.Open();
                SqlCommand ProductSizeCMD = new SqlCommand(ProductSizeQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(ProductSizeCMD);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                datagridviewProductSize.DataSource = dataTable;

                foreach (DataGridViewRow row in datagridviewProductSize.Rows)
                {
                    row.Height = 40;
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }
                datagridviewProductSize.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Load Product Size: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region btnAdd_Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool checkAddData =
                string.IsNullOrEmpty(txtImage.Text) ||
                string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtProductID.Text) ||
                string.IsNullOrEmpty(txtPrice.Text) ? false : true;
            if (checkAddData == false)
            {
                MessageBox.Show(
                    "Can not insert empty data into table Product");
                return;
            }
            try
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM Product WHERE ProductID = @ProductID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@ProductID", txtProductID.Text);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("ID already exists");
                    connection.Close();
                    return;
                }

                byte[] imageData = File.ReadAllBytes(txtImage.Text);
                string insertQuery = "INSERT INTO Product (ProductID, Image, Name, Price) " +
                    "VALUES (@ProductID, @Image, @Name, @Price)";
                SqlCommand cmd = new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@ProductID", txtProductID.Text);
                cmd.Parameters.AddWithValue("@Image", imageData);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Price", Convert.ToInt64(txtPrice.Text));
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("The new product has been added successfully");
                txtImage.Text = "";
                txtName.Text = "";
                txtProductID.Text = "";
                txtPrice.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Add a New Product into table Product: " + ex.Message);
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

        #region btnSelectImage_Click
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select An Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtImage.Text = openFileDialog.FileName;
                }
            }
        }
        #endregion

        #region btnDelete_Click
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (datagridviewProduct.RowCount == 0)
            {
                MessageBox.Show("There is no product data in the Product table");
                return;
            }

            // Kiểm tra xem có dòng nào được chọn hay không
            if (datagridviewProduct.SelectedRows.Count > 0)
            {
                // Danh sách lưu trữ ProductID của các dòng được chọn
                List<string> productIDs = new List<string>();

                // Duyệt qua các dòng được chọn
                foreach (DataGridViewRow row in datagridviewProduct.SelectedRows)
                {
                    if (row.Cells["ProductID"].Value != null)
                    {
                        string productID = row.Cells["ProductID"].Value.ToString();
                        productIDs.Add(productID);
                    }
                }

                try
                {
                    connection.Open();

                    foreach (string productID in productIDs)
                    {
                        // Xóa từ bảng ProductSize trước
                        string DeleteFromProductSizeQuery = "DELETE FROM ProductSize WHERE ProductID = @ProductID";
                        SqlCommand DeleteFromProductSizeCMD = new SqlCommand(DeleteFromProductSizeQuery, connection);
                        DeleteFromProductSizeCMD.Parameters.AddWithValue("@ProductID", productID);
                        DeleteFromProductSizeCMD.ExecuteNonQuery();

                        // Xóa từ bảng Product sau
                        string DeleteFromProductQuery = "DELETE FROM Product WHERE ProductID = @ProductID";
                        SqlCommand DeleteFromProductCMD = new SqlCommand(DeleteFromProductQuery, connection);
                        DeleteFromProductCMD.Parameters.AddWithValue("@ProductID", productID);
                        DeleteFromProductCMD.ExecuteNonQuery();
                    }
                    connection.Close();
                    MessageBox.Show("Selected products have been deleted successfully from Product table");

                    // Tải lại dữ liệu sau khi xóa
                    LoadProductData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting products: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the products you want to delete");
            }
        }
        #endregion

        #region btnEditProduct_Click
        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (datagridviewProduct.RowCount == 0)
            {
                MessageBox.Show("There is no product data");
                return;
            }

            if (datagridviewProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to edit");
                return;
            }

            if (datagridviewProduct.SelectedRows.Count != 1)
            {
                MessageBox.Show("Only one product's information can be edited at a time");
                return;
            }

            if (
                string.IsNullOrEmpty(txtImage.Text) &&
                string.IsNullOrEmpty(txtName.Text) &&
                string.IsNullOrEmpty(txtPrice.Text)
                )
            {
                MessageBox.Show("There are no changes to the product information to edit");
                return;
            }

            if (!string.IsNullOrEmpty(txtProductID.Text))
            {
                MessageBox.Show("Can not edit Product ID.");
                return;
            }

            DataGridViewRow selectedRow = datagridviewProduct.SelectedRows[0];

            string productID = selectedRow.Cells["ProductID"].Value.ToString();
            string updateQuery = "UPDATE Product SET ";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(txtName.Text))
            {
                updateQuery += "Name = @Name, ";
                parameters.Add(new SqlParameter("@Name", txtName.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(txtPrice.Text))
            {
                updateQuery += "Price = @Price, ";
                parameters.Add(new SqlParameter("@Price", Convert.ToInt32(txtPrice.Text)));
            }

            if (!string.IsNullOrEmpty(txtImage.Text))
            {
                byte[] imageData = File.ReadAllBytes(txtImage.Text);
                updateQuery += "Image = @Image, ";
                parameters.Add(new SqlParameter("@Image", imageData));
            }

            updateQuery = updateQuery.TrimEnd(',', ' ');
            updateQuery += " WHERE ProductID = @ProductID";
            parameters.Add(new SqlParameter("@ProductID", productID));

            if (MessageBox.Show("Do you want to save the changes?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(updateQuery, connection);
                    cmd.Parameters.AddRange(parameters.ToArray());
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Product information has been updated successfully.");
                    txtImage.Text = "";
                    txtProductID.Text = "";
                    txtName.Text = "";
                    txtPrice.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error editing product's information: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion

        #region btnAddProductSize_Click
        private void btnAddProductSize_Click(object sender, EventArgs e)
        {
            bool checkAddData =
                string.IsNullOrEmpty(txtSize.Text) ||
                string.IsNullOrEmpty(txtProductID_Size.Text) ||
                string.IsNullOrEmpty(txtSize.Text) ||
                string.IsNullOrEmpty(txtQuantity.Text) ? false : true;
            if (checkAddData == false)
            {
                MessageBox.Show(
                    "Can not insert empty data into table Product Size");
                return;
            }
            try
            {
                connection.Open();
                // Kiểm tra nếu ProductID tồn tại trong bảng Product
                string checkProductIDQuery = "SELECT COUNT(*) FROM Product WHERE ProductID = @ProductID_Size";
                SqlCommand checkProductIDCmd = new SqlCommand(checkProductIDQuery, connection);
                checkProductIDCmd.Parameters.AddWithValue("@ProductID_Size", txtProductID_Size.Text);
                int count1 = (int)checkProductIDCmd.ExecuteScalar();

                if (count1 == 0)
                {
                    MessageBox.Show("The ProductID does not exist in the Product table");
                    return;
                }

                string checkSizeIDQuery = "SELECT COUNT(*) FROM ProductSize WHERE SizeID = @SizeID";
                SqlCommand checkSizeIDCmd = new SqlCommand(checkSizeIDQuery, connection);
                checkSizeIDCmd.Parameters.AddWithValue("@SizeID", txtSizeID.Text);
                int count2 = (int)checkSizeIDCmd.ExecuteScalar();

                if (count2 != 0)
                {
                    MessageBox.Show("SizeID already exists in the ProductSize table");
                    return;
                }

                string insertQuery = "INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity) " +
                    "VALUES (@SizeID, @ProductID, @Size, @Quantity)";
                SqlCommand cmd = new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@SizeID", txtSizeID.Text);
                cmd.Parameters.AddWithValue("@ProductID", txtProductID_Size.Text);
                cmd.Parameters.AddWithValue("@Size", Convert.ToInt32(txtSize.Text));
                cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text));
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("The new product has been added successfully");
                txtSizeID.Text = "";
                txtProductID_Size.Text = "";
                txtSize.Text = "";
                txtQuantity.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Add a New Product into table Product Size: " + ex.Message);
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

        #region btnDeleteProductSize_Click
        private void btnDeleteProductSize_Click(object sender, EventArgs e)
        {
            if (datagridviewProductSize.RowCount == 0)
            {
                MessageBox.Show("There is no product data in the ProductSize table");
                return;
            }

            // Kiểm tra xem có dòng nào được chọn hay không
            if (datagridviewProductSize.SelectedRows.Count > 0)
            {
                // Danh sách lưu trữ SizeID của các dòng được chọn
                List<string> sizeIDList = new List<string>();

                //HashSet<string> productIDHashSet = new HashSet<string>();

                // Duyệt qua các dòng được chọn
                foreach (DataGridViewRow row in datagridviewProductSize.SelectedRows)
                {
                    if (row.Cells["SizeID"].Value != null)
                    {
                        string sizeID = row.Cells["SizeID"].Value.ToString();
                        sizeIDList.Add(sizeID);
                    }
                    //if (row.Cells["ProductID"].Value != null)
                    //{
                    //    string productID = row.Cells["ProductID"].Value.ToString();
                    //    productIDHashSet.Add(productID);
                    //}
                }

                try
                {
                    connection.Open();

                    foreach (string sizeID in sizeIDList)
                    {
                        string DeleteFromProductSizeQuery = "DELETE FROM ProductSize WHERE SizeID = @SizeID";
                        SqlCommand DeleteFromProductSizeCMD = new SqlCommand(DeleteFromProductSizeQuery, connection);
                        DeleteFromProductSizeCMD.Parameters.AddWithValue("@SizeID", sizeID);
                        DeleteFromProductSizeCMD.ExecuteNonQuery();
                    }

                    connection.Close();
                    MessageBox.Show("Selected products have been deleted successfully from ProductSize table");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting products in the ProductSize table: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the products you want to delete");
            }
        }
        #endregion

        #region btnEditProductSize_Click
        private void btnEditProductSize_Click(object sender, EventArgs e)
        {
            if (datagridviewProductSize.RowCount == 0)
            {
                MessageBox.Show("There is no product data in the ProductSize table");
                return;
            }

            if (datagridviewProductSize.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to edit in the ProductSize table");
                return;
            }

            if (datagridviewProductSize.SelectedRows.Count != 1)
            {
                MessageBox.Show("Only one product's information can be edited at a time in the ProductSize table");
                return;
            }

            if (
                string.IsNullOrEmpty(txtProductID_Size.Text) &&
                string.IsNullOrEmpty(txtSize.Text) &&
                string.IsNullOrEmpty(txtQuantity.Text)
                )
            {
                MessageBox.Show("There are no changes to the product information to edit in the ProductSize table");
                return;
            }

            if (!string.IsNullOrEmpty(txtSizeID.Text))
            {
                MessageBox.Show("Cannot edit SizeID from the ProductSize table");
                return;
            }

            DataGridViewRow selectedRow = datagridviewProductSize.SelectedRows[0];

            string sizeID = selectedRow.Cells["SizeID"].Value.ToString();
            string updateQuery = "UPDATE ProductSize SET ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(txtSize.Text))
            {
                updateQuery += "Size = @Size, ";
                parameters.Add(new SqlParameter("@Size", txtSize.Text));
            }

            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                updateQuery += "Quantity = @Quantity, ";
                parameters.Add(new SqlParameter("@Quantity", Convert.ToInt32(txtQuantity.Text)));
            }

            if (!string.IsNullOrEmpty(txtProductID_Size.Text))
            {
                try
                {
                    connection.Open();
                    string checkProductID_SizeQuery = "SELECT COUNT(*) FROM Product WHERE ProductID = @ProductID_Size";
                    SqlCommand cmd = new SqlCommand(checkProductID_SizeQuery, connection);
                    cmd.Parameters.AddWithValue("@ProductID_Size", txtProductID_Size.Text);
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("The ProductID you entered does not exist");
                        return;
                    }
                    else
                    {
                        updateQuery += "ProductID = @ProductID_Size, ";
                        parameters.Add(new SqlParameter("@ProductID_Size", txtProductID_Size.Text));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking ProductID_Size: " + ex.Message);
                    return;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }

            updateQuery = updateQuery.TrimEnd(',', ' ');
            updateQuery += " WHERE SizeID = @SizeID";
            parameters.Add(new SqlParameter("@SizeID", sizeID));

            if (MessageBox.Show("Do you want to save the changes?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(updateQuery, connection);
                    cmd.Parameters.AddRange(parameters.ToArray());
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    connection.Close();
                    MessageBox.Show("Product information has been updated successfully.");
                    txtProductID_Size.Text = "";
                    txtSize.Text = "";
                    txtQuantity.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating product information: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion

        #region btnShowUserInvoiceDetails_Click
        private void btnShowUserInvoiceDetails_Click(object sender, EventArgs e)
        {
            if (datagridviewInvoice.RowCount == 0)
            {
                MessageBox.Show(
                    "Data in the Invoice table is empty");
                return;
            }

            if (datagridviewInvoice.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "No rows have been selected in the Invoice table");
                return;
            }

            if (datagridviewInvoice.SelectedRows.Count != 1)
            {
                MessageBox.Show(
                    "Only one Invoice can be selected to view InvoiceDetails");
                return;
            }
            else 
            {
                try
                {
                    connection.Open();
                    DataGridViewRow selectedRow = datagridviewInvoice.SelectedRows[0];
                    string ID = selectedRow.Cells[0].Value.ToString();
                    string invoiceDetailsQuery = "SELECT * FROM InvoiceDetails WHERE InvoiceID = @ID";
                    SqlCommand sqlCommand = new SqlCommand(invoiceDetailsQuery, connection);
                    sqlCommand.Parameters.AddWithValue("@ID", ID);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    datagridviewInvoiceDetails.DataSource = null;

                    datagridviewInvoiceDetails.DataSource = dataTable;

                    datagridviewInvoiceDetails.Columns["InvoiceID"].HeaderText = "Invoice ID";
                    datagridviewInvoiceDetails.Columns["InvoiceDetailsID"].HeaderText = "ID";
                    datagridviewInvoiceDetails.Columns["ProductID"].HeaderText = "Product ID";
                    datagridviewInvoiceDetails.Columns["SizeID"].HeaderText = "Size ID";
                    datagridviewInvoiceDetails.Columns["ProductName"].HeaderText = "Product Name";
                    datagridviewInvoiceDetails.Columns["Quantity"].HeaderText = "Quantity";
                    datagridviewInvoiceDetails.Columns["Price"].HeaderText = "Price";

                    foreach (DataGridViewRow row in datagridviewInvoiceDetails.Rows)
                    {
                        row.Height = 40;
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "", MessageBoxButtons.OK);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion

        #region btnOrderConfirmation_Click
        private void btnOrderConfirmation_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn trong datagridviewInvoice không
            if (datagridviewInvoice.SelectedRows.Count == 0)
            {
                MessageBox.Show("No rows have been selected in the Invoice table");
                return;
            }

            try
            {
                connection.Open();

                // Duyệt qua từng dòng được chọn trong datagridviewInvoice
                foreach (DataGridViewRow row in datagridviewInvoice.SelectedRows)
                {
                    if (row.DataBoundItem == null) continue;

                    string invoiceID = row.Cells["ID"].Value.ToString(); // Giả định rằng cột ID tồn tại
                    string currentStatusInvoice = row.Cells["StatusInvoice"].Value.ToString(); // Giả định rằng cột StatusInvoice tồn tại

                    if (currentStatusInvoice == "Delivered" || currentStatusInvoice == "Confirm")
                    {
                        MessageBox.Show(
                            "The status of this Invoice cannot be changed to \"Confirmed\"");
                        return;
                    }

                    if (currentStatusInvoice == "Unconfirm")
                    {
                        DateTime currentDateConfirmation = DateTime.Now;

                        // Cập nhật trạng thái hóa đơn thành 'Confirm'
                        string updateStatusInvoiceQuery = "UPDATE Invoice SET Date = @date, StatusInvoice = 'Confirm' WHERE ID = @id";
                        using (SqlCommand updateStatusCmd = new SqlCommand(updateStatusInvoiceQuery, connection))
                        {
                            updateStatusCmd.Parameters.AddWithValue("@id", invoiceID);
                            updateStatusCmd.Parameters.AddWithValue("@date", currentDateConfirmation);
                            updateStatusCmd.ExecuteNonQuery();
                        }
                    }
                }

                // Cập nhật số lượng sản phẩm trong bảng ProductSize
                foreach (DataGridViewRow row in datagridviewInvoiceDetails.Rows)
                {
                    if (row.DataBoundItem == null) continue;

                    string sizeID = row.Cells["SizeID"].Value.ToString(); // Giả định rằng cột SizeID tồn tại
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value); // Giả định rằng cột Quantity tồn tại

                    string updateProductSizeQuery = "UPDATE ProductSize SET Quantity = Quantity - @quantity WHERE SizeID = @sizeID";
                    using (SqlCommand updateProductSizeCmd = new SqlCommand(updateProductSizeQuery, connection))
                    {
                        updateProductSizeCmd.Parameters.AddWithValue("@quantity", quantity);
                        updateProductSizeCmd.Parameters.AddWithValue("@sizeID", sizeID);
                        updateProductSizeCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Selected invoices have been confirmed and product quantities updated successfully!", "", MessageBoxButtons.OK);
                LoadInvoiceData(); // Tải lại dữ liệu hóa đơn
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "", MessageBoxButtons.OK);
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

        #region btnLoad_Click
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadProductData();
        }
        #endregion

        #region btnViewDetails_Click
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn trong datagridviewProduct hay không
            if (datagridviewProduct.SelectedRows.Count > 0)
            {
                try
                {
                    // Lấy ProductID từ dòng được chọn
                    string productId = Convert.ToString(datagridviewProduct.SelectedRows[0].Cells["ProductID"].Value);

                    // Truy vấn thông tin ProductSize dựa trên ProductID
                    string ProductSizeQuery = "SELECT * FROM ProductSize WHERE ProductID = @ProductID";
                    connection.Open();
                    SqlCommand ProductSizeCMD = new SqlCommand(ProductSizeQuery, connection);
                    ProductSizeCMD.Parameters.AddWithValue("@ProductID", productId);
                    SqlDataAdapter adapter = new SqlDataAdapter(ProductSizeCMD);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    datagridviewProductSize.DataSource = dataTable;

                    foreach (DataGridViewRow row in datagridviewProductSize.Rows)
                    {
                        row.Height = 40;
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    }
                    datagridviewProductSize.AllowUserToAddRows = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Load Product Size: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to view details.");
            }
        }
        #endregion
    }
}
