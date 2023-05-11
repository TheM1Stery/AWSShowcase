using System.Data;
using FastEndpoints;
using FluentValidation;

namespace AWSShowcase.Lambda.Endpoints;

public class Request
{
    public required string Expression { get; init; }
}


public class CalculatePostRequestValidator : Validator<Request>
{
    public CalculatePostRequestValidator()
    {
        RuleFor(x => x.Expression).NotEmpty()
            .WithMessage("Expression cannot be empty");
    }
}

public class Response
{
    public required string Answer { get; init; }
}

public class CalculatePost : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/calculate");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var expression = req.Expression;
        var dt = new DataTable();
        try
        {
            var result = dt.Compute(expression, "");
            await SendOkAsync(new Response { Answer = result.ToString()! }, ct);
        }
        catch (Exception)
        {
            AddError("Invalid expression");
            await SendErrorsAsync(cancellation: ct);
        }
    }
    
}

