using System.Text;
using System.Text.RegularExpressions;

namespace PetPaymentSystem.Helpers
{
    public static class MaskHelper
    {
        private const int DefaultLength = 3;
        private const char DefaultChar = '*';
        private const string PanPattern = "(\"pan\"\\s?:\\s?\"\\d{6})(\\d{3,})(\\d{4}\")";
        private const string CvvPattern = "(\"cvv\"\\s?:\\s?\")(\\d{3,})(\")";
        private static readonly Regex PanRegex = new Regex(PanPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex CvvRegex = new Regex(CvvPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static string MaskApiRequest(string request)
        {
            var result = request;
            result = PanRegex.Replace(result, "$1***$3");
            result = CvvRegex.Replace(result, "$1***$3");
            return result;
        }

        public static string MaskHeader(string header)
        {
            return Mask(header, 6, 6, false);
        }

        public static string MaskPan(string pan) => Mask(pan, 6, 4);

        public static string MaskCvv(string cvv) => Mask(cvv, 0, 0);

        private static string Mask(string str, int start, int end, bool preserveLength = true)
        {
            if (str == null) return string.Empty;
            if (str.Length <= start + end) return Replicate(start + end);
            return str.Substring(0, start) + Replicate(preserveLength ? str.Length - (end + start) : DefaultLength) + str.Substring(str.Length - end);
        }
        private static string Replicate(int count)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < count; i++)
            {
                sb.Append(DefaultChar);
            }
            return sb.ToString();
        }
    }
}
