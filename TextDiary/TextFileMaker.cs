using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TextDiary {
    public class TextFileMaker {
        private String currentDirectoryPath = "";
        public String separateCharacter = "-";

        public const string TEXT_FILE_NAME_FORMAT = "yyyyMMdd_HHmmss";

        public TextFileMaker( String setCurrentDirectoryPath ) {
            currentDirectoryPath = setCurrentDirectoryPath;
            if(!Directory.Exists(currentDirectoryPath)) {
                Directory.CreateDirectory(currentDirectoryPath);
            }
        }

        public void createTextFile( String writingText ) {
            String fileName = getDateString() + ".txt";
            Console.WriteLine(fileName);

            writingText = separateLine + "\r" + writingText;
            File.WriteAllText( currentDirectoryPath + "\\" + fileName , writingText );
        }

        public void createTextFile( Todo todo) {
            String fileName = getDateString() + ".txt";

            string writingText = "Completed task." + "\r\n\r\n";
            writingText += todo.content;
        }

        public void createTodoFile( String todoContents) {
            String fileName = getDateString() + ".txt";
            File.WriteAllText(currentDirectoryPath + "\\" + fileName, todoContents);
        }

        public void createTodoXmlFile( String todoContents) {
            Todo todo = new Todo();
            todo.content = todoContents;
            todo.isCompleted = false;
            todo.additionDate = DateTime.Now;

            System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(Todo));

            string xmlFileName =  currentDirectoryPath + "\\" + getDateString() + ".xml";
            StreamWriter sw = new System.IO.StreamWriter(xmlFileName, false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, todo);

            sw.Close();
        }

        public String getDateString() {
            String dateString = DateTime.Now.ToString( TEXT_FILE_NAME_FORMAT );
            dateString = dateString.Replace(":", "");
            dateString = dateString.Replace("/", "");
            dateString = dateString.Replace(" ", "");
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
