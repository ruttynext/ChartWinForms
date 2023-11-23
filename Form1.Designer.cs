namespace ChartWinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LiveChartsCore.SkiaSharpView.RectangularSection rectangularSection1 = new LiveChartsCore.SkiaSharpView.RectangularSection();
            LiveChartsCore.SkiaSharpView.Painting.SolidColorPaint solidColorPaint1 = new LiveChartsCore.SkiaSharpView.Painting.SolidColorPaint();
            this.chartTest1 = new ChartWinForms.ChartTest();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chartTest1
            // 
            this.chartTest1.DataSource = null;
            this.chartTest1.Location = new System.Drawing.Point(214, 51);
            this.chartTest1.Name = "chartTest1";
            this.chartTest1.Ranges = null;
            this.chartTest1.ResolutionChart = null;
            rectangularSection1.Fill = null;
            rectangularSection1.IsVisible = true;
            rectangularSection1.Label = "";
            rectangularSection1.LabelPaint = null;
            rectangularSection1.LabelSize = 12D;
            rectangularSection1.ScalesXAt = 0;
            rectangularSection1.ScalesYAt = 0;
            solidColorPaint1.CurrentTime = ((long)(-9223372036854775808));
            solidColorPaint1.FontFamily = null;
            solidColorPaint1.ImageFilter = null;
            solidColorPaint1.IsAntialias = true;
            solidColorPaint1.IsFill = false;
            solidColorPaint1.IsPaused = false;
            solidColorPaint1.IsStroke = true;
            solidColorPaint1.IsValid = false;
            solidColorPaint1.PathEffect = null;
            solidColorPaint1.RemoveOnCompleted = false;
            solidColorPaint1.SKFontStyle = null;
            solidColorPaint1.SKTypeface = null;
            solidColorPaint1.StrokeCap = SkiaSharp.SKStrokeCap.Butt;
            solidColorPaint1.StrokeJoin = SkiaSharp.SKStrokeJoin.Miter;
            solidColorPaint1.StrokeMiter = 0F;
            solidColorPaint1.StrokeThickness = 3F;
            solidColorPaint1.Style = SkiaSharp.SKPaintStyle.Fill;
            solidColorPaint1.ZIndex = 0D;
            rectangularSection1.Stroke = solidColorPaint1;
            rectangularSection1.Tag = null;
            rectangularSection1.Xi = 45243.34521625D;
            rectangularSection1.Xj = 8D;
            rectangularSection1.Yi = null;
            rectangularSection1.Yj = null;
            rectangularSection1.ZIndex = null;
            this.chartTest1.Sections = new LiveChartsCore.SkiaSharpView.RectangularSection[] {
        rectangularSection1};
            this.chartTest1.Size = new System.Drawing.Size(840, 528);
            this.chartTest1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 824);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartTest1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ChartTest chartTest1;
        private Label label1;
    }
}