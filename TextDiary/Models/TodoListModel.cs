﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace TextDiary {

    public delegate void StatusChanged();

    public class TodoListModel {

        public List<Todo> TodoList {
            get;
            private set;
        } = new List<Todo>();

        public Point CurrentCellAddress {
            get;
            private set;
        } = new Point(0, 0);

        //リストの状態が更新されたら、このイベントを発行してビューに通知する。
        public event StatusChanged statusChanged = delegate { };

        private TextFileReader todoFileReader
            = new TextFileReader(Directory.GetCurrentDirectory() + "\\text" + "\\todos");

        private TextFileMaker todoFileMaker
            = new TextFileMaker(Directory.GetCurrentDirectory() + "\\text" + "\\todos");

        private TextFileMaker textFileMaker
            = new TextFileMaker(Directory.GetCurrentDirectory() + "\\text");

        public FormViewModel FormVM {
            get;
            private set;
        } = new FormViewModel();

        private TodoFileWatcher todoFileWatcher =
            new TodoFileWatcher(Directory.GetCurrentDirectory() + "\\text" + "\\todos");

        public TodoListModel() {
            loadTodoList();
            todoFileWatcher.startWatch();
            todoFileWatcher.todoFileChanged += loadTodoList;
        }

        /// <summary>
        /// 作業中のTodoを削除します。
        /// </summary>
        /// <param name="fvm"></param>
        public void deleteThisTodo(FormViewModel fvm) {
            TodoList.RemoveAt(fvm.currentIndex);
            File.Delete(todoFileReader.findExistedTodoXmlFile(TodoList[fvm.currentIndex]));
            numberTodo();
            statusChanged();
        }

        /// <summary>
        /// 選択しているセルのインデックスを一つ大きいインデックスと入れ替えます。
        /// </summary>
        /// <param name="fvm"></param>
        public void moveDownCurrentItem (FormViewModel fvm) {
            if (fvm.currentCellAddress != null && fvm.currentCellAddress.Y < TodoList.Count - 1) {
                Todo tempTodo = TodoList[fvm.currentCellAddress.Y];
                TodoList.RemoveAt(fvm.currentCellAddress.Y);
                TodoList.Insert(fvm.currentCellAddress.Y + 1, tempTodo);
                fvm.currentIndex += 1;
                FormVM = fvm;
            }

            statusChanged();
        }

        /// <summary>
        /// 選択しているセルのインデックスを一つ小さいインデックスと入れ替えます。
        /// </summary>
        /// <param name="fvm"></param>
        public void moveUpCurrentItem(FormViewModel fvm) {
            if (fvm.currentCellAddress != null && fvm.currentCellAddress.Y > 0) {
                Todo tempTodo = TodoList[fvm.currentCellAddress.Y];
                TodoList.RemoveAt(fvm.currentCellAddress.Y);
                TodoList.Insert(fvm.currentCellAddress.Y - 1, tempTodo);
                fvm.currentIndex -= 1;
                FormVM = fvm;
            }

            statusChanged();
        }

        /// <summary>
        /// Todoリストの末尾にTodoを加えます。
        /// </summary>
        /// <param name="sourceText"></param>
        public void addTodo(String sourceText) {
            Todo todo = new Todo(sourceText);
            todo.deadLine = DateTime.Today.AddDays(1);
            todo.Order = TodoList.Count;
            todoFileMaker.createTodoXmlFile(todo);
            TodoList.Add(todo);
            statusChanged();
        }

        /// <summary>
        /// TodoリストをTodo格納フォルダからロードします。
        /// </summary>
        public void loadTodoList() {

            TodoList = todoFileReader.loadTodosFromXml().ToList();

            List<Todo> sortedTodoList = TodoList.OrderBy(todo => todo.Order).ToList();
            TodoList = sortedTodoList;

            for (int i = 0; i < TodoList.Count; i++) {
                TodoList[i].Order = i;
                string filePath = todoFileReader.findExistedTodoXmlFile(TodoList[i]);
                todoFileMaker.createTodoXmlFile(TodoList[i]);
            }

            statusChanged();
        }

        /// <summary>
        /// Todoが完了済みか示すBoolを切り替えます。
        /// </summary>
        /// <param name="fvm"></param>
        public void toggleIsCompleted(FormViewModel fvm) {
            Todo currentTodo = TodoList[fvm.currentIndex];
            currentTodo.isCompleted = !(currentTodo.isCompleted);

            if (currentTodo.isCompleted) {
                currentTodo.completedDate = DateTime.Now;
            }else {
                currentTodo.completedDate = DateTime.MinValue;
            }

            FormVM = fvm;
            statusChanged();
        }

        /// <summary>
        /// リスト内容に変更はないので、イベントは送出しません
        /// </summary>
        /// <param name="fvm"></param>
        public void saveTodoAsXml(FormViewModel fvm) {
            Todo todo = TodoList[fvm.currentCellAddress.Y];
            if (todoFileReader.findExistedTodoXmlFile(todo) != "") {
                todoFileMaker.createTodoXmlFile(todo);
            }
            else {
                System.Windows.Forms.MessageBox.Show(
                    "Todoの内容を上書き保存しようしましたが、既存のファイルが存在しないか、GUIDが書き換わった可能性があります。");
            }
        }

        /// <summary>
        /// 完了済みTodoをテキストファイルとしてエクスポートします。
        /// FormViewModelのリストに変更はないので、実行してもイベントは送出しません。
        /// </summary>
        public void exportFinishedTodo() {
            List<Todo> finishedTodos = TodoList.Where(t => t.isCompleted).ToList();
            textFileMaker.createTextFile(finishedTodos.ToArray());
        }

        /// <summary>
        /// 完了済みになっているTodoを削除します。
        /// </summary>
        public void deleteFinishedTodo() {
            List<Todo> completedTodos = TodoList.Where(t => t.isCompleted).ToList();
            foreach(Todo todo in completedTodos) {
                File.Delete(
                    todoFileReader.findExistedTodoXmlFile(todo));
            }

            List<Todo> incompleteTodos = TodoList.Where(t => !t.isCompleted).ToList();
            TodoList = incompleteTodos;
            statusChanged();
        }

        /// <summary>
        /// 現在のTodoリストの状態をテキストファイルとして出力します。
        /// </summary>
        public void exportTodoStatusAsTextFile() {
            List<Todo> allTodo = new List<Todo>();

            foreach (Todo selectedTodo in TodoList) {
                allTodo.Add(selectedTodo);
            }
            System.Console.WriteLine(allTodo.Count);
            textFileMaker.createTextFile(allTodo.ToArray());
        }

        /// <summary>
        /// 第一引数に指定したTodoの内容を、第二引数に指定したTodoの内容に書き換えます。
        /// ただし、Todoインスタンスのプロパティのうち、GUID,SourceXmlFileName,Orderの値に関しては
        /// 第二引数のインスタンスの内容に関わらず書き換えを行いません。
        /// これらの値を書き換えると、全く別のTodoインスタンスとなってしまうためです。
        /// </summary>
        /// <param name="baseTodo">書き換えを行うTodoを入力します。</param>
        /// <param name="newContentTodo">書き換え後の新しい内容が記述されたTodoオブジェクトです</param>
        public void rewriteTodo(Todo baseTodo , Todo newContentTodo) {
            baseTodo.content = newContentTodo.content;
            baseTodo.additionDate = newContentTodo.additionDate;
            baseTodo.completedDate = newContentTodo.completedDate;
            baseTodo.isCompleted = newContentTodo.isCompleted;
            baseTodo.deadLine = newContentTodo.deadLine;

            if (todoFileReader.findExistedTodoXmlFile(baseTodo) != "") {
                todoFileMaker.createTodoXmlFile(baseTodo);
            }
            else {
                System.Windows.Forms.MessageBox.Show(
                    "Todoの内容を上書き保存しようしましたが、既存のファイルが存在しないか、GUIDが書き換わった可能性があります。");
            }
        }

        /// <summary>
        /// リスト内の全Todoに番号を振ります。
        /// </summary>
        public void numberTodo() {
            for(int i = 0; i < TodoList.Count; i++) {
                TodoList[i].Order = i;
                string filePath = todoFileReader.findExistedTodoXmlFile(TodoList[i]);
                todoFileMaker.createTodoXmlFile(TodoList[i]);
            }
            statusChanged();
        }

        private void dispatchStatusChanged(FormViewModel fvm) {
            FormVM = fvm;
            statusChanged();
        }
    }
}
