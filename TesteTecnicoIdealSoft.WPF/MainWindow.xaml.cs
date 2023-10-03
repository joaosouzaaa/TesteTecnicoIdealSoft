using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using TesteTecnicoIdealSoft.WPF.Extensions;
using TesteTecnicoIdealSoft.WPF.Requests.Person;
using TesteTecnicoIdealSoft.WPF.Responses.Errors;
using TesteTecnicoIdealSoft.WPF.Responses.Person;

namespace TesteTecnicoIdealSoft.WPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IDisposable
{
    private readonly HttpClient _httpClient;

    public MainWindow()
    {
        const string baseAddress = "https://localhost:7188/api/Person/";

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

    private async void AddPersonAsync(PersonSaveRequest personSaveRequest)
    {
        var addPersonHttpResponseMessage = await _httpClient.PostAsJsonAsync("add-person", personSaveRequest);

        ResetCommandsFields(addPersonHttpResponseMessage);
    }

    private async void UpdatePersonAsync(PersonUpdateRequest personUpdateRequest)
    {
        var udpatePersonHttpResponseMessage = await _httpClient.PutAsJsonAsync("update-person", personUpdateRequest);

        ResetCommandsFields(udpatePersonHttpResponseMessage);
    }

    private void ResetCommandsFields(HttpResponseMessage httpResponseMessage)
    {
        if (httpResponseMessage.StatusCode is HttpStatusCode.OK)
        {
            ResetErrorFieldsAndRefresh();
            return;
        }

        FillErrorFieldsAsync(httpResponseMessage);
    }

    private void ResetErrorFieldsAndRefresh()
    {
        GetAllPeopleAsync();
        txtNomeError.Visibility = Visibility.Hidden;
        txtNomeError.Text = "";
        txtSobrenomeError.Visibility = Visibility.Hidden;
        txtSobrenomeError.Text = "";
        txtTelefoneError.Visibility = Visibility.Hidden;
        txtTelefoneError.Text = "";
    }

    private async void FillErrorFieldsAsync(HttpResponseMessage httpResponseMessage)
    {
        var notificationList = await httpResponseMessage.Content.ReadFromJsonAsync<List<Notification>>();

        if (notificationList.Any(n => n.Key == "Nome"))
        {
            txtNomeError.Visibility = Visibility.Visible;
            txtNomeError.Text = notificationList.FirstOrDefault(n => n.Key == "Nome").Message;
        }

        if (notificationList.Any(n => n.Key == "Sobrenome"))
        {
            txtSobrenomeError.Visibility = Visibility.Visible;
            txtSobrenomeError.Text = notificationList.FirstOrDefault(n => n.Key == "Sobrenome").Message;
        }

        if (notificationList.Any(n => n.Key == "Telefone"))
        {
            txtTelefoneError.Visibility = Visibility.Visible;
            txtTelefoneError.Text = notificationList.FirstOrDefault(n => n.Key == "Telefone").Message;
        }
    }

    private async void DeletePersonAsync(int id)
    {
        await _httpClient.DeleteAsync($"delete-person?id={id}");

        GetAllPeopleAsync();
    }

    private void btnAddPerson_Click(object sender, RoutedEventArgs e)
    {
        var personSaveRequest = new PersonSaveRequest()
        {
            Nome = txtNome.Text,
            Sobrenome = txtSobrenome.Text,
            Telefone = txtTelefone.Text.CleanCharacters()
        };

        AddPersonAsync(personSaveRequest);

        ResetInputFields();
    }

    private void btnEditPerson_Click(object sender, RoutedEventArgs e)
    {
        PersonResponse personResponse = ((FrameworkElement)sender).DataContext as PersonResponse;
        txtId.Text = personResponse.Id.ToString();
        txtNome.Text = personResponse.Nome;
        txtSobrenome.Text = personResponse.Sobrenome;
        txtTelefone.Text = personResponse.Telefone;
    }

    private void btnEditPersonAction_Click(object sender, RoutedEventArgs e)
    {
        var hasId = int.TryParse(txtId.Text, out int id);

        if (!hasId)
            return;

        var personUpdateRequest = new PersonUpdateRequest()
        {
            Id = id,
            Nome = txtNome.Text,
            Sobrenome = txtSobrenome.Text,
            Telefone = txtTelefone.Text.CleanCharacters()
        };

        UpdatePersonAsync(personUpdateRequest);

        ResetInputFields();
    }

    private void ResetInputFields()
    {
        txtId.Text = "";
        txtNome.Text = "";
        txtSobrenome.Text = "";
        txtTelefone.Text = "";
    }

    private void btnDeletePerson_Click(object sender, RoutedEventArgs e)
    {
        PersonResponse personResponse = ((FrameworkElement)sender).DataContext as PersonResponse;

        if (personResponse is null)
            return;

        DeletePersonAsync(personResponse.Id);
    }
}
