using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace TextDiary {

    public class Settings {

        public String currentDirectoryPath;
        public Point windowLocation;
        public Point windowSize;

        public int todoSelectionBackColor = Color.LightSkyBlue.ToArgb();
        public int incompleteTodoBackColor = Color.White.ToArgb();
        public int completedTodoBackColor = Color.White.ToArgb();
        public int expiredTodoBackColor = Color.White.ToArgb();
        public int defaultTodoBackColor = Color.White.ToArgb();

        private static String settingsFilePath = AppDomain.CurrentDomain.BaseDirectory + "settings.xml";

        /// <summary>
        /// アプリケーション実行ファイルのディレクトリに存在する設定ファイル（XML）を読み込みます。
        /// 実行した時点でファイルが存在しなかった場合は、同ディレクトリにXMLファイルを作成します。
        /// </summary>
        public static Settings loadSettingXmlFile() {

            //先にファイルの存在を確認し、もしなければ、一度XMLを作成した上でそれを読み込む。
            if (File.Exists(settingsFilePath) == false) {
                var newSettings = new Settings();
                var serializer = new XmlSerializer(typeof(Settings));
                using (var sw = new StreamWriter(settingsFilePath, false, Encoding.UTF8)) {
                    serializer.Serialize(sw, newSettings);
                    sw.Flush();
                }
            }

            XmlSerializer serializer2 = new XmlSerializer(typeof(Settings));
            var sr = new StreamReader(settingsFilePath, new UTF8Encoding(false));
            Settings settings = (Settings)serializer2.Deserialize(sr);
            sr.Close();
            
            return settings;
        }

        /// <summary>
        /// このSettingsインスタンスを、アプリの実行ファイルのディレクトリにXMLとして保存します。
        /// </summary>
        public void saveAsXml() {
            var xmlSerializer = new XmlSerializer(typeof(Settings));
            using (var sw = new StreamWriter(settingsFilePath, false, Encoding.UTF8)) {
                xmlSerializer.Serialize(sw, this);
                sw.Flush();
            }
        }
    }
}
