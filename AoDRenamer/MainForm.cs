using System;
using System.IO;
using System.Windows.Forms;

namespace AoDRenamer
{
    /// <summary>
    ///     メインフォーム
    /// </summary>
    internal partial class MainForm : Form
    {
        /// <summary>
        ///     リネームエンジン
        /// </summary>
        private readonly AoDRenamerEngine _engine;

        /// <summary>
        ///     コンストラクタ
        /// </summary>
        internal MainForm()
        {
            _engine = new AoDRenamerEngine(this);

            InitializeComponent();

            UpdateTitle();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                folderNameTextBox.Text = args[1];
            }
        }

        /// <summary>
        ///     タイトル文字列を更新する
        /// </summary>
        private void UpdateTitle()
        {
            Text = !string.IsNullOrEmpty(AoDRenamer.VersionName)
                ? string.Format("AoD Renamer Ver {0}", AoDRenamer.VersionName)
                : "AoDRenamer";
        }

        /// <summary>
        ///     参照ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReferenceButtonClick(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(folderNameTextBox.Text) && Directory.Exists(folderNameTextBox.Text))
            {
                dialog.SelectedPath = folderNameTextBox.Text;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                folderNameTextBox.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        ///     開始ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStartButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderNameTextBox.Text) || !Directory.Exists(folderNameTextBox.Text))
            {
                MessageBox.Show("AoD/ICのインストールフォルダ名を指定して下さい。");
                return;
            }

            _engine.BaseFolderName = folderNameTextBox.Text;
            string folderName = Path.GetFileName(_engine.BaseFolderName);
            if (string.IsNullOrEmpty(folderName) || folderName.Equals("pics"))
            {
                _engine.ExtendedMode = false;
            }
            else
            {
                _engine.ExtendedMode = true;
            }

            _engine.Start(typeComboBox.SelectedIndex);
        }

        /// <summary>
        ///     終了ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExitButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        ///     フォルダをドロップした時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormDragDrop(object sender, DragEventArgs e)
        {
            var fileNames = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            folderNameTextBox.Text = fileNames[0];
        }

        /// <summary>
        ///     フォルダをドラッグした時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop)) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        /// <summary>
        ///     ログを追加する
        /// </summary>
        /// <param name="s"></param>
        internal void AppendLog(string s)
        {
            logRichTextBox.AppendText(s);
        }
    }
}