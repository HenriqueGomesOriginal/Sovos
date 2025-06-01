namespace SovosTest.Application.Product.Queries.GetAll;

public record GetAllQueryResult
{
    public GetAllQueryResponseData? Data { get; init; }

    public GetAllQueryResultStatus Result { get; }

    public enum GetAllQueryResultStatus
    {
        Found = 1,
        NotFound = 2
    }

    public static GetAllQueryResult Found(GetAllQueryResponseData response)
    {
        return new(response, GetAllQueryResultStatus.Found);
    }
    public static GetAllQueryResult NotFound()
    {
        return new(null, GetAllQueryResultStatus.NotFound);
    }

    public GetAllQueryResult(GetAllQueryResponseData? response, GetAllQueryResultStatus result)
    {
        Data = response;
        Result = result;
    }

}