using System.Drawing.Imaging;

namespace RectangleDrawer;

public enum DrawingTool
{
    Rectangle,
    Line,
    Circle,
    Text
}

public record DrawingShape(DrawingTool Tool, Point Start, Point End, string? Text = null);

public partial class Form1 : Form
{
    private Point startPoint;
    private Point endPoint;
    private bool isDrawing;
    private readonly List<DrawingShape> shapes = new();
    private Rectangle currentRect;
    private DrawingTool currentTool = DrawingTool.Rectangle;

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
            
            if (currentTool == DrawingTool.Text)
            {
                // Show input dialog for text
                using var textDialog = new Form
                {
                    Text = "Enter Text",
                    Size = new Size(300, 150),
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterParent,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                
                var textBox = new TextBox
                {
                    Location = new Point(20, 20),
                    Size = new Size(240, 25)
                };
                
                var okButton = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(100, 60),
                    Size = new Size(80, 30)
                };
                
                textDialog.Controls.Add(textBox);
                textDialog.Controls.Add(okButton);
                textDialog.AcceptButton = okButton;
                
                if (textDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
                {
                    shapes.Add(new DrawingShape(DrawingTool.Text, startPoint, endPoint, textBox.Text));
                }
            }
            else
            {
                var rect = GetRectangle(startPoint, endPoint);
                if (rect.Width > 0 || rect.Height > 0 || currentTool == DrawingTool.Line)
                {
                    shapes.Add(new DrawingShape(currentTool, startPoint, endPoint));
                }
            }
            
            currentRect = Rectangle.Empty;
            canvas.Invalidate();
        }
    }

    private void Canvas_Paint(object? sender, PaintEventArgs e)
    {
        using var pen = new Pen(Color.Blue, 2);
        using var font = new Font("Arial", 12);
        using var brush = new SolidBrush(Color.Blue);
        
        // Draw all saved shapes
        foreach (var shape in shapes)
        {
            DrawShape(e.Graphics, pen, font, brush, shape);
        }

        // Draw the current shape being drawn
        if (isDrawing && currentTool != DrawingTool.Text)
        {
            using var previewPen = new Pen(Color.Gray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            DrawShapePreview(e.Graphics, previewPen, startPoint, endPoint);
        }
    }

    private void DrawShape(Graphics g, Pen pen, Font font, Brush brush, DrawingShape shape)
    {
        var rect = GetRectangle(shape.Start, shape.End);
        
        switch (shape.Tool)
        {
            case DrawingTool.Rectangle:
                if (rect.Width > 0 && rect.Height > 0)
                    g.DrawRectangle(pen, rect);
                break;
            case DrawingTool.Line:
                g.DrawLine(pen, shape.Start, shape.End);
                break;
            case DrawingTool.Circle:
                if (rect.Width > 0 && rect.Height > 0)
                    g.DrawEllipse(pen, rect);
                break;
            case DrawingTool.Text:
                if (!string.IsNullOrEmpty(shape.Text))
                    g.DrawString(shape.Text, font, brush, shape.Start);
                break;
        }
    }

    private void DrawShapePreview(Graphics g, Pen pen, Point start, Point end)
    {
        var rect = GetRectangle(start, end);
        
        switch (currentTool)
        {
            case DrawingTool.Rectangle:
                if (rect.Width > 0 && rect.Height > 0)
                    g.DrawRectangle(pen, rect);
                break;
            case DrawingTool.Line:
                g.DrawLine(pen, start, end);
                break;
            case DrawingTool.Circle:
                if (rect.Width > 0 && rect.Height > 0)
                    g.DrawEllipse(pen, rect);
                break;
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
            using var font = new Font("Arial", 12);
            using var brush = new SolidBrush(Color.Blue);
            
            foreach (var shape in shapes)
            {
                DrawShape(metaGraphics, pen, font, brush, shape);
            }
        }
        finally
        {
            g.ReleaseHdc(hdc);
        }
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        shapes.Clear();
        canvas.Invalidate();
    }

    private void SelectTool(DrawingTool tool)
    {
        currentTool = tool;
        UpdateToolButtonStyles();
        UpdateInstructions();
    }

    private void UpdateToolButtonStyles()
    {
        btnRectangle.BackColor = currentTool == DrawingTool.Rectangle ? Color.LightBlue : SystemColors.Control;
        btnLine.BackColor = currentTool == DrawingTool.Line ? Color.LightBlue : SystemColors.Control;
        btnCircle.BackColor = currentTool == DrawingTool.Circle ? Color.LightBlue : SystemColors.Control;
        btnText.BackColor = currentTool == DrawingTool.Text ? Color.LightBlue : SystemColors.Control;
    }

    private void UpdateInstructions()
    {
        lblInstructions.Text = currentTool switch
        {
            DrawingTool.Rectangle => "Click and drag to draw a rectangle",
            DrawingTool.Line => "Click and drag to draw a line",
            DrawingTool.Circle => "Click and drag to draw a circle/ellipse",
            DrawingTool.Text => "Click to place text",
            _ => "Select a tool and draw on the canvas"
        };
    }

    private void BtnRectangle_Click(object? sender, EventArgs e) => SelectTool(DrawingTool.Rectangle);
    private void BtnLine_Click(object? sender, EventArgs e) => SelectTool(DrawingTool.Line);
    private void BtnCircle_Click(object? sender, EventArgs e) => SelectTool(DrawingTool.Circle);
    private void BtnText_Click(object? sender, EventArgs e) => SelectTool(DrawingTool.Text);
}
