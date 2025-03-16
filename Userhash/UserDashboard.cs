using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Userhash
{
    public partial class UserDashboard : Form
    {
        private User currentUser;

        public UserDashboard(User user)
        {
            InitializeComponent();
            currentUser = user;
            lblUsername.Text = currentUser.Username;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string newPassword = Interaction.InputBox("Zadejte nové heslo:", "Změna hesla");
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                currentUser.HashedPassword = User.HashPassword(newPassword);
                UserManager.SaveUsers();
                MessageBox.Show("Heslo změněno!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Close();
        }
    }
}