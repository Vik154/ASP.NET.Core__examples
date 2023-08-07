using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _02_RAZOR_PAGES.Pages;

[IgnoreAntiforgeryToken]
public class BindingPagesModel : PageModel {

    // ���� � ������� ����� ������ � ������� name � age (������� �������� ������ �� ����� ��������),
    // �� ��� ������ ����� ������������� ������������ ����������� ���������.
    [BindProperty] public string Name { get; set; } = string.Empty;
    [BindProperty] public int Age { get; set; } 
    
    // ��������� ��������
    public int? Id { get; private set; }
    public void OnGet(int? id) => Id = id?? 0;

    // �������� � ���������
    [BindProperty(SupportsGet = true)] public string Info { get; set; } = "";
    [BindProperty(SupportsGet = true)] public int Num { get; set; }
}
