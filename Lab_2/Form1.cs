using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lab_2
{

    public partial class Form1 : Form
    {
        public bool completenessFlag = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisciplineCours.Items.Add("1 курс"); 
            DisciplineCours.Items.Add("2 курс");
            DisciplineCours.Items.Add("3 курс");
            DisciplineCours.Items.Add("4 курс");
            DisciplineSpeciality.Items.Add("ПОИТ"); 
            DisciplineSpeciality.Items.Add("ДЭВИ");
            DisciplineSpeciality.Items.Add("ПОИБМС"); 
            DisciplineSpeciality.Items.Add("ИСиТ");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (
                    LectorsFullname.Text.Length > 0 && LectorsDepartment.Text.Length > 0 &&
                    LectorsAuditory.Text.Length > 0 && DisciplineName.Text.Length > 0 &&
                    (ExamRadioBtn.Checked || OffsetRadioBtn.Checked) &&
                    DisciplineCours.Text.Length > 0 && DisciplineSpeciality.Text.Length > 0 &&
                    NumberOfLectures.Text.Length > 0 &&
                    NumberOfLabratoryExercises.Text.Length > 0 &&
                    (FirstSemestrRadioBtn.Checked || SecondSemestrRadioBtn.Checked) && LectorsAuditory.Text.Length == 5
                ) completenessFlag = true;

            if (completenessFlag)
            {
                Lector lector = new Lector();
                Discipline discipline = new Discipline();
                lector.fullname = LectorsFullname.Text;
                lector.department = LectorsDepartment.Text;
                lector.auditory = LectorsAuditory.Text;
                discipline.disciplineName = DisciplineName.Text;
                foreach (RadioButton rb in SemestrPanel.Controls)
                {
                    if (rb.Checked) discipline.semestr = rb.Text;
                }
                discipline.cours = DisciplineCours.Text;
                discipline.speciality = DisciplineSpeciality.Text;
                discipline.numberOfLectures = NumberOfLectures.Text;
                discipline.numberOfLabratoryExercises = NumberOfLabratoryExercises.Text;
                foreach (RadioButton rb in TypeOfControlPanel.Controls)
                {
                    if (rb.Checked) discipline.typeOfControl = rb.Text;
                }

                Serialize(lector, discipline);
            }
            else
            {
                MessageBox.Show(
                    "Вы не заполнили все необходимые поля!",
                    "Сообщение об ошибке",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            List<DisciplineForSerialize> disciplinesForSerialize = Deserialize();
            DisplayArea.Text = "";
            foreach (DisciplineForSerialize discipline in disciplinesForSerialize)
            {
                DisplayArea.Text += $"" +
                $"Название дисциплины: {discipline.Discipline.disciplineName} \r\n" +
                $"Cеместр: {discipline.Discipline.semestr} \r\n" +
                $"Курс: {discipline.Discipline.cours} \r\n" +
                $"Специальность: {discipline.Discipline.speciality} \r\n" +
                $"Вид Контроля: {discipline.Discipline.typeOfControl} \r\n" +
                $"Количество лекций: {discipline.Discipline.numberOfLectures} \r\n" +
                $"Количество лабораторных: {discipline.Discipline.numberOfLabratoryExercises} \r\n" +
                $"Лектор: \r\n" +
                $"ФИО: {discipline.Lector.fullname} \r\n" +
                $"Кафедра: {discipline.Lector.department} \r\n" +
                $"Аудитория: {discipline.Lector.auditory} \r\n \r\n \r\n";
            }
        }

        public void Serialize(Lector lector, Discipline discipline)
        {
            DisciplineForSerialize serializeMe = new DisciplineForSerialize(lector, discipline);
            List<DisciplineForSerialize> disciplinesForSerialize;
            if (File.Exists("data.xml")) disciplinesForSerialize = Deserialize();
            else disciplinesForSerialize = new List<DisciplineForSerialize>();
            disciplinesForSerialize.Add(serializeMe);
            XmlSerializer xmlf = new XmlSerializer(disciplinesForSerialize.GetType());

            using (FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate))
            {
                xmlf.Serialize(fs, disciplinesForSerialize);
            }
        }

        public List<DisciplineForSerialize> Deserialize()
        {
            List<DisciplineForSerialize> disciplinesForSerialize = new List<DisciplineForSerialize>();

            XmlSerializer xmlf = new XmlSerializer(disciplinesForSerialize.GetType());

            using (FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate))
            {
                disciplinesForSerialize = (List<DisciplineForSerialize>)xmlf.Deserialize(fs);
            }

            return disciplinesForSerialize;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length == 0) textBox.BackColor = Color.Red;
            else textBox.BackColor = Color.White;
        }

        private void LettersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            TextBox textbox = (TextBox)sender;
            if (!Char.IsLetter(number)) e.Handled = true;
            if (number == (char)Keys.Back || (number == (char)Keys.Space && textbox.Text.Length > 0)) e.Handled = false;
        }

        private void DigitsOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            TextBox textbox = (TextBox)sender;
            if (!Char.IsDigit(number)) e.Handled = true;
            if (!(textbox.Text.Length <= 1)) e.Handled = true;
            if (number == (char)Keys.Back) e.Handled = false;
        }

        private void Auditory_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            TextBox textbox = (TextBox)sender;
            if (!Char.IsDigit(number)) e.Handled = true;
            if (Char.IsDigit(number) && (textbox.Text.Length < 3 || (textbox.Text.Length > 3 && textbox.Text.Length < 5))) e.Handled = false;
            else e.Handled = true;
            if (number == (char)Keys.Back) e.Handled = false;
            if (number == '-' && textbox.Text.Length == 3) e.Handled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    public class Lector
    {
        public string fullname;
        public string department;
        public string auditory;
    }
    public class Discipline
    {
        public string disciplineName;
        public string semestr;
        public string cours;
        public string speciality;
        public string numberOfLectures;
        public string numberOfLabratoryExercises;
        public string typeOfControl;
    }

    [Serializable]
    public class DisciplineForSerialize
    {
        public Lector Lector { get; set; }
        public Discipline Discipline { get; set; }

        public DisciplineForSerialize()
        {

        }

        public DisciplineForSerialize(Lector lector, Discipline discipline)
        {
            Lector = lector;
            Discipline = discipline;
        }
    }
}