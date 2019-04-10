using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary {
    public class Todo {

        public String content {
            get; set;
        }

        public DateTime additionDate {
            get; set;
        }

        public DateTime deadLine {
            get; set;
        }

        public bool isCompleted {
            get; set;
        }
    }
}
