using _08_VIEW_COMPONENTS.Infrostructure;
namespace _08_VIEW_COMPONENTS.Components;

public class PersonInfoViewComponent {

    public string Invoke(Person person) => $"Name: {person.Name}  Age: {person.Age}";
}
