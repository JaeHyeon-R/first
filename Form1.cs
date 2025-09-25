using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        private Panel loginPanel;
        private Panel mainPanel;
        private Label labelUsername;
        private Label labelPassword;
        private TextBox textBoxUsername;
        private TextBox textBoxPassword;
        private Button buttonLogin;
        private PictureBox pictureBoxViewer;
        private Button buttonOpenFile;
        private Button buttonSaveText;
        private Button buttonAddTodo;
        private Button buttonToggleTheme;
        private Button buttonLogout;
        private TextBox textBoxEditor;
        private ListBox listBoxTodos;
        private Label labelImageViewer;
        private Label labelTextEditor;
        private Label labelTodoList;
        private Label labelClockTimer;
        private Label labelClock;
        private Label labelTimer;
        private Button buttonStartTimer;
        private Button buttonResetTimer;
        private Label labelCalculator;
        private TextBox textBoxCalculator;
        private Button buttonAdd;
        private Button buttonSubtract;
        private Button buttonMultiply;
        private Button buttonDivide;
        private Button buttonCalculate;
        private TextBox textBoxSearch;
        private Button buttonSearch;
        private bool isDarkTheme = false;
        private List<string> todoList = new List<string>();
        private Timer clockTimer;
        private Timer workTimer;
        private int timerSeconds = 0;
        private bool isTimerRunning = false;
        private readonly string searchPlaceholder = "검색어 입력";
        private readonly string calcPlaceholder = "숫자1 연산자 숫자2 (예: 5 + 3)";

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            loginPanel = new Panel();
            mainPanel = new Panel();
            labelUsername = new Label();
            labelPassword = new Label();
            textBoxUsername = new TextBox();
            textBoxPassword = new TextBox();
            buttonLogin = new Button();
            pictureBoxViewer = new PictureBox();
            buttonOpenFile = new Button();
            buttonSaveText = new Button();
            buttonAddTodo = new Button();
            buttonToggleTheme = new Button();
            buttonLogout = new Button();
            textBoxEditor = new TextBox();
            listBoxTodos = new ListBox();
            labelImageViewer = new Label();
            labelTextEditor = new Label();
            labelTodoList = new Label();
            labelClockTimer = new Label();
            labelClock = new Label();
            labelTimer = new Label();
            buttonStartTimer = new Button();
            buttonResetTimer = new Button();
            labelCalculator = new Label();
            textBoxCalculator = new TextBox();
            buttonAdd = new Button();
            buttonSubtract = new Button();
            buttonMultiply = new Button();
            buttonDivide = new Button();
            buttonCalculate = new Button();
            textBoxSearch = new TextBox();
            buttonSearch = new Button();
            SuspendLayout();

            // 로그인 패널 설정
            loginPanel.Location = new Point(0, 0);
            loginPanel.Size = new Size(300, 150);
            loginPanel.Visible = true;

            labelUsername.Text = "사용자 이름:";
            labelUsername.Location = new Point(30, 30);
            labelUsername.Size = new Size(100, 20);

            labelPassword.Text = "비밀번호:";
            labelPassword.Location = new Point(30, 60);
            labelPassword.Size = new Size(100, 20);

            textBoxUsername.Location = new Point(130, 30);
            textBoxUsername.Size = new Size(150, 20);

            textBoxPassword.Location = new Point(130, 60);
            textBoxPassword.Size = new Size(150, 20);
            textBoxPassword.UseSystemPasswordChar = true;

            buttonLogin.Text = "로그인";
            buttonLogin.Location = new Point(130, 100);
            buttonLogin.Size = new Size(75, 30);
            buttonLogin.Click += buttonLogin_Click;

            loginPanel.Controls.AddRange(new Control[] { labelUsername, labelPassword, textBoxUsername, textBoxPassword, buttonLogin });

            // 메인 패널 설정
            mainPanel.Location = new Point(0, 0);
            mainPanel.Size = new Size(800, 550);
            mainPanel.Visible = false;

            // 이미지 뷰어 섹션
            labelImageViewer.Text = "이미지 뷰어";
            labelImageViewer.Location = new Point(10, 10);
            labelImageViewer.Size = new Size(380, 20);
            labelImageViewer.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

            pictureBoxViewer.Location = new Point(10, 35);
            pictureBoxViewer.Size = new Size(380, 250);
            pictureBoxViewer.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxViewer.SizeMode = PictureBoxSizeMode.Zoom;

            // 텍스트 편집기 섹션
            labelTextEditor.Text = "텍스트 편집기";
            labelTextEditor.Location = new Point(400, 10);
            labelTextEditor.Size = new Size(380, 20);
            labelTextEditor.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

            textBoxEditor.Location = new Point(400, 35);
            textBoxEditor.Size = new Size(380, 250);
            textBoxEditor.Multiline = true;
            textBoxEditor.ScrollBars = ScrollBars.Vertical;

            textBoxSearch.Location = new Point(400, 290);
            textBoxSearch.Size = new Size(280, 20);
            textBoxSearch.Text = searchPlaceholder;
            textBoxSearch.ForeColor = Color.Gray;
            textBoxSearch.GotFocus += (s, e) =>
            {
                if (textBoxSearch.Text == searchPlaceholder)
                {
                    textBoxSearch.Text = "";
                    textBoxSearch.ForeColor = isDarkTheme ? Color.White : Color.Black;
                }
            };
            textBoxSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
                {
                    textBoxSearch.Text = searchPlaceholder;
                    textBoxSearch.ForeColor = Color.Gray;
                }
            };

            buttonSearch.Text = "네이버 검색";
            buttonSearch.Location = new Point(690, 290);
            buttonSearch.Size = new Size(90, 25);
            buttonSearch.Click += buttonSearch_Click;

            // 할 일 목록 섹션
            labelTodoList.Text = "할 일 목록";
            labelTodoList.Location = new Point(10, 295);
            labelTodoList.Size = new Size(380, 20);
            labelTodoList.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

            listBoxTodos.Location = new Point(10, 320);
            listBoxTodos.Size = new Size(380, 100);
            listBoxTodos.SelectedIndexChanged += listBoxTodos_SelectedIndexChanged;

            // 시계 및 타이머 섹션
            labelClockTimer.Text = "시계 및 타이머";
            labelClockTimer.Location = new Point(400, 320);
            labelClockTimer.Size = new Size(380, 20);
            labelClockTimer.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

            labelClock.Text = DateTime.Now.ToString("HH:mm:ss");
            labelClock.Location = new Point(400, 350);
            labelClock.Size = new Size(100, 20);

            labelTimer.Text = "00:00:00";
            labelTimer.Location = new Point(510, 350);
            labelTimer.Size = new Size(100, 20);

            buttonStartTimer.Text = "타이머 시작";
            buttonStartTimer.Location = new Point(620, 345);
            buttonStartTimer.Size = new Size(80, 25);
            buttonStartTimer.Click += buttonStartTimer_Click;

            buttonResetTimer.Text = "리셋";
            buttonResetTimer.Location = new Point(700, 345);
            buttonResetTimer.Size = new Size(80, 25);
            buttonResetTimer.Click += buttonResetTimer_Click;

            // 계산기 섹션
            labelCalculator.Text = "계산기";
            labelCalculator.Location = new Point(400, 380);
            labelCalculator.Size = new Size(380, 20);
            labelCalculator.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

            textBoxCalculator.Location = new Point(400, 410);
            textBoxCalculator.Size = new Size(160, 20);
            textBoxCalculator.Text = calcPlaceholder;
            textBoxCalculator.ForeColor = Color.Gray;
            textBoxCalculator.GotFocus += (s, e) =>
            {
                if (textBoxCalculator.Text == calcPlaceholder)
                {
                    textBoxCalculator.Text = "";
                    textBoxCalculator.ForeColor = isDarkTheme ? Color.White : Color.Black;
                }
            };
            textBoxCalculator.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBoxCalculator.Text))
                {
                    textBoxCalculator.Text = calcPlaceholder;
                    textBoxCalculator.ForeColor = Color.Gray;
                }
            };
            textBoxCalculator.KeyPress += TextBoxCalculator_KeyPress;

            buttonAdd.Text = "+";
            buttonAdd.Location = new Point(570, 405);
            buttonAdd.Size = new Size(40, 30);
            buttonAdd.Click += (s, e) =>
            {
                if (textBoxCalculator.Text == calcPlaceholder) textBoxCalculator.Text = "";
                textBoxCalculator.Text += " + ";
                textBoxCalculator.ForeColor = isDarkTheme ? Color.White : Color.Black;
            };

            buttonSubtract.Text = "-";
            buttonSubtract.Location = new Point(620, 405);
            buttonSubtract.Size = new Size(40, 30);
            buttonSubtract.Click += (s, e) =>
            {
                if (textBoxCalculator.Text == calcPlaceholder) textBoxCalculator.Text = "";
                textBoxCalculator.Text += " - ";
                textBoxCalculator.ForeColor = isDarkTheme ? Color.White : Color.Black;
            };

            buttonMultiply.Text = "*";
            buttonMultiply.Location = new Point(670, 405);
            buttonMultiply.Size = new Size(40, 30);
            buttonMultiply.Click += (s, e) =>
            {
                if (textBoxCalculator.Text == calcPlaceholder) textBoxCalculator.Text = "";
                textBoxCalculator.Text += " * ";
                textBoxCalculator.ForeColor = isDarkTheme ? Color.White : Color.Black;
            };

            buttonDivide.Text = "/";
            buttonDivide.Location = new Point(720, 405);
            buttonDivide.Size = new Size(40, 30);
            buttonDivide.Click += (s, e) =>
            {
                if (textBoxCalculator.Text == calcPlaceholder) textBoxCalculator.Text = "";
                textBoxCalculator.Text += " / ";
                textBoxCalculator.ForeColor = isDarkTheme ? Color.White : Color.Black;
            };

            buttonCalculate.Text = "=";
            buttonCalculate.Location = new Point(760, 405);
            buttonCalculate.Size = new Size(40, 30);
            buttonCalculate.Click += buttonCalculate_Click;

            // 버튼들 (하단에 일렬로 배치)
            buttonOpenFile.Text = "이미지 열기";
            buttonOpenFile.Location = new Point(10, 525);
            buttonOpenFile.Size = new Size(100, 30);
            buttonOpenFile.Click += buttonOpenFile_Click;

            buttonSaveText.Text = "텍스트 저장";
            buttonSaveText.Location = new Point(120, 525);
            buttonSaveText.Size = new Size(100, 30);
            buttonSaveText.Click += buttonSaveText_Click;

            buttonAddTodo.Text = "할 일 추가";
            buttonAddTodo.Location = new Point(230, 525);
            buttonAddTodo.Size = new Size(100, 30);
            buttonAddTodo.Click += buttonAddTodo_Click;

            buttonToggleTheme.Text = "다크 모드";
            buttonToggleTheme.Location = new Point(570, 525);
            buttonToggleTheme.Size = new Size(100, 30);
            buttonToggleTheme.Click += buttonToggleTheme_Click;

            buttonLogout.Text = "로그아웃";
            buttonLogout.Location = new Point(680, 525);
            buttonLogout.Size = new Size(100, 30);
            buttonLogout.Click += buttonLogout_Click;

            mainPanel.Controls.AddRange(new Control[] {
                pictureBoxViewer, textBoxEditor, listBoxTodos,
                buttonOpenFile, buttonSaveText, buttonAddTodo, buttonToggleTheme, buttonLogout,
                labelImageViewer, labelTextEditor, labelTodoList,
                labelClockTimer, labelClock, labelTimer, buttonStartTimer, buttonResetTimer,
                labelCalculator, textBoxCalculator, buttonAdd, buttonSubtract, buttonMultiply, buttonDivide, buttonCalculate,
                textBoxSearch, buttonSearch
            });

            // 폼 설정
            ClientSize = new Size(800, 560);
            Controls.AddRange(new Control[] { loginPanel, mainPanel });
            Text = "로그인";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // 타이머 설정
            clockTimer = new Timer { Interval = 1000 };
            clockTimer.Tick += (s, e) => labelClock.Text = DateTime.Now.ToString("HH:mm:ss");
            clockTimer.Start();

            workTimer = new Timer { Interval = 1000 };
            workTimer.Tick += (s, e) =>
            {
                timerSeconds++;
                TimeSpan time = TimeSpan.FromSeconds(timerSeconds);
                labelTimer.Text = time.ToString(@"hh\:mm\:ss");
            };

            ResumeLayout(false);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "admin" && textBoxPassword.Text == "admin")
            {
                loginPanel.Visible = false;
                mainPanel.Visible = true;
                Text = "메인 화면";
            }
            else
            {
                MessageBox.Show("사용자 이름 또는 비밀번호가 잘못되었습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "이미지 파일 (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|모든 파일 (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBoxViewer.Image?.Dispose();
                        pictureBoxViewer.Image = new Bitmap(openFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("이미지 로드 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonSaveText_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllText(saveFileDialog.FileName, textBoxEditor.Text);
                        MessageBox.Show("텍스트가 저장되었습니다.", "저장 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("텍스트 저장 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonAddTodo_Click(object sender, EventArgs e)
        {
            using (InputForm inputForm = new InputForm("할 일 추가", "추가할 할 일을 입력하세요:"))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string todo = inputForm.InputText;
                    if (!string.IsNullOrWhiteSpace(todo))
                    {
                        todoList.Add(todo);
                        listBoxTodos.Items.Add(todo);
                    }
                }
            }
        }

        private void listBoxTodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTodos.SelectedIndex >= 0)
            {
                DialogResult result = MessageBox.Show("선택한 할 일을 삭제하시겠습니까?", "할 일 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    todoList.RemoveAt(listBoxTodos.SelectedIndex);
                    listBoxTodos.Items.RemoveAt(listBoxTodos.SelectedIndex);
                }
            }
        }

        private void buttonToggleTheme_Click(object sender, EventArgs e)
        {
            isDarkTheme = !isDarkTheme;
            if (isDarkTheme)
            {
                mainPanel.BackColor = Color.FromArgb(30, 30, 30);
                textBoxEditor.BackColor = Color.FromArgb(50, 50, 50);
                textBoxEditor.ForeColor = Color.White;
                listBoxTodos.BackColor = Color.FromArgb(50, 50, 50);
                listBoxTodos.ForeColor = Color.White;
                textBoxSearch.BackColor = Color.FromArgb(50, 50, 50);
                textBoxSearch.ForeColor = string.IsNullOrWhiteSpace(textBoxSearch.Text) || textBoxSearch.Text == searchPlaceholder ? Color.Gray : Color.White;
                textBoxCalculator.BackColor = Color.FromArgb(50, 50, 50);
                textBoxCalculator.ForeColor = string.IsNullOrWhiteSpace(textBoxCalculator.Text) || textBoxCalculator.Text == calcPlaceholder ? Color.Gray : Color.White;
                labelImageViewer.ForeColor = Color.White;
                labelTextEditor.ForeColor = Color.White;
                labelTodoList.ForeColor = Color.White;
                labelClockTimer.ForeColor = Color.White;
                labelClock.ForeColor = Color.White;
                labelTimer.ForeColor = Color.White;
                labelCalculator.ForeColor = Color.White;
                buttonOpenFile.BackColor = Color.FromArgb(50, 50, 50);
                buttonOpenFile.ForeColor = Color.White;
                buttonSaveText.BackColor = Color.FromArgb(50, 50, 50);
                buttonSaveText.ForeColor = Color.White;
                buttonAddTodo.BackColor = Color.FromArgb(50, 50, 50);
                buttonAddTodo.ForeColor = Color.White;
                buttonToggleTheme.BackColor = Color.FromArgb(50, 50, 50);
                buttonToggleTheme.ForeColor = Color.White;
                buttonLogout.BackColor = Color.FromArgb(50, 50, 50);
                buttonLogout.ForeColor = Color.White;
                buttonSearch.BackColor = Color.FromArgb(50, 50, 50);
                buttonSearch.ForeColor = Color.White;
                buttonStartTimer.BackColor = Color.FromArgb(50, 50, 50);
                buttonStartTimer.ForeColor = Color.White;
                buttonResetTimer.BackColor = Color.FromArgb(50, 50, 50);
                buttonResetTimer.ForeColor = Color.White;
                buttonAdd.BackColor = Color.FromArgb(50, 50, 50);
                buttonAdd.ForeColor = Color.White;
                buttonSubtract.BackColor = Color.FromArgb(50, 50, 50);
                buttonSubtract.ForeColor = Color.White;
                buttonMultiply.BackColor = Color.FromArgb(50, 50, 50);
                buttonMultiply.ForeColor = Color.White;
                buttonDivide.BackColor = Color.FromArgb(50, 50, 50);
                buttonDivide.ForeColor = Color.White;
                buttonCalculate.BackColor = Color.FromArgb(50, 50, 50);
                buttonCalculate.ForeColor = Color.White;
                buttonToggleTheme.Text = "라이트 모드";
            }
            else
            {
                mainPanel.BackColor = SystemColors.Control;
                textBoxEditor.BackColor = Color.White;
                textBoxEditor.ForeColor = Color.Black;
                listBoxTodos.BackColor = Color.White;
                listBoxTodos.ForeColor = Color.Black;
                textBoxSearch.BackColor = Color.White;
                textBoxSearch.ForeColor = string.IsNullOrWhiteSpace(textBoxSearch.Text) || textBoxSearch.Text == searchPlaceholder ? Color.Gray : Color.Black;
                textBoxCalculator.BackColor = Color.White;
                textBoxCalculator.ForeColor = string.IsNullOrWhiteSpace(textBoxCalculator.Text) || textBoxCalculator.Text == calcPlaceholder ? Color.Gray : Color.Black;
                labelImageViewer.ForeColor = Color.Black;
                labelTextEditor.ForeColor = Color.Black;
                labelTodoList.ForeColor = Color.Black;
                labelClockTimer.ForeColor = Color.Black;
                labelClock.ForeColor = Color.Black;
                labelTimer.ForeColor = Color.Black;
                labelCalculator.ForeColor = Color.Black;
                buttonOpenFile.BackColor = SystemColors.Control;
                buttonOpenFile.ForeColor = Color.Black;
                buttonSaveText.BackColor = SystemColors.Control;
                buttonSaveText.ForeColor = Color.Black;
                buttonAddTodo.BackColor = SystemColors.Control;
                buttonAddTodo.ForeColor = Color.Black;
                buttonToggleTheme.BackColor = SystemColors.Control;
                buttonToggleTheme.ForeColor = Color.Black;
                buttonLogout.BackColor = SystemColors.Control;
                buttonLogout.ForeColor = Color.Black;
                buttonSearch.BackColor = SystemColors.Control;
                buttonSearch.ForeColor = Color.Black;
                buttonStartTimer.BackColor = SystemColors.Control;
                buttonStartTimer.ForeColor = Color.Black;
                buttonResetTimer.BackColor = SystemColors.Control;
                buttonResetTimer.ForeColor = Color.Black;
                buttonAdd.BackColor = SystemColors.Control;
                buttonAdd.ForeColor = Color.Black;
                buttonSubtract.BackColor = SystemColors.Control;
                buttonSubtract.ForeColor = Color.Black;
                buttonMultiply.BackColor = SystemColors.Control;
                buttonMultiply.ForeColor = Color.Black;
                buttonDivide.BackColor = SystemColors.Control;
                buttonDivide.ForeColor = Color.Black;
                buttonCalculate.BackColor = SystemColors.Control;
                buttonCalculate.ForeColor = Color.Black;
                buttonToggleTheme.Text = "다크 모드";
            }
        }

        private void TextBoxCalculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxCalculator.Text == calcPlaceholder)
            {
                textBoxCalculator.Text = "";
                textBoxCalculator.ForeColor = isDarkTheme ? Color.White : Color.Black;
            }

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != '*' && e.KeyChar != '/' && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonCalculate_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (textBoxCalculator.Text == calcPlaceholder || string.IsNullOrWhiteSpace(textBoxCalculator.Text))
            {
                MessageBox.Show("계산식을 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string input = textBoxCalculator.Text.Trim();
                string[] parts = input.Split(new char[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    MessageBox.Show("형식: 숫자 연산자 숫자 (예: 5 + 3 또는 5+3)", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                char op = input[parts[0].Length];
                if (!double.TryParse(parts[0], out double num1) || !double.TryParse(parts[1], out double num2))
                {
                    MessageBox.Show("유효한 숫자를 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double result = 0;
                switch (op)
                {
                    case '+': result = num1 + num2; break;
                    case '-': result = num1 - num2; break;
                    case '*': result = num1 * num2; break;
                    case '/':
                        if (num2 == 0)
                        {
                            MessageBox.Show("0으로 나눌 수 없습니다.", "계산 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        result = num1 / num2; break;
                    default:
                        MessageBox.Show("유효한 연산자(+,-,*,/)를 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                textBoxCalculator.Text = result.ToString();
                textBoxCalculator.ForeColor = isDarkTheme ? Color.White : Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("계산 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == searchPlaceholder || string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                MessageBox.Show("검색어를 입력하세요.", "검색 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = Uri.EscapeDataString(textBoxSearch.Text.Trim());
                string url = $"https://search.naver.com/search.naver?query={query}";
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
                MessageBox.Show($"네이버에서 '{textBoxSearch.Text}' 검색을 시작합니다.", "검색 시작", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonStartTimer_Click(object sender, EventArgs e)
        {
            if (isTimerRunning)
            {
                workTimer.Stop();
                buttonStartTimer.Text = "타이머 시작";
                isTimerRunning = false;
            }
            else
            {
                workTimer.Start();
                buttonStartTimer.Text = "타이머 정지";
                isTimerRunning = true;
            }
        }

        private void buttonResetTimer_Click(object sender, EventArgs e)
        {
            workTimer.Stop();
            timerSeconds = 0;
            labelTimer.Text = "00:00:00";
            buttonStartTimer.Text = "타이머 시작";
            isTimerRunning = false;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            mainPanel.Visible = false;
            loginPanel.Visible = true;
            Text = "로그인";
            textBoxUsername.Text = "";
            textBoxPassword.Text = "";
            pictureBoxViewer.Image?.Dispose();
            pictureBoxViewer.Image = null;
            textBoxEditor.Text = "";
            textBoxSearch.Text = searchPlaceholder;
            textBoxSearch.ForeColor = Color.Gray;
            textBoxCalculator.Text = calcPlaceholder;
            textBoxCalculator.ForeColor = Color.Gray;
            listBoxTodos.Items.Clear();
            todoList.Clear();
            workTimer.Stop();
            timerSeconds = 0;
            labelTimer.Text = "00:00:00";
            buttonStartTimer.Text = "타이머 시작";
            isTimerRunning = false;
            if (isDarkTheme)
            {
                buttonToggleTheme_Click(null, null);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pictureBoxViewer.Image?.Dispose();
                clockTimer?.Dispose();
                workTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class InputForm : Form
    {
        private TextBox textBoxInput;
        private Button buttonOK;
        private Button buttonCancel;
        private Label labelPrompt;
        public string InputText { get; private set; }

        public InputForm(string title, string prompt)
        {
            InitializeComponents(title, prompt);
        }

        private void InitializeComponents(string title, string prompt)
        {
            Text = title;
            Size = new Size(300, 150);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            labelPrompt = new Label
            {
                Text = prompt,
                Location = new Point(20, 20),
                Size = new Size(240, 20)
            };

            textBoxInput = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(240, 20)
            };

            buttonOK = new Button
            {
                Text = "확인",
                Location = new Point(100, 80),
                Size = new Size(75, 30),
                DialogResult = DialogResult.OK
            };
            buttonOK.Click += (s, e) => { InputText = textBoxInput.Text; };

            buttonCancel = new Button
            {
                Text = "취소",
                Location = new Point(185, 80),
                Size = new Size(75, 30),
                DialogResult = DialogResult.Cancel
            };

            Controls.AddRange(new Control[] { labelPrompt, textBoxInput, buttonOK, buttonCancel });
            AcceptButton = buttonOK;
            CancelButton = buttonCancel;
        }
    }
}