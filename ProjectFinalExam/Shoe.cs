using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Guna.UI2.WinForms;
using System.Security.Cryptography;

namespace ProjectFinalExam
{
    public partial class frmShoe : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr);
        private string currentSearch = "";
        private string currentSortOrder = "";
        public frmShoe()
        {
            InitializeComponent();
        }

        #region Shoe form Load
        private void Shoe_Load(object sender, EventArgs e)
        {
            guna2VScrollBar1.Visible = true;
            LoadProducts();
        }
        #endregion

        #region cboSort
        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSort.SelectedIndex == 0)
            {
                currentSortOrder = "Price ASC";
            }
            else if (cboSort.SelectedIndex == 1)
            {
                currentSortOrder = "Price DESC";
            }
            else if (cboSort.SelectedIndex == 2)
            {
                currentSortOrder = "Name ASC";
            }
            else if (cboSort.SelectedIndex == 3)
            {
                currentSortOrder = "Name DESC";
            }
            LoadProducts(currentSearch, currentSortOrder);
        }
        #endregion

        #region txtSearch_TextChanged
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            currentSearch = txtSearch.Text;
            LoadProducts(txtSearch.Text);
        }
        #endregion

        #region Load Prouducts
        private void LoadProducts(string search = "", string sortOrder = "")
        {
            try
            {
                panelShowProduct.Controls.Clear();
                btnShoe.FillColor = Color.White;
                btnShoe.ForeColor = Color.Black;
                string query = "SELECT ProductID, Image, Name, Price FROM Product";

                if (!string.IsNullOrEmpty(search))
                {
                    query += " WHERE Name LIKE @search";
                }

                if (!string.IsNullOrEmpty(sortOrder))
                {
                    query += " ORDER BY " + sortOrder;
                }

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (!string.IsNullOrEmpty(search))
                {
                    command.Parameters.AddWithValue("@search", "%" + search + "%");
                }
                SqlDataReader reader = command.ExecuteReader();

                int i = 0;
                int panelWidth = 250;
                int panelHeight = 250;
                int initialOffset = 80;
                int verticalOffset = 20;
                int padding = 20;

                while (reader.Read())
                {
                    Guna2Button btnParent = new Guna2Button();
                    btnParent.Size = new Size(panelWidth, panelHeight);
                    btnParent.Location = new Point(initialOffset + (i % 3) * (panelWidth + padding), verticalOffset + (i / 3) * (panelHeight + padding));
                    btnParent.BackColor = Color.Transparent;
                    btnParent.FillColor = Color.Transparent;
                    btnParent.BorderColor = Color.LightGray;
                    btnParent.BorderRadius = 10;
                    btnParent.BorderThickness = 1;
                    btnParent.Click += new EventHandler(btnParent_Click);

                    var productInfor = new
                    {
                        ID = (string)reader["ProductID"],
                        Image = (byte[])reader["Image"],
                        Name = (string)reader["Name"],
                        Price = Convert.ToInt32(reader["Price"]),
                    };
                    btnParent.Tag = productInfor;

                    Guna2Panel panelImage = new Guna2Panel();
                    panelImage.Size = new Size(panelWidth - 100, panelHeight - 100);
                    panelImage.Location = new Point(50, 10);

                    byte[] imageData = (byte[])reader["Image"];
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        panelImage.BackgroundImage = Image.FromStream(ms);
                    }
                    panelImage.BackgroundImageLayout = ImageLayout.Zoom;

                    Guna2TextBox txtName = new Guna2TextBox();
                    txtName.Size = new Size(panelWidth - 20, 33);
                    txtName.Location = new Point(10, panelHeight - 80);
                    txtName.Text = reader["Name"].ToString();
                    txtName.TextAlign = HorizontalAlignment.Center;
                    txtName.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    txtName.BorderRadius = 10;
                    txtName.BorderThickness = 1;
                    txtName.ReadOnly = true;
                    txtName.ForeColor = Color.Black;
                    txtName.BackColor = Color.Transparent;
                    txtName.Multiline = true;
                    txtName.AutoScroll = true;

                    decimal priceDecimal = 0;
                    priceDecimal += productInfor.Price;
                    Guna2TextBox txtPrice = new Guna2TextBox();
                    txtPrice.Size = new Size(panelWidth - 20, 30);
                    txtPrice.Location = new Point(10, panelHeight - 40);
                    txtPrice.Text = priceDecimal.ToString("N0") + "đ";
                    txtPrice.TextAlign = HorizontalAlignment.Center;
                    txtPrice.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    txtPrice.BorderRadius = 10;
                    txtPrice.BorderThickness = 1;
                    txtPrice.ReadOnly = true;
                    txtPrice.ForeColor = Color.Black;
                    txtPrice.BackColor = Color.Transparent;

                    btnParent.Controls.Add(panelImage);
                    btnParent.Controls.Add(txtName);
                    btnParent.Controls.Add(txtPrice);

                    panelShowProduct.Controls.Add(btnParent);

                    i++;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error load Products: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region btnParent_Click
        private void btnParent_Click(object sender, EventArgs e)
        {
            Guna2Button gunaBtn = sender as Guna2Button;
            var productInfor = (dynamic)gunaBtn.Tag;

            frmProductDetails frmProductDetails = new frmProductDetails
            {
                ProductImage = Image.FromStream(new MemoryStream(productInfor.Image)),
                ProductName = productInfor.Name,
                ProductPrice = productInfor.Price,
                ProductID = productInfor.ID
            };
            this.Hide();
            frmProductDetails.ShowDialog();
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

        #region btnUserAccSetting_Click
        private void btnUserAccSetting_Click(object sender, EventArgs e)
        {
            frmUserAccountSetting frmUserAccountSetting = new frmUserAccountSetting();
            frmUserAccountSetting.ShowDialog();
        }
        #endregion

        #region btnDelivery_Click
        private void btnDelivery_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDelivery frmDelivery = new frmDelivery();
            frmDelivery.ShowDialog();
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
