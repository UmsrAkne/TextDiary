using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;

namespace TextDiary {
    class FormModelView {

        public string inputText = "";

        public BindingList<Todo> todoList = new BindingList<Todo>();
        public Point currentCellAdoress;
    }
}
