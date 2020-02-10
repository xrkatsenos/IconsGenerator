using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IconsGenerator
{
    public partial class IconsGenerator : Form
    {
        public IconsGenerator()
        {
            InitializeComponent();
        }

        private void btnSelectIcon_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            var fileName = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "PNG files (*png)|*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    fileName = openFileDialog.SafeFileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    
                    try
                    {

                        Image img;
                        img = Image.FromFile(filePath);
                        if (img.Width != img.Height)
                        {
                            MessageBox.Show("Image should be square");
                            return;
                        }

                        if (img.Width != 1024)
                        {
                            MessageBox.Show("Image should be 1024x1024");
                            return;
                        }

                        lblIconsPath.Text = filePath;
                        lblGenIconsPath.Text = filePath.Replace(fileName, "");
                        pictureBoxIcon.Image = img;
                        btnGenerate.Enabled = true;
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message);
                        //throw;
                    }
                }
            }
            

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string newPath = lblGenIconsPath.Text;
            createFolder("AppIcons", newPath);
            newPath = lblGenIconsPath.Text + "AppIcons\\";


            if (checkBoxiPhone.Checked || checkBoxiPad.Checked || checkBoxMac.Checked || checkBoxWatch.Checked)
            {
                //create appstore.png file and...
                createFolder("Assets.xcassets", newPath);
                newPath += "Assets.xcassets\\";
                createFolder("AppIcon.appiconset", newPath);
            }

            if (checkBoxiPhone.Checked)
            {
            }

            if (checkBoxiPad.Checked)
            {
            }

            if (checkBoxMac.Checked)
            {
            }

            if (checkBoxWatch.Checked)
            {
            }

            

            if (checkBoxAndroid.Checked)
            {
                newPath = lblGenIconsPath.Text + "AppIcons\\";

                createFolder("android", newPath);
                newPath += "android\\";

                createFolder("mipmap-hdpi", newPath);
                createFolder("mipmap-mdpi", newPath);
                createFolder("mipmap-xhdpi", newPath);
                createFolder("mipmap-xxhdpi", newPath);
                createFolder("mipmap-xxxhdpi", newPath);

                //create playstore.png file

            }



        }

        void createFolder (string name, string path)
        {
            DirectoryInfo di = Directory.CreateDirectory(path + name);
        }

        //Image resizeImage (int size)
        //{
        //    Image img;
        //    return img;
        //}

        //Image convertToPNG (Image inputImage)
        //{
        //    Image img;
        //    return img;
        //}


    }
}
