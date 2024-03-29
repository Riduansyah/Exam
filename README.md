## Exam

**Review Code → 1**

```cs
    if (application != null)
    {
        if (application.protected != null)
        {
                return application.protected.shieldLastRun;
        }
    }


   //Betuk sederhana dari potongan kode diatas
   return application?.protected?.shieldLastRun;
        
```

**Review Code → 2**

```cs
public ApplicationInfo GetInfo()
{
    var application = new ApplicationInfo
    {
        Path = "C:/apps/",
        Name = "Shield.exe"
    };

    return application;
}


//Berikut bentuk sederhana dari potongan kode diatas
public (string Path, string Name) GetAppInfo()
{
   return ("C:/apps/", "Shield.exe");
}
        
```

**Review Code → 3**

```cs
class Laptop
{
    public string Os { get; set; } // can be modified
    public Laptop(string os)
    {
        Os = os;
    }
}
var laptop = new Laptop("macOs");
Console.WriteLine(laptop.Os); // Laptop os: macOs


//fungsi set dihapus karena pada class diatas untuk melakukan perubahan value terhadap property Os atau tidak digunakan sama sekali
public string Os { get; } 
        
```

**Product**:  soal nomor 4, program ini melakukan perulangan diamana setiap perulangan tersebut di tampung atau di tambahkan kedalam list, hal ini juga dapat menimbulkan kebocoran memori, karena program ini hanya melakukan penambahan terhadap list tetapi tidak pernah menghapus list tesebut sehingga jika program ini dijalankan terus menuerus akan membuat pemakaian memori semakin banyak, untuk itu ditambahkan fungsi untuk menghapus list ketika perulangannya selesai :

```cs
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
        
```

**File** : ProgramEmpat.cs

**EventPublisher**:  soal nomor 5, pada program EventPublisher tidak pernah melepaskan metode OnMyEvent, yang menyebabkan objek EventSubscriber tetap ada/hidup meskipun tidak diperlukan lagi, untuk melepaskan/membuang event tersebut maka ditambahan fungsi Dispose berikut potongan source yang dilakukan perubahan :

```cs

    class EventSubscriber : IDisposable
    {
       private readonly EventPublisher eventPublisher;
       private bool disposedValue = false;

      public EventSubscriber(EventPublisher publisher)
      {
         eventPublisher = publisher;
         publisher.MyEvent += OnMyEvent;
      }

      protected virtual void Dispose(bool disposing)
      {
         if (disposedValue) return;

         if (disposing)
         {
            eventPublisher.MyEvent -= OnMyEvent;
         }

         disposedValue = true;
         }

       public void Dispose()
        {
           Dispose(disposing: true);
           GC.SuppressFinalize(this);
      }        
```

**File** : ProgramLima.cs

**TreeNode**:  soal nomor 6, TreeNode ini membuat objek TreeNode sebagai akar dari struktur pohon/node. Program ini kemudian berulang kali membuat subpohon baru sebagai anak dari node akar. Program ini tidak pernah menghapus subpohon lama, sehingga terus menggunakan memori. agar memori tetap aman maka perlu merubah kelas TreeNode dan menambahkan method baru untuk menghapus node subpohon :

```cs
         while (true)
         {
            // create a new subtree of 10000 nodes  
            var newNode = new TreeNode();
            for (int i = 0; i < 10000; i++)
            {
               var childNode = new TreeNode();
               newNode.AddChild(childNode);
            }
           rootNode.AddChild(newNode);

           //hapus node
           if (rootNode.Children.Count > 10)
           {
             rootNode.RemoveChildIndex(0);
          }
    }
        
    class TreeNode
    {
        private readonly List<TreeNode> _children = new List<TreeNode>();
        public IReadOnlyList<TreeNode> Children => _children;

        public void AddChild(TreeNode child)
        {
            _children.Add(child);
        }


        //method baru, hapus node
        public void RemoveChildIndex(int index)
        {
            _children.RemoveAt(index);
        }
    }
        
```

**File** : ProgramEnam.cs

**Cache**:  Soal nomor 7, pada kode program ini ada kebocoran memori karena tidak ada batasan untuk object yang dibuat, maka dari itu untuk mencegahnya dengan cara menambahkan waktu untuk object tersebut (expired) agar object tersebut akan hilang berdasarkan waktu yang ditentukan, berikut potongan code yg ditambakan dari source sebelumnya:

```cs
        public static void Add(int key, object value)
        {
            _cache.Add(key, new SampleData { Data = value, TimeExpired = DateTime.Now + TimeSpan.FromMinutes(5)});
        }
        
        private class SampleData
        {
            public object Data { get; set; }
            public DateTime TimeExpired { get; set; }
        }
        
```

**File** : ProgramTujuh.cs

**Web Application Development using Clean Architecture**

*   Project WebApi belum include Autentikasi untuk user, project ini hanya bisa upload dan menampilkan file document
*   Project WebApp/ Web Client dibangun dengan Angular v15

**Instalasi**

1\. Unduh source code - WebApi  
2\. Kemudian unzip file tersebut, buka file project yg sebelumnya sudah di unzip menggunakan Visual Studio 2022 buka file appsettings.json ganti atau sesuaikan server dan nama database (SQL Server 2022 Management Studio).  
3\. Buka Package Manager Console di Visual Studio dan ketik: `Update-Database`  
4\. Run project

1\. Unduh source code - WebApp  
2\. Kemudian unzip file WebApp, buka file tersebut menggunakan Visual Studio Code selanjutnya ubah file **environments/environment.ts** sesuaikan link web api yang sudah di running   
3\. Buka command prompt console di Visual Studio Code dan ketikan: `mpm install` tunggu sampai installasi **node package** selesai  
4\. Kemudian ketikan: `ng serve -o`

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/961fa6d642aea80330c0aca992a9b08c1e86360f63b092f7.png)

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/da023f57b049ec0492c579eb935c2ed7b15b6fcb9610399c.png)

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/f8cfc149057ab4805b2fb648e3cbb0eef0e720598906dee5.jpeg)
