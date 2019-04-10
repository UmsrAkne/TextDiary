using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDiary {
    public class TodoFileWatcher {

        String targetPath { get; }
        Timer timer = new Timer();

        public TodoFileWatcher( String targetDirectoryPath ) {

            if (System.IO.Directory.Exists(targetDirectoryPath) == false) {
                throw new System.IO.FileNotFoundException("指定されたディレクトリが存在しません");
            }

            this.targetPath = targetDirectoryPath;
            timer.Interval = 10000;
        }

        public void startWatch() {
            timer.Enabled = true;
        }

        public void stopWatch() {
            timer.Enabled = false;
        }

        private void timer_Tick(object sender , EventArgs e) {
            Console.WriteLine("timerEventは動いています");
        }

    }
}
