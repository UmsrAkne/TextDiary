using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TextDiary {
    public class FormViewModel {

        public string inputedText = "";

        public List<Todo> todoList = new List<Todo>();
        public int currentIndex;
        public Point currentCellAddress;
        public string currentDataPropertyName;
        public string text = "";
    }
}
