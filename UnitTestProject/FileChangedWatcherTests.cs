using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary.Tests {
    [TestClass()]
    public class FileChangedWatcherTests {
        [TestMethod()]
        public void startWatchTest() {
            TodoFileWatcher fcw = new TodoFileWatcher (@"C:\Users\main\Desktop");
        }

        [TestMethod()]
        public void stopWatchTest() {
            TodoFileWatcher fcw = new TodoFileWatcher(@"C:\Users\main\Desktop");
        }
    }
}