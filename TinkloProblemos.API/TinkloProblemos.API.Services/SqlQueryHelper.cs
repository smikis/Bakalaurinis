using System.Text;

namespace TinkloProblemos.API.Services
{
    public static class SqlQueryHelper
    {
        public static string ConvertToSqlSearchQuery(string searchTerm)
        {
            var words = searchTerm.Split(' ');
            if (words.Length == 1)
            {
                return $"+{searchTerm}*";
            }

            var searchQuery = new StringBuilder();
            foreach (var word in words)
            {
                searchQuery.Append($"+{word}* ");
            }

            return searchQuery.ToString();
        }
    }
}
