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
                openFileDialog.Filter = "icon files (*.ico)|*.ico|All files (*.*)|*.*";
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
                }
            }
            lblIconsPath.Text = filePath;
            lblGenIconsPath.Text = filePath.Replace(fileName,"");
            //pictureBoxIcon.Image = Bitmap.FromHicon(new Icon(filePath, new Size(380, 380)).Handle);

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

        }

    }
}
