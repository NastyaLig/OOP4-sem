
using System;
using System.Collections.Generic;
using System.Text;

namespace lab4
{
    // Строитель — это порождающий паттерн проектирования, который позволяет создавать сложные объекты пошагово. 
    // Строитель даёт возможность использовать один и тот же код строительства для получения разных представлений объектов.
    public interface ILectorBuilder
    {
        void AddFullname(string type);
        void AddAuditory(string type);
        void AddDepartment(string type);
        void reset();
    }

    public class LectorBuilder : ILectorBuilder
    {

        private Lector _lector = new Lector();

        public void AddFullname(string type)
        {
            _lector.fullname = type;
        }
        public void AddAuditory(string type)
        {
            _lector.auditory = type;
        }
        public void AddDepartment(string type)
        {
            _lector.department = type;
        }
        public Lector getLector()
        {
            return _lector;
        }
        public void reset()
        {
            _lector = new Lector();
        }
    }

    public class LectorDirector
    {
        private ILectorBuilder _lectorBuilder;
        public LectorDirector(ILectorBuilder lectorBuilder)
        {
            _lectorBuilder = lectorBuilder;
        }

        public void makeStandartLector(string fullname, string auditory, string department)
        {
            _lectorBuilder.reset();
            _lectorBuilder.AddFullname(fullname);
            _lectorBuilder.AddAuditory(auditory);
            _lectorBuilder.AddDepartment(department);
        }

        public void makeEmptyLector()
        {
            _lectorBuilder.reset();
        }
    }

}