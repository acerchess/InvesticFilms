using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace InvesticFilms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nHello");
            Console.WriteLine("Obtaining film details, please wait...\n");

            var characters = GetFilmDetailsFromApi();
            
            DisplayFilmDetails(characters);

            Console.ReadLine();
        }

        private static List<Character> GetFilmDetailsFromApi()
        {

            string url = "https://swapi.dev/api/people?page=1";

            List<Character> characters = new List<Character>();

            do
            {
                using (WebClient client = new WebClient())
                using (var stream = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var jsonString = reader.ReadLine();
                        //  var data = JsonConvert.DeserializeObject<dynamic>(jsonString);

                        data data = JsonConvert.DeserializeObject<data>(jsonString);
                        url = data.next;
                        foreach (var character in data.results)
                        {
                            characters.Add(new Character
                            {
                                CharacterName = character.name,
                                Films = character.films.ToList()

                            });
                        }
                    }
                }

            } while (!string.IsNullOrWhiteSpace(url));

            return characters;
        }

        private static void DisplayFilmDetails(List<Character> characters)
        {
            Console.WriteLine("Displaying Film Results:\n");


            var AllFilms = characters.SelectMany(x => x.Films).ToList();

            var UniqueFilms = AllFilms.Distinct().ToList();

            Console.WriteLine($"Total Films: {UniqueFilms.Count}\n");
            
            Console.WriteLine($"Film names:");
            foreach (var film in UniqueFilms)
            {
                Console.WriteLine($"  {film}");
            }

            List<FilmDetails> FilmDetails = new List<FilmDetails>();

            foreach (var film in UniqueFilms)
            {
                var details = new FilmDetails();
                details.Film = film;
                var actors = characters.Where(x => x.Films.Contains(film)).Select(x => x.CharacterName).ToList();
                details.Characters = actors;
                FilmDetails.Add(details);
            }
            Console.WriteLine($"\nSee film details below:");

            foreach (var filmCharacters in FilmDetails)
            {
                Console.WriteLine($"\nFilm Name:{filmCharacters.Film}");
                Console.WriteLine($"  Character Count:  {filmCharacters.TotalCharacters}");
                Console.WriteLine($"  Character Names:");
                if (filmCharacters.Characters.Count > 0)
                {
                    foreach (var charactor in filmCharacters.Characters)
                    {
                        Console.WriteLine($"     {charactor}");
                    }
                }
                
            }

            Console.WriteLine("\n Please scroll up to view all film details.\n Thank you");

        }
    }


}
