using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApuestasApp.Forms;

public class ImagePreviewForm : Form
{
    private readonly PictureBox _pictureBox;

    public ImagePreviewForm(Image image)
    {
        Text = "Vista de imagen";
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = true;
        MinimizeBox = false;
        FormBorderStyle = FormBorderStyle.Sizable;

        var width = Math.Max(400, Math.Min(image.Width, 1000));
        var height = Math.Max(300, Math.Min(image.Height, 800));
        ClientSize = new Size(width, height);

        _pictureBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = new Bitmap(image)
        };

        Controls.Add(_pictureBox);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_pictureBox.Image != null)
            {
                _pictureBox.Image.Dispose();
                _pictureBox.Image = null;
            }

            _pictureBox.Dispose();
        }

        base.Dispose(disposing);
    }
}
