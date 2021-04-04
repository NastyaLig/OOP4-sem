
using System;
using System.Collections.Generic;
using System.Text;

namespace lab4
{

    // Абстрактная фабрика — это порождающий паттерн проектирования, который позволяет создавать семейства 
    // связанных объектов, не привязываясь к конкретным классам создаваемых объектов.



    public interface IDisciplineFactory
    {
        Lector createLector(string fullname, string auditory, string department);
        Discipline createDiscipline(string disciplineName, string semestr, string cours, string speciality, string numberOfLectures, string numberOfLabratoryExercises, string typeOfControl);
    }

    public class DisciplineFactory : IDisciplineFactory
    {
        public Lector createLector(string fullname, string auditory, string department)
        {
            Lector lector = new Lector();
            lector.fullname = fullname;
            lector.auditory = auditory;
            lector.department = department;
            return lector;
        }

        public Discipline createDiscipline(string disciplineName, string semestr, string cours, string speciality, string numberOfLectures, string numberOfLabratoryExercises, string typeOfControl)
        {
            Discipline discipline = new Discipline();
            discipline.disciplineName = disciplineName;
            discipline.semestr = semestr;
            discipline.cours = cours;
            discipline.speciality = speciality;
            discipline.numberOfLectures = numberOfLectures;
            discipline.numberOfLabratoryExercises = numberOfLabratoryExercises;
            discipline.typeOfControl = typeOfControl;
            return discipline;
        }

        public Discipline createDiscipline(string disciplineName, string cours, string speciality, string numberOfLectures, string numberOfLabratoryExercises)
        {
            Discipline discipline = new Discipline();
            discipline.disciplineName = disciplineName;
            discipline.cours = cours;
            discipline.speciality = speciality;
            discipline.numberOfLectures = numberOfLectures;
            discipline.numberOfLabratoryExercises = numberOfLabratoryExercises;
            return discipline;
        }
    }
}