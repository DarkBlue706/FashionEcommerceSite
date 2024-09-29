using ShirtCompany.Controllers;

namespace ShirtCompany.Models.Repositories
{
    public static class ShirtRepository
    {
            private static List<Shirt> shirts = new List<Shirt>()
            {
                new Shirt { ShirtID = 1, Brand = "MyBrand", Color = "Blue", Gender = "women", Price = 30, Size = 10},
                new Shirt { ShirtID = 2, Brand = "MyBrand", Color = "Black", Gender = "Men", Price = 30, Size = 10},
                new Shirt { ShirtID = 3, Brand = "YourBrand", Color = "Pink", Gender = "men", Price = 30, Size = 10},
                new Shirt { ShirtID = 4, Brand = "YourBrand", Color = "Yellow", Gender = "Women", Price = 30, Size = 10},
            };

            public static bool ShirtExists(int id)
            {
                return shirts.Any(x=> x.ShirtID == id);
            }
            public static Shirt? GetShirtById(int id)
            {
                return shirts.FirstOrDefault(X=> X.ShirtID == id);
            }

    };
}