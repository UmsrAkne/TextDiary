using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace TextDiary {

    public delegate void StatusChanged();
    public delegate void AppearanceChanged();

    public class DataGridViewModel {

        public List<Todo> TodoList {
            get;
            private set;
        } = new List<Todo>();

        public Point CurrentCellAddress {
            get;
            private set;
        } = new Point(0, 0);


        //リストの状態が更新されたら、このイベントを発行してビューに通知する。
        public event StatusChanged statusChanged;

        //リストの外見のみが更新されたときにイベントを発行。ビューに通知
        public event AppearanceChanged appearanceChanged;

        private TextFileReader todoFileReader
            = new TextFileReader(Directory.GetCurrentDirectory() + "\\text" + "\\todos");

        private TextFileMaker todoFileMaker
            = new TextFileMaker(Directory.GetCurrentDirectory() + "\\text" + "\\todos");

        private TextFileMaker textFileMaker
            = new TextFileMaker(Directory.GetCurrentDirectory() + "\\text");

        public FormViewModel FormVM{
            get;
            private set;
        }

        public DataGridViewModel() {
            loadTodoList();
        }

        public void changeCurrentCell(FormViewModel fvm) {
            dispatchAppearanceChanged(fvm);
        }

        public void moveDownCurrentItem (FormViewModel fvm) {
            if (fvm.currentCellAddress != null && fvm.currentCellAddress.Y < fvm.todoList.Count - 1) {
                Todo tempTodo = fvm.todoList[fvm.currentCellAddress.Y];
                fvm.todoList.RemoveAt(fvm.currentCellAddress.Y);
                fvm.todoList.Insert(fvm.currentCellAddress.Y + 1, tempTodo);
                fvm.currentCellAddress.Y += 1;
            }

            dispatchStatusChanged(fvm);
        }

        public void moveUpCurrentItem(FormViewModel fvm) {
            if (fvm.currentCellAddress != null && fvm.currentCellAddress.Y > 0) {
                Todo tempTodo = fvm.todoList[fvm.currentCellAddress.Y];
                fvm.todoList.RemoveAt(fvm.currentCellAddress.Y);
                fvm.todoList.Insert(fvm.currentCellAddress.Y - 1, tempTodo);
                fvm.currentCellAddress.Y -= 1;
            }

            dispatchStatusChanged(fvm);
        }

        public void addTodo(String sourceText) {
            Todo todo = new Todo(sourceText);
            todo.deadLine = DateTime.Today.AddDays(1);
            todo.Order = TodoList.Count;
            todoFileMaker.createTodoXmlFile(todo);
            TodoList.Add(todo);
            statusChanged();
        }

        public void loadTodoList() {

            TodoList = todoFileReader.loadTodosFromXml().ToList();

            List<Todo> sortedTodoList = TodoList.OrderBy(todo => todo.Order).ToList();
            TodoList = sortedTodoList;

            for (int i = 0; i < TodoList.Count; i++) {
                TodoList[i].Order = i;
                string filePath = todoFileReader.findExistedTodoXmlFile(TodoList[i]);
                todoFileMaker.createTodoXmlFile(TodoList[i]);
            }
        }

        public void toggleIsCompleted(FormViewModel fvm) {
            Todo currentTodo = fvm.todoList[fvm.currentCellAddress.Y];
            currentTodo.isCompleted = !(currentTodo.isCompleted);

            if (currentTodo.isCompleted) {
                currentTodo.completedDate = DateTime.Now;
            }else {
                currentTodo.completedDate = DateTime.MinValue;
            }

            dispatchStatusChanged(fvm);
        }

        /// <summary>
        /// リスト内容に変更はないので、イベントは送出しません
        /// </summary>
        /// <param name="fvm"></param>
        public void saveTodoAsXml(FormViewModel fvm) {
            Todo todo = fvm.todoList[fvm.currentCellAddress.Y];
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

        public void numberTodo() {
            for(int i = 0; i < TodoList.Count; i++) {
                TodoList[i].Order = i;
            }
            statusChanged();
        }

        private void dispatchStatusChanged(FormViewModel fvm) {
            FormVM = fvm;
            statusChanged();
        }

        private void dispatchAppearanceChanged(FormViewModel fvm) {
            FormVM = fvm;
            appearanceChanged();
        }
    }
}
