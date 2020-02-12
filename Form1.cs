using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

        Image img;

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

            String androidPath = newPath + "android\\";
            String iOSPath = newPath + "Assets.xcassets\\";

            if (checkBoxiPhone.Checked || checkBoxiPad.Checked || checkBoxMac.Checked || checkBoxWatch.Checked)
            {
                //create appstore.png file
                Image tmpimage = ResizeImage(img, 1024);
                saveImage(tmpimage, newPath, "appstore.png");

                createFolder("Assets.xcassets", newPath);
                createFolder("AppIcon.appiconset", iOSPath);
                iOSPath += "AppIcon.appiconset\\";

                saveImage(ResizeImage(img, 1024), iOSPath, "1024.png");

            }


            if (checkBoxiPhone.Checked)
            {
                saveImage(ResizeImage(img, 29), iOSPath, "29.png");
                saveImage(ResizeImage(img, 40), iOSPath, "40.png");
                saveImage(ResizeImage(img, 57), iOSPath, "57.png");
                saveImage(ResizeImage(img, 58), iOSPath, "58.png");
                saveImage(ResizeImage(img, 60), iOSPath, "60.png");
                saveImage(ResizeImage(img, 80), iOSPath, "80.png");
                saveImage(ResizeImage(img, 87), iOSPath, "87.png");
                saveImage(ResizeImage(img, 114), iOSPath, "114.png");
                saveImage(ResizeImage(img, 120), iOSPath, "120.png");
                saveImage(ResizeImage(img, 180), iOSPath, "180.png");
            }

            if (checkBoxiPad.Checked)
            {
                saveImage(ResizeImage(img, 20), iOSPath, "20.png");
                saveImage(ResizeImage(img, 29), iOSPath, "29.png");
                saveImage(ResizeImage(img, 40), iOSPath, "40.png");
                saveImage(ResizeImage(img, 50), iOSPath, "50.png");
                saveImage(ResizeImage(img, 58), iOSPath, "58.png");
                saveImage(ResizeImage(img, 72), iOSPath, "72.png");
                saveImage(ResizeImage(img, 76), iOSPath, "76.png");
                saveImage(ResizeImage(img, 80), iOSPath, "80.png");
                saveImage(ResizeImage(img, 100), iOSPath, "100.png");
                saveImage(ResizeImage(img, 144), iOSPath, "144.png");
                saveImage(ResizeImage(img, 152), iOSPath, "152.png");
                saveImage(ResizeImage(img, 167), iOSPath, "167.png");
            }

            if (checkBoxMac.Checked)
            {
                saveImage(ResizeImage(img, 16), iOSPath, "16.png");
                saveImage(ResizeImage(img, 32), iOSPath, "32.png");
                saveImage(ResizeImage(img, 64), iOSPath, "64.png");
                saveImage(ResizeImage(img, 128), iOSPath, "128.png");
                saveImage(ResizeImage(img, 256), iOSPath, "256.png");
                saveImage(ResizeImage(img, 512), iOSPath, "512.png");
            }

            if (checkBoxWatch.Checked)
            {
                saveImage(ResizeImage(img, 48), iOSPath, "48.png");
                saveImage(ResizeImage(img, 55), iOSPath, "55.png");
                saveImage(ResizeImage(img, 58), iOSPath, "58.png");
                saveImage(ResizeImage(img, 80), iOSPath, "80.png");
                saveImage(ResizeImage(img, 87), iOSPath, "87.png");
                saveImage(ResizeImage(img, 88), iOSPath, "88.png");
                saveImage(ResizeImage(img, 100), iOSPath, "100.png");
                saveImage(ResizeImage(img, 172), iOSPath, "172.png");
                saveImage(ResizeImage(img, 196), iOSPath, "196.png");
                saveImage(ResizeImage(img, 216), iOSPath, "216.png");
            }


            if (checkBoxAndroid.Checked)
            {

                //create playstore.png file
                Image tmpimage = ResizeImage(img, 512);
                saveImage(tmpimage, newPath, "playstore.png");

                createFolder("android", newPath);

                string tmpPath;

                createFolder("mipmap-hdpi", androidPath);
                tmpPath = androidPath + "mipmap-hdpi\\";
                saveImage(ResizeImage(img, 72), tmpPath, "ic_launcher.png");

                createFolder("mipmap-mdpi", androidPath);
                tmpPath = androidPath + "mipmap-mdpi\\";
                saveImage(ResizeImage(img, 48), tmpPath, "ic_launcher.png");

                createFolder("mipmap-xhdpi", androidPath);
                tmpPath = androidPath + "mipmap-xhdpi\\";
                saveImage(ResizeImage(img, 96), tmpPath, "ic_launcher.png");

                createFolder("mipmap-xxhdpi", androidPath);
                tmpPath = androidPath + "mipmap-xxhdpi\\";
                saveImage(ResizeImage(img, 144), tmpPath, "ic_launcher.png");

                createFolder("mipmap-xxxhdpi", androidPath);
                tmpPath = androidPath + "mipmap-xxxhdpi\\";
                saveImage(ResizeImage(img, 192), tmpPath, "ic_launcher.png");


            }




            try
            {
                using (StreamReader r = new StreamReader(Application.StartupPath + @"/content.json"))
                {
                    string json = r.ReadToEnd();
                    File.WriteAllText(iOSPath + "Contents.json", json);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to read and create Contents JSON file.");
            }

            MessageBox.Show("Icon files created! Enjoy..");


        }

        void createFolder (string name, string path)
        {
            DirectoryInfo di = Directory.CreateDirectory(path + name);
        }

  
        public static Bitmap ResizeImage(Image image, int size)
        {
            var destRect = new Rectangle(0, 0, size, size);
            var destImage = new Bitmap(size, size);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        void saveImage(Image img, string path, string imgName)
        {
            img.Save(path + "\\" + imgName, ImageFormat.Png);
        }


    }
}
