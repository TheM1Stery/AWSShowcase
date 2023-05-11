using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;

namespace AWSShowcaseSite.Web.Pages;

public class Request
{
    public required string Expression { get; init; }
}

public class Response
{
    public required string Answer { get; init; }
}

public interface ILambdaCalculatorClient
{
    [Post("/calculate")]
    Task<ApiResponse<Response>> Calculate([Body] Request expression);
}


public class IndexModel : PageModel
{
    private readonly ILambdaCalculatorClient _client;

    [BindProperty] public string Expression { get; set; } = string.Empty;

    public string? Answer { get; private set; }
    
    public bool IsError { get; private set; }

    public IndexModel(ILambdaCalculatorClient client)
    {
        _client = client;
    }

    public async Task OnPost()
    {
        if (string.IsNullOrWhiteSpace(Expression))
            return;
        var result = await _client.Calculate(new Request { Expression = Expression });
        if (!result.IsSuccessStatusCode)
        {
            IsError = true;
            return;
        }
        Answer = result.Content.Answer;
        IsError = false;
    }
}