using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary {
    public class Todo {

        public Todo() {
            this.additionDate = DateTime.Now;
        }

        public Todo( string content ) {
            this.additionDate = DateTime.Now;
            this.content = content;
        }

        public int Order {
            get; set;
        }

        public bool isCompleted {
            get; set;
        }

        public String content {
            get; set;
        }

        public DateTime additionDate {
            get; set;
        }

        public DateTime completedDate {
            get; set;
        }

        public DateTime deadLine {
            get; set;
        }

        public String linkedXmlFilePath = "";

        public Guid guid { get; set; } = Guid.NewGuid();

        public override string ToString() {
            string newLine = Environment.NewLine;
            string dateTimeFormat = "yyyy/MM/dd HH:mm:ss";

            string text = "";

            text += "追加日時 - 終了日時 : ";
            text += additionDate.ToString(dateTimeFormat) + " - ";

            if (completedDate == DateTime.MinValue) text += newLine + newLine;
            else text += completedDate.ToString(dateTimeFormat) + newLine + newLine;

            text += content + newLine + newLine;

            if(deadLine == DateTime.MinValue) text += "締切日時 : " + "未設定" + newLine;
            else text += "締切日時 : " + deadLine.ToString(dateTimeFormat) + newLine + newLine; ;

            return text;
        }

        public static bool isEqual(Todo todoA, Todo todoB) {
            if (String.Equals(todoA.content, todoB.content) == false) {
                return false;
            }

            if (DateTime.Equals(todoA.additionDate, todoB.additionDate) == false) {
                return false;
            }

            if (DateTime.Equals(todoA.deadLine, todoB.deadLine) == false){
                return false;
            }

            if (todoA.isCompleted != todoB.isCompleted){
                return false;
            }

            if (DateTime.Equals(todoA.completedDate, todoB.completedDate) == false) {
                return false;
            }

            if ((todoA.guid.Equals(todoB.guid)) == false){
                return false;
            }

            return true;
            
        }

    }
}
