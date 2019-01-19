namespace TextDiary {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            Sgry.Azuki.FontInfo fontInfo1 = new Sgry.Azuki.FontInfo();
            this.azukiControl = new Sgry.Azuki.WinForms.AzukiControl();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayTextFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayLatestFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBgPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // azukiControl
            // 
            this.azukiControl.BackColor = System.Drawing.Color.White;
            this.azukiControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.azukiControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.azukiControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.azukiControl.DrawingOption = ((Sgry.Azuki.DrawingOption)(((((Sgry.Azuki.DrawingOption.DrawsFullWidthSpace | Sgry.Azuki.DrawingOption.DrawsTab) 
            | Sgry.Azuki.DrawingOption.DrawsEol) 
            | Sgry.Azuki.DrawingOption.ShowsDirtBar) 
            | Sgry.Azuki.DrawingOption.HighlightsMatchedBracket)));
            this.azukiControl.FirstVisibleLine = 0;
            this.azukiControl.Font = new System.Drawing.Font("MeiryoKe_UIGothic", 12F);
            fontInfo1.Name = "MeiryoKe_UIGothic";
            fontInfo1.Size = 12;
            fontInfo1.Style = System.Drawing.FontStyle.Regular;
            this.azukiControl.FontInfo = fontInfo1;
            this.azukiControl.ForeColor = System.Drawing.Color.Black;
            this.azukiControl.HighlightsCurrentLine = false;
            this.azukiControl.LinePadding = 3;
            this.azukiControl.Location = new System.Drawing.Point(0, 26);
            this.azukiControl.Name = "azukiControl";
            this.azukiControl.ScrollPos = new System.Drawing.Point(0, 0);
            this.azukiControl.ShowsHScrollBar = false;
            this.azukiControl.ShowsLineNumber = false;
            this.azukiControl.Size = new System.Drawing.Size(584, 416);
            this.azukiControl.TabIndex = 0;
            this.azukiControl.ViewWidth = 4101;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.visibleToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 26);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openBgPictureToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // visibleToolStripMenuItem
            // 
            this.visibleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayTextFilesToolStripMenuItem,
            this.displayLatestFileToolStripMenuItem});
            this.visibleToolStripMenuItem.Name = "visibleToolStripMenuItem";
            this.visibleToolStripMenuItem.Size = new System.Drawing.Size(57, 22);
            this.visibleToolStripMenuItem.Text = "Visible";
            // 
            // displayTextFilesToolStripMenuItem
            // 
            this.displayTextFilesToolStripMenuItem.Name = "displayTextFilesToolStripMenuItem";
            this.displayTextFilesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.displayTextFilesToolStripMenuItem.Text = "Display text files";
            // 
            // displayLatestFileToolStripMenuItem
            // 
            this.displayLatestFileToolStripMenuItem.Name = "displayLatestFileToolStripMenuItem";
            this.displayLatestFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.displayLatestFileToolStripMenuItem.Text = "Display latest file";
            // 
            // openBgPictureToolStripMenuItem
            // 
            this.openBgPictureToolStripMenuItem.Name = "openBgPictureToolStripMenuItem";
            this.openBgPictureToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.openBgPictureToolStripMenuItem.Text = "Open_BgPicture";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 442);
            this.Controls.Add(this.azukiControl);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Opacity = 0.7D;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sgry.Azuki.WinForms.AzukiControl azukiControl;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayTextFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayLatestFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBgPictureToolStripMenuItem;
    }
}

