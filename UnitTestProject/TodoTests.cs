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
            Assert.IsTrue(Todo.isEqual(todoA, todoA));

            Todo todoB = new Todo();
            Assert.IsFalse(Todo.isEqual(todoA, todoB));
        }
    }
}