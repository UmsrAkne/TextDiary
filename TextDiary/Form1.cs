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
    public delegate void KeyEvent(FormViewModel formVm , KeyEventArgs e);

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
        public event DGVCellSelectionChanged dgvCellSelectionChanged;
        public event DGVCellClicked dgvCellClicked;
        public event ExportTheFinishedTodosMenuClick exportTheFinishedTodosMenuClick;
        public event KeyEvent keyEvent;

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

            //コントローラーにはFormが持っているモデルの参照を渡す
            //取得の必要は無いので、setアクセサのみを公開している。
            mainFormController.dataGridViewModel = this.dataGridViewModel;

            azukiControl.KeyDown += this.keyboardEventHandler;
            dataGridView.KeyDown += dataGridViewKeyControlEventHandler;
            dataGridView.SelectionChanged += DGVCellSelectionChangedHandler;
            dataGridView.CellClick += dgvCellClickedEventHandler;

            dataGridView.DataSource = this.todoList;

            dataGridView.CurrentCellDirtyStateChanged += dataGridView_currentCellDirtStateChanged;

            dataGridView.CellPainting += drawCheckBoxInCell;

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
                fvm.text = azukiControl.Text;
                fvm.currentCellAddress = dataGridView.CurrentCellAddress;
                fvm.currentDataPropertyName =
                    dataGridView.Columns[fvm.currentCellAddress.X].DataPropertyName;
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
                dataGridView[fvm.currentCellAddress.X, fvm.currentCellAddress.Y];

            for (int i = 0; i < dataGridView.Rows.Count; i++) {
                if (dataGridView.Rows[i].HasDefaultCellStyle == false) continue;
                if (dataGridView.Rows[i].DefaultCellStyle.BackColor == Color.White) continue;
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }

            dataGridView.Rows[fvm.currentCellAddress.Y].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            azukiControl.Text = fvm.text;
        }

        private void updateAppearance(){
            FormViewModel fvm = dataGridViewModel.FormVM;
            if(dataGridView.CurrentCellAddress.X < 0 || dataGridView.CurrentCellAddress.Y < 0) {
                return;
            }

            for(int i = 0; i < dataGridView.Rows.Count; i++) {
                if (dataGridView.Rows[i].HasDefaultCellStyle == false) continue;
                if (dataGridView.Rows[i].DefaultCellStyle.BackColor == Color.White) continue;
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            
            dataGridView.CurrentCell = dataGridView[fvm.currentCellAddress.X , fvm.currentCellAddress.Y];
            dataGridView.Rows[fvm.currentCellAddress.Y].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            System.Console.WriteLine("updateLook");
        }

        private void dataGridViewKeyControlEventHandler(object sender, KeyEventArgs e) {
            //イベントを送信
            dataGridViewKeyboardEventHandler(ViewModel, e);
        }

        private void DGVCellSelectionChangedHandler(object sender, EventArgs e) {
            if (dataGridView.CurrentCell != null) {
                dgvCellSelectionChanged(ViewModel);
            }
        }

        private void dgvCellClickedEventHandler(object sender , DataGridViewCellEventArgs e) {
            dgvCellClicked(ViewModel);
        }

        private void drawCheckBoxInCell(object sender, DataGridViewCellPaintingEventArgs e) {

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

        private void dataGridView_currentCellDirtStateChanged(object sender, EventArgs e) {
            string currentColumnDataPropertyName = dataGridView.Columns[dataGridView.CurrentCellAddress.X].DataPropertyName;

            if((currentColumnDataPropertyName == "isCompleted")&&(dataGridView.IsCurrentCellDirty)) {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
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

            if(keyEvent != null) keyEvent(ViewModel , e);

            if (e.Control == true && e.KeyCode == Keys.Enter && !isLogReading) {
                textFileMaker.createTextFile(azukiControl.Text);
                azukiControl.Text = "";
                e.Handled = true;
            }
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

                if (todoFileReader.findExistedTodoXmlFile(todo) == "") {
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
        }
    }
}
