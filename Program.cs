namespace Console_setup
{
    class AppSettingsHelper
    {
        public ConsoleColor Background { get; protected set; }
        public string backgroundStr { get; protected set; }
        public ConsoleColor Foreground { get; protected set; }
        public string foregroundStr { get; protected set; }
        public int height { get; set; }
        public int width { get; set; }
        public string title { get; set; }

        public AppSettingsHelper() {
            Background = Console.BackgroundColor;
            Foreground = Console.ForegroundColor;
        }
        public AppSettingsHelper(string background, string foreground, int height, int width, string title)
        {
            SetSettings(background, foreground, height, width, title);
        }

        public void SetSettings(string background, string foreground, int height, int width, string title)
        {
            backgroundStr = background;
            if (!Enum.TryParse(background, true, out ConsoleColor backgroundEnum)) throw new ArgumentException($"Unable to parse {background}.");
            foregroundStr = foreground;
            if (!Enum.TryParse(foreground, true, out ConsoleColor foregroundEnum)) throw new ArgumentException($"Unable to parse {foreground}.");
            this.height = height;
            this.width = width;
            this.title = title;

            Background = backgroundEnum;
            Foreground = foregroundEnum;
        }
        public void SaveSettings(string fname)
        {
            using (BinaryWriter bw = new BinaryWriter(new FileStream(fname, FileMode.Create, FileAccess.Write)))
            {
                bw.Write(backgroundStr);
                bw.Write(foregroundStr);
                bw.Write(height);
                bw.Write(width);
                bw.Write(title);
            } 
        }
        public void LoadSettings(string fname)
        {
            using (BinaryReader br = new BinaryReader(new FileStream(fname, FileMode.Open, FileAccess.Read)))
            {
                string bgStr = br.ReadString();
                string fgStr = br.ReadString();
                int h = br.ReadInt32();
                int w = br.ReadInt32();
                string title = br.ReadString();
                //Console.WriteLine(bgStr + fgStr + h + w + title);
                SetSettings(bgStr, fgStr, h, w, title);
            }
        }
        public void ApplySettings() 
        {
            Console.BackgroundColor = Background;
            Console.ForegroundColor = Foreground;
            Console.WindowHeight = height;
            Console.WindowWidth = width;
            Console.Title = title;
            Console.WriteLine(Console.Title);
        }

    }
    internal class Program
    {


        static void Main(string[] args)
        {
            AppSettingsHelper app = new AppSettingsHelper("DarkMagenta", "Cyan", 50, 60, "AppConsole");
            app.SaveSettings("data.dat");

            AppSettingsHelper app2 = new AppSettingsHelper();
            app2.LoadSettings("data.dat");
            app2.ApplySettings();

            Console.WriteLine(new string('*', 100));
        }
    }
}