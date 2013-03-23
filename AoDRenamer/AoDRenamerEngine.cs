using System.IO;
using System.Text;

namespace AoDRenamer
{
    /// <summary>
    /// リネームエンジン
    /// </summary>
    internal class AoDRenamerEngine
    {
        /// <summary>
        /// メインフォーム
        /// </summary>
        private readonly MainForm _mainForm;

        /// <summary>
        /// INIファイルの格納場所
        /// </summary>
        private string _iniFolderName;

        /// <summary>
        /// 相対パス名
        /// </summary>
        private string _relativePathName;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mainForm">メインフォーム</param>
        internal AoDRenamerEngine(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        /// <summary>
        /// 基準フォルダ名
        /// </summary>
        internal string BaseFolderName { get; set; }

        /// <summary>
        /// 拡張モードかどうか
        /// </summary>
        internal bool ExtendedMode { get; set; }

        /// <summary>
        /// リネームを開始する
        /// </summary>
        /// <param name="typeIndex">パッチの種類</param>
        internal void Start(int typeIndex)
        {
            AppendLog(!string.IsNullOrEmpty(AoDRenamer.VersionName)
                          ? string.Format("AoD Renamer Ver {0}\n\n", AoDRenamer.VersionName)
                          : "AoD Renamer\n\n");
            SetGameType(typeIndex);

            RenameFiles();

            AppendLog("\n");

            DeleteFiles();
        }

        /// <summary>
        /// INIファイルの格納場所を設定する
        /// </summary>
        /// <param name="typeIndex">コンボボックスのインデックス値</param>
        private void SetGameType(int typeIndex)
        {
            switch (typeIndex)
            {
                case 1: // Arsenal of Democracy
                    _iniFolderName = "aod";
                    AppendLog("GameType: Arsenal of Democracy\n\n");
                    break;
                case 2: // Iron Cross over Armageddon
                    _iniFolderName = "icdda";
                    AppendLog("GameType: Iron Cross (Armageddon)\n\n");
                    break;
                case 3: // Iron Cross over Arsenal of Democracy
                    _iniFolderName = "icaod";
                    AppendLog("GameType: Iron Cross (Arsenal of Democracy)\n\n");
                    break;
                case 4: // Iron Cross over Darkest Hour
                    _iniFolderName = "icdh";
                    AppendLog("GameType: Iron Cross (Darkest Hour)\n\n");
                    break;
                default:
                    SetPatchTypeAutoDetect();
                    break;
            }
        }

        /// <summary>
        /// INIファイルの格納場所を自動認識で設定する
        /// </summary>
        private void SetPatchTypeAutoDetect()
        {
            if (!ExtendedMode)
            {
                _iniFolderName = "simple";
                return;
            }

            if (File.Exists(Path.Combine(BaseFolderName, "AODGame.exe")))
            {
                if (File.Exists(Path.Combine(BaseFolderName, "Parches Hoi2-AoD.exe")))
                {
                    _iniFolderName = "icaod";
                    AppendLog("GameType: Iron Cross (Arsenal of Democracy)\n\n");
                }
                else
                {
                    _iniFolderName = "aod";
                    AppendLog("GameType: Arsenal of Democracy\n\n");
                }
            }
            else if (File.Exists(Path.Combine(BaseFolderName, "Hoi2.exe")) &&
                     File.Exists(Path.Combine(BaseFolderName, "IronCross_Setup.exe")))
            {
                _iniFolderName = "icdda";
                AppendLog("GameType: Iron Cross (Armageddon)\n\n");
            }
            else if (File.Exists(Path.Combine(BaseFolderName, "Darkest Hour.exe")) &&
                     Directory.Exists(Path.Combine(BaseFolderName, "Mods\\IronCross")))
            {
                _iniFolderName = "icdh";
                AppendLog("GameType: Iron Cross (Darkest Hour)\n\n");
            }
            else
            {
                _iniFolderName = "simple";
                AppendLog("GameType: Unknown\n\n");
            }
        }

        /// <summary>
        /// ファイルを順にリネームする
        /// </summary>
        private void RenameFiles()
        {
            _relativePathName = "";

            var sr = new StreamReader(Path.Combine(_iniFolderName, "rename.ini"), Encoding.Unicode);
            string line;
            var separator = new[] {','};
            while ((line = sr.ReadLine()) != null)
            {
                // 空行は読み飛ばす
                if (string.IsNullOrEmpty(line))
                {
                    AppendLog("\n");
                    continue;
                }

                // コメント行はコメント内容をログ出力して読み飛ばす
                if (line[0] == '#')
                {
                    AppendLog(string.Format("{0}\n", line));
                    continue;
                }

                // $で始まる行は相対パス指定と見なす
                if (line[0] == '$')
                {
                    // 拡張モードの場合のみ相対パスを変更する
                    if (ExtendedMode)
                    {
                        _relativePathName = line.Substring(1);
                        AppendLog(string.Format("{0}:\n", _relativePathName));
                    }
                    continue;
                }

                // ファイルをリネームする
                string[] token = line.Split(separator);
                if (token.Length == 2)
                {
                    RenameFile(token[0], token[1]);
                }
            }
        }

        /// <summary>
        /// ファイルをリネームする
        /// </summary>
        /// <param name="originalFileName">変更前のファイル名</param>
        /// <param name="targetFileName">変更先のファイル名</param>
        private void RenameFile(string originalFileName, string targetFileName)
        {
            string currentFolderName = Path.Combine(BaseFolderName, _relativePathName);
            string originalPathName = Path.Combine(currentFolderName, originalFileName);
            string targetPathName = Path.Combine(currentFolderName, targetFileName);

            // 変更元のファイルがなければスキップ
            if (!File.Exists(originalPathName))
            {
                AppendLog(string.Format("SKIP  : {0}\n", originalFileName));
                return;
            }

            // 変更先にファイルがある場合
            if (File.Exists(targetPathName))
            {
                // 変更先のファイルの方が古ければ上書き
                if (File.GetLastWriteTime(targetPathName) < File.GetLastWriteTime(originalPathName))
                {
                    File.Delete(targetPathName);
                    File.Move(originalPathName, targetPathName);
                    AppendLog(string.Format("OVRWRT: {0} => {1}\n", originalFileName, targetFileName));
                }
                    // 変更先のファイルの方が新しいか同じならば削除
                else
                {
                    File.Delete(originalPathName);
                    AppendLog(string.Format("DELETE: {0}\n", originalFileName));
                }
            }
            else
            {
                // 変更先にファイルがなければリネーム
                File.Move(originalPathName, targetPathName);
                AppendLog(string.Format("RENAME: {0} => {1}\n", originalFileName, targetFileName));
            }
        }

        /// <summary>
        /// ファイルを順に削除する
        /// </summary>
        private void DeleteFiles()
        {
            _relativePathName = "";

            var sr = new StreamReader(Path.Combine(_iniFolderName, "delete.ini"), Encoding.Unicode);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                // 空行は読み飛ばす
                if (string.IsNullOrEmpty(line))
                {
                    AppendLog("\n");
                    continue;
                }

                // コメント行はコメント内容をログ出力して読み飛ばす
                if (line[0] == '#')
                {
                    AppendLog(string.Format("{0}\n", line));
                    continue;
                }

                // $で始まる行は相対パス指定と見なす
                if (line[0] == '$')
                {
                    // 拡張モードの場合のみ相対パスを変更する
                    if (ExtendedMode)
                    {
                        _relativePathName = line.Substring(1);
                        AppendLog(string.Format("{0}:\n", _relativePathName));
                    }
                    continue;
                }

                // ファイルを削除する
                DeleteFile(line);
            }
        }

