// Класс для представления в Index модели
namespace _10_Entity_Framework.Models; 

public class IndexViewModel {
    public IEnumerable<User> Users { get; set; } = new List<User>();
    public SortViewModel SortViewModel { get; set; } = new SortViewModel(SortState.NameAsc);
}
