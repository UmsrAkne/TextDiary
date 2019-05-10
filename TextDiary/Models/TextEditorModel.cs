using System;
using System.IO;

namespace TextDiary.Models {

    public delegate void TextChanged();

    public class TextEditorModel {

        public event TextChanged textChanged;
        
        private TextFileMaker textFileMaker = new TextFileMaker(Directory.GetCurrentDirectory() + "\\text");

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
        }

        public void createTextFile(String writingText) {
            textFileMaker.createTextFile(writingText);
            Text = "";
        }
    }
}
