
# VarDataLib

VarDataLib, C# ile değişken uzunlukta tam sayıların (VarInt, VarLong, VarShort) kodlanması ve çözülmesi için geliştirilmiş bir kütüphanedir. Ayrıca işaretli (signed) tam sayılar için ZigZag kodlamayı destekler.

## Neden VarDataLib?

- Küçük sayıları daha az bayt kullanarak kodlayarak **bant genişliği ve depolama alanı tasarrufu** sağlar.
- **Ağ protokolleri**, **oyun geliştirme** ve **dosya serileştirme** gibi alanlarda kullanıma uygundur.
- Hem işaretli hem işaretsiz tam sayıları destekler.
- Basit ve kullanımı kolay bir API sunar.

## Özellikler

- VarInt, VarLong, VarShort türleri için codec’ler
- İşaretli tam sayılar için ZigZag kodlama
- Etkin serileştirme ve deseralize işlemleri
- Kolay entegrasyon

## Kurulum

```shell
dotnet add package VarDataLib
```

## Kullanım Örneği

```csharp
using System;
using System.IO;
using VarDataLib.Types;

class Program
{
    static void Main()
    {
        int değer = -123456;
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);
        using var reader = new BinaryReader(ms);

        VarInt.Write(writer, değer);
        ms.Position = 0;
        int çözülen = VarInt.Read(reader);

        Console.WriteLine($"Orijinal: {değer}, Çözülen: {çözülen}");
    }
}
```

## Lisans

MIT Lisansı ile dağıtılmaktadır. İstediğiniz gibi kullanabilir ve değiştirebilirsiniz.
