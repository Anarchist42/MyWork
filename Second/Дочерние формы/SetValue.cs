using System;
using System.Windows.Forms;
namespace Second
{
    public partial class SetValue : Form
    {
        public SetValue()
        {
            InitializeComponent();
            /*Передаем данные в буфер */
            GlobalConst.Buffer[0] = "";
            GlobalConst.Buffer[1] = "";
        }
        private void TextBoxXCoordinate_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа, бэкспейс, запятая*/
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8
                && (e.KeyChar != 44 || TextBoxXCoordinate.Text.IndexOf(",") > -1))
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
            /*Нельзя вводить первой запятую*/
            if (TextBoxXCoordinate.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
        }
        private void TextBoxYCoordinate_KeyPress(object sender, KeyPressEventArgs e)
        {          
            /*Можно вводить только числа, бэкспейс, запятая и минус*/
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8
                && (e.KeyChar != 44 || TextBoxXCoordinate.Text.IndexOf(",") > -1)
                && (e.KeyChar != 45 || TextBoxXCoordinate.Text.IndexOf("-") > -1))
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
            /*Нельзя вводить первой запятую*/
            if (TextBoxXCoordinate.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
            /*Нельзя вводить не первым знак минус*/
            if (TextBoxYCoordinate.TextLength > 0 && e.KeyChar == 45)
                e.Handled = true;
        }
        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            /*Передаем данные в буфер*/
            GlobalConst.Buffer[0] = TextBoxXCoordinate.Text;
            GlobalConst.Buffer[1] = TextBoxYCoordinate.Text;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}