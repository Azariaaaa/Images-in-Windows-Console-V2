using System;
using System.Drawing;
using System.Net;
using System.Text.Json.Nodes;

namespace ImagesInConsole
{
    internal class Program
    {
        public static string url = "https://pokeapi.co/api/v2/pokemon/";
        static void Main(string[] args)
        {
            string pokemonName = AskForAPokemon();
            string url = GetPokemonImage(pokemonName);
            Bitmap image = DownloadImageFromUrl(url, pokemonName);
            //Bitmap picture = new Bitmap("C:\\Users\\krust\\OneDrive\\Bureau\\C#\\ImagesInConsole\\007.png");
            PixelWriter pw = new PixelWriter(image, 200);
            pw.PictureAnalyzer();
        }

        static string AskForAPokemon()
        {
            Console.WriteLine("Veuillez entrer le nom d'un pokemon : ");
            string? pokemonName = Console.ReadLine();
            return pokemonName;
        }

        static string GetPokemonImage(string pokemonName)
        {
            string fullUrl = url + pokemonName;
            string json = new WebClient().DownloadString(fullUrl);
            JsonObject root = JsonNode.Parse(json).AsObject();
            return (string)root["sprites"]["other"]["official-artwork"]["front_default"];
        }

        static Bitmap DownloadImageFromUrl(string urlToDownload, string pokemonName)
        {
            string url = urlToDownload;

            WebClient client = new WebClient();
            byte[] imageData = client.DownloadData(url);

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imageData))
            {
                Bitmap bitmap = new Bitmap(ms);

                //bitmap.Save(pokemonName + ".png");
                //bitmap.Save("C:\\Users\\krust\\OneDrive\\Bureau\\C#\\PokeApiJsonConsole\\" + pokemonName + ".png");

                return bitmap;
            }
        }
    }
}
