namespace KeyLogger
{
    public partial class KeyLogger : Form
    {
        public TextBox OutputTextBox => textBox1;

        public KeyLogger()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
