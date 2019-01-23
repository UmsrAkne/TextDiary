using System;
using System.Windows.Forms;

namespace TextDiary {
    public partial class Form1 : Form {

        Settings settings;
        TextFileMaker textFileMaker;
        TextFileReader textFileReader;
        String latestText = "";
        Boolean isLogReading = false;

        BackGroundPictureForm backGroundPictureForm = new BackGroundPictureForm();

        public Form1() {
            InitializeComponent();
            azukiControl.KeyDown += this.keyboardEventHandler;
            backGroundPictureForm.Show();
            backGroundPictureForm.Location = this.Location;
            backGroundPictureForm.Size = this.Size;
            this.Move += synchronizeBackGroundWindowLocationWithThisWindow;
            this.Activated += setWindowsOnTopMost;
        }

        private void Form1_Load(object sender, EventArgs e) {
            settings = new Settings();
            settings.currentDirectoryPath = System.IO.Directory.GetCurrentDirectory() + "\\text";
            textFileMaker = new TextFileMaker(settings.currentDirectoryPath);
            textFileReader = new TextFileReader(settings.currentDirectoryPath);

            displayTextFilesToolStripMenuItem.Click += (object Sender, EventArgs eventArgs) => {
                if(azukiControl.Text.Length != 0) {
                    latestText = azukiControl.Text;
                }
                isLogReading = true;
                azukiControl.Text = textFileReader.readTextFilesFromCurrentDirectory();
            };

            displayLatestFileToolStripMenuItem.Click += (object Sender, EventArgs eventArgs) => {
                isLogReading = false;
                azukiControl.Text = latestText;
            };

            openBgPictureToolStripMenuItem.Click += (object Sender, EventArgs eventArgs) => {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    backGroundPictureForm.loadPicture(openFileDialog.FileName);
                }
            };
        } 

        private void keyboardEventHandler(object sender , KeyEventArgs e) {
            if (e.Control == true && e.KeyCode == Keys.Enter && !isLogReading) {
                textFileMaker.createTextFile(azukiControl.Text);
                azukiControl.Text = "";
                e.Handled = true;
            }
        }

        private void synchronizeBackGroundWindowLocationWithThisWindow(object sender , EventArgs e){
            backGroundPictureForm.Location = this.Location;
        }

        private void setWindowsOnTopMost(Object sender , EventArgs e) {
            backGroundPictureForm.TopMost = true;
            backGroundPictureForm.TopMost = false;
            this.TopMost = true;
            this.TopMost = false;
        }
    }
}
