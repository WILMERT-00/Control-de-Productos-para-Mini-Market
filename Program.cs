List<string> categories = new List<string>();
List<string> brands = new List<string>();

Dictionary<string, int> inventory_products = new();

bool exit = false;

while (!exit)
{
    try
    {
        Console.WriteLine("CONTROL DE PRODUCTOS\n");

        Console.WriteLine("Lista de opciones:\n");
        Console.WriteLine("1. Registrar productos");
        Console.WriteLine("2. Registrar categorias");
        Console.WriteLine("3. Registrar marcas");
        Console.WriteLine("4. Agregar productos");
        Console.WriteLine("5. Eliminar productos");
        Console.WriteLine("6. Consultar existencias");
        Console.WriteLine("7. Actualizar informacion");
        Console.WriteLine("8. Listar registros");
        Console.WriteLine("9. Salir del programa\n");

        int select_option;
        Console.WriteLine("Selecciona una opcion: ");

        if (!int.TryParse(Console.ReadLine(), out select_option))
        {
            Console.WriteLine("ERROR: Debes introducir un numero.");
            continue;
        }
        if (select_option < 1 || select_option > 9)
        {
            Console.WriteLine("ERROR: Selecciona una opcion entre 1 y 9.");
            continue;
        }
        if (select_option == 9)
        {
            exit = true;
        }

        int counter = 0;
        string? advance;

        switch (select_option)
        {
            case 1:
                Console.WriteLine($"\nHaz seleccionado la opcion {select_option}: REGISTRAR PRODUCTOS\n");
                bool exit_1 = false;

                while (!exit_1)
                {
                    try
                    {
                        Console.Write("Ingresa el nombre del producto que quieres agregar: ");
                        string name_product = (Console.ReadLine() ?? "").Trim().ToLower();
                        if (string.IsNullOrWhiteSpace(name_product))
                        {
                            Console.WriteLine("El nombre no puede estar vacio");
                            continue;
                        }
                        if (!inventory_products.ContainsKey(name_product))
                        {
                            inventory_products.Add(name_product, 0);
                            Console.WriteLine($"\nHas registrado \"{name_product}\" en la lista de productos\n");
                        }
                        else
                        {
                            Console.WriteLine("El producto ya esta registrado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"No se pudo registrar el producto: {ex.Message}");
                    }

                    while (true)
                    {
                        Console.WriteLine("¿Deseas agregar algun otro producto? s / n");
                        advance = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (advance == "s")
                        {
                            break;
                        }
                        else if (advance == "n")
                        {
                            exit_1 = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Debes ingresar \"s\" o \"n\"");
                            continue;
                        }
                    }

                    counter++;
                }
                break;
            case 2:
                Console.WriteLine($"\nHaz seleccionado la opcion {select_option}: REGISTRAR CATEGORIAS\n");
                bool exit_2 = false;

                while (!exit_2)
                {
                    Console.Write("Ingresa el nombre de la categoria que quieres agregar: ");
                    string new_categorie = (Console.ReadLine() ?? "").Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(new_categorie))
                    {
                        Console.WriteLine("El nombre no puede estar vacio.");
                        continue;
                    }
                    if (categories.Contains(new_categorie))
                    {
                        Console.WriteLine("Esa categoria ya esta registrada.");
                        continue;
                    }
                    categories.Add(new_categorie);
                    Console.WriteLine($"\nHas registrado \"{new_categorie}\" a la lista de categorias\n");

                    while (true)
                    {
                        Console.WriteLine("¿Deseas agregar otra categoria? s / n");
                        advance = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (advance == "s")
                        {
                            break;
                        }
                        else if (advance == "n")
                        {
                            exit_2 = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Debes ingresar \"s\" o \"n\"");
                            continue;
                        }
                    }

                    counter++;
                }
                break;
            case 3:
                Console.WriteLine($"\nHaz seleccionado la opcion {select_option}: REGISTRAR MARCAS");
                bool exit_3 = false;

                while (!exit_3)
                {
                    Console.Write("Ingresa el nombre de la marca que quieres agregar: ");
                    string new_brands = (Console.ReadLine() ?? "").Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(new_brands))
                    {
                        Console.WriteLine("El nombre no puede estar vacio.");
                        continue;
                    }
                    if (brands.Contains(new_brands))
                    {
                        Console.WriteLine("Esa marca ya esta registrada.");
                        continue;
                    }
                    brands.Add(new_brands);

                    Console.WriteLine($"\nHas registrado \"{new_brands}\" a la lista de marcas\n");

                    while (true)
                    {
                        Console.WriteLine("¿Deseas agregar otra marca? s / n");
                        advance = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (advance == "s")
                        {
                            break;
                        }
                        else if (advance == "n")
                        {
                            exit_3 = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Debes ingresar \"s\" o \"n\"");
                            continue;
                        }
                    }

                    counter++;
                }
                break;
            case 4:
                bool exit_4 = false;
                Console.WriteLine($"\nHaz seleccionado la opcion {select_option}: AGREGAR PRODUCTOS");

                while (!exit_4)
                {
                    Console.WriteLine("Ingresa el producto que quieres agregar?");
                    string product_to_add = (Console.ReadLine() ?? "").Trim().ToLower();

                    if (inventory_products.ContainsKey(product_to_add))
                    {
                        Console.WriteLine("\n¿Que cantidad quieres agregar?");
                        int amount_to_add;

                        if (!int.TryParse(Console.ReadLine(), out amount_to_add))
                        {
                            Console.WriteLine("ERROR: Debes introducir un numero.");
                            continue;
                        }
                        if (amount_to_add <= 0)
                        {
                            Console.WriteLine("ERROR: La cantidad debe ser mayor que cero.");
                            continue;
                        }

                        inventory_products[product_to_add] += amount_to_add;

                        Console.WriteLine($"\nStock actualizado con exito!! Ahora hay {inventory_products[product_to_add]} unidades de {product_to_add}.");
                    }
                    else
                    {
                        Console.WriteLine("El producto que quieres agregar no esta registrado!!!!");
                    }

                    while (true)
                    {
                        Console.WriteLine("\n¿Quieres agregar otro producto? s / n");
                        advance = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (advance == "s")
                        {
                            break;
                        }
                        else if (advance == "n")
                        {
                            exit_4 = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Debes ingresar \"s\" o \"n\"");
                            continue;
                        }
                    }
                }
                break;
            case 5:
                bool exit_5 = false;

                while (!exit_5)
                {
                    if (inventory_products.Count == 0)
                    {
                        Console.WriteLine("No existen productos registrados.");
                        break;
                    }

                    Console.WriteLine("\n--- PRODUCTOS REGISTRADOS ---");

                    foreach (var product in inventory_products)
                    {
                        Console.WriteLine($"{product.Key} | {product.Value} unidades"
                        );
                    }

                    Console.WriteLine("\nIngresa el nombre del producto que deseas eliminar: ");
                    string product_to_delete = (Console.ReadLine() ?? "").Trim().ToLower();

                    if (inventory_products.ContainsKey(product_to_delete))
                    {
                        Console.WriteLine($"El producto tiene {inventory_products[product_to_delete]} unidades");

                        Console.WriteLine("¿Seguro que quieres eliminarlo? s / n");

                        advance = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (advance == "s")
                        {
                            inventory_products.Remove(product_to_delete);

                            Console.WriteLine("Producto eliminado correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("Eliminacion cancelada.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: El producto no existe.");
                    }
                    while (true)
                    {
                        Console.WriteLine("¿Deseas eliminar otro producto? s / n: ");
                        advance = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (advance == "s")
                        {
                            break;
                        }
                        else if (advance == "n")
                        {
                            exit_5 = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Debes ingresar \"s\" o \"n\"");
                            continue;
                        }
                    }
                }
                break;
            case 6:
                bool exit_6 = false;
                Console.WriteLine($"\nHaz seleccionado la opcion {select_option}: CONSULTAR EXISTENCIAS\n");

                while (true)
                {
                    Console.WriteLine("\nMENU DE BUSQUEDA");
                    Console.WriteLine("1. Buscar por nombre");
                    Console.WriteLine("2. Buscar productos sin existencias");
                    Console.WriteLine("3. Buscar productos con existencias");
                    Console.WriteLine("4. Volver al menu principal");

                    int select_option_6;

                    if (!int.TryParse(Console.ReadLine(), out select_option_6))
                    {
                        Console.WriteLine("ERROR: Debes introducir un numero.");
                        continue;
                    }
                    if (select_option_6 < 1 || select_option_6 > 4)
                    {
                        Console.WriteLine("ERROR: Selecciona una opcion entre 1 y 4.");
                        continue;
                    }
                    if (select_option_6 == 4)
                    {
                        break;
                    }

                    switch (select_option_6)
                    {
                        case 1:
                            exit_6 = false;

                            while (!exit_6)
                            {
                                Console.WriteLine("Ingresa el producto que quieres consultar: ");
                                string searched_product = (Console.ReadLine() ?? "").Trim().ToLower();

                                if (inventory_products.ContainsKey(searched_product))
                                {
                                    Console.WriteLine($"{searched_product} : {inventory_products[searched_product]} unidades.");
                                }
                                else
                                {
                                    Console.WriteLine("El producto ingresado no existe");
                                }

                                while (true)
                                {
                                    Console.WriteLine("\n¿Quieres consultar las existencias de otro producto? s / n");
                                    advance = (Console.ReadLine() ?? "").Trim().ToLower();

                                    if (advance == "s")
                                    {
                                        break;
                                    }
                                    else if (advance == "n")
                                    {
                                        exit_6 = true;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR: Debes ingresar \"s\" o \"n\"");
                                        continue;
                                    }
                                }
                            }
                            break;
                        case 2:
                            Console.WriteLine("\n-------Productos sin existencias-------\n");

                            bool found_without_stock = false;

                            foreach (var product in inventory_products)
                            {
                                if (product.Value == 0)
                                {
                                    Console.WriteLine(product.Key);
                                    found_without_stock = true;
                                }
                            }
                            if (!found_without_stock)
                            {
                                Console.WriteLine("No hay productos sin existencias");
                            }
                            break;
                        case 3:
                            Console.WriteLine("\n-------Productos con existencias-------\n");

                            bool found_with_stock = false;
                            foreach (var product in inventory_products)
                            {
                                if (product.Value > 0)
                                {
                                    Console.WriteLine($"{product.Key} | {product.Value} unidades");
                                    found_with_stock = true;
                                }
                            }
                            if (!found_with_stock)
                            {
                                Console.WriteLine("No hay productos con existencias");
                            }
                            break;
                    }
                }
                break;
            case 7:
                Console.WriteLine($"\nHaz seleccionado la opcion {select_option}: ACTUALIZAR INFORMACION");

                while (true)
                {
                    Console.WriteLine("\n------Actualizar informacion-----\n");
                    Console.WriteLine("1. Actualizar productos");
                    Console.WriteLine("2. Actualizar categorias");
                    Console.WriteLine("3. Actualizar marcas");
                    Console.WriteLine("4. Salir al menu principal");

                    int select_option_7;

                    if (!int.TryParse(Console.ReadLine(), out select_option_7))
                    {
                        Console.WriteLine("ERROR: Debes ingresar un numero.");
                        continue;
                    }
                    if (select_option_7 < 1 || select_option_7 > 4)
                    {
                        Console.WriteLine("ERROR: Ingrese un numero entre 1 y 4");
                        continue;
                    }
                    if (select_option_7 == 4)
                    {
                        break;
                    }

                    switch (select_option_7)
                    {
                        case 1:
                            Console.WriteLine($"Has seleccionado la opcion {select_option_7}: ACTUALIZAR PRODUCTOS");

                            Console.WriteLine("Este es el inventario actual:\n");
                            foreach (var product in inventory_products)
                            {
                                Console.WriteLine($"{product.Key} | {product.Value} und");
                            }

                            Console.WriteLine("Ingresa el producto que quieres actualizar");
                            string update_to_product = (Console.ReadLine() ?? "").Trim().ToLower();

                            if (inventory_products.ContainsKey(update_to_product))
                            {
                                Console.WriteLine("¿Que deseas actualizar?");
                                Console.WriteLine("1. Nombre");
                                Console.WriteLine("2. Stock");
                                int option;

                                if (!int.TryParse(Console.ReadLine(), out option))
                                {
                                    Console.WriteLine("ERROR: Debes ingresar un numero.");
                                    break;
                                }
                                if (option < 1 || option > 2)
                                {
                                    Console.WriteLine("Opcion no disponible.");
                                    break;
                                }

                                if (option == 1)
                                {
                                    string old_name = update_to_product;
                                    Console.Write("Ingresa el nuevo nombre del producto: ");
                                    string new_name = (Console.ReadLine() ?? "").Trim().ToLower();

                                    if (string.IsNullOrWhiteSpace(new_name))
                                    {
                                        Console.WriteLine("ERROR: El nombre no puede estar vacio.");
                                        break;
                                    }
                                    if (inventory_products.ContainsKey(new_name))
                                    {
                                        Console.WriteLine("ERROR: Ya existe un producto con ese nombre.");
                                        break;
                                    }

                                    int amount = inventory_products[update_to_product];

                                    inventory_products.Remove(update_to_product);

                                    inventory_products.Add(new_name, amount);

                                    Console.WriteLine($"Has actualizado con exito el producto: \"{old_name}\" por \"{new_name}\"");
                                }
                                else if (option == 2)
                                {
                                    Console.Write("Ingresa el nuevo stock disponible del producto: ");
                                    int new_amount;

                                    if (!int.TryParse(Console.ReadLine(), out new_amount))
                                    {
                                        Console.WriteLine("ERROR: Debes ingresar un valor numerico.");
                                        break;
                                    }
                                    if (new_amount < 0)
                                    {
                                        Console.WriteLine("ERROR: El valor ingresado no puede ser negativo.");
                                        break;
                                    }
                                    inventory_products[update_to_product] = new_amount;

                                    Console.WriteLine($"Has actualizado con exito el stock del producto: \"{update_to_product}\" por \"{inventory_products[update_to_product]}\"");
                                }
                                else
                                {
                                    Console.Write("La opcion que ingresaste no esta disponible");
                                }
                            }
                            break;
                        case 2:
                            Console.WriteLine($"Has seleccionado la opcion {select_option_7}: ACTUALIZAR CATEGORIAS");

                            Console.WriteLine("Que categoria deseas actualizar:");
                            string old_categorie = (Console.ReadLine() ?? "").Trim().ToLower();

                            int position = categories.IndexOf(old_categorie);

                            if (position != -1)
                            {
                                Console.WriteLine("Escribe el nuevo nombre para esta categoria: ");
                                string new_categorie = (Console.ReadLine() ?? "").Trim().ToLower();
                                if (string.IsNullOrWhiteSpace(new_categorie))
                                {
                                    Console.WriteLine("El nombre no puede estar vacio.");
                                    break;
                                }
                                if (categories.Contains(new_categorie))
                                {
                                    Console.WriteLine("Esa categoria ya existe.");
                                    break;
                                }
                                categories[position] = new_categorie;

                                Console.WriteLine($"!Listo!!! Has actualizado con exito el nombre de la categoria {old_categorie} por {new_categorie}");
                            }
                            else
                            {
                                Console.WriteLine("Lo siento, esa categoría no existe en la lista.");
                            }
                            break;
                        case 3:
                            Console.WriteLine($"Has seleccionado la opcion {select_option_7}: ACTUALIZAR MARCAS");

                            Console.WriteLine("Que marca deseas actualizar:");
                            string old_brand = (Console.ReadLine() ?? "").Trim().ToLower();

                            int position_brand = brands.IndexOf(old_brand);

                            if (position_brand != -1)
                            {
                                Console.WriteLine("Escribe el nuevo nombre para esta categoria: ");
                                string new_brand = (Console.ReadLine() ?? "").Trim().ToLower();

                                if (string.IsNullOrWhiteSpace(new_brand))
                                {
                                    Console.WriteLine("ERROR: El nombre no puede estar vacio.");
                                    break;
                                }
                                if (brands.Contains(new_brand))
                                {
                                    Console.WriteLine("ERROR: Esa marca ya existe.");
                                    break;
                                }
                                brands[position_brand] = new_brand;

                                Console.WriteLine($"!Listo!!! Has actualizado con exito el nombre de la marca {old_brand} por {new_brand}");
                            }
                            else
                            {
                                Console.WriteLine("Lo siento, esa marca no existe en la lista.");
                            }
                            break;
                    }
                }
                break;
            case 8:
                bool exit_8 = false;

                while (!exit_8)
                {
                    Console.WriteLine("\n--- Listar Registros ---\n");
                    Console.WriteLine("1. Listar productos");
                    Console.WriteLine("2. Listar categorias");
                    Console.WriteLine("3. Listar marcas");
                    Console.WriteLine("4. Listar todo");
                    Console.WriteLine("5. Volver al menu principal");

                    int list_option;
                    Console.WriteLine("Seleccione una opcion: ");

                    if (!int.TryParse(Console.ReadLine(), out list_option))
                    {
                        Console.WriteLine("ERROR: Debe ingresar un valor numerico.");
                        continue;
                    }
                    if (list_option < 1 || list_option > 5)
                    {
                        Console.WriteLine("ERROR: Ingrese un numero entre 1 y 5.");
                        continue;
                    }
                    if (list_option == 5)
                    {
                        break;
                    }

                    switch (list_option)
                    {
                        case 1:
                            Console.WriteLine("\n--- INVENTARIO DE PRODUCTOS ---");

                            if (inventory_products.Count == 0)
                            {
                                Console.WriteLine("No hay productos registrados.");
                            }
                            else
                            {
                                foreach (var product in inventory_products)
                                {
                                    Console.WriteLine($"{product.Key} | {product.Value} unidades");
                                }
                            }

                            break;
                        case 2:
                            Console.WriteLine("\n--- CATEGORIAS ---");

                            if (categories.Count == 0)
                            {
                                Console.WriteLine("No hay categorias registradas.");
                            }
                            else
                            {
                                foreach (string category in categories)
                                {
                                    Console.WriteLine(category);
                                }
                            }
                            break;
                        case 3:
                            Console.WriteLine("\n--- MARCAS ---");

                            if (brands.Count == 0)
                            {
                                Console.WriteLine("No hay marcas registradas.");
                            }
                            else
                            {
                                foreach (string brand in brands)
                                {
                                    Console.WriteLine(brand);
                                }
                            }
                            break;
                        case 4:
                            Console.WriteLine("\n--- INVENTARIO DE PRODUCTOS ---");
                            if (inventory_products.Count == 0)
                            {
                                Console.WriteLine("No hay productos registrados.");
                            }
                            else
                            {
                                foreach (var product in inventory_products)
                                {
                                    Console.WriteLine($"{product.Key} | {product.Value} unidades");
                                }
                            }

                            Console.WriteLine("\n--- CATEGORIAS ---");
                            if (categories.Count == 0)
                            {
                                Console.WriteLine("No hay categorias registradas.");
                            }
                            else
                            {
                                foreach (string category in categories)
                                {
                                    Console.WriteLine(category);
                                }
                            }                            

                            Console.WriteLine("\n--- MARCAS ---");
                            if (brands.Count == 0)
                            {
                                Console.WriteLine("No hay marcas registradas.");
                            }
                            else
                            {
                                foreach (string brand in brands)
                                {
                                    Console.WriteLine(brand);
                                }
                            }
                            
                            break;
                    }
                }

                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ha ocurrido un error inesperado.");
        Console.WriteLine($"Detalles técnicos: {ex.Message}");
    }    
}