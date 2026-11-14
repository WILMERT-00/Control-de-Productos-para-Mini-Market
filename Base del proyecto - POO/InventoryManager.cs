namespace Control_de_Productos_para_Mini_Market_POO
{
    public class InventoryManager
    {
        private List<Product> products = new List<Product>();
        private List<Category> categories = new List<Category>();
        private List<Brand> brands = new List<Brand>();

        public bool RegisterCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string normalizedName = name.Trim().ToLower();

            if (categories.Any(c => c.Name == normalizedName))
                return false;

            categories.Add(new Category(normalizedName));
            return true;
        }

        public bool RegisterBrand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string normalizedName = name.Trim().ToLower();

            if (brands.Any(b => b.Name == normalizedName))
                return false;

            brands.Add(new Brand(normalizedName));
            return true;
        }

        public bool RegisterProduct(string name, int stock, string categoryName, string brandName)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(brandName) || stock < 0)
            {
                return false;
            }

            string normalizedName = name.Trim().ToLower();
            string normalizedCategory = categoryName.Trim().ToLower();
            string normalizedBrand = brandName.Trim().ToLower();

            if (products.Any(p => p.Name == normalizedName))
                return false;

            Category? category = categories.FirstOrDefault(
                c => c.Name == normalizedCategory);

            Brand? brand = brands.FirstOrDefault(
                b => b.Name == normalizedBrand);

            if (category == null || brand == null)
            {
                return false;
            }

            Product newProduct = new Product(
                normalizedName,
                stock,
                category,
                brand);

            products.Add(newProduct);

            return true;
        }

        //BUSQUEDAS

        public Product? FindProduct(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string normalizedName = name.Trim().ToLower();

            return products.FirstOrDefault(
                p => p.Name == normalizedName);
        }

        public Category? FindCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string normalizedName = name.Trim().ToLower();

            return categories.FirstOrDefault(
                c => c.Name == normalizedName);
        }

        public Brand? FindBrand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string normalizedName = name.Trim().ToLower();

            return brands.FirstOrDefault(
                b => b.Name == normalizedName);
        }

        // AGREGAR Y REDUCIR STOCK

        public bool AddProductStock(string name, int amount)
        {
            Product? product = FindProduct(name);

            if (product == null || amount <= 0)
                return false;

            product.AddStock(amount);
            return true;
        }

        public bool RemoveProductStock(string name, int amount)
        {
            Product? product = FindProduct(name);

            if (product == null || amount <= 0 || amount > product.Stock)
                return false;

            product.ReduceStock(amount);
            return true;
        }

        // ACTUALIZAR PRODUCTOS

        public bool UpdateProductName(string oldName, string newName)
        {
            Product? product = FindProduct(oldName);

            if (product == null || string.IsNullOrWhiteSpace(newName))
                return false;

            string normalizedName = newName.Trim().ToLower();

            if (products.Any(p => p.Name == normalizedName && p != product))
                return false;

            product.EditName(normalizedName);
            return true;
        }

        public bool UpdateProductStock(string name, int newStock)
        {
            Product? product = FindProduct(name);

            if (product == null || newStock < 0)
                return false;

            if (newStock > product.Stock)
            {
                product.AddStock(newStock - product.Stock);
            }
            else if (newStock < product.Stock)
            {
                product.ReduceStock(product.Stock - newStock);
            }

            return true;
        }

        public bool UpdateProductCategory(string productName, string categoryName)
        {
            Product? product = FindProduct(productName);
            Category? category = FindCategory(categoryName);

            if (product == null || category == null)
                return false;

            product.EditCategory(category);
            return true;
        }

        public bool UpdateProductBrand(string productName, string brandName)
        {
            Product? product = FindProduct(productName);
            Brand? brand = FindBrand(brandName);

            if (product == null || brand == null)
                return false;

            product.EditBrand(brand);
            return true;
        }

        // ACTUALIZAR CATEGORIAS Y MARCAS

        public bool UpdateCategory(string oldName, string newName)
        {
            Category? category = FindCategory(oldName);

            if (category == null || string.IsNullOrWhiteSpace(newName))
                return false;

            string normalizedName = newName.Trim().ToLower();

            if (categories.Any(c => c.Name == normalizedName && c != category))
                return false;

            category.EditName(normalizedName);
            return true;
        }

        public bool UpdateBrand(string oldName, string newName)
        {
            Brand? brand = FindBrand(oldName);

            if (brand == null || string.IsNullOrWhiteSpace(newName))
                return false;

            string normalizedName = newName.Trim().ToLower();

            if (brands.Any(b => b.Name == normalizedName && b != brand))
                return false;

            brand.EditName(normalizedName);
            return true;
        }

        // ELIMINAR PRODUCTOS

        public bool DeleteProduct(string name)
        {
            Product? product = FindProduct(name);

            if (product == null)
                return false;

            products.Remove(product);
            return true;
        }

        // CONSULTAR POR CATEGORIA Y MARCA

        public List<Product> GetProductsByCategory(string categoryName)
        {
            string normalizedName = categoryName.Trim().ToLower();

            return products.Where(p => p.Category.Name == normalizedName).ToList();
        }

        public List<Product> GetProductsByBrand(string brandName)
        {
            string normalizedName = brandName.Trim().ToLower();

            return products.Where(p => p.Brand.Name == normalizedName).ToList();
        }

        // CONSULTAR EXISTENCIAS

        public List<Product> GetProductsWithStock()
        {
            return products.Where(p => p.Stock > 0).ToList();
        }

        public List<Product> GetProductsWithoutStock()
        {
            return products.Where(p => p.Stock == 0).ToList();
        }

        // LISTAR REGISTROS

        public List<Product> GetAllProducts()
        {
            return new List<Product>(products);
        }

        public List<Category> GetAllCategories()
        {
            return new List<Category>(categories);
        }

        public List<Brand> GetAllBrands()
        {
            return new List<Brand>(brands);
        }
    }
}
