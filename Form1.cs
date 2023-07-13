using designMode.observer;
using NumberGames;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace designMode
{
    public partial class Form1 : Form
    {

        const int font_size = 200;
        int transitionTime = 2000;
        int[] arrlevelDelay = new int[] { 10000, 5000, 2000, 500, 60 };



        int currentLevel =3;
        int successCount = 0;



        public DrawHelper draw;
        Form form2;
        PictureBox pictureBox;
        Image CImage;
        Point ClickPost;
        NumberInfo ClickNumberInfo;
        public static bool IsCanPlay = false;
        int numrange = 10;
        List<Point> Retangels;
        FMS fMS;
        Stopwatch sw = Stopwatch.StartNew();
        System.Timers.Timer timer;
   
        string mp3path = System.IO.Directory.GetCurrentDirectory() + "\\click.mp3";

        /// <summary>
        /// 生成画图信息  文字坐标
        /// </summary>
        void InitNumberInfo()
        {
            int[] wordarr = new int[numrange];
            for (int i = 0; i < numrange; i++)
            {
                wordarr[i] = i;
            }

            Random rand = new Random();
            for (int i = 0; i < numrange; i++)
            {
                int j = rand.Next(0, numrange);
                int temp = wordarr[i];
                wordarr[i] = wordarr[j];
                wordarr[j] = temp;
            }
            HashSet<Point> hash = new HashSet<Point>();
            List<Point> listpont = new List<Point>();
            bool flag = true;
            while (flag)
            {

                int Index = RandomNumberGenerator.GetInt32(0, Retangels.Count);
                hash.Add(Retangels[Index]);
                if (hash.Count == numrange)
                {
                    listpont = hash.ToList();
                    flag = false;
                }
            }

            draw.NumberInfos.Clear();
            for (int idx = 0; idx < numrange; idx++)
            {
                draw.NumberInfos.Add(new NumberInfo()
                {
                    BlockPoint = listpont[idx],
                    Order = wordarr[idx],
                    Word = wordarr[idx].ToString(),
                });

            }

        }

        void SplitRetangle()
        {

            int area = font_size / 2;
            int width = form2.Width;
            int height = form2.Height;
            int squareSide = font_size;
            int rows = (int)MathF.Floor(height / squareSide);
            int cols = (int)MathF.Floor(width / squareSide);
            Retangels = new List<Point>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int x = j * squareSide;
                    int y = i * squareSide;
                    //Trace.WriteLine($"x: {x}, y: {y}");
                    if (x != 0 && y != 0)
                        Retangels.Add(new Point(x, y));
                }
            }



        }



        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;         
            this.StartPosition = FormStartPosition.CenterScreen; 
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Taskmanager.TaskNotify.action += TaskNotify_action;
            draw = new DrawHelper(font_size);

        }

        private dynamic TaskNotify_action(dynamic mouseevent)
        {
           
            ClickPost = new Point(mouseevent.X, mouseevent.Y);
            ClickNumberInfo = PosHelper.getNumberInfromClick(ClickPost, draw.NumberInfos, font_size);
            if (ClickNumberInfo == null)
            {
                return false;
            }
            Musicplay.PlayMusic(mp3path);
            if (ClickNumberInfo.Order == successCount)
            {

                successCount++;
                fMS.NextTo(FMS.State.success);
                if (successCount == 10 && currentLevel == 4)
                {
                    sw.Stop();
                    currentLevel++;
                    Reset();
                    fMS.NextTo(FMS.State.win);
                }
                else if (successCount == 10)
                {
                    sw.Stop();
                    currentLevel++;
                    Reset();
                    MessageBox.Show($"恭喜通过第{currentLevel}关！用时{sw.Elapsed}秒");                
                    fMS.NextTo(FMS.State.ready);
                }
            }
            else
            {

                fMS.NextTo(FMS.State.fail);
            }


            return false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            fMS = new FMS();
            fMS.AddState(FMS.State.ready, Ready);
            fMS.AddState(FMS.State.play, Play);
            fMS.AddState(FMS.State.success, Success);
            fMS.AddState(FMS.State.fail, Fail);
            fMS.AddState(FMS.State.win, Win);

        }


        private void btnStart_Click(object sender, EventArgs e)
        {


            CreateForm2();
            SplitRetangle();        
            fMS.start();
         

        }

        void Reset()
        {
            if (CImage != null)
            {

                CImage.Dispose();
            }
            successCount = 0;


        }

        void Ready()
        {

            CImage = draw.DrawReady(form2.Width, form2.Height, currentLevel,arrlevelDelay[currentLevel], pictureBox);
            fMS.NextTo(FMS.State.play);
        }
        void Play()
        {


            InitNumberInfo();
            CImage = draw.DrawImg(CImage);
            pictureBox.Image = CImage;
            pictureBox.Refresh();
            //Thread.Sleep(arrlevelDelay[currentLevel]);
            timeUp();
         
        }

        void Fail()
        {
            IsCanPlay = false;
            draw.DrawFail(CImage, ClickNumberInfo.BlockPoint);
            pictureBox.Image = CImage;
            pictureBox.Refresh();
            Thread.Sleep(2000);
            Reset();
            fMS.NextTo(FMS.State.ready);
        }

        void Success()
        {

            draw.DrawSuccess(CImage, ClickNumberInfo.BlockPoint);
            pictureBox.Image = CImage;
            pictureBox.Refresh();
        }
        void Win()
        {
  
            MessageBox.Show($"牛逼！！！恭喜通过第{currentLevel}关！用时{sw.Elapsed}秒");
        }

        void CreateForm2()
        {
            form2 = Form2.Instance;
            pictureBox = form2.Controls.Find("pictureBox1", false)[0] as PictureBox;
            form2.Show();

        }

        void timeUp()
        {
            timer = new System.Timers.Timer();
            timer.Interval = arrlevelDelay[currentLevel];
            timer.Elapsed += TimeOut;
            timer.Start();
        }

        private void TimeOut(object? sender, System.Timers.ElapsedEventArgs e)
        {

            timer.Stop();
            sw.Restart();
            draw.DrawBlock(CImage);
            this.Invoke(new Action(() =>
            {
                pictureBox.Image = CImage;
                pictureBox.Refresh();
                IsCanPlay = true;
            }));
           
        }

       
    }
}