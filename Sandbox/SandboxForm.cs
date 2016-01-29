using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSubCompiler;

namespace Sandbox
{
    public partial class SandboxForm : Form
    {
        public SandboxForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string code = codeTextBox.Text;
            string processed = Preprocessor.Preprocess(code);
            var tokens = Lexer.Lex(processed);

            int i = 0;
            CSubCompiler.AST.ExpressionNode en = CSubCompiler.AST.ExpressionNode.Parse(tokens, ref i);
        }

        private void SandboxForm_Load(object sender, EventArgs e)
        {
            button1_Click(null, null); //Temp
        }
    }
}
