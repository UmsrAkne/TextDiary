using System;
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
            loadPicture(@"C:\testImage.jpge");
        }

        public void loadPicture( String imageFileUrl ) {
            try {
                pictureBox.Image = Image.FromFile( imageFileUrl );
            }catch(Exception e) {
                System.Console.WriteLine("画像ファイルのオープンに失敗しました");
            }
        }
    }
}
