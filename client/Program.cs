using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;

class Program
{
    static async Task Main()
    {
        var uploadUrl = "https://myp22.itstep.click/api/Galleries/upload";
        string url = "https://picsum.photos/1200/800?grayscale";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(uploadUrl);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Connection was successful");
                }
                else
                {
                    Console.WriteLine("Connection was unsuccessful");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection was unsuccessful");
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        

        var httpClient = new HttpClient();
        string input = "";
        
        List<string> reports = new List<string>();
        
        while(input != "exit")
        {
            Console.WriteLine("Please enter path to the image (to exit enter exit): ");
            input = Console.ReadLine();
            if (input == "exit")
            {
                break;
            }
            
            byte[] imageBytes = File.ReadAllBytes(input);
            string imageString = Convert.ToBase64String(imageBytes);

            var payload = new { photo = imageString };
            var response = await httpClient.PostAsJsonAsync(uploadUrl, payload);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("the image was sent successfully");
                reports.Add(input);
            }
            else
            {
                Console.WriteLine("The upload was unsuccessful");
            }
            
            Console.WriteLine();
        }

        Console.WriteLine("-------   Report   -------");
        Console.WriteLine("There were " + reports.Count + " report(s)");
        int count = 1;
        foreach (var r in reports)
        {
            Console.WriteLine(count + ") " + r);
        }
    }
}
