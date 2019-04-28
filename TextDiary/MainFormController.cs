using System;
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

        public MainFormController( Form1 form) {
            this.form = form;
            form.dataGridViewKeyboardEventHandler += dgvKeyboardEventHandler;
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
    }
}
