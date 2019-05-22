using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TextDiary {

    public delegate void DataGridViewKeyboardEventHandler(FormViewModel formVM , KeyEventArgs e);
    public delegate void DGVCellSelectionChanged(FormViewModel formVM);
    public delegate void DGVCellClicked(FormViewModel formVM);
    public delegate void ExportTheFinishedTodosMenuClick(FormViewModel formVM);
    public delegate void TextEditorKeyEvgent(String inputedText , KeyEventArgs e);
    public delegate void CompletionCheckBoxClick(FormViewModel formVm);
    public delegate void ExportTodoStatusAsTextFile_MenuItemClick(FormViewModel formVM);
    public delegate void ContextMenuClick_DeleteThisTodo(FormViewModel formVM);

    public partial class Form1 : Form {

        Settings settings;

        TextFileMaker textFileMaker;
        TextFileReader textFileReader;

        private MainFormController mainFormController;
        private DataGridViewModel dataGridViewModel = new DataGridViewModel();
        private Models.TextEditorModel textEditorModel = new Models.TextEditorModel();

        public event DataGridViewKeyboardEventHandler dataGridViewKeyboardEventHandler;
        public event DGVCellSelectionChanged dgvCellSelectionChanged;
        public event DGVCellClicked dgvCellClicked;
        public event ExportTheFinishedTodosMenuClick exportTheFinishedTodosMenuClick;
        public event TextEditorKeyEvgent textEditorKeyEvent;
        public event CompletionCheckBoxClick completionCheckBoxClick = delegate { };
        public event ExportTodoStatusAsTextFile_MenuItemClick exportTodoStatusAsTextFile_MenuItemClick = delegate { };
        public event ContextMenuClick_DeleteThisTodo contextMenuClick_DeleteThisTodo = delegate { };
   
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
            dataGridViewModel.statusChanged += updateDataGridView;
            dataGridViewModel.appearanceChanged += updateAppearance;
            textEditorModel.textChanged += updateAzukiControl;

            //コントローラーにはFormが持っているモデルの参照を渡す
            //取得の必要は無いので、setアクセサのみを公開している。
            mainFormController.dataGridViewModel = this.dataGridViewModel;
            mainFormController.textEditorModel = this.textEditorModel;

            updateDataGridView();

            azukiControl.KeyDown += this.keyboardEventHandler;
            dataGridView.KeyDown += dataGridViewKeyControlEventHandler;
            dataGridView.CurrentCellChanged += DGVCellSelectionChangedHandler;
            dataGridView.CellMouseClick += dgvCellClickedEventHandler;
            dataGridView.CellContextMenuStripNeeded += dgvCellContextMenuStripNeededEventHandler;

            dataGridView.ContextMenuStrip.Items["deleteThisTodo"].Click += 
                (sender, e) => contextMenuClick_DeleteThisTodo(ViewModel);

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

            this.Activated += setWindowsOnTopMost;

            //わずかでも遅延して処理を行えれば問題ないので、間隔はごくごく短く設定する
            delayProcessTimer.Interval = 40;
            delayProcessTimer.Tick += delayProcess;

            var drl = (System.Diagnostics.DefaultTraceListener)System.Diagnostics.Trace.Listeners["Default"];
            drl.LogFileName = System.AppDomain.CurrentDomain.BaseDirectory + "log.txt";

            settings = new Settings();
            settings.currentDirectoryPath = System.IO.Directory.GetCurrentDirectory() + "\\text";
            textFileMaker = new TextFileMaker(settings.currentDirectoryPath);
            textFileReader = new TextFileReader(settings.currentDirectoryPath);

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
        /// セル上でコンテキストメニューが表示されたとき、カレントセルをそのセルに変更する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCellContextMenuStripNeededEventHandler(
            object sender, DataGridViewCellContextMenuStripNeededEventArgs e) {
            dataGridView.CurrentCell = dataGridView[e.ColumnIndex, e.RowIndex];
        }

        /// <summary>
        /// モデルクラスから情報を取得して、テキストボックスを更新する。
        /// </summary>
        private void updateAzukiControl() {
            azukiControl.Text = textEditorModel.Text;
        }

        private FormViewModel viewModel = new FormViewModel();
        private FormViewModel ViewModel {
            get {
                viewModel.text = azukiControl.Text;
                viewModel.currentCellAddress = dataGridView.CurrentCellAddress;
                viewModel.currentIndex = dataGridView.CurrentCellAddress.Y;
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
                currentCellAddress = dataGridView.CurrentCellAddress;
            }

            todoList.Clear();
            List<Todo> list = dataGridViewModel.TodoList;
            foreach(Todo todo in list) {
                todoList.Add(todo);
            }

            dataGridView.DataSource = todoList;

            if(dataGridView.Rows.Count <= currentCellAddress.Y) {
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

            dataGridView.Rows[ dataGridView.CurrentCellAddress.Y ].DefaultCellStyle.BackColor = Color.LightSkyBlue;
        }

        /// <summary>
        /// TodoListModelから情報を取得して、データグリッドビューの見た目（セルの背景色等）のみの更新を行う。
        /// </summary>
        private void updateAppearance(){
            FormViewModel fvm = dataGridViewModel.FormVM;
            if(dataGridView.CurrentCellAddress.X < 0 || dataGridView.CurrentCellAddress.Y < 0) {
                return;
            }

            Point currentCellAddress = dataGridViewModel.CurrentCellAddress;
            dataGridView.CurrentCell = dataGridView[currentCellAddress.X, currentCellAddress.Y];

            for (int i = 0; i < dataGridView.Rows.Count; i++) {
                if (dataGridView.Rows[i].HasDefaultCellStyle == false) continue;
                if (dataGridView.Rows[i].DefaultCellStyle.BackColor == Color.White) continue;
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }


            dataGridView.Rows[dataGridView.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.LightSkyBlue;
        }

        private void dataGridViewKeyControlEventHandler(object sender, KeyEventArgs e) {
            dataGridViewKeyboardEventHandler(ViewModel, e);
        }

        private void DGVCellSelectionChangedHandler(object sender, EventArgs e) {
            if (dataGridView.CurrentCell != null) {
                dgvCellSelectionChanged(ViewModel);
            }
        }

        private void dgvCellClickedEventHandler(object sender , DataGridViewCellMouseEventArgs e) {
            if (dataGridView.Columns[dataGridView.CurrentCellAddress.X].DataPropertyName == "isCompleted") {
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
        /// Azukiコントロールにセットされたキーボードイベントハンドラです
        /// </summary>
        /// <param name="sender">イベント発火元</param>
        /// <param name="e">キーイベントの内容です</param>
        private void keyboardEventHandler(object sender , KeyEventArgs e) {
            textEditorKeyEvent(azukiControl.Text, e);
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
