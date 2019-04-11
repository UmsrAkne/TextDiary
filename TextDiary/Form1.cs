using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TextDiary {
    public partial class Form1 : Form {

        Settings settings;

        TextFileMaker textFileMaker;
        TextFileReader textFileReader;

        TextFileMaker todoFileMaker;
        TextFileReader todoFileReader;

        TodoFileWatcher todoFileWatcher;

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
            azukiControl.KeyDown += this.keyboardEventHandler;

            dataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;

            dataGridView.DataSource = this.todoList;

            dataGridView.CellValueChanged += dataGridView_cellValueChanged;
            dataGridView.CurrentCellDirtyStateChanged += dataGridView_currentCellDirtStateChanged;

            backGroundPictureForm.Show();
            backGroundPictureForm.Location = this.Location;
            backGroundPictureForm.Size = this.Size;
            this.Move += synchronizeBackGroundWindowLocationWithThisWindow;

            this.Activated += setWindowsOnTopMost;

            //わずかでも遅延して処理を行えれば問題ないので、間隔はごくごく短く設定する
            delayProcessTimer.Interval = 40;
            delayProcessTimer.Tick += delayProcess;
        }

        private void dataGridView_currentCellDirtStateChanged(object sender, EventArgs e) {
            if(dataGridView.CurrentCellAddress.X == 0 && dataGridView.IsCurrentCellDirty) {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView_cellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if(e.ColumnIndex == 0) {
                if((Boolean)dataGridView[e.ColumnIndex , e.RowIndex].Value) {
                    this.dataGridView["CompletedDate", e.RowIndex].Value = DateTime.Now.ToString("MM/dd hh:mm");
                }
                else {
                    this.dataGridView["CompletedDate", e.RowIndex].Value = "";
                }
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
                todo.deadLine = DateTime.Today;
                todoFileMaker.createTodoXmlFile(todo);
                this.todoList.Add(todo);
                textFilePosted = true;
            }

            if (textFilePosted) {
                azukiControl.Text = "";
                e.Handled = true;
                loadTodoList();
            }
        }

        private void loadTodoList() {
            foreach (Todo todo in todoFileReader.loadTodosFromXml()) {
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
    }
}
