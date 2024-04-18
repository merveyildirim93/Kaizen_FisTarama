using Newtonsoft.Json;
using System.Data;
using static Kaizen_FisTarama.Models.JsonItems;

class Program
{
    static void Main()
    {
        var json = File.ReadAllText(@"..\..\..\response.json");
        List<Root> jsonItems = JsonConvert.DeserializeObject<List<Root>>(json);
        jsonItems.RemoveAt(0);
        List<Root> thresholdItems = new List<Root>();
        List<Root> receive = new List<Root>();
        List<Referance> referanceList = new List<Referance>();

        double yLow = 0;
        double yHigh = 0;
        double range = 0;
        double average = 0;
        double threshold = 0;

        if (jsonItems.Count > 0)
        {
            //burada her bir kelimenin tepe ve dik noktaları bulunarak maksimum boyunun hesaplanması sağlandı
            for (var i = 1; i < jsonItems.Count; i++)
            {
                yLow = jsonItems[i].boundingPoly.vertices[0].y > jsonItems[i].boundingPoly.vertices[1].y ? jsonItems[i].boundingPoly.vertices[1].y : jsonItems[i].boundingPoly.vertices[0].y;

                yHigh = jsonItems[i].boundingPoly.vertices[2].y > jsonItems[i].boundingPoly.vertices[3].y ? jsonItems[i].boundingPoly.vertices[2].y : jsonItems[i].boundingPoly.vertices[3].y;

                range += yHigh - yLow;
            }

            //eşik değerini bulduk
            double avg = Math.Ceiling((range / jsonItems.Count) / 2);
            threshold = avg;

            //şimdi her bir kelimeyi sırayla kontrol ederek eşik değerimize uygun olanları bulacağız
            for (var i = 0; i < jsonItems.Count; i++)
            {
                // JSON öğelerinden bir öğe alarak y_lt değerini alıyoruz
                int yLeftTop = jsonItems[0].boundingPoly.vertices[0].y;

                // y_lt değeri kullanılarak filtreleme işlemi gerçekleştiriliyor
                var filteredItems = jsonItems.Where(item => item.boundingPoly.vertices[0].y < (yLeftTop + threshold) && item.boundingPoly.vertices[0].y > (yLeftTop - threshold)).ToList();


                foreach (var row in filteredItems)
                {
                    Referance referance = new Referance
                    {
                        desc = row.description,
                        xLeftTop = row.boundingPoly.vertices[0].x,
                        xLeftBottom = row.boundingPoly.vertices[3].x,
                        xRightTop = row.boundingPoly.vertices[1].x,
                        xRightBottom = row.boundingPoly.vertices[2].x,
                        threshold = yLeftTop
                    };
                    referanceList.Add(referance);
                    jsonItems.Remove(row);
                }
                i = 0;
            }

            string desc = "";
            int thresholdReferance = 0;

            for (var i = 0; i < referanceList.Count; i++)
            {
                thresholdReferance = referanceList[0].threshold;
                List<Referance> referances = new List<Referance>();
                referances = referanceList.Where(x => x.threshold == thresholdReferance).OrderBy(x => x.xLeftTop).ToList();
                desc = "";
                foreach (var row in referances)
                {
                    if (row.desc == "")
                    {
                        desc = row.desc;
                    }
                    else
                    {
                        desc += " " + row.desc;
                    }
                    referanceList.Remove(row);
                }

                Root newItem = new Root();
                newItem.description = desc;
                receive.Add(newItem);
                i = 0; // listeden elemanı sildiğimiz için veri kaybı olmaması adına i değerini her defasında sıfırlıyoruz
            }

            string dosyaYolu = @"..\..\..\receiveResponse.txt";
            // Eğer dosya zaten varsa, dosyayı açıp mevcut içeriği sileriz
            if (File.Exists(dosyaYolu))
            {
                File.Delete(dosyaYolu);
            }
            using (StreamWriter writer = new StreamWriter(dosyaYolu))
            {
                writer.WriteLine("Line | Text");
                writer.WriteLine("-----|----------");
                int line = 1;
                for (int i = 0; i < receive.Count; i++)
                {
                    writer.WriteLine($"{line,-5}| {receive[i].description}");
                    line++;
                }
            }
        }
        else
        {
            Console.WriteLine("Dosyadan veri okunamadı");
        }
    }
}

