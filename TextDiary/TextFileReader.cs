using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary {
    class TextFileReader {

        private String currentDirectoryPath;

        public TextFileReader( String settingCurrentDirectoryPath ) {
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

    }
}
