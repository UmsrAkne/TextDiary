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
        public void loadTodosFromXmlTest() {
            TextFileReader tr = new TextFileReader(@"C\Users\main\Documents\Visual Studio 2015\Projects\TextDiary\TextDiary\bin\Debug\text\todos");
            Todo[] todos = tr.loadTodosFromXml();
            Console.WriteLine(todos.Count());

            System.Console.WriteLine(todos[0].additionDate.ToString());
        }
    }
}