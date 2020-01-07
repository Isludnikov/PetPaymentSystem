using System.Collections.Generic;
using PetPaymentSystem.Models;

namespace PetPaymentSystem.Helpers
{
    public static class CurrencyHelper
    {
        public static IEnumerable<Currency> GetCurrencies()
        {
            return new List<Currency>
            {
                new Currency{Alfa3 = "EUR", Decimals = 2, FullNameEng = "Euro", FullNameRus = "Евро"},
                new Currency{Alfa3 = "RUB", Decimals = 2, FullNameEng = "Russian ruble", FullNameRus = "Российский рубль"},
                new Currency{Alfa3 = "USD", Decimals = 2, FullNameEng = "United States dollar", FullNameRus = "Американский доллар"},

            };
        }
    }
}
