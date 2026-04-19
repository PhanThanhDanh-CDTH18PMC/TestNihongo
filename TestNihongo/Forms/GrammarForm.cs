using NihongoVocabTrainer.Models;
using NihongoVocabTrainer.Services;

namespace NihongoVocabTrainer.Forms
{
    public partial class GrammarForm : Form
    {
        private readonly GrammarService _grammarService = new GrammarService();

        private readonly string _dataDirectoryPath;

        private List<GrammarItem> _grammarItems = new List<GrammarItem>();

        private GrammarItem? _selectedGrammarItem;

        /// <summary>
        /// 学習データが変更されたかどうかを取得します。
        /// </summary>
        public bool IsChanged { get; private set; }

        /// <summary>
        /// 文法学習画面を初期化します。
        /// </summary>
        /// <param name="dataDirectoryPath">Dataフォルダパス</param>
        public GrammarForm(string dataDirectoryPath)
        {
            InitializeComponent();

            _dataDirectoryPath = dataDirectoryPath;
        }

        /// <summary>
        /// 画面読み込み時に文法データを表示します。
        /// </summary>
        private void GrammarForm_Load(object sender, EventArgs e)
        {
            #region 初期表示

            ApplyCuteDesign();

            cboGrammarLevel.Items.Clear();
            cboGrammarLevel.Items.Add("All");
            cboGrammarLevel.Items.Add("N5");
            cboGrammarLevel.Items.Add("N4");
            cboGrammarLevel.Items.Add("N3");
            cboGrammarLevel.Items.Add("N2");
            cboGrammarLevel.Items.Add("N1");
            cboGrammarLevel.SelectedIndex = 0;

            _grammarItems = _grammarService.LoadAllFromDataDirectory(_dataDirectoryPath);

            DisplayGrammarItems(_grammarItems);

            #endregion
        }

        /// <summary>
        /// 文法一覧を表示します。
        /// </summary>
        /// <param name="grammarItems">表示対象</param>
        private void DisplayGrammarItems(List<GrammarItem> grammarItems)
        {
            #region 一覧表示

            dgvGrammar.SuspendLayout();

            try
            {
                dgvGrammar.DataSource = null;
                dgvGrammar.AutoGenerateColumns = true;
                dgvGrammar.DataSource = grammarItems;

                ApplyDataGridViewColumnSettings();
            }
            finally
            {
                dgvGrammar.ResumeLayout();
            }

            #endregion
        }

        /// <summary>
        /// 一覧の列設定を行います。
        /// </summary>
        private void ApplyDataGridViewColumnSettings()
        {
            #region 列設定

            dgvGrammar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (dgvGrammar.Columns["Pattern"] != null)
            {
                dgvGrammar.Columns["Pattern"].HeaderText = "文法";
                dgvGrammar.Columns["Pattern"].Width = 150;
            }

            if (dgvGrammar.Columns["Meaning"] != null)
            {
                dgvGrammar.Columns["Meaning"].HeaderText = "意味";
                dgvGrammar.Columns["Meaning"].Width = 230;
            }

            if (dgvGrammar.Columns["Level"] != null)
            {
                dgvGrammar.Columns["Level"].HeaderText = "レベル";
                dgvGrammar.Columns["Level"].Width = 70;
            }

            if (dgvGrammar.Columns["Usage"] != null)
            {
                dgvGrammar.Columns["Usage"].Visible = false;
            }

            if (dgvGrammar.Columns["ExampleJapanese"] != null)
            {
                dgvGrammar.Columns["ExampleJapanese"].Visible = false;
            }

            if (dgvGrammar.Columns["ExampleVietnamese"] != null)
            {
                dgvGrammar.Columns["ExampleVietnamese"].Visible = false;
            }

            if (dgvGrammar.Columns["CorrectCount"] != null)
            {
                dgvGrammar.Columns["CorrectCount"].Visible = false;
            }

            if (dgvGrammar.Columns["WrongCount"] != null)
            {
                dgvGrammar.Columns["WrongCount"].Visible = false;
            }

            if (dgvGrammar.Columns["IsDifficult"] != null)
            {
                dgvGrammar.Columns["IsDifficult"].Visible = false;
            }

            #endregion
        }

        /// <summary>
        /// 選択された文法の詳細を表示します。
        /// </summary>
        private void dgvGrammar_SelectionChanged(object sender, EventArgs e)
        {
            #region 詳細表示

            if (dgvGrammar.CurrentRow?.DataBoundItem is not GrammarItem selectedGrammarItem)
            {
                return;
            }

            _selectedGrammarItem = selectedGrammarItem;

            lblPattern.Text = selectedGrammarItem.Pattern;
            lblMeaning.Text = selectedGrammarItem.Meaning;
            txtUsage.Text = selectedGrammarItem.Usage;
            txtExampleJapanese.Text = selectedGrammarItem.ExampleJapanese;
            txtExampleVietnamese.Text = selectedGrammarItem.ExampleVietnamese;

            #endregion
        }

        /// <summary>
        /// キーワードとレベルで文法を検索します。
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            #region 検索

