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

            Todo todoA = new Todo();
            Todo todoB = new Todo();

            Assert.IsTrue(Todo.isEqual(todoA, todoB));

            todoA.content = "test";

            Assert.IsFalse(Todo.isEqual(todoA, todoB));

            todoB.content = "test";

            Assert.IsTrue(Todo.isEqual(todoA, todoB));

        }
    }
}