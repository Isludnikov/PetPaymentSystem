using PetPaymentSystem.Models;
using System.Collections.Generic;

namespace PetPaymentSystem.Helpers
{
    public class LanguageHelper
    {
        public static IEnumerable<Language> GetLanguages()
        {
            return new List<Language>
            {
                new Language {Iso639_3 = "ENG"},
                new Language {Iso639_3 = "RUS"},

            };
        }
    }
}
