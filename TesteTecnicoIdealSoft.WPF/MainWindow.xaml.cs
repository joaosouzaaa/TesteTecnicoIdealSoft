using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TesteTecnicoIdealSoft.WPF.Responses;

namespace TesteTecnicoIdealSoft.WPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IDisposable
{
    private readonly HttpClient _httpClient;

    public MainWindow()
    {
        // base Address if you're running on docker
        const string baseAddress = "https://localhost:64748/api/Person/";
        // base Address if you're running api directly
        // const string baseAddress = "https://localhost:7188/api/Person/";

        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(baseAddress)
        };

        InitializeComponent();

        GetAllPeopleAsync();
    }

    public void Dispose() =>
        _httpClient.Dispose();

    private async void GetAllPeopleAsync()
    {
        var personResponseList = await _httpClient.GetFromJsonAsync<List<PersonResponse>>("get-all-people");
        dgPerson.DataContext = personResponseList; 
    }
}
