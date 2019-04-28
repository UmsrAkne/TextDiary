using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TextDiary {

    public delegate void DataGridViewKeyboardEventHandler(FormViewModel formVM , KeyEventArgs e);
    public delegate void ExportTheFinishedTodosMenuClick(FormViewModel formVM);

    public partial class Form1 : Form {

        Settings settings;

        TextFileMaker textFileMaker;
        TextFileReader textFileReader;

        TextFileMaker todoFileMaker;
        TextFileReader todoFileReader;

        TodoFileWatcher todoFileWatcher;

        private MainFormController mainFormController;
        private DataGridViewModel dataGridViewModel = new DataGridViewModel();
        public event DataGridViewKeyboardEventHandler dataGridViewKeyboardEventHandler;
        public event ExportTheFinishedTodosMenuClick exportTheFinishedTodosMenuClick;

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

            //コントローラーにはFormが持っているモデルの参照を渡す
            //取得の必要は無いので、setアクセサのみを公開している。
            mainFormController.dataGridViewModel = this.dataGridViewModel;

            azukiControl.KeyDown += this.keyboardEventHandler;
            dataGridView.KeyDown += dataGridViewKeyControlEventHandler;

            dataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;

            dataGridView.DataSource = this.todoList;

            dataGridView.CellValueChanged += dataGridView_cellValueChanged;
            dataGridView.CurrentCellDirtyStateChanged += dataGridView_currentCellDirtStateChanged;
            dataGridView.CellFormatting += convertDateTimeFormat;

            //行を選択時に水色に、離れた時に白に戻す。
            dataGridView.SelectionChanged += (sender, e) => coloringCurrentRow(System.Drawing.Color.LightSkyBlue);
            dataGridView.CellLeave += (sender, e) => coloringCurrentRow(System.Drawing.Color.White);

            dataGridView.CellPainting += drawCheckBoxInCell;
            dataGridView.CellClick += toggleCheckBoxImage;

            dataGridView.CellBeginEdit += (sender , e) => {
                if (dataGridView[e.ColumnIndex, e.RowIndex].Value is string)
                    dataGridView.ImeMode = ImeMode.NoControl;
            };

            backGroundPictureForm.Show();
            backGroundPictureForm.Location = this.Location;
            backGroundPictureForm.Size = this.Size;
            this.Move += synchronizeBackGroundWindowLocationWithThisWindow;

            this.Activated += setWindowsOnTopMost;

            //わずかでも遅延して処理を行えれば問題ないので、間隔はごくごく短く設定する
            delayProcessTimer.Interval = 40;
            delayProcessTimer.Tick += delayProcess;

            var drl = (System.Diagnostics.DefaultTraceListener)System.Diagnostics.Trace.Listeners["Default"];
            drl.LogFileName = System.AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        }

        private FormViewModel ViewModel {
            get {
                FormViewModel fvm = new FormViewModel();
                fvm.inputedText = azukiControl.Text;
                fvm.currentCellAdoress = dataGridView.CurrentCellAddress;
                fvm.todoList = new List<Todo>(todoList);

                return fvm;
            }
        }

        //DataGridViewModelで更新があってイベントが発行されたときに実行する。
        private void updateDataGridView() {
            todoList.Clear();
            FormViewModel fvm = dataGridViewModel.FormVM;
            foreach(Todo todo  in fvm.todoList) {
                todoList.Add(todo);
            }

            dataGridView.CurrentCell = null;
            dataGridView.CurrentCell =
                dataGridView[fvm.currentCellAdoress.X, fvm.currentCellAdoress.Y];
        }

        private void dataGridViewKeyControlEventHandler(object sender, KeyEventArgs e) {
            //イベントを送信
            dataGridViewKeyboardEventHandler(ViewModel, e);
        }

        private void toggleCheckBoxImage(object sender, DataGridViewCellEventArgs e) {
            if ((e.ColumnIndex == 0)&&(e.RowIndex >= 0)) {
                DataGridViewCell currentCell = dataGridView[e.ColumnIndex, e.RowIndex];
                if (currentCell.Value is bool) {
                    currentCell.Value = !(bool)currentCell.Value;
                }
            }
        }

        private void drawCheckBoxInCell(object sender, DataGridViewCellPaintingEventArgs e) {

            dataGridView.ImeMode = ImeMode.Disable;

            if ((e.ColumnIndex != 0) || (e.RowIndex < 0)) return;

            DataGridViewCell currentCell = dataGridView[e.ColumnIndex, e.RowIndex];
            e.PaintBackground(e.ClipBounds, true);

            if (currentCell.Value is bool) {

                if ((bool)currentCell.Value) {
                    e.Graphics.DrawImage(Properties.Resources.checkBoxImage, e.CellBounds);
                }
                else {
                    e.Graphics.DrawImage(Properties.Resources.unCheckBoxImage, e.CellBounds);
                }
            }

            e.Handled = true; //処理を既に行ったのでもう処理しなくていいよって通知。
        }

        private void coloringCurrentRow( System.Drawing.Color color ) {
            if (dataGridView.CurrentCellAddress.Y >= 0) {
                DataGridViewCellCollection currentRowCells = dataGridView.Rows[dataGridView.CurrentCellAddress.Y].Cells;
                foreach (DataGridViewCell cell in currentRowCells) {
                    cell.Style.BackColor = color;
                }
            }
        }

        private void convertDateTimeFormat(object sender, DataGridViewCellFormattingEventArgs e) {
            if (e.Value is DateTime) {
                DateTime dateTime = (DateTime)e.Value;
                if (dateTime == DateTime.MinValue) {
                    e.Value = "";
                    return;
                }
                e.Value = dateTime.ToString("MM/dd HH:mm");
            }
        }

        private void dataGridView_currentCellDirtStateChanged(object sender, EventArgs e) {
            string currentColumnDataPropertyName = dataGridView.Columns[dataGridView.CurrentCellAddress.X].DataPropertyName;

            if((currentColumnDataPropertyName == "isCompleted")&&(dataGridView.IsCurrentCellDirty)) {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView_cellValueChanged(object sender, DataGridViewCellEventArgs e) {

            string currentColumnName = dataGridView.Columns[e.ColumnIndex].DataPropertyName;
            if (currentColumnName == "isCompleted") {

                //０でなく-1を指定するのは、次のループ内で要素が見つからなかったらエラーを吐くようにするため
                int completedDateColumnIndex = -1; 

                for(int i = 0; i < dataGridView.Columns.Count; i++) {
                    if(dataGridView.Columns[i].DataPropertyName == "completedDate") {
                        completedDateColumnIndex = i;
                        break;
                    }
                }

                if ((Boolean)dataGridView[e.ColumnIndex , e.RowIndex].Value) {

                    DateTime nowDateTime = DateTime.Now;
                    dataGridView[completedDateColumnIndex, e.RowIndex].Value = nowDateTime;
                    todoList[e.RowIndex].completedDate = nowDateTime;
                }
                else {
                    dataGridView[completedDateColumnIndex, e.RowIndex].Value = DateTime.MinValue;
                    todoList[e.RowIndex].completedDate = DateTime.MinValue;
                }
            }

            //同一のGUIDを持ったTodo(XML)ファイルを検索する。
            string existedTodoXmlFilePath = todoFileReader.findExistedTodoXmlFile(todoList[e.RowIndex]);
            if (existedTodoXmlFilePath != "") {
                textFileMaker.createTodoXmlFile(todoList[e.RowIndex]);
            }
            else {
                MessageBox.Show(
                    "Todoの内容を上書き保存しようしましたが、既存のファイルが存在しないか、GUIDが書き換わった可能性があります。");
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            settings = new Settings();
            settings.currentDirectoryPath = System.IO.Directory.GetCurrentDirectory() + "\\text";
            textFileMaker = new TextFileMaker(settings.currentDirectoryPath);
            textFileReader = new TextFileReader(settings.currentDirectoryPath);

            const String TODOS_DIRECTORY_NAME = "\\todos";

            todoFileMaker = new TextFileMaker( settings.currentDirectoryPath + TODOS_DIRECTORY_NAME );
            todoFileReader = new TextFileReader(settings.currentDirectoryPath + TODOS_DIRECTORY_NAME );
            todoFileWatcher = new TodoFileWatcher(settings.currentDirectoryPath + TODOS_DIRECTORY_NAME);
            todoFileWatcher.todoFileChanged += loadTodoList;
            todoFileWatcher.startWatch();

            loadTodoList();
            displayTextFilesToolStripMenuItem.Click += (object Sender, EventArgs eventArgs) => {
                if(azukiControl.Text.Length != 0) {
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

        private void keyboardEventHandler(object sender , KeyEventArgs e) {

            Boolean textFilePosted = false;

            if (e.Control == true && e.KeyCode == Keys.Enter && !isLogReading) {
                textFileMaker.createTextFile(azukiControl.Text);
                textFilePosted = true;
            }

            //Todo投稿用ボタン
            if (e.Control == true && e.KeyCode == Keys.T && !isLogReading) {
                Todo todo = new Todo( azukiControl.Text );
                todo.deadLine = DateTime.Today.AddDays(1);
                todo.Order = this.todoList.Count + 1;
                todoFileMaker.createTodoXmlFile(todo);
                this.todoList.Add(todo);
                textFilePosted = true;
            }

            if (textFilePosted) {
                azukiControl.Text = "";
                e.Handled = true;
            }
        }

        /// <summary>
        /// 終了済みにチェックされているTodoを表から削除し、テキストデータに記録します。
        /// </summary>
        private void clearFinishedTodo() {
            System.ComponentModel.BindingList<Todo> newTodoList = new System.ComponentModel.BindingList<Todo>();
            List<Todo> finishedTodos = new List<Todo>();

            foreach(Todo selectedTodo in todoList) {
                if (selectedTodo.isCompleted) {

                    string linkedXmlFilePath = todoFileReader.findExistedTodoXmlFile(selectedTodo);
                    finishedTodos.Add(selectedTodo);
                    System.IO.File.Delete(linkedXmlFilePath);
                }
            }

            textFileMaker.createTextFile(finishedTodos.ToArray());
            loadTodoList();
        }

        private void loadTodoList() {
            todoList.Clear();

            List<Todo> tempTodoList = new List<Todo>();
            int index = 0;
            foreach (Todo todo in todoFileReader.loadTodosFromXml()) {
                if(todo.Order == 0) {
                    todo.Order = index;
                    index++;
                }
                tempTodoList.Add(todo);
            }

            index = 0;
            tempTodoList = tempTodoList.OrderBy(td => td.Order).ToList();

            foreach(Todo todo in tempTodoList) {
                todo.Order = index;
                index++;

                if (todoFileReader.findExistedTodoXmlFile(todo) != "") {
                    todoFileMaker.createTodoXmlFile(todo);
                }

                todoList.Add(todo);
            }
        }

        private void synchronizeBackGroundWindowLocationWithThisWindow(object sender , EventArgs e){
            backGroundPictureForm.Location = this.Location;
        }

        private void setWindowsOnTopMost(Object sender , EventArgs e) {

            this.Activated -= setWindowsOnTopMost;
            backGroundPictureForm.TopMost = true;
            backGroundPictureForm.TopMost = false;

            delayProcessTimer.Start();
        }

        private void delayProcess(object sender, EventArgs e){
            this.TopMost = true;
            this.TopMost = false;
            this.Activated += setWindowsOnTopMost;
            delayProcessTimer.Stop();
        }

        //すべてのTodoをテキストファイルに出力する
        private void exportTheCurrentStateToTextFileToolStripMenuItem_Click(object sender, EventArgs e) {
            List<Todo> allTodo = new List<Todo>();

            foreach (Todo selectedTodo in todoList) {
                allTodo.Add(selectedTodo);
            }

            textFileMaker.createTextFile(allTodo.ToArray());
        }

        private void exportTheFinishedTodosAndItDleteToolStripMenuItem_Click(object sender, EventArgs e) {
            exportTheFinishedTodosMenuClick( ViewModel );
            //clearFinishedTodo();
        }
    }
}
