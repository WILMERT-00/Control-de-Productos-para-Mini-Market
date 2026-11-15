using Control_de_Productos_Mini_Market_SQL;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

InventoryManager inventory = new InventoryManager();
InventoryApplication program = new InventoryApplication();
program.Start(inventory);

public class InventoryApplication
{
    public void Start(InventoryManager inventory)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n===== CONTROL DE PRODUCTOS =====\n");
            Console.WriteLine("1. Registrar producto");
            Console.WriteLine("2. Registrar categoria");
            Console.WriteLine("3. Registrar marca");
            Console.WriteLine("4. Agregar stock");
            Console.WriteLine("5. Reducir stock");
            Console.WriteLine("6. Eliminar producto");
            Console.WriteLine("7. Consultar productos");
            Console.WriteLine("8. Actualizar informacion");
            Console.WriteLine("9. Agrupar por categoria");
            Console.WriteLine("10. Agrupar por marca");
            Console.WriteLine("11. Listar registros");
            Console.WriteLine("12. Salir");

            Console.WriteLine("Selecciona una opcion: ");
            int select_option;

            if (!int.TryParse(Console.ReadLine(), out select_option))
            {
                Console.WriteLine("ERROR: Debes introducir un numero.");
                continue;
            }
            if (select_option < 1 || select_option > 12)
            {
                Console.WriteLine("ERROR: Selecciona una opcion entre 1 y 12.");
                continue;
            }
            if (select_option == 12)
            {
                exit = true;
            }

