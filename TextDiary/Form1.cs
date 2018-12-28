using System;
using System.Windows.Forms;

namespace TextDiary {
    public partial class Form1 : Form {

        Settings settings;
        TextFileMaker textFileMaker;
        TextFileReader textFileReader;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            settings = new Settings();
            settings.currentDirectoryPath = System.IO.Directory.GetCurrentDirectory() + "\\text";
            textFileMaker = new TextFileMaker(settings.currentDirectoryPath);
        }
    }
}
