using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TextDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary.Tests {
    [TestClass()]
    public class TextFileReaderTests {
        [TestMethod()]
        public void TextFileReaderTest() {

            string currentDirectoryPath = System.IO.Directory.GetCurrentDirectory() + "\\testTextsDirectory";

            if (Directory.Exists(currentDirectoryPath)) {
                Assert.Fail("存在しないディレクトリのパスを指定してください。有効なパスが指定されています。");
            }

            try {
                TextFileReader reader = new TextFileReader(currentDirectoryPath);
            }
            catch (FileNotFoundException) {

                //例外をキャッチできたら、次はディレクトリを作成した上でnewする。
                Directory.CreateDirectory(currentDirectoryPath);
                if (Directory.Exists(currentDirectoryPath) == false) {
                    Assert.Fail("テスト用のディレクトリが作成できていません");
                }

                TextFileReader reader2 = new TextFileReader(currentDirectoryPath);

                //リターンする前にディレクトリの削除;
                Directory.Delete(currentDirectoryPath);

                return;

            }

            Assert.Fail("存在しないディレクトリが指定されましたが、例外が発生していません。");
        }

        [TestMethod()]
        public void readTextFileAsTodoListTest() {
            //テキストファイルを読み込んだ結果、正しいTodo配列が返ってくるかをテストします

            string currentDirectoryPath = Directory.GetCurrentDirectory() + "\\testTextFile";
            if (Directory.Exists(currentDirectoryPath))
                Directory.Delete(currentDirectoryPath, true);

            Directory.CreateDirectory(currentDirectoryPath);

            const string todoContent = "todoの内容です";

            TextFileMaker writer = new TextFileMaker(currentDirectoryPath );
            writer.createTodoFile(todoContent);

            TextFileReader reader = new TextFileReader(currentDirectoryPath);
            Todo[] todos = reader.readTextFileAsTodoList();

            Assert.AreEqual(todos.Length, 1 , "読み込まれているファイル数がおかしいです。");

            Todo todo = todos[0];

            //DateTimはnull非許容。新規作成したオブジェクトと等価なら値が代入されてない
            Assert.AreNotEqual(todo.deadLine.ToString(), new DateTime().ToString());
            Assert.AreNotEqual(todo.additionDate.ToString(), new DateTime().ToString());

            Assert.AreEqual(todoContent, todo.content);
            Assert.IsFalse(todo.isCompleted);

            //作成したディレクトリの内容を全て削除
            Directory.Delete(currentDirectoryPath, true);            
        }
    }
}