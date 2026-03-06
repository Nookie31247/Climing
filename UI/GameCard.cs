using System.Diagnostics;
using CppCode;

namespace UI
{
    /// <summary>
    /// 게임 카드를 표시하는 UI 클래스입니다.
    /// </summary>
    public partial class GameCard : UserControl
    {
        private PictureBox pictureBox;
        private Label nameLabel;
        private string dirPath = "";

        public GameCard()
        {
            // 게임 카드의 크기 설정
            this.Width = 320;
            this.Height = 480;
            this.Margin = new Padding(15);

            // 게임 이미지의 크기 설정
            pictureBox = new PictureBox();
            pictureBox.Width = 300;
            pictureBox.Height = 400;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Location = new Point((this.Width - pictureBox.Width) / 2, 5);

            // 게임 이름의 스타일 설정
            nameLabel = new Label();
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            nameLabel.Font = new Font("Arial", 15, FontStyle.Bold);
            nameLabel.ForeColor = Color.White;
            nameLabel.Width = this.Width;
            nameLabel.Height = 40;
            nameLabel.Location = new Point(0, pictureBox.Bottom + 12);
            nameLabel.AutoEllipsis = true;

            this.Controls.Add(pictureBox);
            this.Controls.Add(nameLabel);

            pictureBox.Click += GameCard_Click;
            nameLabel.Click += GameCard_Click;
        }

        /// <summary>
        /// 게임 카드를 설정합니다.
        /// 드라이브가 정상적으로 마운트되지 않았을 시 false를 반환합니다.
        /// </summary>
        /// <param name="game">설정할 Game 객체</param>
        /// <returns>게임 카드 설정 성공 여부</returns>
        public bool SetGame(Game game)
        {
            // 게임 실행 경로를 저장
            // 드라이브 마운트가 정상적으로 이루어지지 않았을 때 게임을 저장하지 않음.
            string driveLetter = ISCSI.getDriveLetter();
            if (driveLetter == null)
            {
                return false;
            }
            dirPath = driveLetter[0] + game.dirPath;

            nameLabel.Text = game.name;         // 게임 이름을 표시
            pictureBox.Load(game.imageUrl);     // 게임 이미지를 서버에서 받아온 후 표시

            return true;
        }

        /// <summary>
        /// 게임 카드 클릭 시 게임 실행
        /// </summary>
        private void GameCard_Click(object sender, EventArgs e)
        {
            try
            {
                // 게임 실행 파일 존재 여부 확인
                if (File.Exists(dirPath))
                {
                    // 게임 파일이 exe 형식일 때
                    if (dirPath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                    {
                        Process.Start(dirPath);
                    }

                    // 게임 파일이 url 형식일 때
                    else if (dirPath.EndsWith(".url", StringComparison.OrdinalIgnoreCase))
                    {
                        string url = ExtractUrlFromShortcut(dirPath);
                        if (!string.IsNullOrEmpty(url))
                        {
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = url,
                                UseShellExecute = true // 브라우저로 열기 위해 필요
                            });
                        }
                        else
                        {
                            MessageBox.Show($".url 파일에서 주소를 읽을 수 없습니다:\n{dirPath}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"게임 실행 파일을 찾을 수 없습니다:\n{dirPath}", "실행 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"게임 실행 중 오류 발생:\n{ex.Message}", "실행 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {

        }

        /// <summary>
        ///  url 바로가기로부터 url 주소를 추출합니다.
        /// </summary>
        /// <param name="filePath">추출할 바로가기의 경로</param>
        /// <returns>url 주소</returns>
        private string ExtractUrlFromShortcut(string filePath)
        {
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (line.StartsWith("URL=", StringComparison.OrdinalIgnoreCase))
                {
                    return line.Substring(4).Trim();
                }
            }
            return null;
        }
    }
}
