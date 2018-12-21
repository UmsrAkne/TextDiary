using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary {
    class TextFileMaker {
        private String currentDirectoryPath = "";
        public String separateCharacter = "-";

        public TextFileMaker( String setCurrentDirectoryPath ) {
            currentDirectoryPath = setCurrentDirectoryPath;
        }

        public void createTextFile( String writingText ) {

        }

        public String getDataString() {
            DateTime date = System.DateTime.Now;
            return date.ToString();
        }

        public String separateLine {
            get {
                String line = "";
                const int SEPARATE_LENGTH = 5;
                for(int i = 0; i < SEPARATE_LENGTH; i++) {
                    line += separateCharacter;
                }
                return line;
            }
        }
    }
}
