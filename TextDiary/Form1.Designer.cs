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
            this.components = new System.ComponentModel.Container();
            Sgry.Azuki.FontInfo fontInfo1 = new Sgry.Azuki.FontInfo();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBgPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayTextFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayLatestFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.azukiControl = new Sgry.Azuki.WinForms.AzukiControl();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.isCompletedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.additionDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completedDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deadLineDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.todoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.todoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.visibleToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 26);
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
            // openBgPictureToolStripMenuItem
            // 
            this.openBgPictureToolStripMenuItem.Name = "openBgPictureToolStripMenuItem";
            this.openBgPictureToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.openBgPictureToolStripMenuItem.Text = "Open_BgPicture";
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.azukiControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 26);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 536);
            this.tableLayoutPanel1.TabIndex = 2;
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
            this.azukiControl.Location = new System.Drawing.Point(3, 3);
            this.azukiControl.Name = "azukiControl";
            this.azukiControl.ScrollPos = new System.Drawing.Point(0, 0);
            this.azukiControl.ShowsHScrollBar = false;
            this.azukiControl.ShowsLineNumber = false;
            this.azukiControl.Size = new System.Drawing.Size(778, 144);
            this.azukiControl.TabIndex = 1;
            this.azukiControl.ViewWidth = 4101;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isCompletedDataGridViewCheckBoxColumn,
            this.contentDataGridViewTextBoxColumn,
            this.additionDateDataGridViewTextBoxColumn,
            this.completedDateDataGridViewTextBoxColumn,
            this.deadLineDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.todoBindingSource;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MeiryoKe_Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView.Location = new System.Drawing.Point(3, 153);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowTemplate.Height = 40;
            this.dataGridView.Size = new System.Drawing.Size(778, 380);
            this.dataGridView.TabIndex = 2;
            // 
            // isCompletedDataGridViewCheckBoxColumn
            // 
            this.isCompletedDataGridViewCheckBoxColumn.DataPropertyName = "isCompleted";
            this.isCompletedDataGridViewCheckBoxColumn.FillWeight = 23.16602F;
            this.isCompletedDataGridViewCheckBoxColumn.HeaderText = "";
            this.isCompletedDataGridViewCheckBoxColumn.Name = "isCompletedDataGridViewCheckBoxColumn";
            this.isCompletedDataGridViewCheckBoxColumn.Width = 30;
            // 
            // contentDataGridViewTextBoxColumn
            // 
            this.contentDataGridViewTextBoxColumn.DataPropertyName = "content";
            this.contentDataGridViewTextBoxColumn.FillWeight = 298.1037F;
            this.contentDataGridViewTextBoxColumn.HeaderText = "content";
            this.contentDataGridViewTextBoxColumn.Name = "contentDataGridViewTextBoxColumn";
            this.contentDataGridViewTextBoxColumn.Width = 400;
            // 
            // additionDateDataGridViewTextBoxColumn
            // 
            this.additionDateDataGridViewTextBoxColumn.DataPropertyName = "additionDate";
            this.additionDateDataGridViewTextBoxColumn.FillWeight = 135.6236F;
            this.additionDateDataGridViewTextBoxColumn.HeaderText = "additionDate";
            this.additionDateDataGridViewTextBoxColumn.Name = "additionDateDataGridViewTextBoxColumn";
            this.additionDateDataGridViewTextBoxColumn.Width = 150;
            // 
            // completedDateDataGridViewTextBoxColumn
            // 
            this.completedDateDataGridViewTextBoxColumn.DataPropertyName = "completedDate";
            this.completedDateDataGridViewTextBoxColumn.FillWeight = 143.1066F;
            this.completedDateDataGridViewTextBoxColumn.HeaderText = "completedDate";
            this.completedDateDataGridViewTextBoxColumn.Name = "completedDateDataGridViewTextBoxColumn";
            this.completedDateDataGridViewTextBoxColumn.Width = 150;
            // 
            // deadLineDataGridViewTextBoxColumn
            // 
            this.deadLineDataGridViewTextBoxColumn.DataPropertyName = "deadLine";
            this.deadLineDataGridViewTextBoxColumn.HeaderText = "deadLine";
            this.deadLineDataGridViewTextBoxColumn.Name = "deadLineDataGridViewTextBoxColumn";
            this.deadLineDataGridViewTextBoxColumn.Visible = false;
            // 
            // todoBindingSource
            // 
            this.todoBindingSource.DataSource = typeof(TextDiary.Todo);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Opacity = 0.7D;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.todoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayTextFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayLatestFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBgPictureToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sgry.Azuki.WinForms.AzukiControl azukiControl;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource todoBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCompletedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn additionDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn completedDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deadLineDataGridViewTextBoxColumn;
    }
}

