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
            File.WriteAllText(currentDirectoryPath + "\\" + fileName, writingText);
        }

        public void createTextFile(Todo[] todos) {
            string fileName = getDateString();
            string writingText = "Completed taks" + "\r\n";

            foreach(Todo todo in todos) {
                writingText += "追加日時 : " + todo.additionDate.ToString() + "\r\n";
                writingText += todo.content + "\r\n";
                writingText += "終了日時 : " + todo.completedDate.ToString() + "\r\n" + "\r\n";
            }

            File.WriteAllText(currentDirectoryPath + "\\" + fileName, writingText);
        }

        public void createTodoXmlFile( Todo todo) {

            System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(Todo));

            if((todo.linkedXmlFilePath == "")||(File.Exists(todo.linkedXmlFilePath) == false)) {
                todo.linkedXmlFilePath = currentDirectoryPath + "\\" + getDateString() + ".xml";
            }

            StreamWriter sw = new System.IO.StreamWriter(todo.linkedXmlFilePath, false, new System.Text.UTF8Encoding(false));
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
