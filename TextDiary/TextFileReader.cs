using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary {
    public class TextFileReader {

        private String currentDirectoryPath;

        public TextFileReader( String settingCurrentDirectoryPath ) {
            if (System.IO.Directory.Exists(settingCurrentDirectoryPath) == false) {
                throw new FileNotFoundException("ファイルが存在しない、またはファイルにアクセスできません。");
            }

            currentDirectoryPath = settingCurrentDirectoryPath;

        }

        public void setCurrentDirectoryPath( String settingCurrentDirectoryPath ) {
            currentDirectoryPath = settingCurrentDirectoryPath;
        }

        public String readTextFilesFromCurrentDirectory() {
            string[] filePaths = System.IO.Directory.GetFiles( currentDirectoryPath, "*.txt");

            String text = "";

            for(int i = 0; i < filePaths.Length; i++) {
                text += System.IO.Path.GetFileName(filePaths[i]);
                System.IO.StreamReader reader = new System.IO.StreamReader(filePaths[i]);
                text += reader.ReadToEnd() + "\n\n";
                reader.Close();
            }

            return text;
        }

        public Todo[] readTextFileAsTodoList() {
            string[] filePaths = Directory.GetFiles(currentDirectoryPath, "*.txt");
            Todo[] todos = new Todo[ filePaths.Length ];

            for(int i = 0; i < filePaths.Length; i++) {
                Todo todo = new Todo();

                string pureFileName = Path.GetFileNameWithoutExtension(filePaths[i]);
                todo.additionDate = DateTime.ParseExact(pureFileName, TextFileMaker.TEXT_FILE_NAME_FORMAT , null);
                todo.deadLine = DateTime.MaxValue;

                StreamReader reader = new StreamReader( filePaths[i] );
                todo.content = reader.ReadToEnd();
                todos[i] = todo;
                reader.Close();
            }

            return todos;
        }

    }
}
