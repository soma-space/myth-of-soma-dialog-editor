namespace DialogEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// MDI
    /// </summary>
    public partial class frmMDIParent : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmMDIParent"/> class.
        /// </summary>
        public frmMDIParent()
        {
            this.InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            FrmDialogEdit childForm = new FrmDialogEdit();
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Myth of Soma Dialog Files (*.lib)|*.lib|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                FrmDialogEdit childForm = new FrmDialogEdit();
                childForm.MdiParent = this;
                childForm.Text = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                childForm.LoadSomaLib(fileName);
                childForm.Show();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Myth of Soma Dialog Files (*.lib)|*.lib|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            // If child form closed, remove tabPage
            ((sender as Form).Tag as TabPage).Dispose();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.tabControl.SelectedTab != null) && (this.tabControl.SelectedTab.Tag != null))
            {
                (this.tabControl.SelectedTab.Tag as Form).Left = -1337;
                (this.tabControl.SelectedTab.Tag as Form).BringToFront();
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (this.tabControl.SelectedTab.Tag as Form).Close();
        }

        private void DialogImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                MessageBox.Show("Cannot export anything because no dialog loaded.");
            }
        }

        private void DialogRectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                MessageBox.Show("Cannot export anything because no dialog loaded.");
            }
        }

        private void FrmMDIParent_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                this.tabControl.Visible = false; // If no any child form, hide tabControl
            }
            else
            {
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized;

                // If child form is new and no has tabPage, create new tabPage
                if (this.ActiveMdiChild.Tag == null)
                {
                    // Add a tabPage to tabControl with child form caption
                    TabPage tp = new TabPage(this.ActiveMdiChild.Text);
                    tp.Tag = this.ActiveMdiChild;
                    tp.Parent = this.tabControl;
                    tp.Show();

                    this.ActiveMdiChild.Tag = tp;
                    this.ActiveMdiChild.FormClosed += new FormClosedEventHandler(this.ActiveMdiChild_FormClosed);
                }

                this.tabControl.SelectedTab = this.ActiveMdiChild.Tag as TabPage;
                if (!this.tabControl.Visible)
                {
                    this.tabControl.Visible = true;
                }
            }

            // this.ActiveMdiChild.ResumeLayout();
        }
    }
}
