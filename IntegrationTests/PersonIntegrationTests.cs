using IntegrationTests.Fixture;
using System.Net;
using System.Net.Http.Json;
using TestBuilders;
using TesteTecnicoIdealSoft.API.Constants.RouteConstants;
using TesteTecnicoIdealSoft.API.DataTransferObjects.Response.Person;
using TesteTecnicoIdealSoft.API.Entities;

namespace IntegrationTests;
public sealed class PersonIntegrationTests : BaseIntegrationTests<Person>
{
    private const string basePersonRequestUri = "api/Person/";

    public PersonIntegrationTests(HttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    [Fact]
    public async Task AddPersonAsync_SuccessfulScenario_Returns200OK()
    {
        // A
        var personSaveRequest = PersonBuilder.NewObject().SaveRequestBuild();

        // A
        var addPersonHttpResponseMessage = await _httpClient.PostAsJsonAsync(basePersonRequestUri + PersonRouteConstants.AddPerson, personSaveRequest);

        // A
        Assert.Equal(addPersonHttpResponseMessage.StatusCode, HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdatePersonAsync_SuccessfulScenario_Returns200OK()
    {
        // A
        var personUpdateRequest = PersonBuilder.NewObject().WithId(2).UpdateRequestBuild();
        var addPersonStatusCodeResult = await AddSucessfulPersonAsync();

        // A
        var updatePersonHttpResponseMessage = await _httpClient.PutAsJsonAsync(basePersonRequestUri + PersonRouteConstants.UpdatePerson, personUpdateRequest);

        // A
        Assert.Equal(addPersonStatusCodeResult, HttpStatusCode.OK);
        Assert.Equal(updatePersonHttpResponseMessage.StatusCode, HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeletePersonAsync_SuccessfulScenario_Returns200OK()
    {
        // A
        var id = 1;
        var addPersonStatusCodeResult = await AddSucessfulPersonAsync();

        // A
        var deletePersonHttpResponseMessage = await _httpClient.DeleteAsync(basePersonRequestUri + PersonRouteConstants.DeletePerson + $"?id={id}");

        // A
        Assert.Equal(addPersonStatusCodeResult, HttpStatusCode.OK);
        Assert.Equal(deletePersonHttpResponseMessage.StatusCode, HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAllAsync_SucessfulScenario_ReturnsAllEntities_200OK()
    {
        // A
        var randomRange = new Random().Next(10);

        var isInsertsSucessfull = await AddSucessfulPeopleInRageAsync(randomRange);

        // A
        var personResponseListResult = await _httpClient.GetFromJsonAsync<List<PersonResponse>>(basePersonRequestUri + PersonRouteConstants.GetAllPeople);

        // A
        Assert.True(isInsertsSucessfull);
        Assert.Equal(personResponseListResult.Count, randomRange);
    }

    private async Task<HttpStatusCode> AddSucessfulPersonAsync()
    {
        var personSaveRequest = PersonBuilder.NewObject().SaveRequestBuild();

        var addPersonHttpResponseMessage = await _httpClient.PostAsJsonAsync(basePersonRequestUri + PersonRouteConstants.AddPerson, personSaveRequest);

        return addPersonHttpResponseMessage.StatusCode;
    }

    private async Task<bool> AddSucessfulPeopleInRageAsync(int range)
    {
        var isSuccess = true;
        for (var i = 0; i < range; i++)
        {
            var addPersonStatusCodeResult = await AddSucessfulPersonAsync();
            
            if (addPersonStatusCodeResult is not HttpStatusCode.OK)
                isSuccess = false;
        }

        return isSuccess;
    }
}
