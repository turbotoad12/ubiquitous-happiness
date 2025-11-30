namespace RectangleDrawer;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        canvas = new Panel();
        btnExportEmf = new Button();
        btnClear = new Button();
        toolPanel = new Panel();
        lblInstructions = new Label();
        btnRectangle = new Button();
        btnLine = new Button();
        btnCircle = new Button();
        btnText = new Button();
        toolPanel.SuspendLayout();
        SuspendLayout();

        // canvas
        canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        canvas.BackColor = Color.White;
        canvas.BorderStyle = BorderStyle.FixedSingle;
        canvas.Location = new Point(12, 60);
        canvas.Name = "canvas";
        canvas.Size = new Size(776, 378);
        canvas.TabIndex = 0;
        canvas.Paint += Canvas_Paint;
        canvas.MouseDown += Canvas_MouseDown;
        canvas.MouseMove += Canvas_MouseMove;
        canvas.MouseUp += Canvas_MouseUp;

        // btnExportEmf
        btnExportEmf.Location = new Point(12, 12);
        btnExportEmf.Name = "btnExportEmf";
        btnExportEmf.Size = new Size(100, 35);
        btnExportEmf.TabIndex = 1;
        btnExportEmf.Text = "Export to EMF";
        btnExportEmf.UseVisualStyleBackColor = true;
        btnExportEmf.Click += BtnExportEmf_Click;

        // btnClear
        btnClear.Location = new Point(118, 12);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(75, 35);
        btnClear.TabIndex = 2;
        btnClear.Text = "Clear";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += BtnClear_Click;

        // btnRectangle
        btnRectangle.Location = new Point(210, 12);
        btnRectangle.Name = "btnRectangle";
        btnRectangle.Size = new Size(75, 35);
        btnRectangle.TabIndex = 4;
        btnRectangle.Text = "Rectangle";
        btnRectangle.UseVisualStyleBackColor = true;
        btnRectangle.Click += BtnRectangle_Click;

        // btnLine
        btnLine.Location = new Point(291, 12);
        btnLine.Name = "btnLine";
        btnLine.Size = new Size(75, 35);
        btnLine.TabIndex = 5;
        btnLine.Text = "Line";
        btnLine.UseVisualStyleBackColor = true;
        btnLine.Click += BtnLine_Click;

        // btnCircle
        btnCircle.Location = new Point(372, 12);
        btnCircle.Name = "btnCircle";
        btnCircle.Size = new Size(75, 35);
        btnCircle.TabIndex = 6;
        btnCircle.Text = "Circle";
        btnCircle.UseVisualStyleBackColor = true;
        btnCircle.Click += BtnCircle_Click;

        // btnText
        btnText.Location = new Point(453, 12);
        btnText.Name = "btnText";
        btnText.Size = new Size(75, 35);
        btnText.TabIndex = 7;
        btnText.Text = "Text";
        btnText.UseVisualStyleBackColor = true;
        btnText.Click += BtnText_Click;

        // toolPanel
        toolPanel.Controls.Add(lblInstructions);
        toolPanel.Controls.Add(btnExportEmf);
        toolPanel.Controls.Add(btnClear);
        toolPanel.Controls.Add(btnRectangle);
        toolPanel.Controls.Add(btnLine);
        toolPanel.Controls.Add(btnCircle);
        toolPanel.Controls.Add(btnText);
        toolPanel.Dock = DockStyle.Top;
        toolPanel.Location = new Point(0, 0);
        toolPanel.Name = "toolPanel";
        toolPanel.Size = new Size(800, 54);
        toolPanel.TabIndex = 3;

        // lblInstructions
        lblInstructions.AutoSize = true;
        lblInstructions.Location = new Point(540, 20);
        lblInstructions.Name = "lblInstructions";
        lblInstructions.Size = new Size(300, 15);
        lblInstructions.TabIndex = 3;
        lblInstructions.Text = "Click and drag to draw a rectangle";

        // Form1
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 500);
        Controls.Add(canvas);
        Controls.Add(toolPanel);
        MinimumSize = new Size(600, 400);
        Name = "Form1";
        Text = "Shape Drawer - EMF Exporter";
        toolPanel.ResumeLayout(false);
        toolPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel canvas;
    private Button btnExportEmf;
    private Button btnClear;
    private Panel toolPanel;
    private Label lblInstructions;
    private Button btnRectangle;
    private Button btnLine;
    private Button btnCircle;
    private Button btnText;
}
