namespace DialogEditor
{
    partial class FrmDialogEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDialogEdit));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageBox1 = new Cyotek.Windows.Forms.ImageBoxEx();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.cursorToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.selectionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.zoomToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dialogImageImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dialogImageExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dialogInfoExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.areaRectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonFocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonExtraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonExtraAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonExtraNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonExtraFocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonExtraDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(239, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 466);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.imageBox1);
            this.panel2.Controls.Add(this.statusStrip);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(775, 466);
            this.panel2.TabIndex = 12;
            // 
            // imageBox1
            // 
            this.imageBox1.AlwaysShowHScroll = true;
            this.imageBox1.AlwaysShowVScroll = true;
            this.imageBox1.AutoCenter = false;
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.DragOrigin = new System.Drawing.Point(0, 0);
            this.imageBox1.DragOriginOffset = new System.Drawing.Point(0, 0);
            this.imageBox1.Location = new System.Drawing.Point(0, 0);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(775, 444);
            this.imageBox1.TabIndex = 11;
            this.imageBox1.SelectionMoved += new System.EventHandler(this.ImageBox1_SelectionMoved);
            this.imageBox1.SelectionMoving += new System.ComponentModel.CancelEventHandler(this.ImageBox1_SelectionMoving);
            this.imageBox1.SelectionResized += new System.EventHandler(this.ImageBox1_SelectionResized);
            this.imageBox1.SelectionResizing += new System.ComponentModel.CancelEventHandler(this.ImageBox1_SelectionResizing);
            this.imageBox1.Selected += new System.EventHandler<System.EventArgs>(this.ImageBox1_Selected);
            this.imageBox1.SelectionRegionChanged += new System.EventHandler(this.ImageBox1_SelectionRegionChanged);
            this.imageBox1.Zoomed += new System.EventHandler<Cyotek.Windows.Forms.ImageBoxZoomEventArgs>(this.ImageBox1_Zoomed);
            this.imageBox1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ImageBox1_Scroll);
            this.imageBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.ImageBox1_Paint);
            this.imageBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageBox1_MouseDown);
            this.imageBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageBox1_MouseMove);
            this.imageBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageBox1_MouseUp);
            this.imageBox1.Resize += new System.EventHandler(this.ImageBox1_Resize);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripStatusLabel,
            this.cursorToolStripStatusLabel,
            this.selectionToolStripStatusLabel,
            this.zoomToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 444);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(775, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 17;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusToolStripStatusLabel
            // 
            this.statusToolStripStatusLabel.Name = "statusToolStripStatusLabel";
            this.statusToolStripStatusLabel.Size = new System.Drawing.Size(712, 17);
            this.statusToolStripStatusLabel.Spring = true;
            this.statusToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cursorToolStripStatusLabel
            // 
            this.cursorToolStripStatusLabel.Image = global::DialogEditor.Properties.Resources.cursor;
            this.cursorToolStripStatusLabel.Name = "cursorToolStripStatusLabel";
            this.cursorToolStripStatusLabel.Size = new System.Drawing.Size(16, 17);
            this.cursorToolStripStatusLabel.ToolTipText = "Current Cursor Position";
            // 
            // selectionToolStripStatusLabel
            // 
            this.selectionToolStripStatusLabel.Image = global::DialogEditor.Properties.Resources.selection_select;
            this.selectionToolStripStatusLabel.Name = "selectionToolStripStatusLabel";
            this.selectionToolStripStatusLabel.Size = new System.Drawing.Size(16, 17);
            // 
            // zoomToolStripStatusLabel
            // 
            this.zoomToolStripStatusLabel.Image = global::DialogEditor.Properties.Resources.magnifier_zoom;
            this.zoomToolStripStatusLabel.Name = "zoomToolStripStatusLabel";
            this.zoomToolStripStatusLabel.Size = new System.Drawing.Size(16, 17);
            this.zoomToolStripStatusLabel.ToolTipText = "Zoom";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1014, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.fileToolStripMenuItem.MergeIndex = 0;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 2;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dialogImageImportToolStripMenuItem});
            this.importToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.importToolStripMenuItem.MergeIndex = 3;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // dialogImageImportToolStripMenuItem
            // 
            this.dialogImageImportToolStripMenuItem.Name = "dialogImageImportToolStripMenuItem";
            this.dialogImageImportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dialogImageImportToolStripMenuItem.Text = "Dialog Image";
            this.dialogImageImportToolStripMenuItem.Click += new System.EventHandler(this.DialogImageImportToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dialogImageExportToolStripMenuItem,
            this.dialogInfoExportToolStripMenuItem});
            this.exportToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.exportToolStripMenuItem.MergeIndex = 4;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // dialogImageExportToolStripMenuItem
            // 
            this.dialogImageExportToolStripMenuItem.Name = "dialogImageExportToolStripMenuItem";
            this.dialogImageExportToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dialogImageExportToolStripMenuItem.Text = "Dialog Image";
            this.dialogImageExportToolStripMenuItem.Click += new System.EventHandler(this.DialogImageExportToolStripMenuItem_Click);
            // 
            // dialogInfoExportToolStripMenuItem
            // 
            this.dialogInfoExportToolStripMenuItem.Name = "dialogInfoExportToolStripMenuItem";
            this.dialogInfoExportToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dialogInfoExportToolStripMenuItem.Text = "Dialog Info";
            this.dialogInfoExportToolStripMenuItem.Click += new System.EventHandler(this.DialogInfoExportToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator2.MergeIndex = 5;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.saveToolStripMenuItem.MergeIndex = 6;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.saveAsToolStripMenuItem.MergeIndex = 7;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.areaRectsToolStripMenuItem,
            this.buttonToolStripMenuItem,
            this.buttonExtraToolStripMenuItem});
            this.viewToolStripMenuItem.MergeIndex = 2;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // areaRectsToolStripMenuItem
            // 
            this.areaRectsToolStripMenuItem.Checked = true;
            this.areaRectsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.areaRectsToolStripMenuItem.Name = "areaRectsToolStripMenuItem";
            this.areaRectsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.areaRectsToolStripMenuItem.Text = "All Area";
            this.areaRectsToolStripMenuItem.Click += new System.EventHandler(this.ShowAreaRectsToolStripMenuItem_Click);
            // 
            // buttonToolStripMenuItem
            // 
            this.buttonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAreaToolStripMenuItem,
            this.buttonFocusToolStripMenuItem,
            this.buttonDownToolStripMenuItem});
            this.buttonToolStripMenuItem.Name = "buttonToolStripMenuItem";
            this.buttonToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.buttonToolStripMenuItem.Text = "Button";
            // 
            // buttonAreaToolStripMenuItem
            // 
            this.buttonAreaToolStripMenuItem.Checked = true;
            this.buttonAreaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonAreaToolStripMenuItem.Name = "buttonAreaToolStripMenuItem";
            this.buttonAreaToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.buttonAreaToolStripMenuItem.Text = "Area";
            this.buttonAreaToolStripMenuItem.Click += new System.EventHandler(this.ButtonAreaToolStripMenuItem_Click);
            // 
            // buttonFocusToolStripMenuItem
            // 
            this.buttonFocusToolStripMenuItem.Checked = true;
            this.buttonFocusToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonFocusToolStripMenuItem.Name = "buttonFocusToolStripMenuItem";
            this.buttonFocusToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.buttonFocusToolStripMenuItem.Text = "Focus";
            this.buttonFocusToolStripMenuItem.Click += new System.EventHandler(this.ButtonFocusToolStripMenuItem_Click);
            // 
            // buttonDownToolStripMenuItem
            // 
            this.buttonDownToolStripMenuItem.Checked = true;
            this.buttonDownToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonDownToolStripMenuItem.Name = "buttonDownToolStripMenuItem";
            this.buttonDownToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.buttonDownToolStripMenuItem.Text = "Down";
            this.buttonDownToolStripMenuItem.Click += new System.EventHandler(this.ButtonDownToolStripMenuItem_Click);
            // 
            // buttonExtraToolStripMenuItem
            // 
            this.buttonExtraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonExtraAreaToolStripMenuItem,
            this.buttonExtraNormalToolStripMenuItem,
            this.buttonExtraFocusToolStripMenuItem,
            this.buttonExtraDownToolStripMenuItem});
            this.buttonExtraToolStripMenuItem.Name = "buttonExtraToolStripMenuItem";
            this.buttonExtraToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.buttonExtraToolStripMenuItem.Text = "Button Extra";
            // 
            // buttonExtraAreaToolStripMenuItem
            // 
            this.buttonExtraAreaToolStripMenuItem.Checked = true;
            this.buttonExtraAreaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonExtraAreaToolStripMenuItem.Name = "buttonExtraAreaToolStripMenuItem";
            this.buttonExtraAreaToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.buttonExtraAreaToolStripMenuItem.Text = "Area";
            this.buttonExtraAreaToolStripMenuItem.Click += new System.EventHandler(this.ButtonExtraAreaToolStripMenuItem_Click);
            // 
            // buttonExtraNormalToolStripMenuItem
            // 
            this.buttonExtraNormalToolStripMenuItem.Checked = true;
            this.buttonExtraNormalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonExtraNormalToolStripMenuItem.Name = "buttonExtraNormalToolStripMenuItem";
            this.buttonExtraNormalToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.buttonExtraNormalToolStripMenuItem.Text = "Normal";
            this.buttonExtraNormalToolStripMenuItem.Click += new System.EventHandler(this.ButtonExtraNormalToolStripMenuItem_Click);
            // 
            // buttonExtraFocusToolStripMenuItem
            // 
            this.buttonExtraFocusToolStripMenuItem.Checked = true;
            this.buttonExtraFocusToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonExtraFocusToolStripMenuItem.Name = "buttonExtraFocusToolStripMenuItem";
            this.buttonExtraFocusToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.buttonExtraFocusToolStripMenuItem.Text = "Focus";
            this.buttonExtraFocusToolStripMenuItem.Click += new System.EventHandler(this.ButtonExtraFocusToolStripMenuItem_Click);
            // 
            // buttonExtraDownToolStripMenuItem
            // 
            this.buttonExtraDownToolStripMenuItem.Checked = true;
            this.buttonExtraDownToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonExtraDownToolStripMenuItem.Name = "buttonExtraDownToolStripMenuItem";
            this.buttonExtraDownToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.buttonExtraDownToolStripMenuItem.Text = "Down";
            this.buttonExtraDownToolStripMenuItem.Click += new System.EventHandler(this.ButtonExtraDownToolStripMenuItem_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 369);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(233, 94);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(3, 16);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(233, 353);
            this.treeView1.TabIndex = 11;
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView1_ItemDrag);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView1_DragEnter);
            this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeView1_DragOver);
            this.treeView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TreeView1_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 466);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // frmDialogEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 490);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "frmDialogEdit";
            this.Load += new System.EventHandler(this.FrmDialogEdit_Load);
            this.SizeChanged += new System.EventHandler(this.FrmDialogEdit_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDialogEdit_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDialogEdit_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem areaRectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dialogImageExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dialogInfoExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem buttonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonExtraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonFocusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonExtraAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonExtraNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonExtraFocusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonExtraDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dialogImageImportToolStripMenuItem;
        private Cyotek.Windows.Forms.ImageBoxEx imageBox1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel cursorToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel zoomToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel selectionToolStripStatusLabel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
    }
}

