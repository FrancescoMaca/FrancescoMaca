using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace CryptoNS {

    public partial class Crypto : Form {

        private readonly RijndaelManaged AES = new RijndaelManaged();

        /// <summary>
        /// Default constructor
        /// </summary>
        public Crypto() {
            InitializeComponent();
        }

        /// <summary>
        /// Start encrypting or decrypting the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btStart_Click(object sender, EventArgs e) {

            UpdateProgressBar(pbLoading, 1);

            UpdateLabel(lbStatus, "Status: Starting...");

            // Checks if the password textbox is empty
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("The password cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                tbPassword.Focus();
                
                return;
            }

            if (!string.IsNullOrEmpty(tbFilePath.Text)) {

                // Encoding
                if (rbEncode.Checked) {
                    Crypto_StartEncoding();
                }
                //decoding
                else {
                    Crypto_StartDecoding();
                }

                return;
            }
            
            
            if (!string.IsNullOrEmpty(tbFolderPath.Text)) {
                string[] filePaths = new string[0];

                try {
                    filePaths = cbDeepScanMode.Checked ? Directory.GetFiles(tbFolderPath.Text, "*", SearchOption.AllDirectories) : Directory.GetFiles(tbFolderPath.Text);
                }
                catch (UnauthorizedAccessException) {
                    MessageBox.Show("The application doesn't have enough privileges to\naccess the selected folder.\nTry to run Crypto as administrator.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex) {
                    MessageBox.Show("Error: " + ex, "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (rbEncode.Checked) {
                    Crypto_StartMultipleEncoding(filePaths);
                }
                else {
                    Crypto_StartMultipleDecoding(filePaths);
                }
            }
        }

        /// <summary>
        /// Updates a label with the text given.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        private void UpdateLabel(Label label, string text) {
            //if the Label is declared on a different thread, this will
            //call the function in the UI thread
            if (label.InvokeRequired) {
                label.Invoke((Action)(() => UpdateLabel(label, text)));
                return;
            }

            label.Text = text;
        }

        /// <summary>
        /// Updates the progress bar with a certain mode.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="mode"></param>
        private void UpdateProgressBar(ProgressBar p, int mode) {
            //if the Label is declared on a different thread, this will
            //call the function in the UI thread
            if (p.InvokeRequired) {
                p.Invoke((Action)(() => UpdateProgressBar(p, 0)));
                return;
            }

            if (mode == 0) {
                p.PerformStep();
            }
            else {
                p.Value = 0;
            }
        }

        /// <summary>
        /// Starts the encoding of a single file.
        /// </summary>
        private void Crypto_StartEncoding()
        {
            Task.Run(() => {
                Crypto_ErrorHandler(EncodeFile(tbFilePath.Text, tbPassword.Text), true);
            });
        }

        /// <summary>
        /// Starts the decoding of a single file
        /// </summary>
        private void Crypto_StartDecoding()
        {
            Task.Run(() =>
            {
                Crypto_ErrorHandler(DecodeFile(tbFilePath.Text, tbPassword.Text), true);       
            });
        }

        /// <summary>
        /// Starts multiple encoding on a list of files.
        /// </summary>
        /// <param name="files"></param>
        private void Crypto_StartMultipleEncoding(string[] files)
        {
            // Keeps tracks of all errors happened
            int errors = 0;

            foreach (string filePath in files)
            {
                FileInfo finfo = new FileInfo(filePath);

                //setting up loading bar maximum value
                pbLoading.Maximum = (int)finfo.Length / 1048576;

                CryptoUtil.State result_code = 0;

                Task.Run(() => result_code = EncodeFile(filePath, tbPassword.Text));

                // Resets progress bar
                UpdateProgressBar(pbLoading, 1);

                Crypto_ErrorHandler(result_code, false);
            }
        }

        /// <summary>
        /// Starts multiple decoding on a list of files.
        /// </summary>
        /// <param name="files"></param>
        private void Crypto_StartMultipleDecoding(string[] files)
        {
            int errors = 0;

            foreach(string filePath in files)
            {
                FileInfo finfo = new FileInfo(filePath);
                CryptoUtil.State result_code = 0;

                //setting up loading bar maximum value
                pbLoading.Maximum = (int)finfo.Length / 1048576;

                if (pbLoading.Maximum % 1048576 != 0)
                    UpdateProgressBar(pbLoading, 0);

                Task.Run(() => result_code = DecodeFile(filePath, tbPassword.Text));

                // Resets progress bar
                UpdateProgressBar(pbLoading, 1);

                Crypto_ErrorHandler(result_code, false);
            }

            if (errors != 0)
            {
                MessageBox.Show($"{errors} errors have occurred in the decoding! Try to decode\nthem again one by one.");
            }
            else
            {
                MessageBox.Show("No problem occurred! All files have been decrypted successfully!");
            }
        }

        /// <summary>
        /// Handles the decoding and encoding errors.
        /// </summary>
        /// <param name="ret"></param>
        private void Crypto_ErrorHandler(CryptoUtil.State ret, bool showMessages)
        {
            switch (ret)
            {
                case CryptoUtil.State.Valid:
                    UpdateLabel(lbStatus, "Status: Completed");

                    if (showMessages)
                        MessageBox.Show("The task has been completed successfully!", "Task Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case CryptoUtil.State.NotFound:
                    UpdateLabel(lbStatus, "Status: Error");

                    if (showMessages)
                        MessageBox.Show($"The file \"{tbFilePath.Text}\" was not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case CryptoUtil.State.AttributeError:
                case CryptoUtil.State.Canceled:

                    UpdateLabel(lbStatus, "Status: Canceled");

                    try
                    {
                        File.Delete(tbFilePath.Text + ".aes");
                    }
                    catch (Exception) { }
                    if (showMessages)
                        MessageBox.Show("The task has been canceled.", "Task Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                case CryptoUtil.State.Unhandled:

                    UpdateLabel(lbStatus, "Status: Error");

                        DialogResult res = MessageBox.Show("An unknown error occurred!\nYou want to restart Crypto?\nOtherwise it could have more problems.", "Unknown Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (res == DialogResult.Yes)
                    {
                        Application.Restart();
                        Environment.Exit(0);
                    }
                    break;
            }
        }

        /// <summary>
        /// Encodes the file selected in tbFilePath with an AES encryption
        /// with a password chosen by the user.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="password"></param>
        /// <returns>0 = no errors, 1 = not found, 3 = other exception</returns>
        private CryptoUtil.State EncodeFile(string FilePath, string password) {

            var clock = new Stopwatch();
            clock.Start();

            //check if the file exists
            if (!File.Exists(FilePath)) {

                // Sets focus on the textbox
                tbFilePath.Focus();

                MessageBox.Show("Selected file doesn't exist!", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return CryptoUtil.State.NotFound;
            }

            //check if the file is read-only
            if (new FileInfo(FilePath).IsReadOnly) {
                DialogResult result = MessageBox.Show("The file is read-only, do you wanna change that?\nIf you select \"No\" the task will terminate.", "File attribute error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    new FileInfo(FilePath).IsReadOnly = false;
                else
                    return CryptoUtil.State.AttributeError;
            }

            //check if the file is already encrypted
            if (FilePath.Contains(".aes")) {
                DialogResult result = MessageBox.Show("The file seems already encoded, do you want to encode it again?\nIf you select \"No\" the task will terminate.", "File error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    return CryptoUtil.State.Canceled;
            }

            byte[] salt = new byte[32];

            //creating salt and using it to create IV and Key
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider()) {
                for (int i = 0; i < 10000; i++) {

                    // Fille the buffer with the generated data
                    rng.GetBytes(salt);
                }

                //mixes the password with the salt
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, 100000);
                AES.Key = rfc2898DeriveBytes.GetBytes(AES.LegalKeySizes[0].MaxSize / 8);
                AES.IV = rfc2898DeriveBytes.GetBytes(AES.BlockSize / 8);
            }

            FileStream CryptedFile;

            //checks if the file that will be created aready exists
            try {
                CryptedFile = new FileStream(FilePath + ".aes", FileMode.Create);
            }
            catch(IOException) {
                DialogResult res = MessageBox.Show($"The file {FilePath} already exists. Do you want to overwrite it?", "File Error" , MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.Cancel)
                    return CryptoUtil.State.Canceled;
                else
                    CryptedFile = new FileStream(FilePath, FileMode.Truncate);
            }

            //create output file name
            using (CryptedFile) {
                //write salt to the begining of the output file
                CryptedFile.Write(salt, 0, salt.Length);

                using (CryptoStream cryptoStream = new CryptoStream(CryptedFile, AES.CreateEncryptor(), CryptoStreamMode.Write))
                using (FileStream InputFile = new FileStream(FilePath, FileMode.Open)) {
                    //create 1Mb buffer so only this amount will allocate in the memory and not the whole file
                    byte[] buffer = new byte[1048576];

                    //crypting file
                    try {
                        int read;

                        while ((read = InputFile.Read(buffer, 0, buffer.Length)) > 0) {
                            //writes encoded file
                            cryptoStream.Write(buffer, 0, read);

                            //call method to update texts in the UI
                            UpdateProgressBar(pbLoading, 0);

                            //updating status text
                            UpdateLabel(lbStatus, "Status: " + pbLoading.Value + "Mb of " + pbLoading.Maximum + "Mb encrypted");

                            //updating status in percentage
                            UpdateLabel(lbProgress, "Progress: " + (int)(((float)pbLoading.Value / pbLoading.Maximum) * 100.0f) + "%");
                        }

                        //sets the status to "Completed" when the application finishes
                        UpdateLabel(lbStatus, "Status: Completed");
                    }
                    catch (Exception e) {
                        MessageBox.Show("Error: " + e.Message, "Unhandled Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return CryptoUtil.State.Unhandled;
                    }
                }
            }

            try {
                //deletes the non-crypted file
                File.Delete(FilePath);
            }
            catch (FileNotFoundException) {
                MessageBox.Show($"Cannot delete the file \"{FilePath}\" since it cannot be found!", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return CryptoUtil.State.Valid;
        }

        /// <summary>
        /// Decodes the file selected in tbFilePath, after three times you decrypt
        /// the file with the wrong password, the files encrypts itself with a random
        /// password and a random salt.
        /// </summary>
        /// <param name="FilePath">The path of the file to decode.</param>
        /// <param name="password">The password you want to encode the file with.</param>
        /// <returns>0 = no errors, 1 = not found, 2 = cryptographic error, 3 = other exception</returns>
        private CryptoUtil.State DecodeFile(string FilePath, string password) {

            //check if the file exists
            if (!File.Exists(FilePath)) {

                // Sets focus on the textbox
                tbFilePath.Focus();

                MessageBox.Show("Selected file doesn't exist!", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return CryptoUtil.State.NotFound;
            }

            if (new FileInfo(FilePath).IsReadOnly) {
                DialogResult result = MessageBox.Show("The file is read-only, do you wanna change that?\nIf you select \"No\" the task will terminate.", "File attribute error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    new FileInfo(FilePath).IsReadOnly = false;
                else
                    return CryptoUtil.State.AttributeError;
            }

            //check if the file is already encrypted
            if (!FilePath.Contains(".aes")) {
                MessageBox.Show("The file is not encoded, you cannot decode a file that isn't encoded!", "File error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return CryptoUtil.State.AttributeError;
            }


            string fileWithoutExt = FilePath.Substring(0, FilePath.Length - 4);

            using (FileStream CryptedFile = new FileStream(FilePath, FileMode.Open)) {
                byte[] salt = new byte[32];
                CryptedFile.Read(salt, 0, salt.Length);

                using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, 100000)) {
                    AES.Key = key.GetBytes(AES.LegalKeySizes[0].MaxSize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                }

                FileStream OutputFile = null;

                //checks if the file that will be created aready exists
                try {
                    OutputFile = new FileStream(fileWithoutExt, FileMode.CreateNew);
                }
                catch (IOException) {
                    DialogResult res = MessageBox.Show($"The file {fileWithoutExt} already exists. Do you want to overwrite it?", "File Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (res == DialogResult.Cancel)
                        return CryptoUtil.State.Canceled;
                    else
                        OutputFile = new FileStream(fileWithoutExt, FileMode.Truncate);
                }

                using (CryptoStream cryptoStream = new CryptoStream(CryptedFile, AES.CreateDecryptor(), CryptoStreamMode.Read))
                using (OutputFile) {
                    //creating 1Mb buffer to read/write the decoded file
                    byte[] buffer = new byte[1048576];

                    //decrypting file
                    try {
                        int read;

                        while ((read = cryptoStream.Read(buffer, 0, buffer.Length)) > 0) {
                            //writes decoded file
                            OutputFile.Write(buffer, 0, read);

                            //call method to update texts in the UI
                            UpdateProgressBar(pbLoading, 0);

                            //updating status text
                            UpdateLabel(lbStatus, "Status: " + pbLoading.Value + "Mb of " + pbLoading.Maximum + "Mb decrypted");

                            //updating status in percentage
                            UpdateLabel(lbProgress, "Progress: " + (int)(((float)pbLoading.Value / pbLoading.Maximum) * 100.0f) + "%");
                        }

                        //sets the status to "Completed" when the application finishes
                        UpdateLabel(lbStatus, "Status: Completed");
                    }
                    catch (CryptographicException) {

                        //deletes the empty file if the password is wrong
                        try
                        {
                            // Closes output file
                            OutputFile.Dispose();   

                            // Removes the file half-decrypted
                            File.Delete(fileWithoutExt);
                        }
                        catch (Exception) { }

                        MessageBox.Show("The password inserted is wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return CryptoUtil.State.InvalidPassword;
                    }
                    catch (Exception ex) {
                        MessageBox.Show("Exception: " + ex.Message);
                        MessageBox.Show("Exception not handled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return CryptoUtil.State.Unhandled;
                    }
                }
            }

            try {
                File.Delete(FilePath);
            }
            catch (FileNotFoundException) {
                MessageBox.Show($"The file \"{FilePath}\" coudln't be found.", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return CryptoUtil.State.Valid;
        }
   
    }
}
