using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TextDiary {

    public class Settings {

        public String currentDirectoryPath;
        public Point windowLocation;
        public Point windowSize;

        public int todoSelectionBackColor = Color.LightSkyBlue.ToArgb();
        public int incompleteTodoBackColor = Color.White.ToArgb();
        public int completedTodoBackColor = Color.White.ToArgb();
        public int expiredTodoBackColor = Color.White.ToArgb();
    }
}
