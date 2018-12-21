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
            this.azukiControl.Font = new System.Drawing.Font("MeiryoKe_UIGothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            fontInfo1.Name = "MeiryoKe_UIGothic";
            fontInfo1.Size = 12;
            fontInfo1.Style = System.Drawing.FontStyle.Regular;
            this.azukiControl.FontInfo = fontInfo1;
            this.azukiControl.ForeColor = System.Drawing.Color.Black;
            this.azukiControl.HighlightsCurrentLine = false;
            this.azukiControl.LinePadding = 3;
            this.azukiControl.Location = new System.Drawing.Point(0, 0);
            this.azukiControl.Name = "azukiControl";
            this.azukiControl.ScrollPos = new System.Drawing.Point(0, 0);
            this.azukiControl.ShowsHScrollBar = false;
            this.azukiControl.ShowsLineNumber = false;
            this.azukiControl.Size = new System.Drawing.Size(584, 442);
            this.azukiControl.TabIndex = 0;
            this.azukiControl.ViewWidth = 4101;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 442);
            this.Controls.Add(this.azukiControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sgry.Azuki.WinForms.AzukiControl azukiControl;
    }
}

