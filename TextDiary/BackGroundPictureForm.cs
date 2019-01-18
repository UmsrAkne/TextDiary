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
            pictureBox.Image = Image.FromFile(@"C:\testImage.jpg");
            //pictureBox.Image = Image.FromFile(@AppDomain.CurrentDomain.BaseDirectory + "/res/testImage.jpg");
        }
    }
}
