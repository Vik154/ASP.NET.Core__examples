using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _02_RAZOR_PAGES.Pages;

// ���� ������� ��������� ��������� �����, ������ ��� ������ Razor
// ��� ��������� ���������� ���� ���������� ����������� ����� - AntiforgeryToken.
[IgnoreAntiforgeryToken]
public class PostRequestModel : PageModel {

    public string Message { get; private set; } = "";

    public void OnGet() => Message = "������� ���: ";
    public void OnPost(string name) => Message = $"������ {name}!";    
}
