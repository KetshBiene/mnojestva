using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mnojestva
{
    public partial class Form1 : Form
    {
        static bool auto = false;
        static Random rnd = new Random();
        static string[] alphabet = new string[] {"A","B","C","D","E","F","G",
                "H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
        
        double[] A_affiliations;
        double[] B_affiliations;

        int ALength;
        int BLength;

        string[] elementsOfA;
        string[] elementsOfB;

        public Form1()
        {
            InitializeComponent();
        }

        void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        static double[] Randomize(int _sum)
        {
            Random rand = new Random();

            List<double> randNumbers = new List<double>();
            double sum = _sum;
            double temp = sum;
            double randNum;

            while (sum != 0)
            {
                randNum = (double)rand.Next(1, 11) / 10;
                temp -= randNum;
                if (temp > 0)
                {
                    sum -= randNum;
                    randNumbers.Add(Math.Round(randNum, 1));
                }
                else
                {
                    randNumbers.Add(Math.Round(sum, 1));
                    break;
                }
            }

            double[] arr = new double[randNumbers.Count];

            for (int i = 0; i < randNumbers.Count; i++)
            {
                arr[i] = randNumbers[i];
            }
            return arr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int operation = comboBox1.SelectedIndex;
            if (operation == -1) { MessageBox.Show("Выберите операцию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            if (!auto) 
            { //Ручной ввод
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
                { MessageBox.Show("Какая-то из строк ввода мноежства А или В пуста", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                elementsOfA = textBox1.Text.Split(' ');
                string[] Aaff = textBox2.Text.Split(' ');

                elementsOfB = textBox3.Text.Split(' ');
                string[] Baff = textBox4.Text.Split(' ');

                if (elementsOfA.Length != Aaff.Length) { MessageBox.Show("Число членов массива А не совпадает с числом значений. Проверьте, не стоит ли два пробела подрряд", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (elementsOfB.Length != Baff.Length) { MessageBox.Show("Число членов массива B не совпадает с числом значений. Проверьте, не стоит ли два пробела подрряд", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                A_affiliations = new double[Aaff.Length];
                B_affiliations = new double[Baff.Length];

                try
                {
                    for (int i = 0; i < Aaff.Length; i++) A_affiliations[i] = Convert.ToDouble(Aaff[i]);
                    for (int i = 0; i < Baff.Length; i++) B_affiliations[i] = Convert.ToDouble(Baff[i]);
                }
                catch (FormatException) { MessageBox.Show("Степени принадлежности должны быть представлены в виде вещественного числа, а дробная часть должна отделяться точкой. " +
                    "Проверьте, не стоит ли два пробела подрряд", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                ALength = A_affiliations.Length;
                BLength = B_affiliations.Length;
            }
            Clear();

            Set A = new Set(elementsOfA, A_affiliations);
            Set B = new Set(elementsOfB, B_affiliations);

            double SumA = 0;
            double SumB = 0;

            for (int i = 0; i < ALength; i++)
                SumA += A.affiliations[i];

            for (int i = 0; i < BLength; i++)
                SumB += B.affiliations[i];

            for (int i = 0; i < ALength; i++)
            {
                textBox1.Text += A.elements[i];
                textBox2.Text += A.affiliations[i];
                if (i < ALength - 1) 
                {
                    textBox1.Text += " ";
                    textBox2.Text += " ";
                }
            }

            for (int i = 0; i < BLength; i++)
            {
                textBox3.Text += B.elements[i];
                textBox4.Text += B.affiliations[i];
                if (i < BLength - 1)
                {
                    textBox3.Text += " ";
                    textBox4.Text += " ";
                }
            }

            //textBox5.Text = SumA.ToString();
            //textBox6.Text = SumB.ToString();
            Set C = null;
            if (operation == 0) { C = A.Union(B); }
            if (operation == 1) { C = A.Intersect(B); }
            if (operation == 2) { C = A.Difference(B); }
            if (operation == 3) { C = B.Difference(A); }
            if (operation == 4) { C = A.Symmetricdiff(B); }
            if (operation == 5) { C = A.Extension(); }
            if (operation == 6) { C = B.Extension(); }

            for (int i = 0; i < C.elements.Length; i++)
            {
                textBox5.Text += C.elements[i] + " ";
                textBox6.Text += C.affiliations[i] + " ";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                auto = true;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;

                int ASum = rnd.Next(1, 9);
                int BSum = 10 - ASum;
                Clear();

                A_affiliations = Randomize(ASum);
                B_affiliations = Randomize(BSum);

                ALength = A_affiliations.Length;
                BLength = B_affiliations.Length;

                elementsOfA = new string[ALength];
                elementsOfB = new string[BLength];

                for (int i = 0; i < ALength;)
                {
                    string element = alphabet[rnd.Next(alphabet.Length)];
                    if (elementsOfA.Contains(element))
                        continue;
                    elementsOfA[i] = element;
                    textBox1.Text += element;
                    textBox2.Text += A_affiliations[i];
                    if (i < ALength - 1)
                    {
                        textBox1.Text += " ";
                        textBox2.Text += " ";
                    }
                    i++;
                }

                for (int i = 0; i < BLength;)
                {
                    string element = alphabet[rnd.Next(alphabet.Length)];
                    if (elementsOfB.Contains(element))
                        continue;
                    elementsOfB[i] = element;
                    textBox3.Text += element;
                    textBox4.Text += B_affiliations[i];
                    if (i < BLength - 1)
                    {
                        textBox3.Text += " ";
                        textBox4.Text += " ";
                    }
                    i++;
                }

            }
            else
            {
                auto = false;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Заполнение всех элементов массива производится в соответствующих строках через пробел. Избегайте повторения членов в пределах одного множетсва. Нельзя ставить два пробела подряд. " +
                "Дробная часть степеней принадлежности отделяется точкой", "Информация", MessageBoxButtons.OK,  MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
