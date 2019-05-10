using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary.Models {

    public delegate void TextChanged();

    public class TextEditorModel {

        public event TextChanged textChanged;

        public String Text {
            get;
            private set;
        } = "";

        public void clearText() {
            Text = "";
            textChanged();
        }

    }
}
