using Course_project.Models.Enums;

namespace Course_project.ViewModels
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; } // значение для сортировки по имени

        public SortState TypeSort { get; private set; }
        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            TypeSort = sortOrder == SortState.TypeAsc ? SortState.TypeDesc : SortState.TypeAsc;
            Current = sortOrder;
        }
    }
}
