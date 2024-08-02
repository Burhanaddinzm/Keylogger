namespace KeyLogger;

partial class KeyLogger
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox textBox1;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyLogger));
        textBox1 = new TextBox();
        SuspendLayout();
        // 
        // textBox1
        // 
        textBox1.Dock = DockStyle.Fill;
        textBox1.Location = new Point(0, 0);
        textBox1.Multiline = true;
        textBox1.Name = "textBox1";
        textBox1.ScrollBars = ScrollBars.Vertical;
        textBox1.Size = new Size(300, 500);
        textBox1.TabIndex = 0;
        textBox1.TextChanged += textBox1_TextChanged;
        // 
        // KeyLogger
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(300, 500);
        Controls.Add(textBox1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "KeyLogger";
        Text = "KeyLogger";
        Load += Form1_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}
