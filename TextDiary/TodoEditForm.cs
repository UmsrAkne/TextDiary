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
    public delegate void CancelEdit();

    public partial class TodoEditForm : Form {

        public event EndEdit endEdit = delegate { };
        public event CancelEdit cancelEdit = delegate { };

        private Todo currentTodo;

        public TodoEditForm() {
            InitializeComponent();

            applyButton.Click += (sender, e) => { endEdit(); };
            cancelButton.Click += (sender, e) => { cancelEdit(); };
            this.KeyPreview = true;
            KeyDown += keyboardEvent;
        }

        /// <summary>
        /// Todo内の情報を各コントロール内にセットします。
        /// </summary>
        /// <param name="todo">ウィンドウ内で取り扱うTodoです。</param>
        public void setTodo(Todo todo) {
            currentTodo = todo;
            textEditWindow.Text = todo.content;
            additionDatePicker.Value = todo.additionDate;

            if(todo.completedDate == DateTime.MinValue) {
                completeDatePicker.Value = DateTimePicker.MinimumDateTime;
            }
            else {
                completeDatePicker.Value = todo.completedDate;
            }

            isCompleteCheckBox.Checked = todo.isCompleted;
        }

        private void keyboardEvent(object sender, KeyEventArgs e) {
            if (e.Control == true && e.KeyCode == Keys.Enter) {
                endEdit();
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Escape) {
                e.Handled = true;
                cancelEdit();
            }
        }

    }
}
