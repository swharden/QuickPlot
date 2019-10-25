using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDemos
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Debugger.IsAttached)
                btnQuickstart_Click(null, null);
        }

        private void btnQuickstart_Click(object sender, EventArgs e)
        {
            using (var frm = new FormQuickstart())
                frm.ShowDialog();
        }

        private void btnMultiY_Click(object sender, EventArgs e)
        {
            using (var frm = new FormMultiY())
                frm.ShowDialog();
        }

        private void btnLinkedSubplots_Click(object sender, EventArgs e)
        {
            using (var frm = new FormLinkedSubplots())
                frm.ShowDialog();
        }
    }
}
