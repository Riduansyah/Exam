        class Laptop
        {
            public string Os { get; }
            public Laptop(string os)
            {
                Os = os;
            }
        }
        var laptop = new Laptop("macOs");
        Console.WriteLine(laptop.Os);
