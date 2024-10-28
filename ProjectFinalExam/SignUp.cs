using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace ProjectFinalExam
{
    public partial class frmSignUpUser : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr);
        public frmSignUpUser()
        {
            InitializeComponent();
            toggleSeeUnseeConfirmPassword.CheckedChanged += new EventHandler(toggleSeeUnseeConfirmPassword_CheckedChanged);
            toggleSeeUnseePassword.CheckedChanged += new EventHandler(toggleSeeUnseePassword_CheckedChanged);
            txtFullName.TextChanged += new EventHandler(CheckInputDataCreateAccount);
            txtUsername.TextChanged += new EventHandler(CheckInputDataCreateAccount);
            txtPassword.TextChanged += new EventHandler(CheckInputDataCreateAccount);
            txtConfirmPassword.TextChanged += new EventHandler(CheckInputDataCreateAccount);
            txtGmail.TextChanged += new EventHandler(CheckInputDataCreateAccount);
        }

        #region CheckInputDataCreateAccount
        private void CheckInputDataCreateAccount(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFullName.Text) &&
                !string.IsNullOrEmpty(txtUsername.Text) &&
                !string.IsNullOrEmpty(txtPassword.Text) &&
                !string.IsNullOrEmpty(txtConfirmPassword.Text) &&
                !string.IsNullOrEmpty(txtGmail.Text))
            {
                btnCreate.FillColor = Color.LightCoral;
                btnCreate.FillColor2 = Color.OrangeRed;
            }
            else
            {
                btnCreate.FillColor = Color.LightGray;
                btnCreate.FillColor2 = Color.Gray;
            }
        }
        #endregion

        #region isValidRegex
        private bool isValidFullName(string fullname)
        {
            string fullnameRegex = @"^[A-Za-z\s]+$";
            Regex regex = new Regex(fullnameRegex);
            return regex.IsMatch(fullname);
        }

        private bool isValidUsername(string username)
        {
            string usernameRegex = @"^[a-zA-Z0-9]{6,}$";
            Regex regex = new Regex(usernameRegex);
            return regex.IsMatch(username);
        }

        private bool isValidGmail(string gmail)
        {
            string gmailRegex = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            Regex regex = new Regex(gmailRegex);
            return regex.IsMatch(gmail);
        }

        private bool isValidPassword(string password)
        {
            string passwordRegex = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^-_+=<>~`,./?;':{}])[A-Za-z\d@$!%*?&#]{8,}$";
            Regex regex = new Regex(passwordRegex);
            return regex.IsMatch(password);
        }
        #endregion

        #region btnCreate_Click
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtConfirmPassword.Text) ||
                string.IsNullOrEmpty(txtGmail.Text) ||
                string.IsNullOrEmpty(txtFullName.Text))
            {
                MessageBox.Show(
                    "Please fill in all required information to register!",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            if (!txtPassword.Text.Trim().Equals(txtConfirmPassword.Text.Trim()))
            {
                lblNotiConfirmPassword.Text = "Do not match Password";
                lblNotiConfirmPassword.ForeColor = Color.Red;
                btnErrorShowConfirmPasswordRegex.Visible = true;
            }
            else
            {
                lblNotiConfirmPassword.Text = "";
                btnErrorShowConfirmPasswordRegex.Visible = false;
            }

            if (!isValidFullName(txtFullName.Text))
            {
                lblNotiFullName.Text = "Invalid Full Name";
                lblNotiFullName.ForeColor = Color.Red;
                btnErrorShowFullNameRegex.Visible = true;
            }
            else
            {
                lblNotiFullName.Text = "";
                btnErrorShowFullNameRegex.Visible = false;
            }

            if (!isValidUsername(txtUsername.Text))
            {
                lblNotiUsername.Text = "Invalid Username";
                lblNotiUsername.ForeColor = Color.Red;
                btnErrorShowUsernameRegex.Visible = true;
            }
            else
            {
                lblNotiUsername.Text = "";
                btnErrorShowUsernameRegex.Visible = false;
            }

            if (!isValidGmail(txtGmail.Text))
            {
                lblNotiGmail.Text = "Invalid Gmail!";
                lblNotiGmail.ForeColor = Color.Red;
                btnErrorShowGmailRegex.Visible = true;

            }
            else
            {
                lblNotiGmail.Text = "";
                btnErrorShowGmailRegex.Visible = false;
            }

            if (!isValidPassword(txtPassword.Text))
            {
                lblNotiPassword.Text = "Invaid Password";
                lblNotiPassword.ForeColor = Color.Red;
                btnErrorShowPasswordRegex.Visible = true;
            }
            else
            {
                lblNotiPassword.Text = "";
                btnErrorShowPasswordRegex.Visible = false;
            }

            if (string.IsNullOrEmpty(lblNotiUsername.Text) &&
                string.IsNullOrEmpty(lblNotiPassword.Text) &&
                string.IsNullOrEmpty(lblNotiConfirmPassword.Text) &&
                string.IsNullOrEmpty(lblNotiGmail.Text) &&
                string.IsNullOrEmpty(lblNotiFullName.Text))
            {
                try
                {
                    connection.Open();

                    string checkUsernameQuery = "SELECT COUNT(*) FROM UserAccount WHERE Username = @newUserName";
                    SqlCommand checkUsernameCMD = new SqlCommand(checkUsernameQuery, connection);
                    checkUsernameCMD.Parameters.AddWithValue("@newUserName", txtUsername.Text.Trim());
                    int countUsername = Convert.ToInt32(checkUsernameCMD.ExecuteScalar());

                    if (countUsername > 0)
                    {
                        tooltipUsername.SetToolTip(btnErrorShowUsernameRegex,
                            "This Username already exit");
                        btnErrorShowUsernameRegex.Visible = true;
                        return;
                    }
                    else
                    {
                        tooltipFullName.SetToolTip(btnErrorShowFullNameRegex,
                        "Your full name only includes letters and spaces.");
                    }

                    string checkGmailQuery = "SELECT COUNT (*) FROM UserAccount WHERE Gmail = @newGmail";
                    SqlCommand checkGmailCMD = new SqlCommand(checkGmailQuery, connection);
                    checkGmailCMD.Parameters.AddWithValue("@newGmail", txtGmail.Text.Trim());
                    int countGmail = Convert.ToInt32(checkGmailCMD.ExecuteScalar());

                    if (countGmail > 0)
                    {
                        tooltipGmail.SetToolTip(btnErrorShowGmailRegex,
                            "This Gmail already exit");
                        btnErrorShowGmailRegex.Visible = true;
                        return;
                    }
                    else
                    {
                        tooltipGmail.SetToolTip(btnErrorShowGmailRegex,
                            "Your Gmail must be at least 6 characters long\r" +
                            "and not contain any special characters.");
                    }

                    string query = "INSERT INTO UserAccount (Name, Username, Gmail, Password) VALUES (@FullName, @Username, @Gmail, @Password)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gmail", txtGmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show(
                            "Registration successful!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Đóng form đăng ký và trở về form đăng nhập
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Registration failed!",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "An error occurred: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        #endregion

        #region btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmSignUp_Load
        private void frmSignUp_Load(object sender, EventArgs e)
        {
            lblNotiFullName.Text = "";
            lblNotiUsername.Text = "";
            lblNotiPassword.Text = "";
            lblNotiConfirmPassword.Text = "";
            lblNotiGmail.Text = "";

            btnErrorShowFullNameRegex.Visible = false;
            btnErrorShowUsernameRegex.Visible = false;
            btnErrorShowPasswordRegex.Visible = false;
            btnErrorShowConfirmPasswordRegex.Visible = false;
            btnErrorShowGmailRegex.Visible = false;

            tooltipFullName.SetToolTip(btnErrorShowFullNameRegex,
                "Your full name only includes letters and spaces.");

            tooltipUsername.SetToolTip(btnErrorShowUsernameRegex,
                "Your Username must be at least 6 characters long,\r" +
                "contain letters and numbers.");

            tooltipGmail.SetToolTip(btnErrorShowGmailRegex,
                "Your Gmail must be contain @gmail.com");

            tooltipPassword.SetToolTip(btnErrorShowPasswordRegex,
                "Your Password must be at least 8 characters long,\r" +
                "contain at least 1 uppercase letter, 1 number, 1 special character.");

            tooltipConfirmPassword.SetToolTip(btnErrorShowConfirmPasswordRegex,
                "Password and Confirm Password do not match");

            btnCreate.FillColor = Color.LightGray;
            btnCreate.FillColor2 = Color.Gray;
        }
        #endregion

        #region toggleSwitch
        private void toggleSeeUnseePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleSeeUnseePassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void toggleSeeUnseeConfirmPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleSeeUnseeConfirmPassword.Checked)
            {
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txtConfirmPassword.PasswordChar = '•';
            }
        }
        #endregion
    }
}
