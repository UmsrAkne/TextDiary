using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary.Tests {
    [TestClass()]
    public class TextFileMakerTests {
        [TestMethod()]
        public void createTodoXmlFileTest() {

            TextFileMaker textFileMaker = 
                new TextFileMaker(@"C\Users\main\Documents\Visual Studio 2015\Projects\TextDiary\TextDiary\bin\Debug\text\todos");

            Assert.IsTrue(System.IO.Directory.Exists(
                @"C\Users\main\Documents\Visual Studio 2015\Projects\TextDiary\TextDiary\bin\Debug\text\todos"));

            textFileMaker.createTodoXmlFile(new Todo("test"));
        }
    }
}