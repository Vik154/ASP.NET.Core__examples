// Анализ конфигурации
using System.Text;
namespace _01_BASE_CONCEPT.Services;

// Привязка данных
public class Person {
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public List<string> Languages { get; set; } = new();
    public Company? Company { get; set; }
}

public class Company {
    public string Title { get; set; } = "";
    public string Country { get; set; } = "";
}


public class SectionContent {

    public static string GetSectionContent(IConfiguration configSection) {
        StringBuilder contentBuild = new StringBuilder();

        foreach (var section in configSection.GetChildren()) {
            contentBuild.Append($"\"{section.Key}\"");
            if (section.Value == null) {
                string subSectionContent = GetSectionContent(section);
                contentBuild.Append($"{{\n{subSectionContent}}},\n");
            }
            else {
                contentBuild.Append($"\"{section.Value}\",\n");
            }
        }
        return contentBuild.ToString();
    }
}