            string keyword = txtSearch.Text.Trim();
            string level = cboGrammarLevel.SelectedItem?.ToString() ?? "All";

            var result = _grammarItems
                .Where(x =>
                    string.IsNullOrWhiteSpace(keyword) ||
                    x.Pattern.Contains(keyword) ||
                    x.Meaning.Contains(keyword) ||
                    x.Usage.Contains(keyword) ||
                    x.ExampleJapanese.Contains(keyword) ||
                    x.ExampleVietnamese.Contains(keyword))
                .Where(x =>
                    level == "All" ||
                    x.Level == level)
                .ToList();

            DisplayGrammarItems(result);

            #endregion
        }

        /// <summary>
        /// 検索条件を解除します。
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            #region リセット

            txtSearch.Text = string.Empty;
            cboGrammarLevel.SelectedIndex = 0;

            DisplayGrammarItems(_grammarItems);

            #endregion
        }

        /// <summary>
        /// 選択中の文法を正解として記録します。
        /// </summary>
        private void btnKnow_Click(object sender, EventArgs e)
        {
            #region 正解記録

            if (_selectedGrammarItem == null)
            {
                return;
            }

            _selectedGrammarItem.CorrectCount++;
            _selectedGrammarItem.IsDifficult = false;

            IsChanged = true;

            MessageBox.Show("正解として記録しました。");

            #endregion
        }

        /// <summary>
        /// 選択中の文法を苦手として記録します。
        /// </summary>
        private void btnDontKnow_Click(object sender, EventArgs e)
        {
            #region 不正解記録

            if (_selectedGrammarItem == null)
            {
                return;
            }

            _selectedGrammarItem.WrongCount++;
            _selectedGrammarItem.IsDifficult = true;

            IsChanged = true;

            MessageBox.Show("苦手文法として記録しました。");

            #endregion
        }

        /// <summary>
        /// 画面を閉じます。
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            #region 閉じる

            Close();

            #endregion
        }

        /// <summary>
        /// かわいいデザインを適用します。
        /// </summary>
        private void ApplyCuteDesign()
        {
            #region デザイン設定

            BackColor = Color.FromArgb(255, 250, 245);
            Text = "🌸 Grammar Study 🌸";

            lblPattern.Font = new Font("Yu Gothic UI", 22F, FontStyle.Bold);
            lblPattern.BackColor = Color.White;
            lblPattern.ForeColor = Color.FromArgb(31, 41, 55);

            lblMeaning.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblMeaning.BackColor = Color.White;
            lblMeaning.ForeColor = Color.FromArgb(31, 41, 55);

            txtUsage.BackColor = Color.White;
            txtExampleJapanese.BackColor = Color.White;
            txtExampleVietnamese.BackColor = Color.White;

            StyleButton(btnSearch, Color.FromArgb(186, 230, 253));
            StyleButton(btnReset, Color.FromArgb(226, 232, 240));
            StyleButton(btnKnow, Color.FromArgb(187, 247, 208));
            StyleButton(btnDontKnow, Color.FromArgb(254, 202, 202));
            StyleButton(btnClose, Color.FromArgb(226, 232, 240));

            StyleDataGridView();

            #endregion
        }

        /// <summary>
        /// ボタンのデザインを設定します。
        /// </summary>
        /// <param name="button">対象ボタン</param>
        /// <param name="backColor">背景色</param>
        private void StyleButton(Button button, Color backColor)
        {
            #region ボタンデザイン

            button.BackColor = backColor;
            button.ForeColor = Color.FromArgb(31, 41, 55);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;

            #endregion
        }

        /// <summary>
        /// 一覧デザインを設定します。
        /// </summary>
        private void StyleDataGridView()
        {
            #region 一覧デザイン

            dgvGrammar.BackgroundColor = Color.White;
            dgvGrammar.BorderStyle = BorderStyle.FixedSingle;
            dgvGrammar.GridColor = Color.FromArgb(229, 231, 235);

            dgvGrammar.EnableHeadersVisualStyles = false;
            dgvGrammar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 237, 213);
            dgvGrammar.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvGrammar.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold);

            dgvGrammar.DefaultCellStyle.BackColor = Color.White;
            dgvGrammar.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvGrammar.DefaultCellStyle.Font = new Font("Yu Gothic UI", 10F);
            dgvGrammar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 215, 170);
            dgvGrammar.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);

            dgvGrammar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 247, 237);

            dgvGrammar.RowHeadersVisible = false;
            dgvGrammar.AllowUserToAddRows = false;
            dgvGrammar.AllowUserToResizeRows = false;
            dgvGrammar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrammar.MultiSelect = false;
            dgvGrammar.ReadOnly = true;

            dgvGrammar.RowTemplate.Height = 32;
            dgvGrammar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            #endregion
        }

        /// <summary>
        /// 文法データを保存します。
        /// </summary>
        public void SaveGrammar()
        {
            #region 保存

            _grammarService.SaveAllByLevel(_dataDirectoryPath, _grammarItems);

            #endregion
        }
    }
}
