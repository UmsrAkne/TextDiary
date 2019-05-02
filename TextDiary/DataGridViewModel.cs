using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextDiary {

    public delegate void StatusChanged();
    public delegate void AppearanceChanged();

    public class DataGridViewModel {

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

        public void changeCurrentCell(FormViewModel fvm) {
            dispatchAppearanceChanged(fvm);
        }

        public void moveDownCurrentItem (FormViewModel fvm) {
            if (fvm.currentCellAdoress != null && fvm.currentCellAdoress.Y < fvm.todoList.Count - 1) {
                Todo tempTodo = fvm.todoList[fvm.currentCellAdoress.Y];
                fvm.todoList.RemoveAt(fvm.currentCellAdoress.Y);
                fvm.todoList.Insert(fvm.currentCellAdoress.Y + 1, tempTodo);
                fvm.currentCellAdoress.Y += 1;
            }

            dispatchStatusChanged(fvm);
        }

        public void moveUpCurrentItem(FormViewModel fvm) {
            if (fvm.currentCellAdoress != null && fvm.currentCellAdoress.Y > 0) {
                Todo tempTodo = fvm.todoList[fvm.currentCellAdoress.Y];
                fvm.todoList.RemoveAt(fvm.currentCellAdoress.Y);
                fvm.todoList.Insert(fvm.currentCellAdoress.Y - 1, tempTodo);
                fvm.currentCellAdoress.Y -= 1;
            }

            dispatchStatusChanged(fvm);
        }

        public void loadTodoList(FormViewModel fvm) {

            fvm.todoList = todoFileReader.loadTodosFromXml().ToList();
            sortByTodoOrder(fvm);
            for(int i = 0; i < fvm.todoList.Count; i++) {
                fvm.todoList[i].Order = i;
                string filePath = todoFileReader.findExistedTodoXmlFile(fvm.todoList[i]);
                todoFileMaker.createTodoXmlFile(fvm.todoList[i]);
            }

            dispatchStatusChanged(fvm);
        }

        /// <summary>
        /// 完了済みTodoをテキストファイルとしてエクスポートします。
        /// FormViewModelのリストに変更はないので、実行してもイベントは送出しません。
        /// </summary>
        /// <param name="fvm"></param>
        public void exportFinishedTodo(FormViewModel fvm) {
            List<Todo> finishedTodos = fvm.todoList.Where(t => t.isCompleted).ToList();
            textFileMaker.createTextFile(finishedTodos.ToArray());
        }

        public void deleteFinishedTodo(FormViewModel fvm) {
            List<Todo> incompleteTodos = fvm.todoList.Where(t => !t.isCompleted).ToList();
            fvm.todoList = incompleteTodos;

            dispatchStatusChanged(fvm);
        }

        private void sortByTodoOrder(FormViewModel fvm) {
            List<Todo> sortedTodoList = fvm.todoList.OrderBy(todo => todo.Order).ToList();
            fvm.todoList = sortedTodoList;
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
