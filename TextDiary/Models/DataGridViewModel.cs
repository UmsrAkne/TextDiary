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
        public event StatusChanged statusChanged = delegate { };

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

        private TodoFileWatcher todoFileWatcher =
            new TodoFileWatcher(Directory.GetCurrentDirectory() + "\\text" + "\\todos");

        public void deleteThisTodo(FormViewModel fvm) {
            TodoList.RemoveAt(fvm.currentIndex);
            File.Delete(todoFileReader.findExistedTodoXmlFile(TodoList[fvm.currentIndex]));
            numberTodo();
            statusChanged();
        }

        public DataGridViewModel() {
            loadTodoList();
            todoFileWatcher.startWatch();
            todoFileWatcher.todoFileChanged += loadTodoList;
        }

        public void changeCurrentCell(FormViewModel fvm) {
            this.CurrentCellAddress = fvm.currentCellAddress;
            appearanceChanged();
        }

        public void moveDownCurrentItem (FormViewModel fvm) {
            if (fvm.currentCellAddress != null && fvm.currentCellAddress.Y < TodoList.Count - 1) {
                Todo tempTodo = TodoList[fvm.currentCellAddress.Y];
                TodoList.RemoveAt(fvm.currentCellAddress.Y);
                TodoList.Insert(fvm.currentCellAddress.Y + 1, tempTodo);
                fvm.currentIndex += 1;
            }

            statusChanged();
        }

        public void moveUpCurrentItem(FormViewModel fvm) {
            if (fvm.currentCellAddress != null && fvm.currentCellAddress.Y > 0) {
                Todo tempTodo = TodoList[fvm.currentCellAddress.Y];
                TodoList.RemoveAt(fvm.currentCellAddress.Y);
                TodoList.Insert(fvm.currentCellAddress.Y - 1, tempTodo);
                fvm.currentIndex -= 1;
            }

            statusChanged();
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

            statusChanged();
        }

        public void toggleIsCompleted(FormViewModel fvm) {
            Todo currentTodo = TodoList[fvm.currentIndex];
            currentTodo.isCompleted = !(currentTodo.isCompleted);

            if (currentTodo.isCompleted) {
                currentTodo.completedDate = DateTime.Now;
            }else {
                currentTodo.completedDate = DateTime.MinValue;
            }

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

        public void exportTodoStatusAsTextFile() {
            List<Todo> allTodo = new List<Todo>();

            foreach (Todo selectedTodo in TodoList) {
                allTodo.Add(selectedTodo);
            }
            System.Console.WriteLine(allTodo.Count);
            textFileMaker.createTextFile(allTodo.ToArray());
        }

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

        private void dispatchAppearanceChanged(FormViewModel fvm) {
            FormVM = fvm;
            appearanceChanged();
        }
    }
}
