using _05_MODELS.Models;
namespace _05_MODELS.ViewModels;

public class IndexViewModel {
    public IEnumerable<Person> People { get; set; } = new List<Person>();
    public IEnumerable<CompanyModel> Companies { get; set; } = new List<CompanyModel>();
}
