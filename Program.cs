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

namespace InvesticFilms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetFilmBuddies();
        }

        private static void GetFilmBuddies()
        {
            int pageNumber = 1;
            string nextUrl = "https://swapi.dev/api/people?page=1"; 
            List<string> Titles = new List<string>();
            int totalPages = 1;
            List<Character> Characters = new List<Character>();
            do
            {
                using (WebClient client = new WebClient())
                using (var stream = client.OpenRead(nextUrl))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        //var jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());

                        var jsonString = reader.ReadLine();
                        var data = JsonConvert.DeserializeObject<dynamic>(jsonString);
                        nextUrl = (string)data.next;

                    //    totalPages = (int)results.total_pages;

                        foreach (var info in data.results)
                        {

                            string characterName = (string)info.name;

                            Characters.Add(new Character { Name = characterName });
                            var films = info.films;

                            //foreach (var title in info.films)
                            //{

                            //}
                            //string story_title = (string)info.story_title;

                            //if (!string.IsNullOrWhiteSpace(title))
                            //{
                            //    Titles.Add(title);
                            //}
                            //else if (!string.IsNullOrWhiteSpace(story_title))
                            //{
                            //    Titles.Add(story_title);
                            //}


                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(nextUrl))
                {
                    break;
                }
                //pageNumber++;
            } while (true);

            var characters = Characters;


            foreach (var character in Characters)
            {
                Console.WriteLine($"Name: {character.Name} ");
            }
        }
    }


    public class Featured
    {
        public Character character { get; set; }

        public List<Film> Films { get; set; }
    }
    public class Character
    {
        public string Name { get; set; }
    }

    public class Film
    {
        public string Name { get; set; }
    }

}
