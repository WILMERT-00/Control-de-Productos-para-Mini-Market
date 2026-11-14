using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Productos_para_Mini_Market_POO
{
    public class Brand
    {
        public string Name { get; private set; }

        public Brand (string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }

            Name = name.Trim().ToLower();
        }

        public void EditName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }

            Name = newName.Trim().ToLower();
        }
    }
}
