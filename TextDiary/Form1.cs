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
    public delegate void TextEditorKeyEvgent(String inputedText , KeyEventArgs e);
    public delegate void CompletionCheckBoxClick(FormViewModel formVm);
    public delegate void ExportTodoStatusAsTextFile_MenuItemClick(FormViewModel formVM);

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
        public event KeyEvent keyEvent;
        public event TextEditorKeyEvgent textEditorKeyEvent;
        public event CompletionCheckBoxClick completionCheckBoxClick = delegate { };
        public event ExportTodoStatusAsTextFile_MenuItemClick exportTodoStatusAsTextFile_MenuItemClick = delegate { };
   
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
            dataGridView.SelectionChanged += DGVCellSelectionChangedHandler;
            dataGridView.CellClick += dgvCellClickedEventHandler;

            dataGridView.DataSource = this.todoList;

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

        private void updateAzukiControl() {
            azukiControl.Text = textEditorModel.Text;
        }

        private FormViewModel ViewModel {
            get {
                FormViewModel fvm = new FormViewModel();
                fvm.text = azukiControl.Text;
                fvm.currentCellAddress = dataGridView.CurrentCellAddress;
                fvm.currentIndex = dataGridView.CurrentCellAddress.Y;
                fvm.currentDataPropertyName =
                    dataGridView.Columns[fvm.currentCellAddress.X].DataPropertyName;
                fvm.todoList = new List<Todo>(todoList);

                return fvm;
            }
        }

        //DataGridViewModelで更新があってイベントが発行されたときに実行する。
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
            if (dataGridView.Columns[dataGridView.CurrentCellAddress.X].DataPropertyName == "isCompleted") {
                completionCheckBoxClick(ViewModel);
            }

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

        private void Form1_Load(object sender, EventArgs e) {
            settings = new Settings();
            settings.currentDirectoryPath = System.IO.Directory.GetCurrentDirectory() + "\\text";
            textFileMaker = new TextFileMaker(settings.currentDirectoryPath);
            textFileReader = new TextFileReader(settings.currentDirectoryPath);

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
            textEditorKeyEvent(azukiControl.Text, e);
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

        private void exportTheCurrentStateToTextFileToolStripMenuItem_Click(object sender, EventArgs e) {
            exportTodoStatusAsTextFile_MenuItemClick(ViewModel);
        }

        private void exportTheFinishedTodosAndItDleteToolStripMenuItem_Click(object sender, EventArgs e) {
            exportTheFinishedTodosMenuClick( ViewModel );
        }
    }
}
