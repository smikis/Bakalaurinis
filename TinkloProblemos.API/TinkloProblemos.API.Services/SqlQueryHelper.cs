using System.Text;

namespace TinkloProblemos.API.Services
{
    public static class SqlQueryHelper
    {
        public static string ConvertToSqlSearchQuery(string searchTerm)
        {
            if (searchTerm == null) return "";
            var words = searchTerm.Split(' ');
            if (words.Length == 1)
            {
                return $"+{searchTerm}*";
            }

            var searchQuery = new StringBuilder();
            foreach (var word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    searchQuery.Append($"+{word}* ");
                }             
            }

            return searchQuery.ToString();
        }
    }
}
