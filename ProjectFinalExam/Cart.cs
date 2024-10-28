using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectFinalExam
{
    public partial class frmCart : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr);
        string unconfirm = "Unconfirm";
        public frmCart()
        {
            InitializeComponent();
            LoadCart();
        }

        #region frmCart_Load
        private void frmCart_Load(object sender, EventArgs e)
        {
            btnErrorShowCustomerNameRegex.Visible = false;
            btnErrorShowPhoneNumberRegex.Visible = false;
            btnErrorShowDeliveryAddressRegex.Visible = false;
            tooltipCustomerName.SetToolTip(
                btnErrorShowCustomerNameRegex,
                "Customer Name only includes letters and spaces");
            tooltipPhoneNumber.SetToolTip(
                btnErrorShowPhoneNumberRegex,
                "The phone number must be 10 digits long and start with a 0");
            tooltipDeliveryAddress.SetToolTip(
                btnErrorShowDeliveryAddressRegex,
                "The shipping address may contain letters, numbers, and the characters / and -");
            LoadCart();
            checkBox_Click(sender, e);
        }
        #endregion

        #region Load Cart
        private void LoadCart()
        {
            panelShowOrder.Controls.Clear(); // Clear existing controls
            int currentID = UserSession.Instance.CurrentUserID;
            connection.Open();
            string query = "SELECT * FROM ShoppingCart WHERE UserID = @currentUserID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@currentUserID", currentID);
            SqlDataReader reader = cmd.ExecuteReader();

            int i = 0;
            int panelWidth = panelShowOrder.Width - 50;
            int panelHeight = 150;
            int initialOffset = 20;
            int verticalOffset = 20;
            int padding = 20;

            while (reader.Read())
            {
                // Lấy thông tin từ cơ sở dữ liệu
                string productID = reader["ProductID"].ToString();
                string sizeID = reader["SizeID"].ToString();
                int size = Convert.ToInt32(reader["Size"]);
                int quantity = Convert.ToInt32(reader["Quantity"]);
                byte[] imageData = (byte[])reader["Image"];
                string productName = reader["Name"].ToString();
                int productPrice = Convert.ToInt32(reader["Price"]);

                // Tính tổng giá của sản phẩm
                int totalPrice = (productPrice * quantity) + 30000;

                // Tạo panel chính
                Guna2Panel panelParent = new Guna2Panel
                {
                    Size = new Size(panelWidth, panelHeight),
                    Location = new Point(initialOffset, verticalOffset + i * (panelHeight + padding)),
                    BackColor = Color.Transparent,
                    FillColor = Color.White,
                    BorderRadius = 15,
                    Tag = new { ProductID = productID, SizeID = sizeID, Quantity = quantity, ProductName = productName, Size = size, Price = productPrice} // Lưu thông tin vào Tag
                };

                // Tạo CheckBox
                Guna2CheckBox checkBox = new Guna2CheckBox
                {
                    Size = new Size(20, 20),
                    Location = new Point(30, (panelHeight - 20) / 2), // Center vertically within the panel
                    BackColor = Color.Transparent,
                    ForeColor = Color.Blue,
                    Tag = totalPrice // Lưu tổng giá vào Tag
                };
                checkBox.CheckedChanged += CheckBox_CheckedChanged; // Attach event handler
                checkBox.Click += checkBox_Click;
                panelParent.Controls.Add(checkBox);

                // Tạo picture box
                Guna2PictureBox pictureBox = new Guna2PictureBox
                {
                    Size = new Size(panelWidth - 520, panelHeight - 40), // Adjusted size
                    Location = new Point(checkBox.Right + 10, 10), // Adjusted position within panel
                    BackColor = Color.Transparent,
                    BorderRadius = 10,
                    SizeMode = PictureBoxSizeMode.Zoom,
                };
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pictureBox.Image = Image.FromStream(ms);
                }
                panelParent.Controls.Add(pictureBox);

                // Tạo panel cho tên sản phẩm
                Guna2HtmlLabel labelName = new Guna2HtmlLabel
                {
                    Size = new Size(panelWidth - 300, panelHeight - 50),
                    Location = new Point(pictureBox.Right + 10, 15),
                    BackColor = Color.Transparent,
                    Text = productName,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    TextAlignment = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    MaximumSize = new Size(panelWidth - 300, 0),
                };
                panelParent.Controls.Add(labelName);

                decimal price = 0;
                price += productPrice;
                // Tạo label cho giá sản phẩm
                Guna2HtmlLabel labelPrice = new Guna2HtmlLabel
                {
                    Size = new Size(panelWidth - 300, panelHeight - 50),
                    Location = new Point(pictureBox.Right + 10, labelName.Bottom + 10),
                    BackColor = Color.Transparent,
                    Text = price.ToString("N0") + "đ",
                    ForeColor = Color.Red,
                    Font = new Font("Segoe UI", 12, FontStyle.Regular),
                    TextAlignment = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                };
                panelParent.Controls.Add(labelPrice);

                // Tạo label cho số lượng sản phẩm
                Guna2HtmlLabel labelQuantity = new Guna2HtmlLabel
                {
                    Size = new Size(panelWidth - 300, panelHeight - 50),
                    Location = new Point(pictureBox.Right + 10, labelPrice.Bottom + 10),
                    BackColor = Color.Transparent,
                    Text = "Quantity: " + quantity.ToString(),
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 12, FontStyle.Regular),
                    TextAlignment = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                };
                panelParent.Controls.Add(labelQuantity);

                // Tạo label cho Size và đặt nó cạnh labelQuantity
                Guna2HtmlLabel labelSize = new Guna2HtmlLabel
                {
                    Size = new Size(panelWidth - 300, panelHeight - 50),
                    Location = new Point(labelQuantity.Right + 10, labelQuantity.Top),
                    BackColor = Color.Transparent,
                    Text = "Size: " + size,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 12, FontStyle.Regular),
                    TextAlignment = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                };
                panelParent.Controls.Add(labelSize);

                // Tạo nút Delete
                Guna2Button btnDelete = new Guna2Button
                {
                    Size = new Size(80, 30),
                    Location = new Point(panelWidth - 100, (panelHeight - 0) / 2),
                    Text = "Delete",
                    ForeColor = Color.White,
                    FillColor = Color.Red,
                    BorderRadius = 10,
                };
                btnDelete.Click += (s, ev) =>
                {
                    var panelData = (dynamic)panelParent.Tag;
                    using (SqlConnection deleteConnection = new SqlConnection(Properties.Settings.Default.connStr))
                    {
                        deleteConnection.Open();

                        string deleteQuery = "DELETE FROM ShoppingCart WHERE SizeID = @SizeID";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, deleteConnection);
                        deleteCmd.Parameters.AddWithValue("@SizeID", panelData.SizeID);
                        deleteCmd.ExecuteNonQuery();
                    }

                    LoadCart();
                };
                panelParent.Controls.Add(btnDelete);

                panelShowOrder.Controls.Add(panelParent);

                i++;
            }

            reader.Close();
            connection.Close();
            panelShowOrder.Padding = new Padding(0, 0, 0, padding * 3);
            panelShowOrder.Refresh();
        }
        #endregion

        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region checkBox_Click
        private void checkBox_Click(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
        }
        #endregion

        #region btnDeleteAll_Click
        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                string checkDataQuery = "SELECT COUNT(*) FROM ShoppingCart";
                connection.Open();
                SqlCommand cmd = new SqlCommand(checkDataQuery, connection);
                int count = (int)cmd.ExecuteScalar();

                if (count < 1)
                {
                    MessageBox.Show(
                        "Your shopping cart is empty",
                        "",
                        MessageBoxButtons.OK);
                }
                else
                {
                    string deleteQuery = "DELETE FROM ShoppingCart";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show(
                        "All items have been removed from your shopping cart",
                        "",
                        MessageBoxButtons.OK);
                    connection.Close();
                    LoadCart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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

        #region CheckBox_CheckedChanged
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotalPayment();
        }
        #endregion

        #region UpdateTotalPayment
        private void UpdateTotalPayment()
        {
            decimal totalPayment = 0;

            // Duyệt qua tất cả các điều khiển trong panelShowOrder
            foreach (Control control in panelShowOrder.Controls)
            {
                if (control is Guna2Panel panel)
                {
                    // Lấy giá của sản phẩm từ checkbox
                    foreach (Control childControl in panel.Controls)
                    {
                        if (childControl is Guna2CheckBox checkBox && checkBox.Checked)
                        {
                            // Trích xuất giá từ Tag của checkbox
                            if (checkBox.Tag is int totalPrice)
                            {
                                totalPayment += totalPrice;
                            }
                        }
                    }
                }
            }
            decimal shippingtotal = 30000;
            // Cập nhật giá trị vào txtTotalPayment
            txtTotalPayment.Text = totalPayment.ToString("N0") + " vnđ";
            txtShippingTotal.Text = shippingtotal.ToString("N0") + " vnđ";
        }
        #endregion

        #region checkboxSelectAll_Click_1
        private void checkboxSelectAll_Click_1(object sender, EventArgs e)
        {
            bool isChecked = checkboxSelectAll.Checked;

            // Duyệt qua tất cả các điều khiển trong panelShowOrder
            foreach (Control control in panelShowOrder.Controls)
            {
                if (control is Guna2Panel panel)
                {
                    // Duyệt qua các checkbox trong panel
                    foreach (Control childControl in panel.Controls)
                    {
                        if (childControl is Guna2CheckBox checkBox)
                        {
                            checkBox.Checked = isChecked;
                        }
                    }
                }
            }

            UpdateTotalPayment(); // Cập nhật tổng tiền
        }
        #endregion

        #region isValid_Functions
        private bool isValidCustomerName(string customerName)
        {
            string customerNameRegex = @"^[A-Za-z\s]+$";
            Regex regex = new Regex(customerNameRegex);
            return regex.IsMatch(customerName);
        }

        private bool isValidPhoneNumber(string phoneNumber)
        {
            string phoneNumberRegex = "^0[1-9]\\d{8}$";
            Regex regex = new Regex(phoneNumberRegex);
            return regex.IsMatch(phoneNumber);
        }

        private bool isValidDeliveryAddress(string deliveryAdddress)
        {
            string deliveryAdddressRegex = "^[A-Za-z0-9/-]+$";
            Regex regex = new Regex(deliveryAdddressRegex);
            return regex.IsMatch(deliveryAdddress);
        }
        #endregion

        #region btnPlaceOrder_Click
        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            bool isEmptyCustomerInformation =
                string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtPhoneNumber.Text) ||
                string.IsNullOrEmpty(txtDeliveryAddress.Text);

            bool isRegexCustomerInformation =
                isValidCustomerName(txtName.Text) &&
                isValidPhoneNumber(txtPhoneNumber.Text) &&
                isValidDeliveryAddress(txtDeliveryAddress.Text);

            if (panelShowOrder.Controls.Count == 0)
            {
                MessageBox.Show("Your cart is empty!", "", MessageBoxButtons.OK);
                return;
            }

            bool isAnyCheckBoxChecked = false;
            foreach (Control control in panelShowOrder.Controls)
            {
                if (control is Guna2Panel panel)
                {
                    foreach (Control childControl in panel.Controls)
                    {
                        if (childControl is Guna2CheckBox checkBox && checkBox.Checked)
                        {
                            isAnyCheckBoxChecked = true;
                            break;
                        }
                    }
                }
                if (isAnyCheckBoxChecked)
                {
                    break;
                }
            }

            if (!isAnyCheckBoxChecked)
            {
                MessageBox.Show("Please select at least one product to place an order!", "", MessageBoxButtons.OK);
                return;
            }

            if (isEmptyCustomerInformation)
            {
                btnErrorShowCustomerNameRegex.Visible = true;
                btnErrorShowPhoneNumberRegex.Visible = true;
                btnErrorShowDeliveryAddressRegex.Visible = true;
                MessageBox.Show("Customer information cannot be empty!", "", MessageBoxButtons.OK);
                return;
            }

            if (isRegexCustomerInformation)
            {
                btnErrorShowCustomerNameRegex.Visible = true;
                btnErrorShowPhoneNumberRegex.Visible = true;
                btnErrorShowDeliveryAddressRegex.Visible = true;
                MessageBox.Show("The customer information entered is invalid!", "", MessageBoxButtons.OK);
                return;
            }

            int totalPayment = 30000;
            foreach (Control control in panelShowOrder.Controls)
            {
                if (control is Guna2Panel panel)
                {
                    foreach (Control childControl in panel.Controls)
                    {
                        if (childControl is Guna2CheckBox checkBox && checkBox.Checked)
                        {
                            var tagData = panel.Tag as dynamic;
                            if (tagData != null)
                            {
                                int price = tagData.Price;
                                int quantity = tagData.Quantity;
                                totalPayment += price * quantity;
                            }
                        }
                    }
                }
            }

            string insertInvoiceQuery = @"
                INSERT INTO Invoice (UserID, Date, CustomerName, CustomerPhoneNumber, DeliveryAddress, TotalPayment, StatusInvoice)
                OUTPUT Inserted.ID
                VALUES (@UserID, @Date, @CustomerName, @CustomerPhoneNumber, @DeliveryAddress, @TotalPayment, @StatusInvoice)";

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr))
            {
                try
                {
                    connection.Open();
                    SqlCommand insertInvoiceCmd = new SqlCommand(insertInvoiceQuery, connection);
                    insertInvoiceCmd.Parameters.AddWithValue("@UserID", UserSession.Instance.CurrentUserID);
                    insertInvoiceCmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    insertInvoiceCmd.Parameters.AddWithValue("@CustomerName", txtName.Text.ToString());
                    insertInvoiceCmd.Parameters.AddWithValue("@CustomerPhoneNumber", txtPhoneNumber.Text.ToString());
                    insertInvoiceCmd.Parameters.AddWithValue("@DeliveryAddress", txtDeliveryAddress.Text.ToString());
                    insertInvoiceCmd.Parameters.AddWithValue("@TotalPayment", Convert.ToInt32(totalPayment));
                    insertInvoiceCmd.Parameters.AddWithValue("@StatusInvoice", unconfirm);

                    int invoiceID = (int)insertInvoiceCmd.ExecuteScalar();

                    string insertInvoiceDetailsQuery = @"
                        INSERT INTO InvoiceDetails (InvoiceID, ProductID, SizeID, ProductName, Size, Quantity, Price) 
                        VALUES (@InvoiceID, @ProductID, @SizeID, @ProductName, @Size, @Quantity, @Price)";

                    string deleteShoppingCartQuery = "DELETE FROM ShoppingCart WHERE SizeID = @SizeID AND ProductID = @ProductID";

                    foreach (Control controls in panelShowOrder.Controls)
                    {
                        if (controls is Guna2Panel panel)
                        {
                            foreach (Control childControl in panel.Controls)
                            {
                                if (childControl is Guna2CheckBox checkBox && checkBox.Checked)
                                {
                                    var tagData = panel.Tag as dynamic;
                                    if (tagData != null)
                                    {
                                        SqlCommand insertInvoiceDetailsCmd = new SqlCommand(insertInvoiceDetailsQuery, connection);
                                        insertInvoiceDetailsCmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                                        insertInvoiceDetailsCmd.Parameters.AddWithValue("@ProductID", tagData.ProductID);
                                        insertInvoiceDetailsCmd.Parameters.AddWithValue("@SizeID", tagData.SizeID);
                                        insertInvoiceDetailsCmd.Parameters.AddWithValue("@ProductName", tagData.ProductName);
                                        insertInvoiceDetailsCmd.Parameters.AddWithValue("@Size", tagData.Size);
                                        insertInvoiceDetailsCmd.Parameters.AddWithValue("@Quantity", tagData.Quantity);
                                        insertInvoiceDetailsCmd.Parameters.AddWithValue("@Price", tagData.Price);
                                        insertInvoiceDetailsCmd.ExecuteNonQuery();

                                        SqlCommand deleteShoppingCartCmd = new SqlCommand(deleteShoppingCartQuery, connection);
                                        deleteShoppingCartCmd.Parameters.AddWithValue("@SizeID", tagData.SizeID);
                                        deleteShoppingCartCmd.Parameters.AddWithValue("@ProductID", tagData.ProductID);
                                        deleteShoppingCartCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }

                    LoadCart();
                    foreach (Control control in panelShowOrder.Controls)
                    {
                        if (control is Guna2Panel panel)
                        {
                            foreach (Control childControl in panel.Controls)
                            {
                                if (childControl is Guna2CheckBox checkBox)
                                {
                                    checkBox.Checked = false;
                                }
                            }
                        }
                    }

                    connection.Close();
                    txtName.Text = "";
                    txtPhoneNumber.Text = "";
                    txtDeliveryAddress.Text = "";
                    txtShippingTotal.Text = "";
                    txtTotalPayment.Text = "";
                    btnErrorShowCustomerNameRegex.Visible = false;
                    btnErrorShowPhoneNumberRegex.Visible = false;
                    btnErrorShowDeliveryAddressRegex.Visible = false;
                    MessageBox.Show("Order placed successfully!", "", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "", MessageBoxButtons.OK);
                }
            }
        }

        #endregion
    }
}
