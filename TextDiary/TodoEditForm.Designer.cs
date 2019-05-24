namespace TextDiary {
    partial class TodoEditForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            Sgry.Azuki.FontInfo fontInfo1 = new Sgry.Azuki.FontInfo();
            this.textEditWindow = new Sgry.Azuki.WinForms.AzukiControl();
            this.additionDatePicker = new System.Windows.Forms.DateTimePicker();
            this.completeDatePicker = new System.Windows.Forms.DateTimePicker();
            this.isCompleteCheckBox = new System.Windows.Forms.CheckBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textEditWindow
            // 
            this.textEditWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.textEditWindow.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textEditWindow.Dock = System.Windows.Forms.DockStyle.Top;
            this.textEditWindow.DrawingOption = ((Sgry.Azuki.DrawingOption)((((((Sgry.Azuki.DrawingOption.DrawsFullWidthSpace | Sgry.Azuki.DrawingOption.DrawsTab) 
            | Sgry.Azuki.DrawingOption.DrawsEol) 
            | Sgry.Azuki.DrawingOption.HighlightCurrentLine) 
            | Sgry.Azuki.DrawingOption.ShowsLineNumber) 
            | Sgry.Azuki.DrawingOption.HighlightsMatchedBracket)));
            this.textEditWindow.FirstVisibleLine = 0;
            this.textEditWindow.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            fontInfo1.Name = "MS UI Gothic";
            fontInfo1.Size = 12;
            fontInfo1.Style = System.Drawing.FontStyle.Regular;
            this.textEditWindow.FontInfo = fontInfo1;
            this.textEditWindow.ForeColor = System.Drawing.Color.Black;
            this.textEditWindow.Location = new System.Drawing.Point(0, 0);
            this.textEditWindow.Name = "textEditWindow";
            this.textEditWindow.ScrollPos = new System.Drawing.Point(0, 0);
            this.textEditWindow.ShowsDirtBar = false;
            this.textEditWindow.Size = new System.Drawing.Size(484, 115);
            this.textEditWindow.TabIndex = 0;
            this.textEditWindow.Text = "default";
            this.textEditWindow.ViewWidth = 4135;
            // 
            // additionDatePicker
            // 
            this.additionDatePicker.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.additionDatePicker.Location = new System.Drawing.Point(127, 150);
            this.additionDatePicker.Name = "additionDatePicker";
            this.additionDatePicker.Size = new System.Drawing.Size(200, 23);
            this.additionDatePicker.TabIndex = 1;
            // 
            // completeDatePicker
            // 
            this.completeDatePicker.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.completeDatePicker.Location = new System.Drawing.Point(127, 179);
            this.completeDatePicker.Name = "completeDatePicker";
            this.completeDatePicker.Size = new System.Drawing.Size(200, 23);
            this.completeDatePicker.TabIndex = 2;
            // 
            // isCompleteCheckBox
            // 
            this.isCompleteCheckBox.AutoSize = true;
            this.isCompleteCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.isCompleteCheckBox.Location = new System.Drawing.Point(12, 121);
            this.isCompleteCheckBox.Name = "isCompleteCheckBox";
            this.isCompleteCheckBox.Size = new System.Drawing.Size(91, 20);
            this.isCompleteCheckBox.TabIndex = 3;
            this.isCompleteCheckBox.Text = "Complete";
            this.isCompleteCheckBox.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            this.applyButton.BackColor = System.Drawing.Color.Azure;
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.Location = new System.Drawing.Point(13, 227);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = false;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(117, 227);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label1.Location = new System.Drawing.Point(12, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Addition date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label2.Location = new System.Drawing.Point(10, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Completed date";
            // 
            // TodoEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.isCompleteCheckBox);
            this.Controls.Add(this.completeDatePicker);
            this.Controls.Add(this.additionDatePicker);
            this.Controls.Add(this.textEditWindow);
            this.Name = "TodoEditForm";
            this.Text = "TodoEditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sgry.Azuki.WinForms.AzukiControl textEditWindow;
        private System.Windows.Forms.DateTimePicker additionDatePicker;
        private System.Windows.Forms.DateTimePicker completeDatePicker;
        private System.Windows.Forms.CheckBox isCompleteCheckBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}