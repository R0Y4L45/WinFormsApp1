namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Point LocationXY;
        Point LocationX1Y1;


        public Form1()
        {
            InitializeComponent();

            button1.Text = "Baku";
            button2.Text = "London";
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            LocationXY = e.Location;


        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Label? l = new();

            if (e.Button == MouseButtons.Left)
            {

                Point p = new();
                LocationX1Y1 = e.Location;

                int x = LocationX1Y1.X - LocationXY.X;
                int y = LocationX1Y1.Y - LocationXY.Y;

                if (x < 0 && y < 0)
                {
                    p.X = (-1) * x;
                    p.Y = (-1) * y;
                    l.Location = LocationX1Y1;
                }
                else if (x < 0)
                {
                    p.X = (-1) * x;
                    p.Y = y;
                    l.Location = new(LocationXY.X + x, LocationXY.Y);
                }
                else if (y < 0)
                {
                    p.X = x;
                    p.Y = (-1) * y;
                    l.Location = new(LocationXY.X, LocationXY.Y + y);
                }
                else
                {
                    p.X = x;
                    p.Y = y;
                    l.Location = LocationXY;
                }


                l.BackColor = Color.Black;
                l.Size = new(p.X, p.Y);

                if (p.X < 10 && p.Y < 10)
                    MessageBox.Show("Your rectangler size is smaller than 10x10", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    Controls.Add(l);


                l.MouseHover += (s, e) =>
                {
                    Label? l1 = (s as Label);
                    Text = $"Coordinate : {l1?.Location.X}, {l1?.Location.Y}     {l.Size}";
                };
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            int x = Random.Shared.Next(0, 750);
            int y = Random.Shared.Next(0, 430);
            label1.Location = new(x, y);
        }

        public static class CurrentTime
        {
            public static DateTime StartTime = DateTime.Now;
        }

        private void TimerTick(object? sender, EventArgs e) // Task 3
        {
            label2.Text = CurrentTime.StartTime.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.baku;
            CurrentTime.StartTime = DateTime.Now;
            label2.Text = DateTime.Now.Hour.ToString() + ':' + DateTime.Now.Minute.ToString() + ':' + DateTime.Now.Second.ToString();
            Controls.Add(label2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.london;
            var BritishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

            DateTime dt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

            DateTime DateTimeInBritishLocal = TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.Utc, BritishZone);
            CurrentTime.StartTime = DateTimeInBritishLocal;
            label2.Text = DateTimeInBritishLocal.Hour.ToString() + ':' + DateTimeInBritishLocal.Minute.ToString() + ':' + DateTimeInBritishLocal.Second.ToString();
            Controls.Add(label2);
        }


        private void Form1_Load(object sender, EventArgs e) // Task 3
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += TimerTick;

            timer.Start();
        }

    }
}