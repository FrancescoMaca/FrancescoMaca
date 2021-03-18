using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CryptoNS {

    static class CryptoUtil {

        public enum State {
            Valid, NotFound, AttributeError, Canceled, Unhandled, InvalidPassword
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// Checks thorugh the opened forms and searches the requested form.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static Form GetForm(string form) {
            foreach (Form f in Application.OpenForms)
                if (f.Name == form)
                    return f;

            return null;
        }

        /// <summary>
        /// Returns if a form is opened.
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public static bool isFormOpen(string formName) {
            foreach (Form form in Application.OpenForms) {
                if (form.Name == formName)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the size and the label (B,Kb,Mb..) of a given number of bytes.
        /// </summary>
        /// <param name="size">Number of bytes to return with label</param>
        /// <returns></returns>
        public static string getSizeExt(long size) {

            //sets the file size label
            string[] units = new string[] { "B", "Kb", "Mb", "Gb", "Tb" };
            if (size == 0)
                return size + units[0];

            int place = Convert.ToInt32(Math.Floor(Math.Log(size, 1024)));
            double num = Math.Round(size / Math.Pow(1024, place), 1);
            return num + units[place];
        }

        /// <summary>
        /// Sets the form border.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="thickness"></param>
        public static void drawBorder(Form f, int thickness, Color color) {
            f.Paint += (object o, PaintEventArgs e) => e.Graphics.DrawRectangle(new Pen(color, thickness), f.DisplayRectangle);
        }

        /// <summary>
        /// Get the size of a directory.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static long getDirectorySize(string path, bool recursive)
        {
            string[] files = Directory.GetFiles(path, "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            long totSize = 0l;

            foreach(string file in files)
            {
                totSize += new FileInfo(file).Length;
            }

            return totSize;
        }

    }
}
