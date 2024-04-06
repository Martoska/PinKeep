using Microsoft.AspNetCore.Mvc.Rendering;
using Course_project.Models;
using Course_project.Models.Enums;

namespace Course_project.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Collection> companies, CollectionType? company, string name)
        {
            Companies = new SelectList(companies, "Id", "Name", company);
            SelectedCompany = ((int?)company);
            SelectedName = name;
        }
        public SelectList Companies { get; private set; } 
        public int? SelectedCompany { get; private set; }   
        public string SelectedName { get; private set; }    
    }
}
