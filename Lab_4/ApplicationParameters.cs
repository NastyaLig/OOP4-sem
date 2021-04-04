
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace lab4
{
    class ApplicationParameters
    {
        Form1 mainForm = Form1.GetInstance();
        public Color bgColor;
        public Font font;
        public int height;
        public int width;

        private ApplicationParameters()
        {
            bgColor = mainForm.BackColor;
            font = mainForm.Font;
            height = mainForm.Height;
            width = mainForm.Width;
        }

        private static ApplicationParameters _instance;

        // Это статический метод, управляющий доступом к экземпляру одиночки.
        // При первом запуске, он создаёт экземпляр одиночки и помещает его в
        // статическое поле. При последующих запусках, он возвращает клиенту
        // объект, хранящийся в статическом поле.
        public static ApplicationParameters GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ApplicationParameters();
            }
            return _instance;
        }
    }
}