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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTheCurrentStateToTextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTheFinishedTodosAndItDleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.azukiControl = new Sgry.Azuki.WinForms.AzukiControl();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteThisTodo = new System.Windows.Forms.ToolStripMenuItem();
            this.isCompleted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.additionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deadLineDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.todoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.todoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.visibleToolStripMenuItem,
            this.exportToolStripMenuItem});
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
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportTheCurrentStateToTextFileToolStripMenuItem,
            this.exportTheFinishedTodosAndItDleteToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(58, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // exportTheCurrentStateToTextFileToolStripMenuItem
            // 
            this.exportTheCurrentStateToTextFileToolStripMenuItem.Name = "exportTheCurrentStateToTextFileToolStripMenuItem";
            this.exportTheCurrentStateToTextFileToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportTheCurrentStateToTextFileToolStripMenuItem.Text = "Export current Todos";
            this.exportTheCurrentStateToTextFileToolStripMenuItem.Click += new System.EventHandler(this.exportTheCurrentStateToTextFileToolStripMenuItem_Click);
            // 
            // exportTheFinishedTodosAndItDleteToolStripMenuItem
            // 
            this.exportTheFinishedTodosAndItDleteToolStripMenuItem.Name = "exportTheFinishedTodosAndItDleteToolStripMenuItem";
            this.exportTheFinishedTodosAndItDleteToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportTheFinishedTodosAndItDleteToolStripMenuItem.Text = "Export finished todos and it dlete";
            this.exportTheFinishedTodosAndItDleteToolStripMenuItem.Click += new System.EventHandler(this.exportTheFinishedTodosAndItDleteToolStripMenuItem_Click);
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
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isCompleted,
            this.order,
            this.content,
            this.additionDate,
            this.completedDate,
            this.deadLineDataGridViewTextBoxColumn});
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView.DataSource = this.todoBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MeiryoKe_Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView.Location = new System.Drawing.Point(3, 153);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView.RowTemplate.Height = 40;
            this.dataGridView.Size = new System.Drawing.Size(778, 383);
            this.dataGridView.TabIndex = 2;
            // 
            // order
            // 
            this.order.DataPropertyName = "Order";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.order.DefaultCellStyle = dataGridViewCellStyle1;
            this.order.HeaderText = "No.";
            this.order.Name = "order";
            this.order.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.order.Width = 25;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteThisTodo});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 26);
            // 
            // deleteThisTodo
            // 
            this.deleteThisTodo.Name = "deleteThisTodo";
            this.deleteThisTodo.Size = new System.Drawing.Size(169, 22);
            this.deleteThisTodo.Text = "Delete this todo";
            // 
            // isCompleted
            // 
            this.isCompleted.DataPropertyName = "isCompleted";
            this.isCompleted.FillWeight = 23.16602F;
            this.isCompleted.HeaderText = "";
            this.isCompleted.Name = "isCompleted";
            this.isCompleted.ReadOnly = true;
            this.isCompleted.Width = 40;
            // 
            // content
            // 
            this.content.DataPropertyName = "content";
            this.content.FillWeight = 298.1037F;
            this.content.HeaderText = "content";
            this.content.Name = "content";
            this.content.Width = 400;
            // 
            // additionDate
            // 
            this.additionDate.DataPropertyName = "additionDate";
            dataGridViewCellStyle2.Format = "MM/dd HH:mm";
            this.additionDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.additionDate.FillWeight = 135.6236F;
            this.additionDate.HeaderText = "additionDate";
            this.additionDate.Name = "additionDate";
            this.additionDate.Width = 150;
            // 
            // completedDate
            // 
            this.completedDate.DataPropertyName = "completedDate";
            dataGridViewCellStyle3.Format = "MM/dd HH:mm";
            this.completedDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.completedDate.FillWeight = 143.1066F;
            this.completedDate.HeaderText = "completedDate";
            this.completedDate.Name = "completedDate";
            this.completedDate.Width = 150;
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
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTheCurrentStateToTextFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTheFinishedTodosAndItDleteToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCompleted;
        private System.Windows.Forms.DataGridViewTextBoxColumn order;
        private System.Windows.Forms.DataGridViewTextBoxColumn content;
        private System.Windows.Forms.DataGridViewTextBoxColumn additionDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn completedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn deadLineDataGridViewTextBoxColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteThisTodo;
    }
}

