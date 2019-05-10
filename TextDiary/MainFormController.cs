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
        public DataGridViewModel dataGridViewModel {
            private get;
            set;
        }

        public Models.TextEditorModel textEditorModel;

        public MainFormController( Form1 form) {
            this.form = form;
            form.dataGridViewKeyboardEventHandler += dgvKeyboardEventHandler;
            form.dgvCellSelectionChanged += DGVCellSelectionChangedEventHandler;
            form.dgvCellClicked += dgvCellClickedEventHandler;
            form.exportTheFinishedTodosMenuClick += exportTheFinishedTodos_MenuClickEventHandler;
            form.keyEvent += mainFormKeyEventHandler;
            form.textEditorKeyEvent += textEditorKeyEventHandler;
        }

        private void textEditorKeyEventHandler(string inputedText, KeyEventArgs e) {
            if (e.Control == true && e.KeyCode == Keys.Enter) {
                dataGridViewModel.addTodo(inputedText);
            }
        }

        private void mainFormKeyEventHandler(FormViewModel formVm, KeyEventArgs e) {
            if (e.Control == true && e.KeyCode == Keys.T) {
                dataGridViewModel.addTodo(formVm);
            }
        }

        private void dgvCellClickedEventHandler(FormViewModel formVM) {
            if(formVM.currentDataPropertyName == "isCompleted") {
                dataGridViewModel.toggleIsCompleted(formVM);
                dataGridViewModel.saveTodoAsXml(formVM);
            }
        }

        private void DGVCellSelectionChangedEventHandler(FormViewModel formVM) {
            dataGridViewModel.changeCurrentCell(formVM);
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
            dataGridViewModel.exportFinishedTodo(fvm);
            dataGridViewModel.deleteFinishedTodo(fvm);
        }
    }
}
