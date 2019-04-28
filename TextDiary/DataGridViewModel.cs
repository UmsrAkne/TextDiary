using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextDiary {

    public delegate void StatusChanged();

    public class DataGridViewModel {

        //リストの状態が更新されたら、このイベントを発行してビューに通知する。
        public event StatusChanged statusChanged;

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

        public void moveDownCurrentItem (FormViewModel fvm) {
            if (fvm.currentCellAdoress != null && fvm.currentCellAdoress.Y < fvm.todoList.Count - 1) {
                Todo tempTodo = fvm.todoList[fvm.currentCellAdoress.Y];
                fvm.todoList.RemoveAt(fvm.currentCellAdoress.Y);
                fvm.todoList.Insert(fvm.currentCellAdoress.Y + 1, tempTodo);
                fvm.currentCellAdoress.Y += 1;
                FormVM = fvm;
            }

            statusChanged();
        }

        public void moveUpCurrentItem(FormViewModel fvm) {
            if (fvm.currentCellAdoress != null && fvm.currentCellAdoress.Y > 0) {
                Todo tempTodo = fvm.todoList[fvm.currentCellAdoress.Y];
                fvm.todoList.RemoveAt(fvm.currentCellAdoress.Y);
                fvm.todoList.Insert(fvm.currentCellAdoress.Y - 1, tempTodo);
                fvm.currentCellAdoress.Y -= 1;
                FormVM = fvm;
            }

        private void sortByTodoOrder(FormViewModel fvm) {
            List<Todo> sortedTodoList = fvm.todoList.OrderBy(todo => todo.Order).ToList();
            fvm.todoList = sortedTodoList;
        }

        private void dispatchStatusChanged(FormViewModel fvm) {
            FormVM = fvm;
            statusChanged();
        }
    }
}
