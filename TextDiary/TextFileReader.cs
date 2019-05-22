using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TextDiary {
    public class TextFileReader {

        public String currentDirectoryPath { get; set;}

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

        public Todo[] loadTodosFromXml() {
            string[] files = Directory.GetFiles(currentDirectoryPath ,"*.xml" ,SearchOption.TopDirectoryOnly) ;
            List<Todo> todos = new List<Todo>();

            for (int i = 0; i < files.Count(); i++) {

                string fileName = files[i];
                StreamReader sr = null;

                try {
                    sr = new StreamReader(fileName, new System.Text.UTF8Encoding(false));
                }
                catch (IOException ignore) {
                    //ファイルリードの際、ロック中でファイルオープン不可等の理由で例外が発生する可能性がある。
                    //ファイルの読込をしなければ問題なくプログラムを続行できるため、あえてこの例外を無視する。
                }

                if(sr != null) {
                    XmlSerializer serializer = new XmlSerializer(typeof(Todo));
                    todos.Add((Todo)serializer.Deserialize(sr));
                    sr.Close();
                }
            }

            return todos.ToArray();
        }

        public string findExistedTodoXmlFile( Todo existedTodo ) {
            string existedFilePath = "";

            XmlSerializer serializer = new XmlSerializer(typeof(Todo));
            string[] existedXmlFiles = Directory.GetFiles(currentDirectoryPath , "*.xml");


            foreach( string filePath in existedXmlFiles) {

                StreamReader sr = new StreamReader(filePath);
                Todo deserializedTodo = (Todo)serializer.Deserialize(sr);
                sr.Close();

                if (deserializedTodo.guid.Equals(existedTodo.guid)) {
                    existedFilePath = filePath;
                    break;
                }
            }

            return existedFilePath;
        }

    }
}
