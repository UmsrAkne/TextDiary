using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDiary {
    public class TodoFileWatcher {

        public String targetPath { get; }
        private Timer timer = new Timer();
        private TextFileReader textFileReader;
        private Todo[] lastTodoList;

        public delegate void TodoFileChanged();
        public event TodoFileChanged todoFileChanged;

        public TodoFileWatcher( String targetDirectoryPath ) {

            if (System.IO.Directory.Exists(targetDirectoryPath) == false) {
                throw new System.IO.FileNotFoundException("指定されたディレクトリが存在しません");
            }

            this.targetPath = targetDirectoryPath;
            timer.Interval = 1000 * 180;//1000ms = 1s
            timer.Tick += checkTodoSourceXmlChanged;

            this.textFileReader = new TextFileReader(targetDirectoryPath);
        }

        public void startWatch() {
            timer.Enabled = true;
            lastTodoList = textFileReader.loadTodosFromXml();
        }

        public void stopWatch() {
            timer.Enabled = false;
        }

        private void checkTodoSourceXmlChanged(object sender , EventArgs e) {
            Console.WriteLine("timerEventは動いています");
            Todo[] currentTodoList = textFileReader.loadTodosFromXml();

            if(currentTodoList.Count() != lastTodoList.Count()) {
                todoFileChanged();
                lastTodoList = currentTodoList;
                return;
            }

            for (int i = 0; i < currentTodoList.Count(); i++) {
                if (Todo.isEqual(currentTodoList[i], lastTodoList[i]) == false) {
                    todoFileChanged();
                    lastTodoList = currentTodoList;
                    return;
                }
            }

            
        }

    }
}
