using System;

namespace Control_de_Productos_Mini_Market_SQL
{
    public class Category
    {
        public int CategoryId { get; private set; }
        public string Name { get; private set; }

        private Category()
        {
        }

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