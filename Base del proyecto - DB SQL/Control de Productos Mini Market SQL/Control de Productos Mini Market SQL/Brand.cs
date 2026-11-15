using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Productos_Mini_Market_SQL
{
    public class Brand
    {
        public int BrandId { get; private set; }
        public string Name { get; private set; }

        private Brand()
        {
        }

        public Brand(string name)
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
