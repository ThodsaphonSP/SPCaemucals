namespace SPCaemucals.Utility;

public  class CorrelationIdHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationIdHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCorrelateTemplateString()
    {
        return "correlation id {@correlationId} |";
    }
    
    public  string? GetCorrelationId( )
    {
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("CorrelationId", out var correlationIdValue))
        {
            return correlationIdValue.FirstOrDefault();
        }
        return null;
    }
}