using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TextDiary {
    class TextFileMaker {
        private String currentDirectoryPath = "";
        public String separateCharacter = "-";

        public TextFileMaker( String setCurrentDirectoryPath ) {
            currentDirectoryPath = setCurrentDirectoryPath;
            if(!Directory.Exists(currentDirectoryPath)) {
                Directory.CreateDirectory(currentDirectoryPath);
            }
        }

        public void createTextFile( String writingText ) {
            String fileName = getDataString() + ".txt";
            Console.WriteLine(fileName);

            writingText = separateLine + "\r" + writingText;
            File.WriteAllText( currentDirectoryPath + "\\" + fileName , writingText );
        }

        public String getDataString() {
            String dateString = DateTime.Now.ToString();
            dateString = dateString.Replace(":", "");
            dateString = dateString.Replace("/", "");
            dateString = dateString.Replace(" ", "_");
            return dateString;
        }

        public String separateLine {
            get {
                String line = "";
                const int SEPARATE_LENGTH = 30;
                for(int i = 0; i < SEPARATE_LENGTH; i++) {
                    line += separateCharacter;
                }
                return line;
            }
        }
    }
}
