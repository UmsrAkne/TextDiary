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

        private string newLine = Environment.NewLine;

        public const string TEXT_FILE_NAME_FORMAT = "yyyyMMdd_HHmmss";

        public TextFileMaker( String setCurrentDirectoryPath ) {
            currentDirectoryPath = setCurrentDirectoryPath;
            if(!Directory.Exists(currentDirectoryPath)) {
                Directory.CreateDirectory(currentDirectoryPath);
            }
        }

        /// <summary>
        /// 引数の文字列が書き込まれたテキストファイルを生成します。ファイル名は日時に基づき作成されます。
        /// </summary>
        /// <param name="writingText"></param>
        public void createTextFile( String writingText ) {
            String fileName = getDateString() + ".txt";
            Console.WriteLine(fileName);

            writingText = separateLine + newLine + writingText;
            File.WriteAllText( currentDirectoryPath + "\\" + fileName , writingText );
        }

        /// <summary>
        /// 引数に入力したTodoリスト内の全要素のステータスが書き込まれたテキストファイルを生成します。
        /// </summary>
        /// <param name="todos"></param>
        public void createTextFile(Todo[] todos) {
            string fileName = getDateString() + ".txt";
            string writingText = "タスクの状態が確定されました" + newLine + newLine;

            foreach(Todo todo in todos) {
                writingText += separateLine + separateLine + newLine;
                writingText += todo.ToString();
            }

            File.WriteAllText(currentDirectoryPath + "\\" + fileName, writingText);
        }

        /// <summary>
        /// 引数に入力されたTodoからXMLファイルを生成します。
        /// </summary>
        /// <param name="todo"></param>
        public void createTodoXmlFile( Todo todo) {

            System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(Todo));

            if(todo.SourceXmlFileName == "") {
                todo.SourceXmlFileName = getDateString() + ".xml";
            }

            string xmlFilePath = currentDirectoryPath + "\\" + todo.SourceXmlFileName;

            StreamWriter sw = 
                new StreamWriter(xmlFilePath, false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, todo);

            sw.Close();
        }

        /// <summary>
        /// 日付文字列をカスタムフォーマットで取得できます。
        /// </summary>
        public String getDateString() {
            String dateString = DateTime.Now.ToString( TEXT_FILE_NAME_FORMAT );
            dateString = dateString.Replace(":", "");
            dateString = dateString.Replace("/", "");
            dateString = dateString.Replace(" ", "");
            return dateString;
        }

        /// <summary>
        /// 区切り線を文字列で取得できます。
        /// 区切り線を構成する文字列はseparateCharacterフィールドで指定でき、デフォルトは "-" です。
        /// </summary>
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
