using System;

namespace Control_de_Productos_para_Mini_Market_POO
{
    public class Category
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("El nombre no puede estar vacio.");
            }
            Name = name.Trim().ToLower();
        }
        public void EditName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("El nombre no puede estar vacio.");
                return;
            }

            Name = newName.Trim().ToLower();
        }
    }
}