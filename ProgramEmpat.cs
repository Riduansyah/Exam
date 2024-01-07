namespace MemoryLeakExample
{
    class ProgramEmpat
    {
        static void Main(string[] args)
        {
            var myList = new List<Product>();
            while (true)
            {
                // populate list with 1000 integers
                for (int i = 0; i < 1000; i++)
                {
                    myList.Add(new Product(Guid.NewGuid().ToString(), i));
                }
                // do something with the list object
                Console.WriteLine(myList.Count);
                myList.Clear(); //hapus semua list product
            }
        }
    }

    class Product
    {
        public Product(string sku, decimal price)
        {
            SKU = sku;
            Price = price;
        }
        public string SKU { get; set; }
        public decimal Price { get; set; }
    }
}
