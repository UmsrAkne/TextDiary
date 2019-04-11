using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary.Tests {
    [TestClass()]
    public class TodoTests {
        [TestMethod()]
        public void isEqualTest() {

            Todo todoA = new Todo("test");
            Todo todoB = new Todo("test");

            Assert.IsTrue(Todo.isEqual(todoA, todoB));

            todoA.content = "test2";

            Assert.IsFalse(Todo.isEqual(todoA, todoB));

            todoB.content = "test2";

            Assert.IsTrue(Todo.isEqual(todoA, todoB));

            DateTime completedDate = DateTime.Now;

            todoA.completedDate = completedDate;
            todoB.completedDate = completedDate;

            Assert.IsTrue(Todo.isEqual(todoA, todoB));

        }
    }
}