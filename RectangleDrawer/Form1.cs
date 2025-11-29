using System.Drawing.Imaging;

namespace RectangleDrawer;

public partial class Form1 : Form
{
    private Point startPoint;
    private Point endPoint;
    private bool isDrawing;
    private readonly List<Rectangle> rectangles = new();
    private Rectangle currentRect;

    public Form1()
    {
        InitializeComponent();
    }

    private void Canvas_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDrawing = true;
            startPoint = e.Location;
            endPoint = e.Location;
        }
    }

    private void Canvas_MouseMove(object? sender, MouseEventArgs e)
    {
        if (isDrawing)
        {
            endPoint = e.Location;
            currentRect = GetRectangle(startPoint, endPoint);
            canvas.Invalidate();
        }
    }

    private void Canvas_MouseUp(object? sender, MouseEventArgs e)
    {
        if (isDrawing && e.Button == MouseButtons.Left)
        {
            isDrawing = false;
            var rect = GetRectangle(startPoint, endPoint);
            if (rect.Width > 0 && rect.Height > 0)
            {
                rectangles.Add(rect);
            }
            currentRect = Rectangle.Empty;
            canvas.Invalidate();
        }
    }

    private void Canvas_Paint(object? sender, PaintEventArgs e)
    {
        using var pen = new Pen(Color.Blue, 2);
        
        // Draw all saved rectangles
        foreach (var rect in rectangles)
        {
            e.Graphics.DrawRectangle(pen, rect);
        }

        // Draw the current rectangle being drawn
        if (isDrawing && currentRect.Width > 0 && currentRect.Height > 0)
        {
            using var previewPen = new Pen(Color.Gray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            e.Graphics.DrawRectangle(previewPen, currentRect);
        }
    }

    private static Rectangle GetRectangle(Point p1, Point p2)
    {
        return new Rectangle(
            Math.Min(p1.X, p2.X),
            Math.Min(p1.Y, p2.Y),
            Math.Abs(p2.X - p1.X),
            Math.Abs(p2.Y - p1.Y)
        );
    }

    private void BtnExportEmf_Click(object? sender, EventArgs e)
    {
        using var saveDialog = new SaveFileDialog
        {
            Filter = "Enhanced Metafile (*.emf)|*.emf",
            DefaultExt = "emf",
            FileName = "drawing.emf"
        };

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            ExportToEmf(saveDialog.FileName);
            MessageBox.Show($"Exported successfully to:\n{saveDialog.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void ExportToEmf(string filePath)
    {
        using var g = canvas.CreateGraphics();
        var hdc = g.GetHdc();
        
        try
        {
            using var metafile = new Metafile(filePath, hdc, new Rectangle(0, 0, canvas.Width, canvas.Height), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);
            using var metaGraphics = Graphics.FromImage(metafile);
            metaGraphics.Clear(Color.White);
            
            using var pen = new Pen(Color.Blue, 2);
            foreach (var rect in rectangles)
            {
                metaGraphics.DrawRectangle(pen, rect);
            }
        }
        finally
        {
            g.ReleaseHdc(hdc);
        }
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        rectangles.Clear();
        canvas.Invalidate();
    }
}
