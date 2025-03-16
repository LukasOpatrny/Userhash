using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Userhash
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            lstUsers.Items.Clear();
            foreach (var user in UserManager.GetUsers().Where(u => !(u is Admin)))
            {
                lstUsers.Items.Add(user.Username);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem == null)
            {
                MessageBox.Show("Vyberte uživatele!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string username = lstUsers.SelectedItem.ToString();
            string newPassword = Microsoft.VisualBasic.Interaction.InputBox("Zadejte nové heslo:", "Změna hesla");

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                User user = UserManager.GetUsers().FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    user.HashedPassword = User.HashPassword(newPassword);
                    UserManager.SaveUsers();
                    MessageBox.Show("Heslo změněno!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Změna hesla zrušena.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string newUsername = txtNewUsername.Text;
            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                User newUser = new User(newUsername, "");
                UserManager.AddUser(newUser);
                LoadUsers();
                txtNewUsername.Clear();
            }
        }


        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Close();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem == null)
            {
                MessageBox.Show("Vyberte uživatele!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string username = lstUsers.SelectedItem.ToString();
            User userToDelete = UserManager.GetUsers().FirstOrDefault(u => u.Username == username);
            if (userToDelete != null)
            {
                DialogResult result = MessageBox.Show($"Opravdu chcete smazat uživatele '{username}'?", "Potvrzení smazání", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    UserManager.GetUsers().Remove(userToDelete);
                    UserManager.SaveUsers();
                    LoadUsers();
                    MessageBox.Show("Uživatel smazán!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}