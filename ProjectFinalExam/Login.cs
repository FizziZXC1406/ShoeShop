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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ProjectFinalExam
{
    public partial class frmLogin : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr);

        public frmLogin()
        {
            InitializeComponent();
            toggleSeeUnsee.CheckedChanged += new EventHandler(toggleSeeUnsee_CheckedChanged);
            txtUsername.TextChanged += new EventHandler(CheckInputDataLoginToSetColorForBtnLogin);
            txtPassword.TextChanged += new EventHandler(CheckInputDataLoginToSetColorForBtnLogin);
        }

        #region CheckInputDataLoginToSetColorForBtnLogin
        private void CheckInputDataLoginToSetColorForBtnLogin(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                btnLogin.FillColor = Color.LightCoral;
                btnLogin.FillColor2 = Color.OrangeRed;
            }
            else
            {
                btnLogin.FillColor = Color.LightGray;
                btnLogin.FillColor2 = Color.Gray;
            }
        }
        #endregion

        #region btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Username, Password can not empty");
                return;
            }

            try
            {
                connection.Open();

                // Kiểm tra nếu là tài khoản Admin
                string adminQuery = "SELECT * FROM AdminAccount WHERE AdminName = @username AND Password = @password";
                SqlCommand adminCmd = new SqlCommand(adminQuery, connection);
                adminCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                adminCmd.Parameters.AddWithValue("@password", txtPassword.Text);
                SqlDataReader adminReader = adminCmd.ExecuteReader();

                if (adminReader.Read())
                {
                    // Đăng nhập thành công với tư cách Admin
                    adminReader.Close();
                    connection.Close();

                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    this.Hide();
                    frmMainPageAdmin adminPage = new frmMainPageAdmin(); // Giả sử bạn có một form trang chính riêng cho Admin
                    adminPage.ShowDialog();
                    this.Show();
                    return;
                }
                adminReader.Close();

                // Kiểm tra nếu là tài khoản người dùng bình thường
                string query = "SELECT * FROM UserAccount";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                bool isAuthenticated = false;

                string username = txtUsername.Text;
                string password = txtPassword.Text;

                while (reader.Read())
                {
                    string dataUsername = Convert.ToString(reader["Username"]);
                    string dataGmail = Convert.ToString(reader["Gmail"]);
                    string dataPassword = Convert.ToString(reader["Password"]);

                    if ((txtUsername.Text == dataUsername || txtUsername.Text == dataGmail) && txtPassword.Text == dataPassword)
                    {
                        UserSession.Instance.CurrentUserID = Convert.ToInt32(reader["ID"]);
                        isAuthenticated = true;
                        break;
                    }
                }

                reader.Close();

                if (isAuthenticated)
                {
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    this.Hide();
                    frmMainPageUser mainPage = new frmMainPageUser();
                    mainPage.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show(
                        "Username, Password are incorrect",
                        "Error Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error Login: " + ex.Message);
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

        #region btnSignUp_Click
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            this.Hide();
            frmSignUpUser signUp = new frmSignUpUser();
            signUp.ShowDialog();
            this.Show();
        }
        #endregion

        #region toggleSeeUnsee_CheckedChanged
        private void toggleSeeUnsee_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleSeeUnsee.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }
        #endregion

        #region LoginUser_Load
        private void LoginUser_Load(object sender, EventArgs e)
        {
            btnLogin.FillColor = Color.LightGray;
            btnLogin.FillColor2 = Color.Gray;
        }
        #endregion
    }
}
