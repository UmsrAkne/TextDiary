using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary.Models {

    public delegate void TextChanged();

    public class TextEditorModel {

        public event TextChanged textChanged;

        private String text = "";
        public String Text {
            get {
                return text;
            }
            private set {
                text = value;
                textChanged();//セットされた時点でテキスト変更イベントを通知
            }
        }

        public void clearText() {
            Text = "";
            textChanged();
        }

    }
}
