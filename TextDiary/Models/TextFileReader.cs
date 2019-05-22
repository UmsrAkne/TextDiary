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

        /// <summary>
        /// </summary>
        /// <param name="settingCurrentDirectoryPath">このファイルが読込作業を行うディレクトリのパスです</param>
        public TextFileReader( String settingCurrentDirectoryPath ) {
            if (System.IO.Directory.Exists(settingCurrentDirectoryPath) == false) {
                throw new FileNotFoundException("ファイルが存在しない、またはファイルにアクセスできません。");
            }

            currentDirectoryPath = settingCurrentDirectoryPath;

        }

        /// <summary>
        /// 作業ディレクトリのパスをセットします。
        /// </summary>
        /// <param name="settingCurrentDirectoryPath">指定するパスです</param>
        public void setCurrentDirectoryPath( String settingCurrentDirectoryPath ) {
            currentDirectoryPath = settingCurrentDirectoryPath;
        }

        /// <summary>
        /// テキストファイルをカレントディレクトリから読み込みます
        /// </summary>
        /// <returns>テキストの内容をまとめた文字列を返却します。</returns>
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

        /// <summary>
        /// カレントディレクトリに存在するXMLファイルを読み込みます。
        /// </summary>
        /// <returns>Todo配列が返却されます。</returns>
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

        /// <summary>
        /// 引数に指定したTodoファイルとペアのXMLファイルのパスを返却します。。
        /// ペアのXMLファイルはTodo内のGUIDに基づいて検索されます。
        /// </summary>
        /// <param name="existedTodo">XMLファイルを探すために使うTodoファイルを指定します。</param>
        /// <returns>見つかったXMLファイルのパスです。見つからなかった場合、空の文字列が返却されます。</returns>
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
