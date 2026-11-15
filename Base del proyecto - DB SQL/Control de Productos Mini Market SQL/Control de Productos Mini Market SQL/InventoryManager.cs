
using Microsoft.EntityFrameworkCore;

namespace Control_de_Productos_Mini_Market_SQL
{
    public class InventoryManager
    {
        private readonly MiniMarketDbContext db = new MiniMarketDbContext();

        // REGISTRAR CATEGORIAS Y MARCAS

        public bool RegisterCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string normalizedName = name.Trim().ToLower();

            if (db.Categories.Any(c => c.Name == normalizedName))
                return false;

            db.Categories.Add(new Category(normalizedName));
            db.SaveChanges();

            return true;
        }

        public bool RegisterBrand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string normalizedName = name.Trim().ToLower();

            if (db.Brands.Any(b => b.Name == normalizedName))
                return false;

            db.Brands.Add(new Brand(normalizedName));
            db.SaveChanges();

            return true;
        }

        // REGISTRAR PRODUCTOS

        public bool RegisterProduct(string name, int stock, string categoryName, string brandName)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(categoryName) ||
                string.IsNullOrWhiteSpace(brandName) ||
                stock < 0)
                return false;

            string normalizedName = name.Trim().ToLower();
            string normalizedCategory = categoryName.Trim().ToLower();
            string normalizedBrand = brandName.Trim().ToLower();

            if (db.Products.Any(p => p.Name == normalizedName))
                return false;

            Category? category = FindCategory(normalizedCategory);
            Brand? brand = FindBrand(normalizedBrand);

            if (category == null || brand == null)
                return false;

            Product newProduct = new Product(
                normalizedName,
                stock,
                category,
                brand);

            db.Products.Add(newProduct);
            db.SaveChanges();

            return true;
        }

        // BUSQUEDAS

        public Product? FindProduct(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string normalizedName = name.Trim().ToLower();

            return db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Name == normalizedName);
        }

        public Category? FindCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string normalizedName = name.Trim().ToLower();

            return db.Categories.FirstOrDefault(
                c => c.Name == normalizedName);
        }

        public Brand? FindBrand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string normalizedName = name.Trim().ToLower();

            return db.Brands.FirstOrDefault(
                b => b.Name == normalizedName);
        }

        // AGREGAR Y REDUCIR STOCK

        public bool AddProductStock(string name, int amount)
        {
            Product? product = FindProduct(name);

            if (product == null || amount <= 0)
                return false;

            product.AddStock(amount);
            db.SaveChanges();

            return true;
        }

        public bool RemoveProductStock(string name, int amount)
        {
            Product? product = FindProduct(name);

            if (product == null || amount <= 0 || amount > product.Stock)
                return false;

            product.ReduceStock(amount);
            db.SaveChanges();

            return true;
        }

        // ACTUALIZAR PRODUCTOS

        public bool UpdateProductName(string oldName, string newName)
        {
            Product? product = FindProduct(oldName);

            if (product == null || string.IsNullOrWhiteSpace(newName))
                return false;

            string normalizedName = newName.Trim().ToLower();

            if (db.Products.Any(p =>
                p.Name == normalizedName &&
                p.ProductId != product.ProductId))
                return false;

            product.EditName(normalizedName);
            db.SaveChanges();

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

            db.SaveChanges();
            return true;
        }

        public bool UpdateProductCategory(string productName, string categoryName)
        {
            Product? product = FindProduct(productName);
            Category? category = FindCategory(categoryName);

            if (product == null || category == null)
                return false;

            product.EditCategory(category);
            db.SaveChanges();

            return true;
        }

        public bool UpdateProductBrand(string productName, string brandName)
        {
            Product? product = FindProduct(productName);
            Brand? brand = FindBrand(brandName);

            if (product == null || brand == null)
                return false;

            product.EditBrand(brand);
            db.SaveChanges();

            return true;
        }

        // ACTUALIZAR CATEGORIAS Y MARCAS

        public bool UpdateCategory(string oldName, string newName)
        {
            Category? category = FindCategory(oldName);

            if (category == null || string.IsNullOrWhiteSpace(newName))
                return false;

            string normalizedName = newName.Trim().ToLower();

            if (db.Categories.Any(c =>
                c.Name == normalizedName &&
                c.CategoryId != category.CategoryId))
                return false;

            category.EditName(normalizedName);
            db.SaveChanges();

            return true;
        }

        public bool UpdateBrand(string oldName, string newName)
        {
            Brand? brand = FindBrand(oldName);

            if (brand == null || string.IsNullOrWhiteSpace(newName))
                return false;

            string normalizedName = newName.Trim().ToLower();

            if (db.Brands.Any(b =>
                b.Name == normalizedName &&
                b.BrandId != brand.BrandId))
                return false;

            brand.EditName(normalizedName);
            db.SaveChanges();

            return true;
        }

        // ELIMINAR PRODUCTOS

        public bool DeleteProduct(string name)
        {
            Product? product = FindProduct(name);

            if (product == null)
                return false;

            db.Products.Remove(product);
            db.SaveChanges();

            return true;
        }

        // CONSULTAR POR CATEGORIA Y MARCA

        public List<Product> GetProductsByCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return new List<Product>();

            string normalizedName = categoryName.Trim().ToLower();

            return db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.Category.Name == normalizedName)
                .ToList();
        }

        public List<Product> GetProductsByBrand(string brandName)
        {
            if (string.IsNullOrWhiteSpace(brandName))
                return new List<Product>();

            string normalizedName = brandName.Trim().ToLower();

            return db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.Brand.Name == normalizedName)
                .ToList();
        }

        // CONSULTAR EXISTENCIAS

        public List<Product> GetProductsWithStock()
        {
            return db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.Stock > 0)
                .ToList();
        }

        public List<Product> GetProductsWithoutStock()
        {
            return db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.Stock == 0)
                .ToList();
        }

        // LISTAR REGISTROS

        public List<Product> GetAllProducts()
        {
            return db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToList();
        }

        public List<Category> GetAllCategories()
        {
            return db.Categories.ToList();
        }

        public List<Brand> GetAllBrands()
        {
            return db.Brands.ToList();
        }
    }
}
