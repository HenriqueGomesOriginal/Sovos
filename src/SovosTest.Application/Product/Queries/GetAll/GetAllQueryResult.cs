namespace SovosTest.Application.Product.Queries.GetAll;

/// <summary>
/// Returns for query
/// </summary>
public record GetAllQueryResult
{
    /// <summary>
    /// Get of list of Products
    /// </summary>
    /// <result></result>
    public IEnumerable<GetAll.GetAllQueryItem> Data { get; }

    /// <summary>
    /// Gets the overall status of the Products
    /// </summary>
    /// <value></value>
    public ResultQueryProductStatus Result { get; }

    /// <summary>
    /// Enum containing a list of potential return statuses 
    /// </summary>
    public enum ResultQueryProductStatus
    {
        QuerySuccess = 1
    }

    /// <summary>
    /// Returns a new instance containing a CreateSuccessResult result
    /// </summary>
    /// <param name="ResultId"></param>
    /// <returns></returns>
    public static GetAllQueryResult CreateSuccessResult(IEnumerable<GetAll.GetAllQueryItem> data)
    {
        return new(data, ResultQueryProductStatus.QuerySuccess);
    }

    /// <summary>
    /// /// Private constructor to enforce static method usage when generating Products
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <param name="data"></param>
    /// <param name="ResultFound"></param>
    /// <result></result>
    public GetAllQueryResult(IEnumerable<GetAll.GetAllQueryItem> data, ResultQueryProductStatus ResultQuerySuccess)
    {
        Data = data;
        Result = ResultQuerySuccess;
    }
}

