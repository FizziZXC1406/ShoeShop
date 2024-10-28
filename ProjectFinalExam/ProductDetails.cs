using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace ProjectFinalExam
{
    public partial class frmProductDetails : Form
    {
        public Image ProductImage { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductID { get; set; }
        public int SelectedSize { get; set; }
        public string SelectedSizeID { get; set; }

        SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr);

        int currentSelectedSizeQuantity = 0;
        int current_txtQuantity = 0;

        public frmProductDetails()
        {
            InitializeComponent();
        }

        #region frmProductDetails_Load
        private void frmProductDetails_Load(object sender, EventArgs e)
        {
            txtQuantity.Text = "1";
            tooltipAddToCart.SetToolTip(btnAddToCart, "Add To Cart");
            panelImage.BackgroundImage = ProductImage;
            panelImage.BackgroundImageLayout = ImageLayout.Zoom;
            lblNameProduct.Text = ProductName;
            decimal priceDecimal = 0;
            priceDecimal += Convert.ToDecimal(ProductPrice);
            lblPriceProduct.Text = priceDecimal.ToString("N0") + "đ";
            LoadProductDetails();
        }
        #endregion

        #region LoadProductDetails
        private void LoadProductDetails()
        {
            string query = "SELECT Size, Quantity FROM ProductSize WHERE ProductID = @ProductID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductID", ProductID);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            int initialOffset = 5;
            int verticalOffset = 20;
            int buttonWidth = 60;
            int buttonHeight = 40;
            int padding = 10;
            int i = 0;

            while (reader.Read())
            {
                Guna2Button btnSize = new Guna2Button
                {
                    Size = new Size(buttonWidth, buttonHeight),
                    Location = new Point(
                        initialOffset + (i % 5) * (buttonWidth + padding),
                        verticalOffset + (i / 5) * (buttonHeight + padding)
                    ),
                    FillColor = Color.Salmon,
                    BackColor = Color.Transparent,
                    ForeColor = Color.Black,
                    BorderRadius = 15,
                    BorderColor = Color.DarkGray,
                    BorderThickness = 2,
                    Text = Convert.ToString(reader["Size"]),
                    Tag = reader["Size"]
                };

                btnSize.Click += new EventHandler(btnSize_Click);

                panelAllSizeProduct.Controls.Add(btnSize);
                i++;
            }

            txtQuantity.Text = "1";

            reader.Close();
            connection.Close();
        }
        #endregion

        #region btnSize_Click
        private void btnSize_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy nút kích thước đã nhấn
                Guna2Button btnSize = sender as Guna2Button;
                SelectedSize = Convert.ToInt32(btnSize.Tag);
                connection.Open();

                // Truy vấn cơ sở dữ liệu để lấy SizeID và số lượng còn lại
                string query = "SELECT SizeID, Quantity FROM ProductSize WHERE ProductID = @ProductID AND Size = @Size";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", ProductID);
                command.Parameters.AddWithValue("@Size", SelectedSize);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    SelectedSizeID = reader["SizeID"].ToString();
                    txtSizeQuantity.Text = reader["Quantity"].ToString();
                    currentSelectedSizeQuantity = Convert.ToInt32(reader["Quantity"]);
                }
                else
                {
                    txtSizeQuantity.Text = "1";
                }

                reader.Close();
                connection.Close();

                lblSizeShowWhenSelected.Text = SelectedSize.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error btn Size Click: " + ex.Message);
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

        #region btnPlus_Click
        private void btnPlus_Click(object sender, EventArgs e)
        {
            current_txtQuantity = Convert.ToInt32(txtQuantity.Text);
            if (current_txtQuantity == Convert.ToInt32(currentSelectedSizeQuantity))
            {
                return;
            }
            else
            {
                current_txtQuantity++;
                txtQuantity.Text = Convert.ToString(current_txtQuantity);
            }
        }
        #endregion

        #region btnMinus_Click
        private void btnMinus_Click(object sender, EventArgs e)
        {
            current_txtQuantity = Convert.ToInt32(txtQuantity.Text);
            if (current_txtQuantity < 2)
            {
                return;
            }
            else
            {
                current_txtQuantity--;
                txtQuantity.Text = Convert.ToString(current_txtQuantity);
            }
        }
        #endregion

        #region btnAddToCart_Click
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                byte[] imageBytes = ImageToByteArray(ProductImage);
                string checkQuery = "SELECT COUNT(*) FROM ProductSize WHERE ProductID = @ProductID AND Size = @Size";
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@ProductID", ProductID);
                checkCmd.Parameters.AddWithValue("@Size", SelectedSize);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    if (currentSelectedSizeQuantity == 0)
                    {
                        MessageBox.Show(
                            "The selected size is out of stock. Please choose another size.",
                            "Out of Stock",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                    // Nếu tồn tại, chèn dữ liệu vào bảng ShoppingCart
                    string insertQuery = "INSERT INTO ShoppingCart (Image, ProductID, Name, SizeID, Size, Quantity, Price, UserID) " +
                        "VALUES (@Image, @ProductID, @Name, @SizeID, @Size, @Quantity, @Price, @UserID)";
                    SqlCommand insertCMD = new SqlCommand(insertQuery, connection);

                    insertCMD.Parameters.AddWithValue("@ProductID", ProductID);
                    insertCMD.Parameters.AddWithValue("@Image", imageBytes);
                    insertCMD.Parameters.AddWithValue("@Name", ProductName);
                    insertCMD.Parameters.AddWithValue("@SizeID", SelectedSizeID);
                    insertCMD.Parameters.AddWithValue("@Size", SelectedSize);
                    insertCMD.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text));
                    insertCMD.Parameters.AddWithValue("@Price", ProductPrice);
                    insertCMD.Parameters.AddWithValue("@UserID", UserSession.Instance.CurrentUserID);

                    insertCMD.ExecuteNonQuery();

                    MessageBox.Show("Has been added to the cart");
                }
                else
                {
                    MessageBox.Show("Please choose your shoe size");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error Add To Cart: " + ex.Message);
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

        #region ImageToByteArray
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
        #endregion

        #region btnBack_Click_1
        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
