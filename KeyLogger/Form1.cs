namespace KeyLogger
{
    public partial class Form1 : Form
    {
        public TextBox OutputTextBox => textBox1;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
