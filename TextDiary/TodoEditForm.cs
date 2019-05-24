using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDiary {

    public delegate void EndEdit();

    public partial class TodoEditForm : Form {

        public event EndEdit endEdit;

        public TodoEditForm() {
            InitializeComponent();
        }

        public void setTodo(Todo todo) {
            textEditWindow.Text = todo.content;
            additionDatePicker.Value = todo.additionDate;
            completeDatePicker.Value = todo.completedDate;
            isCompleteCheckBox.Checked = todo.isCompleted;
        }
    }
}
