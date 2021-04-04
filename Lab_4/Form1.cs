
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace lab4
{

    public partial class Form1 : Form
    {
        ApplicationParameters form1Params;
        public bool completenessFlag = false;
        private Form1() //использование паттерна Singleton, это не позволяет создать второй объект Form1 с помощью new
        {
            InitializeComponent();
        }

        // Объект одиночки храниться в статичном поле класса.
        private static Form1 _instance;

        // Это статический метод, управляющий доступом к экземпляру одиночки.
        // При первом запуске, он создаёт экземпляр одиночки и помещает его в
        // статическое поле. При последующих запусках, он возвращает клиенту
        // объект, хранящийся в статическом поле.
        public static Form1 GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Form1();
            }
            return _instance;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisciplineCours.Items.Add("1 курс"); DisciplineCours.Items.Add("2 курс");
            DisciplineCours.Items.Add("3 курс"); DisciplineCours.Items.Add("4 курс");
            DisciplineSpeciality.Items.Add("ПОИТ"); DisciplineSpeciality.Items.Add("ДЭВИ");
            DisciplineSpeciality.Items.Add("ПОИБМС"); DisciplineSpeciality.Items.Add("ИСиТ");
            form1Params = ApplicationParameters.GetInstance();
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
                DisciplineFactory disFactory = new DisciplineFactory();

                LectorBuilder lectorBuilder = new LectorBuilder();
                LectorDirector lectorDirector = new LectorDirector(lectorBuilder);
                lectorDirector.makeStandartLector(LectorsFullname.Text, LectorsAuditory.Text, LectorsDepartment.Text);
                Lector lector = lectorBuilder.getLector();

                //Lector lector = disFactory.createLector(LectorsFullname.Text, LectorsAuditory.Text, LectorsDepartment.Text);
                Discipline discipline = disFactory.createDiscipline(DisciplineName.Text, DisciplineCours.Text, DisciplineSpeciality.Text, NumberOfLectures.Text, NumberOfLabratoryExercises.Text);
                foreach (RadioButton rb in SemestrPanel.Controls)
                {
                    if (rb.Checked) discipline.semestr = rb.Text;
                }
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
            MessageBox.Show(
                    $"{form1Params.height} - высота основной формы",
                    "Сообщение об ошибке",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
            );
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

        private void FirstSemestrRadioBtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DisciplineSpeciality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class Lector : ILectorPrototype
    {
        public string fullname;
        public string department;
        public string auditory;

        public Lector Clone()
        {
            var lector = new Lector();
            lector = (Lector)this.MemberwiseClone();
            return lector;
        }
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
