using Task.Api.Data;
using Task.Api.Models;

namespace Api.Task.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public double GetAdditionalPercentage(string categoryName, int installmentRange)
        {
            double additionalPercentage = 0;

            var category = _context.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == categoryName.ToLower());

            if (category == null) return additionalPercentage;
            var rangeFrom = category.RangeFrom;
            var rangeTo = category.RangeTo;
            var percent = category.Percent;

            if (installmentRange >= rangeFrom && installmentRange <= rangeTo)
            {
                additionalPercentage = percent;
            }
            else
            {
                additionalPercentage = installmentRange / (double)rangeTo * (percent * 2);
            }

            return additionalPercentage;
        }

        public List<Category> GetCategoriesByCategoryName(string categoryName)
        {
            return _context.Categories
                .Where(c => c.CategoryName.ToLower() == categoryName.ToLower())
                .ToList();
        }
    }
}