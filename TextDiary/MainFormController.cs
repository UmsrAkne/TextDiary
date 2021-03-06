﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace TextDiary {

    //MVCにおけるコントローラーの役割をするクラス
    //UIに依存しない処理を行うモデルのインスタンスを保持。
    //ビューであるメインフォームのインスタンスを監視するためにそのインスタンスも保持。

    public class MainFormController {

        private Form1 form;
        private TodoEditForm todoEditForm = new TodoEditForm();

        public TodoListModel dataGridViewModel {
            private get;
            set;
        }

        public Models.TextEditorModel textEditorModel;

        public MainFormController( Form1 form) {
            this.form = form;
            form.dataGridViewKeyboardEventHandler += dgvKeyboardEventHandler;
            form.dgvCellClicked += dgvCellClickedEventHandler;
            form.exportTheFinishedTodosMenuClick += exportTheFinishedTodos_MenuClickEventHandler;
            form.textEditorKeyEvent += textEditorKeyEventHandler;
            form.completionCheckBoxClick += completionCheckBoxClickEventHandler;
            form.exportTodoStatusAsTextFile_MenuItemClick += (fvm) => dataGridViewModel.exportTodoStatusAsTextFile();
            form.contextMenuClick_DeleteThisTodo += (fvm) => dataGridViewModel.deleteThisTodo(fvm);
            form.contextMenuClick_EditThisTodo += (fvm) => showTodoEditWindow(fvm);

            todoEditForm.endEdit += (currentTodo , newTodo) => dataGridViewModel.rewriteTodo(currentTodo, newTodo);
            todoEditForm.cancelEdit += () => { };
        }

        private void textEditorKeyEventHandler(string inputedText, KeyEventArgs e) {
            if (e.Control == true && e.KeyCode == Keys.Enter) {
                textEditorModel.clearText();
                e.Handled = true;
            }

            if (e.Control == true && e.KeyCode == Keys.T) {
                dataGridViewModel.addTodo(inputedText);
                textEditorModel.clearText();
            }
        }

        public void setTodoEditForm(TodoEditForm todoEditForm) {
            this.todoEditForm = todoEditForm;
        }

        private void dgvCellClickedEventHandler(FormViewModel formVM) {
        }

        private void showTodoEditWindow(FormViewModel formVM) {
            todoEditForm.setTodo(formVM.currentTodo);
            todoEditForm.ShowDialog(form);
        }

        private void completionCheckBoxClickEventHandler(FormViewModel formVm) {
            dataGridViewModel.toggleIsCompleted(formVm);
            dataGridViewModel.saveTodoAsXml(formVm);
        }

        private void dgvKeyboardEventHandler(FormViewModel fvm , KeyEventArgs e) {

            if (e.Control == true && e.KeyCode == Keys.Down) {
                dataGridViewModel.moveDownCurrentItem(fvm);
                e.Handled = true;
            }

            if (e.Control == true && e.KeyCode == Keys.Up) {
                dataGridViewModel.moveUpCurrentItem(fvm);
                e.Handled = true;
            }

        }

        private void exportTheFinishedTodos_MenuClickEventHandler(FormViewModel fvm) {
            dataGridViewModel.exportFinishedTodo();
            dataGridViewModel.deleteFinishedTodo();
            dataGridViewModel.numberTodo();
        }
    }
}
