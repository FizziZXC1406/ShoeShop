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
    public partial class frmMainPageUser : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.connStr);

        public frmMainPageUser()
        {
            InitializeComponent();
        }

        #region btnShoe_Click_1
        private void btnShoe_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmShoe shoe = new frmShoe();
            shoe.ShowDialog();
            this.Show();
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
