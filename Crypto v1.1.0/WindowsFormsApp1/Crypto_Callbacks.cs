using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoNS {
    partial class Crypto {

        private const int borderThickness = 10;

        /// <summary>
        /// Close the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Crypto_Close(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Minimize and maximize the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Crypto_Minimaze(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Lets the form be moved with the top bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Crypto_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CryptoUtil.ReleaseCapture();
                CryptoUtil.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        /// <summary>
        /// Draws a border to the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Crypto_DrawBorder(object sender, PaintEventArgs e)
        {
            CryptoUtil.drawBorder((Form)sender, borderThickness, Color.FromArgb(49, 52, 60));
        }

        /// <summary>
        /// Updates form title.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Crypto_UpdateTitle(object sender, EventArgs e)
        {
            if (File.Exists(tbFilePath.Text))
                Process.Start(new FileInfo(tbFilePath.Text).DirectoryName);
            else if (Directory.Exists(tbFolderPath.Text))
                Process.Start(new DirectoryInfo(tbFolderPath.Text).FullName);
        }

        /// <summary>
        /// Pops up the help windows, it focuses it if its already opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Crypto_Help(object sender, EventArgs e)
        {
            if (!CryptoUtil.isFormOpen("Crypto_Help"))
                new Crypto_Help().Show();
            else
                CryptoUtil.GetForm("Crypto_Help").BringToFront();
        }

        /// <summary>
        /// Opens the dialog for the user to choose a folder to encode or decode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btBrowseFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog()
            { RootFolder = Environment.SpecialFolder.Desktop })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbFolderPath.Text = dialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Opens the dialod for the user to choose a file to encode or decode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btBrowseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog
            { Title = "Select a file" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbFilePath.Text = dialog.FileName;
                }
            }
        }

        /// <summary>
        /// Checks if the selected folder exists and outputs a error if it doesn't
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFolderPath_TextChanged(object sender, EventArgs e)
        {
            if (!Directory.Exists(tbFolderPath.Text))
            {

                //makes the program unable to start
                btStart.Enabled = false;

                //sets the text messages
                lbFileSize.Text = "File Size: Unknown";
                lbStatus.Text = "Status: Waiting for an input";
                lbProgress.Text = "Progress: 0%";

                // resets the dynamic title name
                lbDynamicTitle.Text = "No file";

                // Resets the progress bar value
                pbLoading.Value = 0;

                return;
            }

            // if none of the errors above activates then its good
            btStart.Enabled = true;

            // has to do this way since there's no way to calculate direcotry's size without adding each
            // file's size (i tested this on the backup folder of my phone (200k+ files / 250gb) and the program 
            // took minutes to sum every file
            lbFileSize.Text = "File Size: Calculating...";

            // Sets the dynamic title name
            lbDynamicTitle.Text = "Looking in \\" + new DirectoryInfo(tbFolderPath.Text).Name + "";

            // starts a new thread to calculate the file size
            Task.Run(() => {
                try
                {
                    string[] files = cbDeepScanMode.Checked ? Directory.GetFiles(tbFolderPath.Text, "*", SearchOption.AllDirectories) : Directory.GetFiles(tbFolderPath.Text);
                    long size = 0;
                    foreach (string file in files)
                    {
                        size += new FileInfo(file).Length;
                    }

                    UpdateLabel(lbFileSize, "File Size: " + CryptoUtil.getSizeExt(size));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message, "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        /// <summary>
        /// Checks if the selected file exists and outputs a error if it doesn't
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFilePath_TextChanged(object sender, EventArgs e)
        {
            //checks if the file exist
            if (!File.Exists(tbFilePath.Text))
            {
                //makes the program unable to start
                btStart.Enabled = false;

                //sets the text messages
                lbFileSize.Text = "File Size: Unknown";
                lbStatus.Text = "Status: Waiting for an input";
                lbProgress.Text = "Progress: 0%";

                // resets the dynamic title name
                lbDynamicTitle.Text = "No file";

                pbLoading.Value = 0;

                return;
            }

            // Gets the file attributes
            FileInfo fInfo = new FileInfo(tbFilePath.Text);

            // If none of the errors above are triggered then it enables the start button
            btStart.Enabled = true;

            // Sets the dynamic title name
            lbDynamicTitle.Text = "Looking at " + fInfo.Name.Substring(0, fInfo.Name.IndexOf('.'));

            //sets the file size label
            long size = fInfo.Length;

            lbFileSize.Text = "File Size: " + CryptoUtil.getSizeExt(size);

            //setting progress bar maxmimum value
            pbLoading.Maximum = (int)(fInfo.Length / 1048576L);

            //adds 1 in case there is one more cycle to do for the loop (8.4Mbs = 9 cycles, 8Mbs = 8 cycles)
            if (fInfo.Length != 0L && (int)(fInfo.Length / 1048576L) == 0)
                pbLoading.Maximum++;
            else if (pbLoading.Maximum % 1048576 != 0)
                pbLoading.Maximum++;
        }

        /// <summary>
        /// Sets the mode to decode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbDecode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDecode.Checked)
            {
                rbEncode.Checked = false;
            }
        }

        /// <summary>
        /// Sets the mode to encode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbEncode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEncode.Checked)
            {
                rbDecode.Checked = false;
            }
        }

        /// <summary>
        /// Shows the password in plain text to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                tbPassword.UseSystemPasswordChar = false;
            }
            else
            {
                tbPassword.UseSystemPasswordChar = true;
            }
        }

        /// <summary>
        /// Copies the dragged folder's path into the textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFolderPath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                //gets all files in that directory
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                //if thats a directory
                if (Directory.Exists(files[0]))
                {
                    tbFolderPath.Text = files[0];
                }
                //if thats a file it will get the file's directory
                else
                {
                    tbFolderPath.Text = Path.GetDirectoryName(files[0]);
                }
            }
        }

        /// <summary>
        /// Shows the drag cursor copy effets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFolderPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Copies the dragged file's path into the textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFilePath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                tbFilePath.Text = files[0];
            }
        }

        /// <summary>
        /// Shows the drag cursor copy effect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// If enter is pressed in the filepath textbox it will start encryption/decryption.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFilePath_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user wants to start decoding/encoding, he presses enter while focusing the textbox
            if (e.KeyCode == Keys.Enter) {
                btStart_Click(sender, e);
            }
        }

        /// <summary>
        /// If enter is pressed in the folderpath textbox  tab it will start encryption/decryption.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFolderPath_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user wants to start decoding/encoding, he presses enter while focusing the textbox
            if (e.KeyCode == Keys.Enter)
            {
                btStart_Click(sender, e);
            }
        }

        /// <summary>
        /// If enter is pressed in the password textbox it will start encryption/decryption.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user wants to start decoding/encoding, he presses enter while focusing the textbox
            if (e.KeyCode == Keys.Enter)
            {
                btStart_Click(sender, e);
            }
        }

        /// <summary>
        /// Updates real-time the size of the folder based on cbDeepScanMode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDeepScanMode_CheckedChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(tbFolderPath.Text))
            {
                lbFileSize.Text = "File Size: Unknown";
                return;
            }
            
            lbFileSize.Text = "File Size: Calculating...";

            Task.Run(() => {
                long size = CryptoUtil.getDirectorySize(tbFolderPath.Text, cbDeepScanMode.Checked);

                UpdateLabel(lbFileSize, "File Size: " + CryptoUtil.getSizeExt(size));
            });
        }

        /// <summary>
        /// Clears the file textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClearFilePath_Click(object sender, EventArgs e)
        {
            tbFilePath.Text = "";
        }

        /// <summary>
        /// Clears the folder textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClearFolderPath_Click(object sender, EventArgs e)
        {
            tbFolderPath.Text = "";
        }
    }
}
