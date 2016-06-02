using System;
using System.Windows.Forms;
namespace Second
{
    public partial class AddMaterial : Form
    {
        public AddMaterial()
        {
            InitializeComponent();
            /*Передаем данные в буфер */
            GlobalConst.Buffer[0] = "";
            GlobalConst.Buffer[1] = "";
        }
        public AddMaterial(string Name,double Resistance)
        {
            InitializeComponent();
            /*Передаем данные в буфер */
            GlobalConst.Buffer[0] = Name;
            GlobalConst.Buffer[1] = Resistance.ToString();
            TextBoxName.Text = Name;
            TextBoxResistance.Text = Resistance.ToString();
        }
        private void TextBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
        }
        private void TextBoxResistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа, бэкспейс, запятая*/
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8
                && (e.KeyChar != 44 || TextBoxName.Text.IndexOf(",") > -1))
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
            /*Нельзя вводить запятую первой*/
            if (TextBoxName.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
        }
        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            /*Передаем данные в буфер*/
            GlobalConst.Buffer[0] = TextBoxName.Text;
            GlobalConst.Buffer[1] = TextBoxResistance.Text;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}