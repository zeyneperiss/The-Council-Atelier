using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string password = "Ogrenci123!";
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        string passwordHash = Convert.ToBase64String(hash);
        
        Console.WriteLine($"Password Hash for 'Ogrenci123!': {passwordHash}");
        Console.WriteLine();
        Console.WriteLine("SQL Insert Statement:");
        Console.WriteLine($"INSERT INTO Ogrenciler (Ad, Soyad, Email, Telefon, PasswordHash, KayitTarihi)");
        Console.WriteLine($"VALUES ('Test', 'Öğrenci', 'ogrenci@test.com', NULL, '{passwordHash}', GETDATE());");
    }
}