            switch (select_option)
            {
                case 1:
                    RegisterProduct(inventory);
                    break;
                case 2:
                    RegisterCategory(inventory);
                    break;
                case 3:
                    RegisterBrand(inventory);
                    break;
                case 4:
                    ManageStock(inventory, true);
                    break;
                case 5:
                    ManageStock(inventory, false);
                    break;
                case 6:
                    DeleteProduct(inventory);
                    break;
                case 7:
                    SearchProducts(inventory);
                    break;
                case 8:
                    UpdateInformation(inventory);
                    break;
                case 9:
                    GroupByCategory(inventory);
                    break;
                case 10:
                    GroupByBrand(inventory);
                    break;
                case 11:
                    ListAll(inventory);
                    break;
                case 12:
                    exit = true;
                    Console.WriteLine("Programa finalizado.");
                    break;
                default:
                    Console.WriteLine("Selecciona una opcion entre 1 y 12.");
                    break;
            }
        }
    }
    private static string ReadText(string message)
    {
        Console.Write(message);
        return (Console.ReadLine() ?? "").Trim();
    }

    private static int ReadInt(string message)
    {
        int number;

        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out number))
                return number;

            Console.WriteLine("ERROR: Debes ingresar un numero.");
        }
    }

    private void RegisterProduct(InventoryManager inventory)
    {
        Console.WriteLine("\n--- REGISTRAR PRODUCTO ---");

        Console.Write("Nombre del producto: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Stock inicial: ");
        int stock;

        if (!int.TryParse(Console.ReadLine(), out stock))
        {
            Console.WriteLine("Debes introducir un numero.");
            return;
        }

        Console.Write("Categoria: ");
        string category = Console.ReadLine() ?? "";

        Console.Write("Marca: ");
        string brand = Console.ReadLine() ?? "";

        if (inventory.RegisterProduct(name, stock, category, brand))
        {
            Console.WriteLine("Producto registrado correctamente.");
        }
        else
        {
            Console.WriteLine("No se pudo registrar el producto.");
        }
    }

    private static void RegisterCategory(InventoryManager inventory)
    {
        Console.WriteLine("\n--- REGISTRAR CATEGORIA ---");

        string name = ReadText("Nombre de la categoria: ");

        if (inventory.RegisterCategory(name))
            Console.WriteLine("Categoria registrada correctamente.");
        else
            Console.WriteLine("Nombre invalido o categoria ya existente.");
    }

    private static void RegisterBrand(InventoryManager inventory)
    {
        Console.WriteLine("\n--- REGISTRAR MARCA ---");

        string name = ReadText("Nombre de la marca: ");

        if (inventory.RegisterBrand(name))
            Console.WriteLine("Marca registrada correctamente.");
        else
            Console.WriteLine("Nombre invalido o marca ya existente.");
    }

    private static void ManageStock(InventoryManager inventory, bool addStock)
    {
        string name = ReadText("\nNombre del producto: ");
        int amount = ReadInt("Cantidad: ");

        bool result;

        if (addStock)
            result = inventory.AddProductStock(name, amount);
        else
            result = inventory.RemoveProductStock(name, amount);

        if (result)
            Console.WriteLine("Stock actualizado correctamente.");
        else
            Console.WriteLine("No se pudo actualizar el stock. Verifica los datos.");
    }

    private static void DeleteProduct(InventoryManager inventory)
    {
        Console.WriteLine("\n--- ELIMINAR PRODUCTO ---");

        string name = ReadText("Nombre del producto: ");

        Product? product = inventory.FindProduct(name);

        if (product == null)
        {
            Console.WriteLine("El producto no existe.");
            return;
        }

        Console.WriteLine(
            $"Producto: {product.Name} | Stock: {product.Stock}");

        string confirmation = ReadText("¿Deseas eliminarlo? s / n: ");

        if (confirmation.ToLower() == "s")
        {
            inventory.DeleteProduct(name);
            Console.WriteLine("Producto eliminado correctamente.");
        }
        else
        {
            Console.WriteLine("Eliminacion cancelada.");
        }
    }

    private static void SearchProducts(InventoryManager inventory)
    {
        Console.WriteLine("\n--- CONSULTAR PRODUCTOS ---");
        Console.WriteLine("1. Buscar por nombre");
        Console.WriteLine("2. Productos sin existencias");
        Console.WriteLine("3. Productos con existencias");

        int option = ReadInt("Selecciona una opcion: ");

        switch (option)
        {
            case 1:
                string name = ReadText("Nombre del producto: ");

                Product? product = inventory.FindProduct(name);

                if (product != null)
                    PrintProduct(product);
                else
                    Console.WriteLine("Producto no encontrado.");

                break;

            case 2:
                PrintProducts(inventory.GetProductsWithoutStock());
                break;

            case 3:
                PrintProducts(inventory.GetProductsWithStock());
                break;

            default:
                Console.WriteLine("Opcion no disponible.");
                break;
        }
    }

    private static void UpdateInformation(InventoryManager inventory)
    {
        Console.WriteLine("\n--- ACTUALIZAR INFORMACION ---");
        Console.WriteLine("1. Nombre del producto");
        Console.WriteLine("2. Stock del producto");
        Console.WriteLine("3. Categoria del producto");
        Console.WriteLine("4. Marca del producto");
        Console.WriteLine("5. Nombre de una categoria");
        Console.WriteLine("6. Nombre de una marca");

        int option = ReadInt("Selecciona una opcion: ");

        string oldName;
        string newName;
        bool result = false;

        switch (option)
        {
            case 1:
                oldName = ReadText("Nombre actual del producto: ");
                newName = ReadText("Nuevo nombre: ");

                result = inventory.UpdateProductName(oldName, newName);
                break;

            case 2:
                oldName = ReadText("Nombre del producto: ");
                int newStock = ReadInt("Nuevo stock: ");

                result = inventory.UpdateProductStock(oldName, newStock);
                break;

            case 3:
                oldName = ReadText("Nombre del producto: ");
                newName = ReadText("Nueva categoria: ");

                result = inventory.UpdateProductCategory(oldName, newName);
                break;

            case 4:
                oldName = ReadText("Nombre del producto: ");
                newName = ReadText("Nueva marca: ");

                result = inventory.UpdateProductBrand(oldName, newName);
                break;

            case 5:
                oldName = ReadText("Categoria actual: ");
                newName = ReadText("Nuevo nombre: ");

                result = inventory.UpdateCategory(oldName, newName);
                break;

            case 6:
                oldName = ReadText("Marca actual: ");
                newName = ReadText("Nuevo nombre: ");

                result = inventory.UpdateBrand(oldName, newName);
                break;

            default:
                Console.WriteLine("Opcion no disponible.");
                return;
        }

        if (result)
            Console.WriteLine("Informacion actualizada correctamente.");
        else
            Console.WriteLine("No se pudo actualizar. Verifica los datos.");
    }

    private static void GroupByCategory(InventoryManager inventory)
    {
        Console.WriteLine("\n--- AGRUPAR POR CATEGORIA ---");

        string category = ReadText("Nombre de la categoria: ");

        if (inventory.FindCategory(category) == null)
        {
            Console.WriteLine("La categoria no existe.");
            return;
        }

        List<Product> results =
            inventory.GetProductsByCategory(category);

        PrintProducts(results);
    }

    private static void GroupByBrand(InventoryManager inventory)
    {
        Console.WriteLine("\n--- AGRUPAR POR MARCA ---");

        string brand = ReadText("Nombre de la marca: ");

        if (inventory.FindBrand(brand) == null)
        {
            Console.WriteLine("La marca no existe.");
            return;
        }

        List<Product> results =
            inventory.GetProductsByBrand(brand);

        PrintProducts(results);
    }

    private static void ListAll(InventoryManager inventory)
    {
        Console.WriteLine("\n--- PRODUCTOS ---");

        PrintProducts(inventory.GetAllProducts());

        Console.WriteLine("\n--- CATEGORIAS ---");

        List<Category> categories = inventory.GetAllCategories();

        if (categories.Count == 0)
            Console.WriteLine("No hay categorias registradas.");

        foreach (Category category in categories)
            Console.WriteLine(category.Name);

        Console.WriteLine("\n--- MARCAS ---");

        List<Brand> brands = inventory.GetAllBrands();

        if (brands.Count == 0)
            Console.WriteLine("No hay marcas registradas.");

        foreach (Brand brand in brands)
            Console.WriteLine(brand.Name);
    }

    private static void PrintProducts(List<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No se encontraron productos.");
            return;
        }

        foreach (Product product in products)
            PrintProduct(product);
    }

    private static void PrintProduct(Product product)
    {
        Console.WriteLine(
            $"Producto: {product.Name} | " +
            $"Stock: {product.Stock} | " +
            $"Categoria: {product.Category.Name} | " +
            $"Marca: {product.Brand.Name}");
    }
}