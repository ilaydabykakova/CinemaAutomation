using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaAutomation.Models.Modeller
{
    public class ModelFilm
    {
        public int? ID { get; set; }
        public string FilmAdı { get; set; }
        public string Resim { get; set; }
        public string Yonetmen { get; set; }
        public string FilmTuru { get; set; }
        public Seanslar Seans { get; set; }
        public Salonlar Salon { get; set; }
    }
}