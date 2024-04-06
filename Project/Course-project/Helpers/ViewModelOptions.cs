using Course_project.Models;
using Course_project.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Course_project.Helpers
{
    public class ViewModelOptions
    {
        public int PageSizeCollection = 15;

        public IQueryable<Collection> Collections { get; set; }

        public List<Collection> Items;

        public int Count { get; set; }

        public async Task<ViewModelOptions> GetSortedAndFilteredCollection(CollectionType? type, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 15;


            if (type != null)
            {
                Collections = Collections.Where(p => p.Type == type);
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    Collections = Collections.OrderByDescending(s => s.Name);
                    break;
                case SortState.TypeAsc:
                    Collections = Collections.OrderBy(s => s.Type);
                    break;
                case SortState.TypeDesc:
                    Collections = Collections.OrderByDescending(s => s.Type);
                    break;
                default:
                    Collections = Collections.OrderBy(s => s.Name);
                    break;
            }

            this.Count = await Collections.CountAsync();
            this.Items = await Collections.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return this;
        }
    }
}
