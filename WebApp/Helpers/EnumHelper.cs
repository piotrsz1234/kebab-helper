using WebApp.Enums;

namespace WebApp.Helpers
{
    public class EnumHelper
    {
        public static string GetDescription(PriceType type)
        {
            switch (type)
            {
                case PriceType.Unknown:
                    return "Bez znaczenia";
                case PriceType.Cheap:
                    return "Niska";
                case PriceType.Expensive:
                    return "Wysoka";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static string GetDescription(MeatType type)
        {
            switch (type)
            {
                case MeatType.Unknown:
                    return "Bez znaczenia";
                case MeatType.Beef:
                    return "wołowina";
                case MeatType.Pork:
                    return "wieprzowina";
                case MeatType.Chicken:
                    return "kurczak";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static string GetDescription(VegetableType type)
        {
            switch (type)
            {
                case VegetableType.Unknown:
                    return "Bez znaczenia";
                case VegetableType.Yes:
                    return "Z_warzywami";
                case VegetableType.No:
                    return "Bez_warzyw";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static string GetDescription(SosType type)
        {
            switch (type)
            {
                case SosType.Unknown:
                    return "Bez znaczenia";
                case SosType.NotSpicy:
                    return "Łagodny";
                case SosType.Spicy:
                    return "Ostry";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}