using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Productos_para_Mini_Market_POO
{
    public class Product
    {
        public string Name { get; private set; }
        public int Stock { get; private set; }

        public Category Category { get; private set; }
        public Brand Brand { get; private set; }

        public Product(string name, int stock, Category category, Brand brand)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("El nombre no puede estar vacio");
            }

            if(stock < 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor que cero.");
            }

            if(category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            Name = name.Trim().ToLower();
            Stock = stock;
            Category = category;
            Brand = brand;
        }

        public Product()
        {
            Name = string.Empty;
            Stock = 0;
        }

        public void EditName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("El nombre no puede estar vacio");
                return;
            }

            Name = newName.Trim().ToLower();
        }
        public void AddStock(int amount)
        {
            if(amount <= 0)
            {
                Console.WriteLine("La cantidad debe ser mayor que cero.");
                return;
            }

            Stock += amount;
        }

        public void ReduceStock(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("La cantidad debe ser mayor que cero.");
                return;
            }
            if (amount > Stock)
            {
                Console.WriteLine("No puedes restar una cantidad mayor a la disponible.");
                return;
            }

            Stock -= amount;            
        }
        public void EditCategory(Category newCategory)
        {
            if(newCategory == null)
            {
                Console.WriteLine("La categoría no puede ser nula.");
                return;
            }

            Category = newCategory;
        }
        public void EditBrand (Brand newBrand)
        {
            if(newBrand == null)
            {
                Console.WriteLine("La marca no puede ser nula.");
                return;
            }

            Brand = newBrand;
        }
    }
}