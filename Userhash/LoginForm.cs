using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Userhash
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            UserManager.LoadUsers();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            User user = UserManager.Authenticate(username, password);

            if (user != null)
            {
                if (string.IsNullOrEmpty(user.HashedPassword))
                {
                    string newPassword = Interaction.InputBox("Zadejte nové heslo:", "Nastavení hesla");
                    if (!string.IsNullOrWhiteSpace(newPassword))
                    {
                        user.HashedPassword = User.HashPassword(newPassword);
                        UserManager.SaveUsers();
                    }
                    else
                    {
                        MessageBox.Show("Nastavení hesla zrušeno.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                MessageBox.Show("Přihlášení úspěšné!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (user is Admin)
                {
                    new AdminDashboard().Show();
                }
                else
                {
                    new UserDashboard(user).Show();
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Špatné jméno nebo heslo!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}