        /// <summary>
        /// ファイルを削除する
        /// </summary>
        /// <param name="targetFileName">削除対象のファイル名</param>
        private void DeleteFile(string targetFileName)
        {
            string currentFolderName = Path.Combine(BaseFolderName, _relativePathName);
            string targetPathName = Path.Combine(currentFolderName, targetFileName);

            // 削除対象のファイルがなければスキップ
            if (!File.Exists(targetPathName))
            {
                AppendLog(string.Format("SKIP  : {0}\n", targetFileName));
                return;
            }

            // ファイルを削除する
            File.Delete(targetPathName);
            AppendLog(string.Format("DELETE: {0}\n", targetFileName));
        }

        /// <summary>
        /// ログを出力する
        /// </summary>
        /// <param name="s">出力する文字列</param>
        private void AppendLog(string s)
        {
            _mainForm.AppendLog(s);
        }
    }

    /// <summary>
    /// ゲームの種類
    /// </summary>
    internal enum GameType
    {
        AutoDetect, // 自動検知
        ArsenalOfDemocracy, // Arsenal of Democracy
        IronCrossOverArmageddon, // Iron Cross (Hearts of Iron 2 Doomsday Armageddon)
        IronCrossOverArsenalOfDemocracy, // Iron Cross (Arsenal of Democracy)
        IronCrossOverDarkestHour, // Iron Cross (Darkest Hour)
    }
}