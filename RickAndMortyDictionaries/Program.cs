using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq;


namespace RickAndMortyDictionaries
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                string url = "https://rickandmortyapi.com/api/character";

                client.DefaultRequestHeaders.Clear();

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode) //para verificar que el estado de la conexión está bien
                {

                    var res = response.Content.ReadAsStringAsync().Result;
                    //dynamic r = JObject.Parse(res); -> esto no hace falta 

                    var data = JsonConvert.DeserializeObject<RickAndMortyAPI>(res.ToString());

                    //Buscamos en el json el nombre que coincide con Summer Smith
                    var personajeSummer = data?.results.FirstOrDefault(s => s.name == "Summer Smith");

                if (personajeSummer != null) //el if es para controlar de que el personaje esté en el json
                {
                    Console.WriteLine($"Episodios de {personajeSummer.name}:");

                    foreach (var episode in personajeSummer.episode)
                    {
                        Console.WriteLine(episode);
                    }
                }
                else
                {
                    Console.WriteLine("Summer Smith no encontrado.");
                }
            }
                else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
            }
        }
    }
}

    /*
     * Los objetos se utilizan para deserializar la respuesta JSON de la API en una estructura de datos 
     * que sea fácil de manipular en C#
     */
    public class RickAndMortyAPI
{
    public int count { get; set; }
    public int pages { get; set; }
    public string next { get; set; }
    public string prev { get; set; }
    public Character[] results { get; set; }
}

    /**
     *  Esta clase es para crear un objeto con los datos del personaje.
     *  Podríamos ponerlos todos pero solo pondremos los que vamos a usar ya que solo queremos acceder
     *  a los episodios de Summer Smith.
     */
public class Character
{
    public int id { get; set; }
    public string name { get; set; }
    public string[] episode { get; set; }
}
}