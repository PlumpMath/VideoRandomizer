using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace VideoRandomizer
{
    public class FileRandomizer
    {
        public string Path { get; private set; }

        private List<string> filesToPlay = new List<string>();
        private Random random = new Random();
        private Process process;

        public FileRandomizer( string path )
        {
            Path = path;
            AddFilesToList(path);
        }

        private void AddFilesToList(string path)
        {
            var dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs)
            {
                AddFilesToList(dir);
            }
            var files = Directory.GetFiles(path);
            foreach( var file in files)
            {
                filesToPlay.Add(file);
            }
        }

        public void Go()
        {
            ProcessExitHandler(null, null);
        }

        private void ProcessExitHandler(object sender, System.EventArgs e)
        {
            int count = filesToPlay.Count;
            if ( count == 0 )
            {
                return;
            }

            int num = random.Next(count);

            string file = filesToPlay[num];
            filesToPlay.RemoveAt(num);

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "vlc";
            info.Arguments = string.Format("--fullscreen --play-and-exit \"{0}\"", file);

            process = new Process();
            process.StartInfo = info;
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(ProcessExitHandler);
            process.Start();
            
        }

        internal void Close()
        {
            if ( process != null )
            {
                process.Kill();
                process.Close();
            }
        }
    }
}
