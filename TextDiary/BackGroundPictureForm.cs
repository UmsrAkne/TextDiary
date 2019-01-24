using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDiary {
    public partial class BackGroundPictureForm : Form {
        public BackGroundPictureForm() {
            InitializeComponent();
            loadDefaultPicutre();
        }

        public void loadPicture( String imageFileUrl ) {
            try {
                pictureBox.Image = Image.FromFile( imageFileUrl );
            }catch(Exception e) {
                System.Console.WriteLine("画像ファイルのオープンに失敗しました");
            }
        }

        private void loadDefaultPicutre() {
            String imageDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\img";
            if (Directory.Exists(imageDirectory)) {
                String[] imageFiles = Directory.GetFiles(imageDirectory , "*.jpg");
                if (imageFiles.Length > 0) loadPicture(imageFiles[0]);
            }
        }
    }
}
