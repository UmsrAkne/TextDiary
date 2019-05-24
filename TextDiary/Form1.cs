using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TextDiary {

    public delegate void DataGridViewKeyboardEventHandler(FormViewModel formVM , KeyEventArgs e);
    public delegate void DGVCellClicked(FormViewModel formVM);
    public delegate void ExportTheFinishedTodosMenuClick(FormViewModel formVM);
    public delegate void TextEditorKeyEvgent(String inputedText , KeyEventArgs e);
    public delegate void CompletionCheckBoxClick(FormViewModel formVm);
    public delegate void ExportTodoStatusAsTextFile_MenuItemClick(FormViewModel formVM);
    public delegate void ContextMenuClick_DeleteThisTodo(FormViewModel formVM);
    public delegate void ContextMenuClick_EditThisTodo(FormViewModel formVM);

    public partial class Form1 : Form {

        Settings settings;

        TextFileReader textFileReader;

        private MainFormController mainFormController;
        private TodoListModel todoListModel = new TodoListModel();
        private Models.TextEditorModel textEditorModel = new Models.TextEditorModel();

        public event DataGridViewKeyboardEventHandler dataGridViewKeyboardEventHandler;
        public event DGVCellClicked dgvCellClicked;
        public event ExportTheFinishedTodosMenuClick exportTheFinishedTodosMenuClick;
        public event TextEditorKeyEvgent textEditorKeyEvent;
        public event CompletionCheckBoxClick completionCheckBoxClick = delegate { };
        public event ExportTodoStatusAsTextFile_MenuItemClick exportTodoStatusAsTextFile_MenuItemClick = delegate { };
        public event ContextMenuClick_DeleteThisTodo contextMenuClick_DeleteThisTodo = delegate { };
        public event ContextMenuClick_EditThisTodo contextMenuClick_EditThisTodo = delegate { };
   
        String latestText = "";
        Boolean isLogReading = false;

        System.ComponentModel.BindingList<Todo> todoList = new System.ComponentModel.BindingList<Todo>();
        /* 原因は不明ながら、
         * Windows10の環境にてactivated イベントハンドラ内で this.Activateが効かない。
         * そのため、タイマーを使って遅延処理を行い、そこでActivateを行う。
         * 動作上、外見的には問題ないが、気持ち悪いので改善できるならしたいけど無理*/
        Timer delayProcessTimer = new Timer();

        BackGroundPictureForm backGroundPictureForm = new BackGroundPictureForm();

        public Form1() {
            InitializeComponent();

            //コントローラーには自身の参照を渡す。
            mainFormController = new MainFormController(this);

            //Formが持っているモデルに対しては状態変化を監視するためにイベントハンドラをセット。
            todoListModel.statusChanged += updateDataGridView;
            textEditorModel.textChanged += updateAzukiControl;

            //コントローラーにはFormが持っているモデルの参照を渡す
            //取得の必要は無いので、setアクセサのみを公開している。
            mainFormController.dataGridViewModel = this.todoListModel;
            mainFormController.textEditorModel = this.textEditorModel;

            updateDataGridView();

            azukiControl.KeyDown += (sender ,e) => textEditorKeyEvent(azukiControl.Text, e);
            dataGridView.KeyDown += (sender, e) => dataGridViewKeyboardEventHandler(ViewModel, e);
            dataGridView.CurrentCellChanged += (sender, e) => {
                if (dataGridView.CurrentCell != null) {
                }
            };

            dataGridView.RowLeave += (sender, e) => {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            };

            dataGridView.CellMouseClick += dgvCellClickedEventHandler;

            //セル上でコンテキストメニューの表示があったとき、作業セルをそのセルに変更する。
            dataGridView.CellContextMenuStripNeeded += 
                (sender ,e) => dataGridView.CurrentCell = dataGridView[e.ColumnIndex, e.RowIndex];

            dataGridView.ContextMenuStrip.Items["deleteThisTodo"].Click += 
                (sender, e) => contextMenuClick_DeleteThisTodo(ViewModel);

            dataGridView.ContextMenuStrip.Items["editThisTodo"].Click +=
                (sender, e) => contextMenuClick_EditThisTodo(ViewModel);

            dataGridView.DataSource = this.todoList;

            dataGridView.CellPainting += drawCheckBoxInCell;

            dataGridView.CellBeginEdit += (sender , e) => {
                if (dataGridView[e.ColumnIndex, e.RowIndex].Value is string)
                    dataGridView.ImeMode = ImeMode.NoControl;
            };

            backGroundPictureForm.Show();
            backGroundPictureForm.Location = this.Location;
            backGroundPictureForm.Size = this.Size;
            this.Move += (sender , e) => backGroundPictureForm.Location = this.Location;
            this.SizeChanged += (sender, e) => backGroundPictureForm.Size = this.Size;

            this.Activated += setWindowsOnTopMost;

            //わずかでも遅延して処理を行えれば問題ないので、間隔はごくごく短く設定する
            delayProcessTimer.Interval = 40;
            delayProcessTimer.Tick += delayProcess;

            var drl = (System.Diagnostics.DefaultTraceListener)System.Diagnostics.Trace.Listeners["Default"];
            drl.LogFileName = System.AppDomain.CurrentDomain.BaseDirectory + "log.txt";

            textFileReader = new TextFileReader(System.IO.Directory.GetCurrentDirectory() + "\\text");

            displayTextFilesToolStripMenuItem.Click += (object Sender, EventArgs eventArgs) => {
                if (azukiControl.Text.Length != 0) {
                    latestText = azukiControl.Text;
                }
                isLogReading = true;
                azukiControl.Text = textFileReader.readTextFilesFromCurrentDirectory();
            };

            displayLatestFileToolStripMenuItem.Click += (object Sender, EventArgs eventArgs) => {
                isLogReading = false;
                azukiControl.Text = latestText;
            };

            openBgPictureToolStripMenuItem.Click += (object Sender, EventArgs eventArgs) => {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    backGroundPictureForm.loadPicture(openFileDialog.FileName);
                }
            };

        }

        /// <summary>
        /// モデルクラスから情報を取得して、テキストボックスを更新する。
        /// </summary>
        private void updateAzukiControl() {
            azukiControl.Text = textEditorModel.Text;
        }

        /// <summary>
        /// メインフォームの内容を詰めるビューモデル。
        /// コントローラーに情報を渡す際に利用する。
        /// </summary>
        private FormViewModel viewModel = new FormViewModel();
        private FormViewModel ViewModel {
            get {
                viewModel.text = azukiControl.Text;
                viewModel.currentCellAddress = dataGridView.CurrentCellAddress;
                viewModel.currentIndex = dataGridView.CurrentCellAddress.Y;
                viewModel.currentTodo = todoList[ viewModel.currentIndex ];
                viewModel.currentDataPropertyName =
                    dataGridView.Columns[viewModel.currentCellAddress.X].DataPropertyName;

                return viewModel;
            }
        }

        /// <summary>
        /// TodoListModelから情報を取得して、データグリッドビューを更新する。
        /// </summary>
        private void updateDataGridView() {

            Point currentCellAddress = new Point(0, 0);

            if (dataGridView.CurrentCell != null) {
                currentCellAddress = new Point(dataGridView.CurrentCellAddress.X, todoListModel.FormVM.currentIndex);
            }

            //スクロール位置を記録し、リストをリセットした後で、スクロール位置を元に戻す。
            int tempDisplayedIndex = dataGridView.FirstDisplayedScrollingRowIndex;

            todoList.Clear();
            List<Todo> list = todoListModel.TodoList;
            foreach(Todo todo in list) {
                todoList.Add(todo);
            }

            dataGridView.DataSource = todoList;

            if (tempDisplayedIndex > 0) dataGridView.FirstDisplayedScrollingRowIndex = tempDisplayedIndex;

            if (dataGridView.Rows.Count <= currentCellAddress.Y) {
                dataGridView.CurrentCell = dataGridView[0,0];
            }
            else { 
                dataGridView.CurrentCell = dataGridView[currentCellAddress.X, currentCellAddress.Y];
            }

            for (int i = 0; i < dataGridView.Rows.Count; i++) {
                if (dataGridView.Rows[i].HasDefaultCellStyle == false) continue;
                if (dataGridView.Rows[i].DefaultCellStyle.BackColor == Color.White) continue;
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }

            dataGridView.Rows[dataGridView.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.LightSkyBlue;
        }

        /// <summary>
        /// データグリッドビューのセルがクリックされた際に実行される。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCellClickedEventHandler(object sender , DataGridViewCellMouseEventArgs e) {
            if (dataGridView.Columns[dataGridView.CurrentCellAddress.X].DataPropertyName == "isCompleted") {
                //クリックされたセルがチェックボックスだった場合は、通常に加えてこっちのイベントも飛ばす。
                completionCheckBoxClick(ViewModel);
            }

            dgvCellClicked(ViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">イベント発火元のデータグリッドビューです</param>
        /// <param name="e">セルを描画するためのデータ</param>
        private void drawCheckBoxInCell(object sender, DataGridViewCellPaintingEventArgs e) {
            //このメソッドは、データグリッドビューの見た目を変更するためのものなので、モデルクラスとかには移せない。
            //ビュー内で処理する必要がある。

            dataGridView.ImeMode = ImeMode.Disable;

            if ((e.ColumnIndex != 0) || (e.RowIndex < 0)) return;
            if (dataGridView.Columns[e.ColumnIndex].Name != "isCompleted") return;

            bool currentCellChecked = (bool)dataGridView[e.ColumnIndex, e.RowIndex].Value;
            e.PaintBackground(e.ClipBounds, true);

            if (currentCellChecked) {
                e.Graphics.DrawImage(Properties.Resources.checkBoxImage, e.CellBounds);
            }
            else {
                e.Graphics.DrawImage(Properties.Resources.unCheckBoxImage, e.CellBounds);
            }

            e.Handled = true; //処理を既に行ったのでもう処理しなくていいよって通知。
        }

        private void Form1_Load(object sender, EventArgs e) {
        } 

        /// <summary>
        /// バックのフォームを、全ウィンドウ中最前面に移動します。
        /// 処理の最後に、一瞬後に起動するタイマーをスタートします。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setWindowsOnTopMost(Object sender , EventArgs e) {

            this.Activated -= setWindowsOnTopMost;
            backGroundPictureForm.TopMost = true;
            backGroundPictureForm.TopMost = false;

            delayProcessTimer.Start();
        }

        /// <summary>
        /// メインフォームを最前面に持ってきます。
        /// setWindowsOnTopMost　の最後に呼び出され、一瞬後に実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delayProcess(object sender, EventArgs e){
            this.TopMost = true;
            this.TopMost = false;
            this.Activated += setWindowsOnTopMost;
            delayProcessTimer.Stop();
        }

        /// <summary>
        /// 同ツールストリップメニュー項目がクリックされたとき、イベントが発火します。
        /// </summary>
        /// <param name="sender">ツールストリップアイテム</param>
        /// <param name="e"></param>
        private void exportTheCurrentStateToTextFileToolStripMenuItem_Click(object sender, EventArgs e) {
            exportTodoStatusAsTextFile_MenuItemClick(ViewModel);
        }

        /// <summary>
        /// ツールストリップメニュー項目がクリックされたとき、イベントが発火します
        /// </summary>
        /// <param name="sender">ツールストリップアイテム</param>
        /// <param name="e"></param>
        private void exportTheFinishedTodosAndItDleteToolStripMenuItem_Click(object sender, EventArgs e) {
            exportTheFinishedTodosMenuClick( ViewModel );
        }
    }
}
