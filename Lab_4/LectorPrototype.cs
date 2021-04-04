using System;
using System.Collections.Generic;
using System.Text;

namespace lab4
{
    public interface ILectorPrototype // Прототип — это порождающий паттерн проектирования, который позволяет копировать объекты, не вдаваясь в подробности их реализации.
    {
        Lector Clone();
    }

}