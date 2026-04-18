using NihongoVocabTrainer.Models;
using System.Text;

namespace NihongoVocabTrainer.Services
{
    /// <summary>
    /// 模擬試験データの読み込み、保存、PDF管理処理を行います。
    /// </summary>
    public class ExamQuestionService
    {
        /// <summary>
        /// PDFファイルをテストデータフォルダへコピーします。
        /// </summary>
        /// <param name="testDataDirectoryPath">TestDataフォルダパス</param>
        /// <param name="sourcePdfPath">コピー元PDFパス</param>
        /// <returns>コピー先PDFパス</returns>
        public string ImportPdf(string testDataDirectoryPath, string sourcePdfPath)
        {
            #region PDF取込

            string pdfDirectoryPath = Path.Combine(testDataDirectoryPath, "Pdf");

            if (!Directory.Exists(pdfDirectoryPath))
            {
                Directory.CreateDirectory(pdfDirectoryPath);
            }

            string fileName = Path.GetFileName(sourcePdfPath);
            string destinationPath = Path.Combine(pdfDirectoryPath, fileName);

            if (File.Exists(destinationPath))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                destinationPath = Path.Combine(pdfDirectoryPath, $"{fileNameWithoutExtension}_{timestamp}{extension}");
            }

            File.Copy(sourcePdfPath, destinationPath);

            return destinationPath;

            #endregion
        }

        /// <summary>
        /// 問題CSVファイルをテストデータフォルダへコピーします。
        /// </summary>
        /// <param name="testDataDirectoryPath">TestDataフォルダパス</param>
        /// <param name="sourceCsvPath">コピー元CSVパス</param>
        /// <returns>コピー先CSVパス</returns>
        public string ImportQuestionCsv(string testDataDirectoryPath, string sourceCsvPath)
        {
            #region 問題CSV取込

            string csvDirectoryPath = Path.Combine(testDataDirectoryPath, "Csv");

            if (!Directory.Exists(csvDirectoryPath))
            {
                Directory.CreateDirectory(csvDirectoryPath);
            }

            string fileName = Path.GetFileName(sourceCsvPath);
            string destinationPath = Path.Combine(csvDirectoryPath, fileName);

            if (File.Exists(destinationPath))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                destinationPath = Path.Combine(csvDirectoryPath, $"{fileNameWithoutExtension}_{timestamp}{extension}");
            }

            File.Copy(sourceCsvPath, destinationPath);

            return destinationPath;

            #endregion
        }

        /// <summary>
        /// TestData/Csv 配下の問題CSVをすべて読み込みます。
        /// </summary>
        /// <param name="testDataDirectoryPath">TestDataフォルダパス</param>
        /// <returns>問題一覧</returns>
        public List<ExamQuestion> LoadAllQuestions(string testDataDirectoryPath)
        {
            #region 全問題CSV読込

            var questions = new List<ExamQuestion>();

            string csvDirectoryPath = Path.Combine(testDataDirectoryPath, "Csv");

            if (!Directory.Exists(csvDirectoryPath))
            {
                Directory.CreateDirectory(csvDirectoryPath);
                return questions;
            }

            string[] csvFiles = Directory.GetFiles(csvDirectoryPath, "*.csv");

            foreach (string csvFile in csvFiles)
            {
                questions.AddRange(LoadQuestionsFromCsv(csvFile));
            }

            return questions;

            #endregion
        }

        /// <summary>
        /// 問題CSVファイルから問題一覧を読み込みます。
        /// </summary>
        /// <param name="filePath">CSVファイルパス</param>
        /// <returns>問題一覧</returns>
        public List<ExamQuestion> LoadQuestionsFromCsv(string filePath)
        {
            #region 問題CSV読込

            var questions = new List<ExamQuestion>();

            if (!File.Exists(filePath))
            {
                return questions;
            }

            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = SplitCsvLine(line);

                if (parts.Length < 10)
                {
                    continue;
                }

                questions.Add(new ExamQuestion
                {
                    TestId = parts[0],
                    Level = parts[1],
                    QuestionNo = int.TryParse(parts[2], out int questionNo) ? questionNo : 0,
                    Question = parts[3],
                    OptionA = parts[4],
                    OptionB = parts[5],
                    OptionC = parts[6],
                    OptionD = parts[7],
                    CorrectOption = parts[8].Trim().ToUpper(),
                    Explanation = parts[9]
                });
            }

            return questions;

            #endregion
        }

        /// <summary>
        /// サンプル問題CSVを作成します。
        /// </summary>
        /// <param name="testDataDirectoryPath">TestDataフォルダパス</param>
        public void CreateSampleQuestionCsv(string testDataDirectoryPath)
        {
            #region サンプルCSV作成

            string csvDirectoryPath = Path.Combine(testDataDirectoryPath, "Csv");

            if (!Directory.Exists(csvDirectoryPath))
            {
                Directory.CreateDirectory(csvDirectoryPath);
            }

            string filePath = Path.Combine(csvDirectoryPath, "n2_sample_test.csv");

            if (File.Exists(filePath))
            {
                return;
            }

            var lines = new List<string>
            {
                "TestId,Level,QuestionNo,Question,OptionA,OptionB,OptionC,OptionD,CorrectOption,Explanation",
                "n2_sample_test,N2,1,家族は本当にありがたい存在だ。『ありがたい』の意味は？,めずらしい,感謝したい気持ちだ,つまらない,危ない,B,ありがたい＝感謝したい気持ち・貴重であること。",
                "n2_sample_test,N2,2,問題に向き合う。『向き合う』の意味は？,逃げる,対面する・真剣に対応する,忘れる,遊ぶ,B,向き合う＝相手や問題に正面から対応する。",
                "n2_sample_test,N2,3,子どもを養う。『養う』の意味は？,育てる・生活を支える,捨てる,借りる,壊す,A,養う＝育てる・生活を支える。",
                "n2_sample_test,N2,4,家族の絆が強い。『絆』の意味は？,建物,つながり,料理,時間,B,絆＝人と人との強いつながり。"
            };

            File.WriteAllLines(filePath, lines, Encoding.UTF8);

            #endregion
        }

        /// <summary>
        /// CSVファイルの1行をカンマ区切りで分割します。
        /// ダブルクォーテーション内のカンマは分割対象外にします。
        /// </summary>
        /// <param name="line">CSVの1行</param>
        /// <returns>分割後の値</returns>
        private string[] SplitCsvLine(string line)
        {
            #region CSV行分割

            var result = new List<string>();
            var current = new StringBuilder();

            bool isInQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char character = line[i];

                if (character == '"')
                {
                    if (isInQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        isInQuotes = !isInQuotes;
                    }
                }
                else if (character == ',' && !isInQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(character);
                }
            }

            result.Add(current.ToString());

            return result.ToArray();

            #endregion
        }
    }
}
