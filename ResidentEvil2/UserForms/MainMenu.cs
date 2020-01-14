using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResidentEvil2
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PortableSafeButton_Click(object sender, EventArgs e)
        {
            UserForms.PortableSafeDialog myDialog = new UserForms.PortableSafeDialog();

            myDialog.ShowDialog();
        }
    }
}
