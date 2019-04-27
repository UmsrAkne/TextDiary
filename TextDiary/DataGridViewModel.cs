using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDiary {

    public delegate void ListStateChanged();

    class DataGridViewModel {
        public event ListStateChanged listStateChanged;
    }
}
