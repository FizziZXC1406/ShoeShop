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
using System.Web.Configuration;
using System.Windows.Forms;

namespace ProjectFinalExam
{
    public partial class frmUserAccountSetting : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr);
        public frmUserAccountSetting()
        {
            InitializeComponent();
            tabcontrolUserAccountSetting.SelectedIndexChanged += tabcontrolUserAccountSetting_SelectedIndexChanged;
        }

        #region tabcontrolUserAccountSetting_SelectedIndexChanged
        private void tabcontrolUserAccountSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabcontrolUserAccountSetting.SelectedTab == tabpageInformation ||
                tabcontrolUserAccountSetting.SelectedTab == tabpageChangePassword)
            {
                LoadUserAccountInformation();
            }
        }
        #endregion

        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmUserAccountSetting_Load
        private void frmUserAccountSetting_Load(object sender, EventArgs e)
        {
            toggleCurrentPassword.CheckedChanged += new EventHandler(toggleCurrentPassword_CheckedChanged);
            toggleNewPassword.CheckedChanged += new EventHandler(toggleNewPassword_CheckedChanged);
            toggleConfirmNewPassword.CheckedChanged += new EventHandler(toggleConfirmNewPassword_CheckedChanged);
            
            btnShowErrorCurrentPassword.Visible = false;
            btnShowErrorNewPassword.Visible = false;
            btnShowErrorConfirmNewPassword.Visible = false;

            tooltipCurrentPassword.SetToolTip(btnShowErrorCurrentPassword,
                "This password does not match your account");
            
            tooltipNewPassword.SetToolTip(btnShowErrorNewPassword,
                "Your New Password must be at least 8 characters long " +
                "and contain at least 1 uppercase letter, 1 number, and 1 special character");
            
            tooltipConfirmNewPassword.SetToolTip(btnShowErrorConfirmNewPassword,
                "The Confirm New Password does not match the New password");
            LoadUserAccountInformation();
        }
        #endregion

        #region LoadUserAccountInformation
        private void LoadUserAccountInformation()
        {
            try
            {
                if (UserSession.Instance.CurrentUserID <= 0)
                {
                    MessageBox.Show("User ID is not valid.");
                    return;
                }

                if (connection == null)
                {
                    MessageBox.Show("Database connection is not initialized.");
                    return;
                }

                connection.Open();
                string selectQuery = "SELECT * FROM UserAccount WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                cmd.Parameters.AddWithValue("@id", UserSession.Instance.CurrentUserID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = Convert.ToString(reader["Name"]);
                        string userName = Convert.ToString(reader["UserName"]);
                        string password = Convert.ToString(reader["Password"]);
                        string gmail = Convert.ToString(reader["Gmail"]);

                        txtName.Text = name;
                        txtUserName.Text = userName;
                        txtPassword.Text = password;
                        txtGmail.Text = gmail;
                    }
                }
                else
                {
                    MessageBox.Show("No user found with the specified ID.");
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Load User Account Information: " + ex.Message);
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

        #region isValidPassword
        private bool isValidPassword(string password)
        {
            string passwordRegex = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^-_+=<>])[A-Za-z\d@$!%*?&#]{8,}$";
            Regex regex = new Regex(passwordRegex);
            return regex.IsMatch(password);
        }
        #endregion

        #region btnChangePassword_Click
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text) ||
                string.IsNullOrEmpty(txtNewPassword.Text) ||
                string.IsNullOrEmpty(txtConfirmNewPassword.Text))
            {
                MessageBox.Show("Please fill in all required fields to change your password");
                return;
            }

            try
            {
                bool isFindPassword = false;
                bool isNewPasswordValid = false;
                bool isConfirmNewPassword = false;

                connection.Open();
                string selectQuery = "SELECT Password FROM UserAccount WHERE ID = @id";
                SqlCommand selectCMD = new SqlCommand(selectQuery, connection);
                selectCMD.Parameters.AddWithValue("@id", UserSession.Instance.CurrentUserID);
                SqlDataReader reader = selectCMD.ExecuteReader();

                if (reader.Read())
                {
                    string password = reader["Password"].ToString();
                    if (txtCurrentPassword.Text.Equals(password))
                    {
                        isFindPassword = true;
                    }
                }
                reader.Close();

                if (!isFindPassword)
                {
                    btnShowErrorCurrentPassword.Visible = true;
                }
                else
                {
                    btnShowErrorCurrentPassword.Visible = false;
                }

                if (!isValidPassword(txtNewPassword.Text))
                {
                    btnShowErrorNewPassword.Visible = true;
                }
                else
                {
                    isNewPasswordValid = true;
                    btnShowErrorNewPassword.Visible = false;
                }

                if (!txtNewPassword.Text.Equals(txtConfirmNewPassword.Text))
                {
                    btnShowErrorConfirmNewPassword.Visible = true;
                }
                else
                {
                    isConfirmNewPassword = true;
                    btnShowErrorConfirmNewPassword.Visible = false;
                }

                if (isNewPasswordValid && isFindPassword && isConfirmNewPassword)
                {
                    string updateNewPasswordQuery = "UPDATE UserAccount SET Password = @newPassword WHERE ID = @id";
                    SqlCommand updateCMD = new SqlCommand(updateNewPasswordQuery, connection);
                    updateCMD.Parameters.AddWithValue("@newPassword", txtNewPassword.Text);
                    updateCMD.Parameters.AddWithValue("@id", UserSession.Instance.CurrentUserID);
                    updateCMD.ExecuteNonQuery();
                    txtCurrentPassword.Text = "";
                    txtConfirmNewPassword.Text = "";
                    txtNewPassword.Text = "";
                    MessageBox.Show("Password changed successfully!");
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error changing password: " + ex.Message);
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

        #region toggleSwitch
        private void toggleCurrentPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleCurrentPassword.Checked)
            {
                txtCurrentPassword.PasswordChar = '\0';
            }
            else
            {
                txtCurrentPassword.PasswordChar = '•';
            }
        }

        private void toggleNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleNewPassword.Checked)
            {
                txtNewPassword.PasswordChar = '\0';
            }
            else
            {
                txtNewPassword.PasswordChar = '•';
            }
        }

        private void toggleConfirmNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleConfirmNewPassword.Checked)
            {
                txtConfirmNewPassword.PasswordChar = '\0';
            }
            else
            {
                txtConfirmNewPassword.PasswordChar = '•';
            }
        }
        #endregion

        #region btnLogOut_Click
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Log Out Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
                {
                    form.Close();
                }
            }    
        }
        #endregion
    }
}
